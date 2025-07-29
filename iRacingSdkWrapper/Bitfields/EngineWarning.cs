using System;

namespace iRacingSdkWrapper.Bitfields
{
    public class EngineWarning : BitfieldBase<IRacingEngineWarnings>
    {
        public EngineWarning(int value)
            : base(value)
        {
        }
    }

    [Flags]
    public enum IRacingEngineWarnings : uint
    {
        WaterTemperatureWarning = 0x01,
        FuelPressureWarning = 0x02,
        OilPressureWarning = 0x04,
        EngineStalled = 0x08,
        PitSpeedLimiter = 0x10,
        RevLimiterActive = 0x20,
        OilTemperatureWarning = 0x40,
        MandatoryRepairsNeeded = 0x80,
        OptionalRepairsNeeded = 0x100,
    }
}
