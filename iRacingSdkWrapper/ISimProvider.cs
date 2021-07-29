using System;

namespace iRacingSdkWrapper
{
    public interface ISimProvider
    {
        bool IsConnected { get; } 
        TelemetryValue<T> GetTelemetryValue<T>(string name);
        event EventHandler<SdkWrapper.TelemetryUpdatedEventArgs> TelemetryUpdated;
    }
}