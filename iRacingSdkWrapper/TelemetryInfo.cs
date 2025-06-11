using iRSDKSharp;
using iRacingSdkWrapper.Bitfields;

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable HeapView.ObjectAllocation.Evident

namespace iRacingSdkWrapper
{
    /// <summary>
    /// Represents an object from which you can get Telemetry var headers by name
    /// </summary>
    public sealed class TelemetryInfo(iRacingSDK sdk)
    {
        public iRacingSDK Sdk = sdk;

        public TelemetryValue<float> DCMGUKDeployAdapt => new(Sdk, "dcMGUKDeployAdapt");

        public TelemetryValue<float> DCMGUKDeployFixed => new(Sdk, "dcMGUKDeployFixed");

        public TelemetryValue<float> DCMGUKRegenGain => new(Sdk, "dcMGUKRegenGain");

        public TelemetryValue<float> EnergyBatteryToMGU => new(Sdk, "EnergyBatteryToMGU_KLap");

        public TelemetryValue<float> EnergyBudgetBattToMGU => new(Sdk, "EnergyBudgetBattToMGU_KLap");

        public TelemetryValue<float> EnergyERSBattery => new(Sdk, "EnergyERSBattery");

        public TelemetryValue<float> PowerMGUH => new(Sdk, "PowerMGU_H");

        public TelemetryValue<float> PowerMGUK => new(Sdk, "PowerMGU_K");

        public TelemetryValue<float> TorqueMGUK => new(Sdk, "TorqueMGU_K");


        /// <summary>
        /// The number of laps you have completed. Note: on Nordschleife Tourist layout, you can complete a lap without starting a new one!
        /// </summary>
        public TelemetryValue<int> LapCompleted => new(Sdk, "LapCompleted");


        /// <summary>
        /// Seconds since session start. Unit: s
        /// </summary>
        public TelemetryValue<double> SessionTime => new(Sdk, "SessionTime");


        /// <summary>
        /// Session number. 
        /// </summary>
        public TelemetryValue<int> SessionNum => new(Sdk, "SessionNum");


        /// <summary>
        /// Session state. Unit: irsdk_SessionState
        /// </summary>
        public TelemetryValue<IRacingSessionStates> SessionState => new(Sdk, "SessionState");


        /// <summary>
        /// Session ID. 
        /// </summary>
        public TelemetryValue<int> SessionUniqueID { get { return new TelemetryValue<int>(Sdk, "SessionUniqueID"); } }


        /// <summary>
        /// Pit service flags. Unit: irsdk_PitSvFlags
        /// </summary>
        public TelemetryValue<PitServiceFlag> PitServiceFlags => new(Sdk, "PitSvFlags");


        /// <summary>
        /// Pit service status flags. Unit: irsdk_PitSvStatus
        /// </summary>
        public TelemetryValue<IRacingPitSvStatuses> PitSvStatuses => new(Sdk, "PlayerCarPitSvStatus");


        /// <summary>
        /// Session flags. Unit: irsdk_Flags
        /// </summary>
        public TelemetryValue<SessionFlag> SessionFlags => new(Sdk, "SessionFlags");


        /// <summary>
        /// Car Left Right. Unit: irsdk_CarLeftRight
        /// </summary>
        public TelemetryValue<IRacingCarLeftRights> CarLeftRights => new(Sdk, "CarLeftRight");


        /// <summary>
        /// Driver activated flag. 
        /// </summary>
        public TelemetryValue<bool> DriverMarker => new(Sdk, "DriverMarker");


        /// <summary>
        /// 0=replay not playing  1=replay playing. 
        /// </summary>
        public TelemetryValue<bool> IsReplayPlaying => new(Sdk, "IsReplayPlaying");


        ///// <summary>
        ///// Integer replay frame number (60 per second). 
        ///// </summary>
        //public TelemetryValue<int> ReplayFrameNum => new(Sdk, "ReplayFrameNum");


        /// <summary>
        /// Current lap number by car index
        /// WARNING: The CarIdxLap value is 2 instead of 1 on the beginning of the first lap of a race (https://members.iracing.com/jforum/posts/list/2525/1470675.page#10204636)
        /// WARNING: The CarIdxLap may increment early (https://members.iracing.com/jforum/posts/list/1200/1470675.page#8250587)
        /// </summary>
        public TelemetryValue<int[]> CarIdxLap => new(Sdk, "CarIdxLap");


