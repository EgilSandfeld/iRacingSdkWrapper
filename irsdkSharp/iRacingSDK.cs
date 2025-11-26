using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace iRSDKSharp
{
    public enum BroadcastMessageTypes
    {
        CamSwitchPos = 0,  // car position, group, camera
        CamSwitchNum, // driver #, group, camera
        CamSetState, // irsdk_CameraState, unused, unused 
        ReplaySetPlaySpeed, // speed, slowMotion, unused
        ReplaySetPlayPosition, // irsdk_RpyPosMode, Frame Number (high, low)
        ReplaySearch, // irsdk_RpySrchMode, unused, unused
        ReplaySetState, // irsdk_RpyStateMode, unused, unused
        ReloadTextures, // irsdk_ReloadTexturesMode, carIdx, unused
        ChatCommand, // irsdk_ChatCommandMode, subCommand, unused
        PitCommand, // irsdk_PitCommandMode, parameter
        TelemCommand, // irsdk_TelemCommandMode, unused, unused
        FFBCommand, // irsdk_FFBCommandMode, value (float, high, low)
        ReplaySearchSessionTime, // sessionNum, sessionTimeMS (high, low)
        VideoCapture // irsdk_VideoCaptureMode, unused, unused
    };
    
    enum irsdk_BroadcastMsg 
    {
        irsdk_BroadcastChatComand,		      
        irsdk_BroadcastPitCommand,            
        irsdk_BroadcastTelemCommand,		  
        irsdk_BroadcastFFBCommand,		      
        irsdk_BroadcastReplaySearchSessionTime, 
        irsdk_BroadcastVideoCapture,          
        irsdk_BroadcastLast                   // unused placeholder
    };
    
    public enum CamSwitchModeTypes { FocusAtIncident = -3, FocusAtLeader = -2, FocusAtExciting = -1, FocusAtDriver = 0 };
    public enum CameraStateTypes { None = 0x0000, IsSessionScreen = 0x0001, IsScenicActive = 0x0002, CamToolActive = 0x0004, UIHidden = 0x0008, UseAutoShotSelection = 0x0010, UseTemporaryEdits = 0x0020, UseKeyAcceleration = 0x0040, UseKey10xAcceleration = 0x0080, UseMouseAimMode = 0x0100 };
    public enum ReplayPositionModeTypes { Begin = 0, Current, End };
    public enum ReplaySearchModeTypes { ToStart = 0, ToEnd, PreviousSession, NextSession, PreviousLap, NextLap, PreviousFrame, NextFrame, PreviousIncident, NextIncident };
    public enum ReplayStateModeTypes { Erasetape = 0 };
    public enum ReloadTexturesModeTypes { All = 0, CarIdx };
    public enum ChatCommandModeTypes { Macro = 0, BeginChat, Reply, Cancel };

    public enum PitCommandModeTypes
    {
        Clear = 0,
        WS = 1,
        Fuel = 2,
        LF = 3,
        RF = 4,
        LR = 5,
        RR = 6,
        ClearTires = 7,
        FastRepair = 8,
        ClearWS = 9,			// Uncheck Clean the windshield checkbox
        ClearFR = 10,			// Uncheck request a fast repair
        ClearFuel = 11,			// Uncheck add fuel
        TC = 12,				// Change tire compound
    };

    public enum TelemCommandModeTypes { Stop = 0, Start, Restart };

    public class Defines
    {
        public const uint DesiredAccess = 2031619;
        public const string DataValidEventName = "Local\\IRSDKDataValidEvent";
        public const string MemMapFileName = "Local\\IRSDKMemMapFileName";
        public const string BroadcastMessageName = "IRSDK_BROADCASTMSG";
        public const string PadCarNumName = "IRSDK_PADCARNUM";
        public const int MaxString = 32;
        public const int MaxDesc = 64;
        public const int MaxVars = 4096;
        public const int MaxBufs = 4;
        public const int StatusConnected = 1;
        public const int SessionStringLength = 0x20000; // 128k
    }

    public class iRacingSDK
    {
        //VarHeader offsets
        public const int VarOffsetOffset = 4;
        public const int VarCountOffset = 8;
        public const int VarNameOffset = 16;
        public const int VarDescOffset = 48;
        public const int VarUnitOffset = 112;
        public int VarHeaderSize = 144;
        private Encoding encoder;

        public bool IsInitialized = false;

        MemoryMappedFile iRacingFile;
        MemoryMappedViewAccessor FileMapView;
        private readonly Dictionary<string, object> _arrayCache = new ();
        public CiRSDKHeader Header;
        public Dictionary<string, CVarHeader> VarHeaders = new Dictionary<string, CVarHeader>();
        //List<CVarHeader> VarHeaders = new List<CVarHeader>();

        public iRacingSDK()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            encoder = Encoding.GetEncoding(1252);
        }

        
        public bool Startup(CancellationToken token)
        {
            if (IsInitialized)
                return true;

            try
            {
                iRacingFile = MemoryMappedFile.OpenExisting(Defines.MemMapFileName);
                FileMapView = iRacingFile.CreateViewAccessor();
                VarHeaderSize = Marshal.SizeOf(typeof(VarHeader));

                var hEvent = OpenEvent(Defines.DesiredAccess, false, Defines.DataValidEventName);
                var are = new AutoResetEvent(false);
                are.SafeWaitHandle = new SafeWaitHandle(hEvent, true);

                var wh = new WaitHandle[1];
                wh[0] = are;
                Task.Run(() =>
                {
                    try
                    {
                        var index = WaitHandle.WaitAny(new[] { wh[0], token.WaitHandle });
                        if (index == 1)
                            throw new OperationCanceledException(token);
                    }
                    catch (OperationCanceledException)
                    {
                        // Swallow expected cancellation to avoid UnobservedTaskException
                    }
                    catch (ObjectDisposedException)
                    {
                        // Token or handle disposed during shutdown; ignore
                    }
                }, token).ContinueWith(t =>
                {
                    var _ = t.Exception; // observe exceptions to prevent finalizer crashing
                }, TaskContinuationOptions.OnlyOnFaulted);

                Header = new CiRSDKHeader(FileMapView);
                GetVarHeaders();

                IsInitialized = true;
            }
            catch (FileNotFoundException)
            {
                return false; //This is called when the sim is not running, so all normal activity here
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        

        private void GetVarHeaders()
        {
            VarHeaders.Clear();
            for (int i = 0; i < Header.VarCount; i++)
            {
                int type = FileMapView.ReadInt32(Header.VarHeaderOffset + ((i * VarHeaderSize)));
                int offset = FileMapView.ReadInt32(Header.VarHeaderOffset + ((i * VarHeaderSize) + VarOffsetOffset));
                int count = FileMapView.ReadInt32(Header.VarHeaderOffset + ((i * VarHeaderSize) + VarCountOffset));
                byte[] name = new byte[Defines.MaxString];
                byte[] desc = new byte[Defines.MaxDesc];
                byte[] unit = new byte[Defines.MaxString];
                FileMapView.ReadArray<byte>(Header.VarHeaderOffset + ((i * VarHeaderSize) + VarNameOffset), name, 0, Defines.MaxString);
                FileMapView.ReadArray<byte>(Header.VarHeaderOffset + ((i * VarHeaderSize) + VarDescOffset), desc, 0, Defines.MaxDesc);
                FileMapView.ReadArray<byte>(Header.VarHeaderOffset + ((i * VarHeaderSize) + VarUnitOffset), unit, 0, Defines.MaxString);
                string nameStr = encoder.GetString(name).TrimEnd(new char[] { '\0' });
                string descStr = encoder.GetString(desc).TrimEnd(new char[] { '\0' });
                string unitStr = encoder.GetString(unit).TrimEnd(new char[] { '\0' });
                VarHeaders[nameStr] = new CVarHeader(type, offset, count, nameStr, descStr, unitStr);
            }
        }

        public object GetData(string name)
        {
            // Fail-safe guards to avoid NullReferenceException in race conditions
            if (!IsInitialized || Header == null || name == null || FileMapView == null || VarHeaders == null || !VarHeaders.ContainsKey(name)) 
                return null;

            var varOffset = VarHeaders[name].Offset;
            var count = VarHeaders[name].Count;
            switch (VarHeaders[name].Type)
            {
                case CVarHeader.VarType.irChar:
                {
                    var data = new byte[count];
                    FileMapView.ReadArray(Header.Buffer + varOffset, data, 0, count);
                    return encoder.GetString(data).TrimEnd(['\0']);
                }
                
                case CVarHeader.VarType.irBool when count > 1 && !Is360HzTo60HzDataCollection(count):
                {
                    var data = new bool[count];
                    FileMapView.ReadArray(Header.Buffer + varOffset, data, 0, count);
                    return data;
                }
                
                case CVarHeader.VarType.irBool:
                    return FileMapView.ReadBoolean(Header.Buffer + varOffset);
                
                case CVarHeader.VarType.irInt:
                case CVarHeader.VarType.irBitField:
                {
                    if (count > 1 && !Is360HzTo60HzDataCollection(count))
                    {
                        var data = new int[count];
                        FileMapView.ReadArray(Header.Buffer + varOffset, data, 0, count);
                        return data;
                    }

                    return FileMapView.ReadInt32(Header.Buffer + varOffset);
                }
                
                case CVarHeader.VarType.irFloat when count > 1 && !Is360HzTo60HzDataCollection(count):
                {
                    var data = new float[count];
                    FileMapView.ReadArray(Header.Buffer + varOffset, data, 0, count);
                    return data;
                }
                
                case CVarHeader.VarType.irFloat:
                    return FileMapView.ReadSingle(Header.Buffer + varOffset);
                
                case CVarHeader.VarType.irDouble when count > 1 && !Is360HzTo60HzDataCollection(count):
                {
                    var data = new double[count];
                    FileMapView.ReadArray(Header.Buffer + varOffset, data, 0, count);
                    return data;
                }
                
                case CVarHeader.VarType.irDouble:
                    return FileMapView.ReadDouble(Header.Buffer + varOffset);
                
                default:
                    return null;
            }
        }
        
        public T GetValue<T>(string name)
        {
            // Snapshot and cache values to prevent null reference exceptions later
            var initialized = IsInitialized;
            var header = Header;
            var view = FileMapView;
            var vars = VarHeaders;

            // Fail-safe guards to avoid NullReferenceException in race conditions
            if (!initialized || header == null || view == null || vars == null || !vars.TryGetValue(name, out var varHeader))
                return default;

            var varOffset = varHeader.Offset;
            var count = varHeader.Count;
            var type = typeof(T);

            // Handle single value types (structs) - NO BOXING
            if (type == typeof(int)) return (T)(object)view.ReadInt32(header.Buffer + varOffset);
            if (type == typeof(float)) return (T)(object)view.ReadSingle(header.Buffer + varOffset);
            if (type == typeof(double)) return (T)(object)view.ReadDouble(header.Buffer + varOffset);
            if (type == typeof(bool)) return (T)(object)view.ReadBoolean(header.Buffer + varOffset);
            // Add other simple types like byte, char if needed

            // Handle array types - THIS WILL ALLOCATE A NEW ARRAY, which is unavoidable
            // but it's much better than boxing every element.
            if (type == typeof(int[]))
            {
                int[] data;
                if (!_arrayCache.TryGetValue(name, out var cachedObj))
                {
                    data = new int[count];
                    _arrayCache[name] = data;
                }
                else
                {
                    data = (int[])cachedObj;
                }
                view.ReadArray(header.Buffer + varOffset, data, 0, count);
                return (T)(object)data;
            }
            
            if (type == typeof(float[]))
            {
                float[] data;
                if (!_arrayCache.TryGetValue(name, out var cachedObj))
                {
                    data = new float[count];
                    _arrayCache[name] = data;
                }
                else
                {
                    data = (float[])cachedObj;
                }
                view.ReadArray(header.Buffer + varOffset, data, 0, count);
                return (T)(object)data;
            }
            
            if (type == typeof(double[]))
            {
                double[] data;
                if (!_arrayCache.TryGetValue(name, out var cachedObj))
                {
                    data = new double[count];
                    _arrayCache[name] = data;
                }
                else
                {
                    data = (double[])cachedObj;
                }
                view.ReadArray(header.Buffer + varOffset, data, 0, count);
                return (T)(object)data;
            }

            // Fallback for types not handled above (like string, which is special)
            // This will still use the old boxing method if necessary.
            return (T)GetData(name);
        }

        /// <summary>
        /// When iRacing app.ini: 'irsdkLog360Hz=1 ; Log some telemetry at 360 Hz rather than at 60 Hz' the sim sends chunks of 6 data points at 360Hz (so each corresponds to 60Hz)
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static bool Is360HzTo60HzDataCollection(int count)
        {
            return count == 6;
        }

        //private readonly char[] _trimChars = ['\0'];
        
        /*[SuppressMessage("ReSharper.DPA", "DPA0003: Excessive memory allocations in LOH", MessageId = "type: System.Byte[]; size: 102MB")]
        public string GetSessionInfo()
        {
            if (IsInitialized && Header != null)
            {
                var data = new byte[Header.SessionInfoLength];
                FileMapView.ReadArray(Header.SessionInfoOffset, data, 0, Header.SessionInfoLength);
                return encoder.GetString(data).TrimEnd(_trimChars);
            }
            return null;
        }*/
        
        public string GetSessionInfo()
        {
            if (!IsInitialized || Header == null)
                return null;

            int length = Header.SessionInfoLength;
            byte[] rentedArray = ArrayPool<byte>.Shared.Rent(length);
            try
            {
                FileMapView.ReadArray(Header.SessionInfoOffset, rentedArray, 0, length);
                ReadOnlySpan<byte> dataSpan = new ReadOnlySpan<byte>(rentedArray, 0, length);

                int trimmedLength = TrimEndIndex(dataSpan);
                if (trimmedLength == length)  // No trimming needed
                    return encoder.GetString(rentedArray, 0, length);

                byte[] trimmedArray = new byte[trimmedLength];
                dataSpan.Slice(0, trimmedLength).CopyTo(trimmedArray);
                return encoder.GetString(trimmedArray);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(rentedArray);
            }
        }

        private int TrimEndIndex(ReadOnlySpan<byte> span)
        {
            int i = span.Length - 1;
            while (i >= 0 && span[i] == 0)
                i--;
            return i + 1;
        }


        public bool IsConnected()
        {
            try
            {
                if (IsInitialized && Header != null)
                    return (Header.Status & 1) > 0;
            }
            catch (ObjectDisposedException)
            {
                //Ignored
            }
            
            return false;
        }

        public void Shutdown()
        {
            try
            {
                IsInitialized = false;
                var view = FileMapView; FileMapView = null;
                var file = iRacingFile; iRacingFile = null;
                Header = null;
                view?.Dispose();
                file?.Dispose();
            }
            catch 
            {
                //Ignored
            }
        }

        IntPtr GetBroadcastMessageID()
        {
            return RegisterWindowMessage(Defines.BroadcastMessageName);
        }

        IntPtr GetPadCarNumID()
        {
            return RegisterWindowMessage(Defines.PadCarNumName);
        }

        /// <remark>
        /// Does not work as it multiplies the fraction away into an integer realm!
        /// 
        /// The selected code is a single line of C++ code that is used to convert a floating-point number to an integer, while preserving its fractional part. This is achieved by multiplying the floating-point number by `2^16-1` (or `65536.0f` in floating-point representation) before casting it to an integer.
        /// int real = (int)(var2 * 65536.0f);
        /// The multiplication by `65536.0f` effectively moves the fractional part of the floating-point number into the integer part. This is because `65536` is `2^16`, and multiplying by this value is equivalent to shifting the binary representation of the number 16 places to the left. This operation moves the fractional part of the number (the part after the decimal point) into the integer part of the number (the part before the decimal point).
        /// After this multiplication, the floating-point number is cast to an integer using `(int)`. This operation truncates any remaining fractional part, resulting in an integer.
        /// This technique is often used in fixed-point arithmetic, where a fixed number of digits after the decimal point are used to represent fractional values. It allows fractional values to be represented and manipulated using integer operations, which can be more efficient on some hardware.
        ///
        /// From irsdk_defines.h in the SDK v1.18
        /// var2 can be a full 32 bit float
        /// void irsdk_broadcastMsg(irsdk_BroadcastMsg msg, int var1, float var2);
        /// </remark>
        public int BroadcastMessage(BroadcastMessageTypes msg, int var1, float var2)
        {
            // multiply by 2^16-1 to move fractional part to the integer part
            int real = (int)(var2 * 65536.0f);
            return BroadcastMessage(msg, var1, real);
        }

        public int BroadcastMessage(BroadcastMessageTypes msg, int var1, int var2, int var3)
        {
            return BroadcastMessage(msg, var1, MakeLong((short)var2, (short)var3));
        }

        public int BroadcastMessage(BroadcastMessageTypes msg, int var1, int var2)
        {
            IntPtr msgId = GetBroadcastMessageID();
            IntPtr hwndBroadcast = IntPtr.Add(IntPtr.Zero, 0xffff);
            IntPtr result = IntPtr.Zero;
            if (msgId != IntPtr.Zero)
            {
                result = PostMessage(hwndBroadcast, msgId.ToInt32(), MakeLong((short)msg, (short)var1), var2);
            }
            return result.ToInt32();
        }

        [DllImport("user32.dll")]
        private static extern IntPtr RegisterWindowMessage(string lpProcName);

        //[DllImport("user32.dll")]
        //private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        //[DllImport("user32.dll")]
        //private static extern IntPtr PostMessage(IntPtr hWnd, int Msg, int wParam, float lParam);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr OpenEvent(UInt32 dwDesiredAccess, Boolean bInheritHandle, String lpName);

        public int MakeLong(short lowPart, short highPart)
        {
            return (int)(((ushort)lowPart) | (uint)(highPart << 16));
        }

        public static short HiWord(int dword)
        {
            return (short)(dword >> 16);
        }

        public static short LoWord(int dword)
        {
            return (short)dword;
        }
    }

    internal class IgnoreException : Exception
    {
    }

    //144 bytes
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct VarHeader
    {
        //16 bytes: offset = 0
        public int type;
        //offset = 4
        public int offset;
        //offset = 8
        public int count;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public int[] pad;

        //32 bytes: offset = 16
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Defines.MaxString)]
        public string name;
        //64 bytes: offset = 48
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Defines.MaxDesc)]
        public string desc;
        //32 bytes: offset = 112
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Defines.MaxString)]
        public string unit;
    }

    //32 bytes
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct VarBuf
    {
        public int tickCount;
        public int bufOffset;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] pad;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct iRSDKHeader
    {
        //12 bytes: offset = 0
        public int ver;
        public int status;
        public int tickRate;

        //12 bytes: offset = 12
        public int sessionInfoUpdate;
        public int sessionInfoLen;
        public int sessionInfoOffset;

        //8 bytes: offset = 24
        public int numVars;
        public int varHeaderOffset;

        //16 bytes: offset = 32
        public int numBuf;
        public int bufLen;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] pad1;

        //128 bytes: offset = 48
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Defines.MaxBufs)]
        public VarBuf[] varBuf;
    }
}
