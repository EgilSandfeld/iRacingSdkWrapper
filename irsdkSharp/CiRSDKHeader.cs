using System.IO.MemoryMappedFiles;

namespace iRSDKSharp
{
    public class CiRSDKHeader
    {
        //Header offsets
        public const int HVerOffset = 0;
        public const int HStatusOffset = 4;
        public const int HTickRateOffset = 8;
        public const int HSesInfoUpdateOffset = 12;
        public const int HSesInfoLenOffset = 16;
        public const int HSesInfoOffsetOffset = 20;
        public const int HNumVarsOffset = 24;
        public const int HVarHeaderOffsetOffset = 28;
        public const int HNumBufOffset = 32;
        public const int HBufLenOffset = 36;

        MemoryMappedViewAccessor FileMapView = null;

        CVarBuf buffer = null;
        private int newStatus = -1;

        public CiRSDKHeader(MemoryMappedViewAccessor mapView)
        {
            FileMapView = mapView;
            buffer = new CVarBuf(mapView, this);
        }

        public int Version
        {
            get { return FileMapView.ReadInt32(HVerOffset); }
        }

        private int _status;
        public int Status
        {
            get
            {
                newStatus = FileMapView.ReadInt32(HStatusOffset);
                if (_status == newStatus)
                    return _status;
                
                _status = newStatus;
                return _status;
            }
        }

        public int TickRate
        {
            get { return FileMapView.ReadInt32(HTickRateOffset); }
        }

        public int SessionInfoUpdate
        {
            get { return FileMapView.ReadInt32(HSesInfoUpdateOffset); }
        }

        public int SessionInfoLength
        {
            get { return FileMapView.ReadInt32(HSesInfoLenOffset); }
        }

        public int SessionInfoOffset
        {
            get { return FileMapView.ReadInt32(HSesInfoOffsetOffset); }
        }

        public int VarCount
        {
            get
            {
                return FileMapView.ReadInt32(HNumVarsOffset);
            }
        }

        public int VarHeaderOffset
        {
            get { return FileMapView.ReadInt32(HVarHeaderOffsetOffset); }
        }

        public int BufferCount
        {
            get { return FileMapView.ReadInt32(HNumBufOffset); }
        }

        public int BufferLength
        {
            get { return FileMapView.ReadInt32(HBufLenOffset); }
        }

        public int Buffer
        {
            get
            {
                return buffer.OffsetLatest;
            }
        }
    }
}