        /// <summary>
        /// Current number of completed laps by car index. Note: On Nordschleife Tourist layout, cars can complete a lap without starting a new lap!
        /// </summary>
        public TelemetryValue<int[]> CarIdxLapCompleted => new(Sdk, "CarIdxLapCompleted");


        /// <summary>
        /// Percentage distance around lap by car index. Unit: %
        /// </summary>
        public TelemetryValue<float[]> CarIdxLapDistPct => new(Sdk, "CarIdxLapDistPct");


        /// <summary>
        /// Track surface type by car index. Unit: irsdk_TrkLoc
        /// </summary>
        public TelemetryValue<IRacingTrackSurfaces[]> CarIdxTrackSurface => new(Sdk, "CarIdxTrackSurface");


        /// <summary>
        /// Track surface material type by car index. Unit: irsdk_TrkSurf
        /// </summary>
        public TelemetryValue<IRacingTrackSurfaceMaterials[]> CarIdxTrackSurfaceMaterial => new(Sdk, "CarIdxTrackSurfaceMaterial");


        ///// <summary>
        ///// Steering wheel angle by car index. Unit: rad
        ///// </summary>
        //public TelemetryValue<float[]> CarIdxSteer => new(Sdk, "CarIdxSteer");


        /// <summary>
        /// Engine rpm by car index. Unit: revs/min
        /// </summary>
        public TelemetryValue<float[]> CarIdxRPM => new(Sdk, "CarIdxRPM");


        /// <summary>
        /// -1=reverse  0=neutral  1..n=current gear by car index. 
        /// </summary>
        public TelemetryValue<int[]> CarIdxGear => new(Sdk, "CarIdxGear");


        /// <summary>
        /// Number of fast repairs used by car index. Stays 0 in practice and qual 
        /// </summary>
        public TelemetryValue<int[]> CarIdxFastRepairsUsed => new(Sdk, "CarIdxFastRepairsUsed");


        /// <summary>
        /// Session flags by car index
        /// </summary>
        public TelemetryValue<IRacingSessionFlags[]> CarIdxSessionFlags => new(Sdk, "CarIdxSessionFlags");


        // /// <summary>
        // /// Race time behind leader or fastest lap time otherwise
        // /// </summary>
        // public TelemetryValue<float[]> CarIdxF2Time => new(Sdk, "CarIdxF2Time");


        /// <summary>
        /// Estimated time to reach current location on track
        /// </summary>
        public TelemetryValue<float[]> CarIdxEstTime => new(Sdk, "CarIdxEstTime");


        /// <summary>
        /// On pit road between the cones by car index
        /// </summary>
        public TelemetryValue<bool[]> CarIdxOnPitRoad => new(Sdk, "CarIdxOnPitRoad");


        /// <summary>
        /// Cars position in race by car index
        /// </summary>
        public TelemetryValue<int[]> CarIdxPosition => new(Sdk, "CarIdxPosition");


        /// <summary>
        /// Cars class index in race by car index
        /// </summary>
        public TelemetryValue<int[]> CarIdxClass => new(Sdk, "CarIdxClass");


        /// <summary>
        /// Cars class position in race by car index
        /// </summary>
        public TelemetryValue<int[]> CarIdxClassPosition => new(Sdk, "CarIdxClassPosition");


        // /// <summary>
        // /// Cars best lap number
        // /// </summary>
        // public TelemetryValue<int[]> CarIdxBestLapNum => new(Sdk, "CarIdxBestLapNum");


        /// <summary>
        /// -1: No Selection/Unknown
        /// 0: Soft ?
        /// 1: Medium ?
        /// 2: Hard ?
        /// </summary>
        public TelemetryValue<int[]> CarIdxTireCompound => new(Sdk, "CarIdxTireCompound");


        /// <summary>
        /// Tire compound index with which each team has set their best time in Qual - it will be -1 if none has been locked in so far
        /// -1: Not locked
        /// 0: Soft ?
        /// 1: Medium ?
        /// 2: Hard ?
        /// </summary>
        public TelemetryValue<int[]> CarIdxQualTireCompound => new(Sdk, "CarIdxQualTireCompound");


