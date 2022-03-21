using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using iRSDKSharp;
using iRacingSdkWrapper.Broadcast;
using System.Threading.Tasks;

namespace iRacingSdkWrapper
{
    /// <summary>
    /// Provides a useful wrapper of the iRacing SDK.
    /// </summary>
    public sealed class SdkWrapper : IDisposable, ISimProvider
    {
        #region Fields

        internal readonly iRacingSDK sdk;
        private readonly SynchronizationContext context;
        private int waitTime;
        private Mutex readMutex;
        private CancellationTokenSource runCT;
        private Action<string> _logger;

        const string playerCarIdx = "PlayerCarIdx";
        const string sessiontime = "SessionTime";
        
        private Action<EventArgs> _onConnectedDelegate;
        public Action<EventArgs> OnConnectedDelegate
        {
            get { return _onConnectedDelegate ??= OnConnected; }
        }
        
        private Action<EventArgs> _onDisconnectedDelegate;
        public Action<EventArgs> OnDisconnectedDelegate
        {
            get { return _onDisconnectedDelegate ??= OnDisconnected; }
        }
        
        private Action<EventArgs> _onConnectingDelegate;
        public Action<EventArgs> OnConnectingDelegate
        {
            get { return _onConnectingDelegate ??= OnConnecting; }
        }
        
        private Action<TelemetryUpdatedEventArgs> _telemetryUpdatedDelegate;
        public Action<TelemetryUpdatedEventArgs> TelemetryUpdatedDelegate
        {
            get { return _telemetryUpdatedDelegate ??= OnTelemetryUpdated; }
        }
        
        private Action<SessionInfoUpdatedEventArgs> _onSessionInfoUpdated;
        public Action<SessionInfoUpdatedEventArgs> OnSessionInfoUpdatedDelegate
        {
            get { return _onSessionInfoUpdated ??= OnSessionInfoUpdated; }
        }
        
        private readonly TelemetryInfo _telemetryInfo = new (null);
        private TelemetryUpdatedEventArgs _telArgs = null;


        #endregion

        /// <summary>
        /// Creates a new instance of the SdkWrapper.
        /// </summary>
        public SdkWrapper()
        {
            this.context = SynchronizationContext.Current;
            this.sdk = new iRacingSDK();
            this.EventRaiseType = EventRaiseTypes.CurrentThread;

            //readMutex = new Mutex(false);

            this.TelemetryUpdateFrequency = 60;
            this.ConnectSleepTime = 1000;
            _driverId = -1;

            this.Replay = new ReplayControl(this);
            this.Camera = new CameraControl(this);
            this.PitCommands = new PitCommandControl(this);
            this.Chat = new ChatControl(this);
            this.Textures = new TextureControl(this);
            this.TelemetryRecording = new TelemetryRecordingControl(this);
        }

        #region Properties

        /// <summary>
        /// Gets the underlying iRacingSDK object.
        /// </summary>
        public iRacingSDK Sdk => sdk;

        /// <summary>
        /// Gets or sets how events are raised. Choose 'CurrentThread' to raise the events on the thread you created this object on (typically the UI thread), 
        /// or choose 'BackgroundThread' to raise the events on a background thread, in which case you have to delegate any UI code to your UI thread to avoid cross-thread exceptions.
        /// </summary>
        private EventRaiseTypes EventRaiseType { get; set; }

        //private bool _IsRunning;
        /// <summary>
        /// Is the main loop running?
        /// </summary>
        public bool IsRunning => runCT != null;

        private bool _IsConnected;
        /// <summary>
        /// Is the SDK connected to iRacing?
        /// </summary>
        public bool IsConnected => _IsConnected && _driverId > -1;

        private double _TelemetryUpdateFrequency;
        /// <summary>
        /// Gets or sets the number of times the telemetry info is updated per second. The default and maximum is 60 times per second.
        /// </summary>
        public double TelemetryUpdateFrequency
        {
            get { return _TelemetryUpdateFrequency; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("TelemetryUpdateFrequency must be at least 1.");
                if (value > 60)
                    throw new ArgumentOutOfRangeException("TelemetryUpdateFrequency cannot be more than 60.");

                _TelemetryUpdateFrequency = value;

                waitTime = (int)Math.Floor(1000f / value) - 1;
            }
        }

