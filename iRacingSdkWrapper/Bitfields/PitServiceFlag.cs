﻿using System;

namespace iRacingSdkWrapper.Bitfields
{
    public class PitServiceFlag : BitfieldBase<IRacingPitServiceFlags>
    {
        public PitServiceFlag() : this(0) { }

        public PitServiceFlag(int value) : base(value)
        { }
    }

    [Flags]
    public enum IRacingPitServiceFlags : uint
    {
        LFTireChange = 0x0001,
        RFTireChange = 0x0002,
        LRTireChange = 0x0004,
        RRTireChange = 0x0008,
        FuelFill = 0x0010,
        WindshieldTearoff = 0x0020,
        FastRepair = 0x0040
    }
}