        /// <summary>
        ///  Indicates whether or not Race Control has officially locked that team in to that compound choice for the start of the race
        /// -1: Not locked
        /// 0: Soft ?
        /// 1: Medium ?
        /// 2: Hard ?
        /// </summary>
        public TelemetryValue<bool[]> CarIdxQualTireCompoundLocked => new(Sdk, "CarIdxQualTireCompoundLocked");


        /// <summary>
        /// Cars best lap time
        /// </summary>
        public TelemetryValue<float[]> CarIdxBestLapTime => new(Sdk, "CarIdxBestLapTime");


        /// <summary>
        /// Cars last lap time
        /// </summary>
        public TelemetryValue<float[]> CarIdxLastLapTime => new(Sdk, "CarIdxLastLapTime");


        // /// <summary>
        // /// Push2Pass count of usage (or remaining in Race)
        // /// </summary>
        // public TelemetryValue<int[]> CarIdxP2PCount => new(Sdk, "CarIdxP2P_Count");


        // /// <summary>
        // /// Push2Pass active or not
        // /// </summary>
        // public TelemetryValue<bool[]> CarIdxP2PStatus => new(Sdk, "CarIdxP2P_Status");


        /// <summary>
        /// Steering wheel angle. Unit: rad
        /// </summary>
        public TelemetryValue<float> SteeringWheelAngle => new(Sdk, "SteeringWheelAngle");


        /// <summary>
        /// 0=off throttle to 1=full throttle. Unit: %
        /// </summary>
        public TelemetryValue<float> Throttle => new(Sdk, "Throttle");


        /// <summary>
        /// 0=brake released to 1=max pedal force. Unit: %
        /// </summary>
        public TelemetryValue<float> Brake => new(Sdk, "Brake");


        /// <summary>
        /// 0=disengaged to 1=fully engaged. Unit: %
        /// </summary>
        public TelemetryValue<float> Clutch => new(Sdk, "Clutch");


        /// <summary>
        /// -1=reverse  0=neutral  1..n=current gear. 
        /// </summary>
        public TelemetryValue<int> Gear => new(Sdk, "Gear");


        /// <summary>
        /// Engine rpm. Unit: revs/min
        /// </summary>
        public TelemetryValue<float> RPM => new(Sdk, "RPM");


        /// <summary>
        /// Lap count. 
        /// </summary>
        public TelemetryValue<int> Lap => new(Sdk, "Lap");


        /// <summary>
        /// Meters traveled from S/F this lap. Unit: m
        /// </summary>
        public TelemetryValue<float> LapDist => new(Sdk, "LapDist");


        /// <summary>
        /// Percentage distance around lap. Unit: %
        /// </summary>
        public TelemetryValue<float> LapDistPct => new(Sdk, "LapDistPct");


        // /// <summary>
        // /// Laps started in race. 
        // /// </summary>
        public TelemetryValue<int> RaceLaps => new(Sdk, "RaceLaps");


        /// <summary>
        /// Longitudinal acceleration (including gravity). Unit: m/s^2
        /// </summary>
        public TelemetryValue<float> LongAccel => new(Sdk, "LongAccel");


        /// <summary>
        /// Lateral acceleration (including gravity). Unit: m/s^2
        /// </summary>
        public TelemetryValue<float> LatAccel => new(Sdk, "LatAccel");


        /// <summary>
        /// Vertical acceleration (including gravity). Unit: m/s^2
        /// </summary>
        public TelemetryValue<float> VertAccel => new(Sdk, "VertAccel");


        /// <summary>
        /// Roll rate. Unit: rad/s
        /// </summary>
        public TelemetryValue<float> RollRate => new(Sdk, "RollRate");


        /// <summary>
        /// Pitch rate. Unit: rad/s
        /// </summary>
        public TelemetryValue<float> PitchRate => new(Sdk, "PitchRate");


        /// <summary>
        /// Yaw rate. Unit: rad/s
        /// </summary>
        public TelemetryValue<float> YawRate => new(Sdk, "YawRate");


        /// <summary>
        /// GPS vehicle speed. Unit: m/s
        /// </summary>
        public TelemetryValue<float> Speed => new(Sdk, "Speed");


        /// <summary>
        /// X velocity. Unit: m/s
        /// </summary>
        public TelemetryValue<float> VelocityX => new(Sdk, "VelocityX");


        /// <summary>
        /// Y velocity. Unit: m/s
        /// </summary>
        public TelemetryValue<float> VelocityY => new(Sdk,
            "VelocityY");