        /// <summary>
        /// The time in milliseconds between each check if iRacing is running. Use a low value (hundreds of milliseconds) to respond quickly to iRacing startup.
        /// Use a high value (several seconds) to conserve resources if an immediate response to startup is not required.
        /// </summary>
        public int ConnectSleepTime
        {
            get; set;
        }

        private int _driverId;
        private bool _loggedFirst;
        private bool _loggedFirstConnecting;
        private bool memoryFileExists;
        private Object runCTLock = new object();
        private bool initialConnectingWithMemoryExisting;

        /// <summary>
        /// Gets the Id (CarIdx) of yourself (the driver running this application).
        /// </summary>
        public int DriverId => _driverId;

        #region Broadcast messages

        /// <summary>
        /// Controls the replay playback system.
        /// </summary>
        public ReplayControl Replay { get; private set; }

        /// <summary>
        /// Provides control over the replay camera and where it is focused.
        /// </summary>
        public CameraControl Camera { get; private set; }

        /// <summary>
        /// Provides control over the pit commands.
        /// </summary>
        public PitCommandControl PitCommands { get; private set; }

        /// <summary>
        /// Provides control over the chat window.
        /// </summary>
        public ChatControl Chat { get; private set; }

        /// <summary>
        /// Provides control over reloading of car textures.
        /// </summary>
        public TextureControl Textures { get; private set; }

        /// <summary>
        /// Provides control over the telemetry recording system.
        /// </summary>
        public TelemetryRecordingControl TelemetryRecording { get; private set; }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Connects to iRacing and starts the main loop in a background thread.
        /// </summary>
        public void Start(Action<string> logger)
        {
            _logger = logger;
            //Stop();

            lock (runCTLock)
            {
                readMutex = new Mutex(false);
                // Create new cancellation token and run the looper
                runCT = new CancellationTokenSource();
                Task.Run(() => Loop(runCT.Token), runCT.Token);
            }

            _logger?.Invoke($"iRacing SDK wrapper started");
        }

        /// <summary>
        /// Stops the main loop
        /// </summary>
        public void Stop()
        {
            if (!IsRunning)
            {
                _logger?.Invoke($"iRacing SDK wrapper already stopped");
                return;
            }

            lock (runCTLock)
            {
                _logger?.Invoke($"iRacing SDK wrapper stopping");
                if (runCT != null)
                {
                    runCT.Cancel(true);
                    WaitHandle.WaitAny(new[] { runCT.Token.WaitHandle });
                    runCT.Dispose();
                    runCT = null;
                }
                
                sdk?.Shutdown();
            }
            
            _logger?.Invoke($"iRacing SDK wrapper stopped");
        }

        public void StopLogging()
        {
            _logger = null;
        }

        /// <summary>
        /// Return raw data object from the live telemetry.
        /// </summary>
        /// <param name="headerName">The name of the telemetry property to obtain.</param>
        public object GetData(string headerName)
        {
            if (!this.IsConnected) return null;

            return sdk.GetData(headerName);
        }

        /// <summary>
        /// Return live telemetry data wrapped in a TelemetryValue object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the desired object.</typeparam>
        /// <param name="name">The name of the desired object.</param>
        public TelemetryValue<T> GetTelemetryValue<T>(string name)
        {
            return new TelemetryValue<T>(sdk, name);
        }

        /// <summary>
        /// Reads new session info and raises the SessionInfoUpdated event, regardless of if the session info has changed.
        /// </summary>
        public void RequestSessionInfoUpdate()
        {
            try
            {
                var sessionInfo = sdk.GetSessionInfo();
                var time = (double)sdk.GetData("SessionTime");
                var sessionArgs = new SessionInfoUpdatedEventArgs(sessionInfo, time);
                RaiseEvent(OnSessionInfoUpdated, sessionArgs);
            }
            catch (NullReferenceException)
            {
                //Ignored, might happen shortly after wrapper restarts, where sessionInfo is not necessary yet
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + " " + ex.StackTrace);
            }
        }

