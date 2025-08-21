using System;

namespace iRacingSdkWrapper.Bitfields
{
    public class PaceFlags : BitfieldBase<IRacingPaceFlags>
    {
        public PaceFlags() : this(0) { }

        public PaceFlags(int value) : base(value)
        { }
    }

    [Flags]
    public enum IRacingPaceFlags : uint
    {
        EndOfLine = 0x01,
        FreePass = 0x02,
        WavedAround = 0x04,
    }
}