        /// <summary>
        /// Z velocity. Unit: m/s
        /// </summary>
        public TelemetryValue<float> VelocityZ => new(Sdk, "VelocityZ");


        /// <summary>
        /// Yaw orientation. Unit: rad
        /// Relative to whatever orientation the art team laid out the track. Useful for matching overlay data on a map
        /// </summary>
        public TelemetryValue<float> Yaw => new(Sdk, "Yaw");


        /// <summary>
        /// Yaw orientation relative to north. Unit: rad
        /// </summary>
        public TelemetryValue<float> YawNorth => new(Sdk, "YawNorth");


        /// <summary>
        /// Pitch orientation. Unit: rad
        /// </summary>
        public TelemetryValue<float> Pitch => new(Sdk, "Pitch");


        /// <summary>
        /// Roll orientation. Unit: rad
        /// </summary>
        public TelemetryValue<float> Roll => new(Sdk, "Roll");


        /// <summary>
        /// Active camera's focus car index. 
        /// </summary>
        public TelemetryValue<int> CamCarIdx => new(Sdk, "CamCarIdx");


        /// <summary>
        /// Active camera number. 
        /// </summary>
        public TelemetryValue<int> CamCameraNumber => new(Sdk, "CamCameraNumber");


        /// <summary>
        /// Active camera group number. 
        /// </summary>
        public TelemetryValue<int> CamGroupNumber => new(Sdk, "CamGroupNumber");


        // /// <summary>
        // /// State of camera system. Unit: irsdk_CameraState
        // /// </summary>
        // public TelemetryValue<CameraState> CamCameraState => new(Sdk, "CamCameraState");


        /// <summary>
        /// 1=Car on track physics running. 
        /// </summary>
        public TelemetryValue<bool> IsOnTrack => new(Sdk, "IsOnTrack");


        /// <summary>
        /// 1=Car in garage physics running. 
        /// </summary>
        public TelemetryValue<bool> IsInGarage => new(Sdk, "IsInGarage");


        /// <summary>
        /// Output torque on steering shaft. Unit: N*m
        /// </summary>
        public TelemetryValue<float> SteeringWheelTorque => new(Sdk, "SteeringWheelTorque");


        /// <summary>
        /// Force feedback % max torque on steering shaft. Unit: %
        /// </summary>
        public TelemetryValue<float> SteeringWheelPctTorque => new(Sdk, "SteeringWheelPctTorque");


        // /// <summary>
        // /// Percent of shift indicator to light up. Unit: %
        // /// </summary>
        // public TelemetryValue<float> ShiftIndicatorPct => new(Sdk, "ShiftIndicatorPct");


        /// <summary>
        /// Bitfield for warning lights. Unit: irsdk_EngineWarnings
        /// </summary>
        public TelemetryValue<EngineWarning> EngineWarnings => new(Sdk, "EngineWarnings");


        /// <summary>
        /// Liters/Kilowatt-hours of fuel remaining. Unit: l or kWh
        /// </summary>
        public TelemetryValue<float> FuelLevel => new(Sdk, "FuelLevel");


        /// <summary>
        /// Percent fuel remaining. Unit: %
        /// </summary>
        public TelemetryValue<float> FuelLevelPct => new(Sdk, "FuelLevelPct");


        /// <summary>
        /// Replay playback speed. 
        /// </summary>
        public TelemetryValue<int> ReplayPlaySpeed => new(Sdk, "ReplayPlaySpeed");


        /// <summary>
        /// 0=not slow motion  1=replay is in slow motion. 
        /// </summary>
        public TelemetryValue<bool> ReplayPlaySlowMotion => new(Sdk, "ReplayPlaySlowMotion");


        /// <summary>
        /// Seconds since replay session start. Unit: s
        /// </summary>
        public TelemetryValue<double> ReplaySessionTime => new(Sdk, "ReplaySessionTime");


        /// <summary>
        /// Replay session number. 
        /// </summary>
        public TelemetryValue<int> ReplaySessionNum => new(Sdk, "ReplaySessionNum");


        /// <summary>
        /// Engine coolant temp. Unit: C
        /// </summary>
        public TelemetryValue<float> WaterTemp => new(Sdk, "WaterTemp");


        /// <summary>
        /// Engine coolant level. Unit: l
        /// </summary>
        public TelemetryValue<float> WaterLevel => new(Sdk, "WaterLevel");