        private object TryGetSessionNum()
        {
            try
            {
                var sessionnum = sdk.GetData("SessionNum");
                return sessionnum;
            }
            catch
            {
                return null;
            }
        }
        

        private void Loop(CancellationToken ct)
        {
            _logger?.Invoke("iRacing SDK Loop");
            int lastUpdate = -1;
            bool hasConnected = false;
            int tries = 0;

            while (!ct.IsCancellationRequested)
            {
                try
                {
                    // Check if we can find the sim
                    if (sdk.IsConnected())
                    {
                        if (!_IsConnected)
                        {
                            _IsConnected = true;
                            
                            // If this is the first time for the app but iRacing memory mapping file exists in memory, or a server change, restart the iRacing SDK, so we get the new server's memory mapping
                            if (hasConnected || initialConnectingWithMemoryExisting)
                            {
                                //_logger?.Invoke($"iRacing SDK Wrapper restarting");
                                sdk?.Shutdown();
                                memoryFileExists = sdk.Startup();
                                initialConnectingWithMemoryExisting = false;
                            }
                            
                            //_logger?.Invoke($"iRacing SDK Wrapper connected to iRacing");
                            RaiseEvent(OnConnectedDelegate, EventArgs.Empty);
                        }

                        hasConnected = true;

                        readMutex.WaitOne(8);

                        // Parse out your own driver Id
                        if (_driverId == -1)
                        {
                            _driverId = (int)sdk.GetData(playerCarIdx);
                            //_logger?.Invoke($"iRacing SDK Wrapper found player car id {_driverId}");
                        }

                        // Get the session time (in seconds) of this update
                        var time = (double)sdk.GetData(sessiontime);

                        // Raise the TelemetryUpdated event and pass along the lap info and session time
                        _telemetryInfo.Sdk = sdk;
                        if (_telArgs == null)
                            _telArgs = new TelemetryUpdatedEventArgs(_telemetryInfo, time);
                        else 
                            _telArgs.Update(_telemetryInfo, time);
                        
                        RaiseEvent(TelemetryUpdatedDelegate, _telArgs);

                        // Is the session info updated?
                        var newUpdate = sdk.Header?.SessionInfoUpdate ?? lastUpdate;

                        if (newUpdate != lastUpdate)
                        {
                            lastUpdate = newUpdate;

                            //Force check Player Car Id again, to be sure when crossing from warm up practice -> race server practice, that the ID updates.
                            //This is required since iRacing is not shut down inbetween session changes, thus never gets to set DriverId to -1
                            if (_driverId != -1 && _driverId != (int)sdk.GetData(playerCarIdx))
                            {
                                _driverId = (int)sdk.GetData(playerCarIdx);
                                //_logger?.Invoke($"iRacing SDK Wrapper found updated player car id {_driverId}");
                            }

                            // Get the session info string
                            var sessionInfo = sdk.GetSessionInfo();

                            // Raise the SessionInfoUpdated event and pass along the session info and session time.
                            var sessionArgs = new SessionInfoUpdatedEventArgs(sessionInfo, time);
                            RaiseEvent(OnSessionInfoUpdatedDelegate, sessionArgs);
                        }
                    }
                    else if (_IsConnected)
                    {
                        // We have already been initialized before, so the sim is closing
                        RaiseEvent(OnDisconnectedDelegate, EventArgs.Empty);

                        lastUpdate = -1;
                        _IsConnected = false;
                        //_logger?.Invoke($"iRacing SDK Wrapper disconnecting from server");
                    }
                    else
                    {
                        _IsConnected = false;
                        //if (!_loggedFirst)
                        //    _logger?.Invoke($"iRacing SDK Wrapper SDK startup");
                        
                        if (!hasConnected)
                            memoryFileExists = sdk.Startup();

                        if (memoryFileExists 
                            && (sdk.Header == null || sdk.Header.VarCount == 0) 
                            && (!_loggedFirstConnecting || hasConnected))
                        {
                            RaiseEvent(OnConnectingDelegate, EventArgs.Empty);
                            memoryFileExists = false;
                            _loggedFirstConnecting = true;

                            if (!hasConnected)
                                initialConnectingWithMemoryExisting = true;
                        }
                        
                        //if (!_loggedFirst)
                        //    _logger?.Invoke($"iRacing SDK Wrapper SDK startup hasConnected: {hasConnected} memoryFileExists: {memoryFileExists}");

                        _loggedFirst = true;
                    }

                    // Sleep for a short amount of time until the next update is available
                    if (_IsConnected || sdk.Header?.Status == 1)
                    {
                        if (waitTime <= 0 || waitTime > 1000) 
                            waitTime = 15;
                    
                        Thread.Sleep(waitTime);
                    }
                    else
                    {
                        //_logger?.Invoke("iRacing SDK Wrapper sleeping");
                        // Not connected yet, no need to check every 16 ms, let's try again in some time
                        Thread.Sleep(ConnectSleepTime);
                    }
                }
                catch (Exception ex)
                {
                    tries++;

                    if (tries < 5)
                        _logger?.Invoke("iRacing SDK Wrapper error: " + ex.Message + " " + ex.TargetSite + " " + ex.StackTrace);
                    else
                    {
                        string outPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "The Digital Race Engineer"), "ERROR-" + DateTime.UtcNow.ToString("yyyyMMdd--HH-mm-ss") + ".txt");
                        File.WriteAllText(outPath, "iRacing SDK Wrapper error: " + ex.Message + " " + ex.TargetSite + " " + ex.StackTrace);
                        _logger?.Invoke("iRacing SDK Wrapper error file: " + outPath);
                        break;
                    }
                }
            }

            try
            {
                sdk?.Shutdown();
            }
            catch (Exception ex)
            {
                _logger?.Invoke("iRacing SDK Wrapper could not be shutdown: " + ex.Message + " " + ex.TargetSite + " " + ex.StackTrace);
            }
            
            _IsConnected = false;
            _logger?.Invoke("iRacing SDK Wrapper connection cancelled");
        }


        #endregion

        #region Events

        /// <summary>
        /// Event raised when the sim outputs telemetry information (60 times per second).
        /// </summary>
        public event EventHandler<TelemetryUpdatedEventArgs> TelemetryUpdated;

        /// <summary>
        /// Event raised when the sim refreshes the session info (few times per minute).
        /// </summary>
        public event EventHandler<SessionInfoUpdatedEventArgs> SessionInfoUpdated;

        /// <summary>
        /// Event raised when the SDK detects the sim for the first time.
        /// </summary>
        public event EventHandler Connected;

        /// <summary>
        /// Event raised when the SDK no longer detects the sim (sim closed).
        /// </summary>
        public event EventHandler Disconnected;
        
        /// <summary>
        /// Event raised when the SDK no longer detects the sdk header content (when sim is closed, using UI and the user registers and starts joining another server)
        /// </summary>
        public event EventHandler Connecting;

        private void RaiseEvent<T>(Action<T> del, T e)
            where T : EventArgs
        {
            var callback = new SendOrPostCallback(obj => del(obj as T));

            if (context != null && this.EventRaiseType == EventRaiseTypes.CurrentThread)
            {
                // Post the event method on the thread context, this raises the event on the thread on which the SdkWrapper object was created
                context.Post(callback, e);
            }
            else
            {
                // Simply invoke the method, this raises the event on the background thread that the SdkWrapper created
                // Care must be taken by the user to avoid cross-thread operations
                callback.Invoke(e);
            }
        }

        private void OnSessionInfoUpdated(SessionInfoUpdatedEventArgs e)
        {
            var handler = this.SessionInfoUpdated;
            handler?.Invoke(this, e);
        }

        private void OnTelemetryUpdated(TelemetryUpdatedEventArgs e)
        {
            var handler = this.TelemetryUpdated;
            handler?.Invoke(this, e);
        }

        private void OnConnected(EventArgs e)
        {
            var handler = this.Connected;
            handler?.Invoke(this, e);
        }

        private void OnDisconnected(EventArgs e)
        {
            var handler = this.Disconnected;
            handler?.Invoke(this, e);
        }

        private void OnConnecting(EventArgs e)
        {
            var handler = this.Connecting;
            handler?.Invoke(this, e);
        }

        public void Dispose()
        {
            runCT?.Cancel();
        }

        #endregion

        #region Enums

        /// <summary>
        /// The way in which events of the SDK wrapper are raised.
        /// </summary>
        public enum EventRaiseTypes
        {
            /// <summary>
            /// Events are raised on the current thread (the thread on which the SdkWrapper object was created).
            /// </summary>
            CurrentThread,

            /// <summary>
            /// Events are raised on a separate background thread (synchronization / invokation required to update UI).
            /// </summary>
            BackgroundThread
        }

        #endregion

        #region Nested classes

        public class SdkUpdateEventArgs : EventArgs
        {
            public SdkUpdateEventArgs(double time)
            {
                _updateTime = time;
            }

            private double _updateTime;
            /// <summary>
            /// Gets the time (in seconds) when this update occured.
            /// </summary>
            public double UpdateTime
            {
                get { return _updateTime; }
                set => _updateTime = value;
            }
        }

        public class SessionInfoUpdatedEventArgs : SdkUpdateEventArgs
        {
            public SessionInfoUpdatedEventArgs(string sessionInfo, double time) : base(time)
            {
                _SessionInfo = new SessionInfo(sessionInfo, time);
            }

            private readonly SessionInfo _SessionInfo;
            /// <summary>
            /// Gets the session info.
            /// </summary>
            public SessionInfo SessionInfo { get { return _SessionInfo; } }
        }

        public class TelemetryUpdatedEventArgs : SdkUpdateEventArgs
        {
            public TelemetryUpdatedEventArgs(TelemetryInfo info, double time) : base(time)
            {
                _telemetryInfo = info;
            }

            public void Update(TelemetryInfo info, double time)
            {
                _telemetryInfo = info;
                UpdateTime = time;
            }

            private TelemetryInfo _telemetryInfo;
            /// <summary>
            /// Gets the telemetry info object.
            /// </summary>
            public TelemetryInfo TelemetryInfo => _telemetryInfo;
        }

        #endregion

        public string GetTelemetryAsCsv(int playerIdx)
        {
            if (Sdk == null)
                return "";

            StringBuilder csvContent = new StringBuilder();

            string zeroTo63 = "";
            for (int i = 0; i < 64; i++)
                zeroTo63 += ";" + i;

            csvContent.AppendLine("Variable;PlayerValue" + zeroTo63);
            csvContent.AppendLine("TimeUTC;" + DateTime.UtcNow.ToString("HH:mm:ss.fff"));

            foreach (var header in Sdk.VarHeaders)
            {
                try
                {
                    switch (header.Value.Type)
                    {
                        case CVarHeader.VarType.irChar:
                            ExtractVar<string>(playerIdx, header, csvContent);
                            break;
                        case CVarHeader.VarType.irBool:
                            ExtractVar<bool>(playerIdx, header, csvContent);
                            break;
                        case CVarHeader.VarType.irInt:
                            ExtractVar<int>(playerIdx, header, csvContent);
                            break;
                        case CVarHeader.VarType.irBitField:
                            ExtractVar<int>(playerIdx, header, csvContent);
                            break;
                        case CVarHeader.VarType.irFloat:
                            if (header.Value.Count > 1)
                            {
                                // if (header.Value.Count >= playerIdx)
                                //     csvContent.AppendLine(ExtractHeaderKeyValuePair<float>(header, playerIdx).ToString(CultureInfo.InvariantCulture) + ";" + string.Join(";", GetTelemetryValue<float[]>(header.Value.Name).Value.Select(f => f.ToString(CultureInfo.CurrentCulture))));
                                // else
                                // {
                                    for (int i = 0; i < header.Value.Count; i++)
                                        csvContent.AppendLine(ExtractHeaderKeyValuePair<float>(header, i).ToString(CultureInfo.InvariantCulture));
                                // }
                            }
                            else
                                csvContent.AppendLine(ExtractHeaderKeyAndValue<float>(header).ToString(CultureInfo.InvariantCulture));
                            break;
                        case CVarHeader.VarType.irDouble:
                            if (header.Value.Count > 1)
                            {
                                // if (header.Value.Count >= playerIdx)
                                //     csvContent.AppendLine(ExtractHeaderKeyValuePair<double>(header, playerIdx).ToString(CultureInfo.InvariantCulture) + ";" + string.Join(";", GetTelemetryValue<double[]>(header.Value.Name).Value.Select(f => f.ToString(CultureInfo.CurrentCulture))));
                                // else
                                // {
                                    for (int i = 0; i < header.Value.Count; i++)
                                        csvContent.AppendLine(ExtractHeaderKeyValuePair<double>(header, i).ToString(CultureInfo.InvariantCulture));
                                // }
                            }
                            else
                                csvContent.AppendLine(ExtractHeaderKeyAndValue<double>(header).ToString(CultureInfo.InvariantCulture));
                            break;
                    }
                }
                catch
                {
                    csvContent.AppendLine(header.Key + ",null!!");
                }
            }

            return csvContent.ToString();
        }

        private void ExtractVar<T>(int playerIdx, KeyValuePair<string, CVarHeader> header, StringBuilder csvContent)
        {
            if (header.Value.Count > 1)
            {
                if (header.Value.Count >= playerIdx)
                    csvContent.AppendLine(ExtractHeaderKeyValuePair<T>(header, playerIdx) + ";" + string.Join(";", GetTelemetryValue<T[]>(header.Value.Name).Value));
                else
                {
                    for (int i = 0; i < header.Value.Count; i++)
                        csvContent.AppendLine(ExtractHeaderKeyValuePair<T>(header, i));
                }
            }
            else
                csvContent.AppendLine(ExtractHeaderKeyAndValue<T>(header));
        }

        private string ExtractHeaderKeyAndValue<T>(KeyValuePair<string, CVarHeader> header)
        {
            return header.Key + ";" + GetTelemetryValue<T>(header.Value.Name).Value;
        }

        private string ExtractHeaderKeyValuePair<T>(KeyValuePair<string, CVarHeader> header, int i)
        {
            var variable = GetTelemetryValue<T[]>(header.Value.Name);
            return header.Key + "_" + i + ";" + variable.Value[i];
        }

        public string ToPrettyString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"IsRunning;{IsRunning}");
            sb.AppendLine($"IsConnected;{IsConnected}");
            sb.AppendLine($"ConnectSleepTime;{ConnectSleepTime}");
            sb.AppendLine($"DriverId;{DriverId}");
            sb.AppendLine($"_loggedFirst;{_loggedFirst}");
            sb.AppendLine($"sdk;{(sdk != null ? "Exist" : "null")}");
            if (sdk != null)
            {
                sb.AppendLine($"sdk.IsInitialized;{sdk.IsInitialized}");
                sb.AppendLine($"sdk.VarHeaderSize;{sdk.VarHeaderSize}");
                sb.AppendLine($"sdk.IsConnected();{sdk.IsConnected()}");
                if (sdk.Header != null)
                {
                    sb.AppendLine($"sdk.Header.Buffer;{sdk.Header.Buffer}");
                    sb.AppendLine($"sdk.Header.Status;{sdk.Header.Status}");
                    sb.AppendLine($"sdk.Header.Version;{sdk.Header.Version}");
                    sb.AppendLine($"sdk.Header.BufferCount;{sdk.Header.BufferCount}");
                    sb.AppendLine($"sdk.Header.BufferLength;{sdk.Header.BufferLength}");
                    sb.AppendLine($"sdk.Header.TickRate;{sdk.Header.TickRate}");
                    sb.AppendLine($"sdk.Header.VarCount;{sdk.Header.VarCount}");
                    sb.AppendLine($"sdk.Header.SessionInfoLength;{sdk.Header.SessionInfoLength}");
                    sb.AppendLine($"sdk.Header.SessionInfoOffset;{sdk.Header.SessionInfoOffset}");
                    sb.AppendLine($"sdk.Header.SessionInfoUpdate;{sdk.Header.SessionInfoUpdate}");
                    sb.AppendLine($"sdk.Header.VarHeaderOffset;{sdk.Header.VarHeaderOffset}");
                }
                else
                    sb.AppendLine($"sdk.Header;null");
                
            }
            return sb.ToString();
        }
    }
}