        public TelemetryValue<float> ManifoldPress => new(Sdk, "ManifoldPress");


        /// <summary>
        /// Engine fuel pressure. Unit: bar
        /// </summary>
        public TelemetryValue<float> FuelPress => new(Sdk, "FuelPress");


        /// <summary>
        /// Engine oil temperature. Unit: C
        /// </summary>
        public TelemetryValue<float> OilTemp => new(Sdk, "OilTemp");


        /// <summary>
        /// Engine oil pressure. Unit: bar
        /// </summary>
        public TelemetryValue<float> OilPress => new(Sdk, "OilPress");


        /// <summary>
        /// Engine oil level. Unit: l
        /// </summary>
        public TelemetryValue<float> OilLevel => new(Sdk, "OilLevel");


        /// <summary>
        /// Engine voltage. Unit: V
        /// </summary>
        public TelemetryValue<float> Voltage => new(Sdk, "Voltage");

        public TelemetryValue<double> SessionTimeRemain => new(Sdk, "SessionTimeRemain");

        /// <summary>
        /// Integer replay frame number from end of tape
        /// </summary>
        public TelemetryValue<int> ReplayFrameNumEnd => new(Sdk, "ReplayFrameNumEnd");

        public TelemetryValue<float> AirDensity => new(Sdk, "AirDensity");

        public TelemetryValue<float> AirPressure => new(Sdk, "AirPressure");

        public TelemetryValue<float> AirTemp => new(Sdk, "AirTemp");

        public TelemetryValue<float> FogLevel => new(Sdk, "FogLevel");

        public TelemetryValue<int> Skies => new(Sdk, "Skies");

        public TelemetryValue<float> TrackTemp => new(Sdk, "TrackTemp");

        public TelemetryValue<float> TrackTempCrew => new(Sdk, "TrackTempCrew");

        public TelemetryValue<float> RelativeHumidity => new(Sdk, "RelativeHumidity");

        public TelemetryValue<float> Precipitation => new(Sdk, "Precipitation");

        public TelemetryValue<float> WindDir => new(Sdk, "WindDir");

        public TelemetryValue<float> WindVel => new(Sdk, "WindVel");

        /// <summary>
        /// The car's total incident count
        /// </summary>
        public TelemetryValue<int> PlayerCarTeamIncidentCount => new(Sdk, "PlayerCarTeamIncidentCount");

        /// <summary>
        /// Player's contribution to the car's total incident count
        /// </summary>
        public TelemetryValue<int> PlayerCarMyIncidentCount => new(Sdk, "PlayerCarMyIncidentCount");

        /// <summary>
        /// The current driver's contribution to the car's total incident count
        /// </summary>
        public TelemetryValue<int> PlayerCarDriverIncidentCount => new(Sdk, "PlayerCarDriverIncidentCount");

        public TelemetryValue<IRacingTrackSurfaces> PlayerTrackSurface => new(Sdk, "PlayerTrackSurface");

        public TelemetryValue<int> PlayerCarIdx => new(Sdk, "PlayerCarIdx");

        public TelemetryValue<bool> WeatherDeclaredWet => new(Sdk, "WeatherDeclaredWet");

        public TelemetryValue<IRacingTrackWetness> TrackWetness => new(Sdk, "TrackWetness");

        public TelemetryValue<float> LFwearL => new(Sdk, "LFwearL");

        public TelemetryValue<float> LFwearM => new(Sdk, "LFwearM");

        public TelemetryValue<float> LFwearR => new(Sdk, "LFwearR");

        public TelemetryValue<float> RFwearL => new(Sdk, "RFwearL");

        public TelemetryValue<float> RFwearM => new(Sdk, "RFwearM");

        public TelemetryValue<float> RFwearR => new(Sdk, "RFwearR");

        public TelemetryValue<float> LRwearL => new(Sdk, "LRwearL");

        public TelemetryValue<float> LRwearM => new(Sdk, "LRwearM");

        public TelemetryValue<float> LRwearR => new(Sdk, "LRwearR");

        public TelemetryValue<float> RRwearL => new(Sdk, "RRwearL");

        public TelemetryValue<float> RRwearM => new(Sdk, "RRwearM");

        public TelemetryValue<float> RRwearR => new(Sdk, "RRwearR");

        public TelemetryValue<float> LFcoldPressure => new(Sdk, "LFcoldPressure");

        public TelemetryValue<float> RFcoldPressure => new(Sdk, "RFcoldPressure");

        public TelemetryValue<float> LRcoldPressure => new(Sdk, "LRcoldPressure");

        public TelemetryValue<float> RRcoldPressure => new(Sdk, "RRcoldPressure");

        public TelemetryValue<float> DPLFTireColdPress => new(Sdk, "dpLFTireColdPress");

        public TelemetryValue<float> DPRFTireColdPress => new(Sdk, "dpRFTireColdPress");

        public TelemetryValue<float> DPLRTireColdPress => new(Sdk, "dpLRTireColdPress");

        public TelemetryValue<float> DPRRTireColdPress => new(Sdk, "dpRRTireColdPress");

        public TelemetryValue<float> DPTireChange => new(Sdk, "dpTireChange");

        public TelemetryValue<float> DPLFTireChange => new(Sdk, "dpLFTireChange");

        public TelemetryValue<float> DPRFTireChange => new(Sdk, "dpRFTireChange");

        public TelemetryValue<float> DPLRTireChange => new(Sdk, "dpLRTireChange");

        public TelemetryValue<float> DPRRTireChange => new(Sdk, "dpRRTireChange");

        public TelemetryValue<float> PitSvLFP => new(Sdk, "PitSvLFP");

        public TelemetryValue<float> PitSvRFP => new(Sdk, "PitSvRFP");

        public TelemetryValue<float> PitSvLRP => new(Sdk, "PitSvLRP");

        public TelemetryValue<float> PitSvRRP => new(Sdk, "PitSvRRP");

        public TelemetryValue<float> DPFuelAddKg => new(Sdk, "dpFuelAddKg");

        public TelemetryValue<float> DPFuelFill => new(Sdk, "dpFuelFill");

        public TelemetryValue<float> DPWindshieldTearoff => new(Sdk, "dpWindshieldTearoff");

        public TelemetryValue<float> DPFastRepair => new(Sdk, "dpFastRepair");

        public TelemetryValue<float> PitOptRepairLeft => new(Sdk, "PitOptRepairLeft");

        public TelemetryValue<float> PitRepairLeft => new(Sdk, "PitRepairLeft");

        public TelemetryValue<float> PitSvFuel => new(Sdk, "PitSvFuel");

        public TelemetryValue<float> DCBrakeBias => new(Sdk, "dcBrakeBias");

        public TelemetryValue<float> DCTractionControl => new(Sdk, "dcTractionControl");

        public TelemetryValue<float> DCABS => new(Sdk, "dcABS");

        public TelemetryValue<float> SessionTimeOfDay => new(Sdk, "SessionTimeOfDay");

        /// <summary>
        /// Delta time for session last lap
        /// </summary>
        public TelemetryValue<float> LapDeltaToSessionLastlLap => new(Sdk, "LapDeltaToSessionLastlLap");

        /// <summary>
        /// Delta time for session best lap
        /// </summary>
        public TelemetryValue<float> LapDeltaToSessionBestLap => new(Sdk, "LapDeltaToSessionBestLap");

        /// <summary>
        /// Delta time for session best lap is valid
        /// </summary>
        public TelemetryValue<bool> LapDeltaToSessionBestLapOK => new(Sdk, "LapDeltaToSessionBestLap_OK");

        public TelemetryValue<float> LapBestLapTime => new(Sdk, "LapBestLapTime");

        public TelemetryValue<float> LapLastLapTime => new(Sdk, "LapLastLapTime");

        public TelemetryValue<int> PlayerCarDryTireSetLimit => new(Sdk, "PlayerCarDryTireSetLimit");

        public TelemetryValue<int> PitSvTireCompound => new(Sdk, "PitSvTireCompound");
        public TelemetryValue<int> PlayerTireCompound => new(Sdk, "PlayerTireCompound");


        /// <summary>
        /// Current DRS status. 0 = inactive, 1 = can be activated in next DRS zone, 2 = can be activated now, 3 = active.
        /// </summary>
        public TelemetryValue<int> DRSStatus => new(Sdk, "DRS_Status");

        public TelemetryValue<int> DRSCount => new(Sdk, "DRS_Count");

        public TelemetryValue<int> EnterExitReset => new(Sdk, "EnterExitReset");

        public TelemetryValue<int> FastRepairUsed => new(Sdk, "FastRepairUsed");

        public TelemetryValue<int> FastRepairAvailable => new(Sdk, "FastRepairAvailable");

        public TelemetryValue<int> LFTiresAvailable => new(Sdk, "LFTiresAvailable");

        public TelemetryValue<int> RFTiresAvailable => new(Sdk, "RFTiresAvailable");

        public TelemetryValue<int> LRTiresAvailable => new(Sdk, "LRTiresAvailable");

        public TelemetryValue<int> RRTiresAvailable => new(Sdk, "RRTiresAvailable");

        public TelemetryValue<int> LFTiresUsed => new(Sdk, "LFTiresUsed");

        public TelemetryValue<int> RFTiresUsed => new(Sdk, "RFTiresUsed");

        public TelemetryValue<int> LRTiresUsed => new(Sdk, "LRTiresUsed");

        public TelemetryValue<int> RRTiresUsed => new(Sdk, "RRTiresUsed");

        public TelemetryValue<int> LeftTireSetsUsed => new(Sdk, "LeftTireSetsUsed");

        public TelemetryValue<int> RightTireSetsUsed => new(Sdk, "RightTireSetsUsed");

        public TelemetryValue<int> FrontTireSetsUsed => new(Sdk, "FrontTireSetsUsed");

        public TelemetryValue<int> RearTireSetsUsed => new(Sdk, "RearTireSetsUsed");

        public TelemetryValue<int> TireSetsUsed => new(Sdk, "TireSetsUsed");

        public TelemetryValue<int> TireSetsAvailable => new(Sdk, "TireSetsAvailable");

        public TelemetryValue<int> LeftTireSetsAvailable => new(Sdk, "LeftTireSetsAvailable");

        public TelemetryValue<int> RightTireSetsAvailable => new(Sdk, "RightTireSetsAvailable");

        public TelemetryValue<int> FrontTireSetsAvailable => new(Sdk, "FrontTireSetsAvailable");

        public TelemetryValue<int> RearTireSetsAvailable => new(Sdk, "RearTireSetsAvailable");

        public TelemetryValue<int> DisplayUnits => new(Sdk, "DisplayUnits");

        public TelemetryValue<int> SessionLapsRemainEx => new(Sdk, "SessionLapsRemainEx");

        public TelemetryValue<int> PlayerCarPosition => new(Sdk, "PlayerCarPosition");

        public TelemetryValue<int> PlayerCarClassPosition => new(Sdk, "PlayerCarClassPosition");

        /// <summary>
        /// Players best lap number
        /// </summary>
        public TelemetryValue<int> LapBestLap => new(Sdk, "LapBestLap");

        public TelemetryValue<int> RadioTransmitCarIdx => new(Sdk, "RadioTransmitCarIdx");

        public TelemetryValue<int> RadioTransmitRadioIdx => new(Sdk, "RadioTransmitRadioIdx");

        public TelemetryValue<int> RadioTransmitFrequencyIdx => new(Sdk, "RadioTransmitFrequencyIdx");

        public TelemetryValue<bool> PitsOpen => new(Sdk, "PitsOpen");

        public TelemetryValue<bool> BrakeABSactive => new(Sdk, "BrakeABSactive");

        public TelemetryValue<bool> DCTriggerWindshieldWipers => new(Sdk, "dcTriggerWindshieldWipers");

        public TelemetryValue<bool> DCToggleWindshieldWipers => new(Sdk, "dcToggleWindshieldWipers");

        public TelemetryValue<bool> IsDiskLoggingActive => new(Sdk, "IsDiskLoggingActive");

        public TelemetryValue<bool> IsDiskLoggingEnabled => new(Sdk, "IsDiskLoggingEnabled");

        public TelemetryValue<bool> PitstopActive => new(Sdk, "PitstopActive");

        public TelemetryValue<bool> PlayerCarInPitStall => new(Sdk, "PlayerCarInPitStall");

        public TelemetryValue<bool> IsOnTrackCar => new(Sdk, "IsOnTrackCar");

        public TelemetryValue<bool> OnPitRoad => new(Sdk, "OnPitRoad");

        public TelemetryValue<float> PlayerCarTowTime => new(Sdk, "PlayerCarTowTime");
        public TelemetryValue<float> CpuUsageFG => new(Sdk, "CpuUsageFG");
        public TelemetryValue<float> GpuUsage => new(Sdk, "GpuUsage");
    }
}