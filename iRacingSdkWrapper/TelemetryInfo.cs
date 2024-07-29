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

        private TelemetryValue<float> _dcMGUKDeployAdapt;
        public TelemetryValue<float> DCMGUKDeployAdapt => _dcMGUKDeployAdapt ??= new(Sdk, "dcMGUKDeployAdapt");

        private TelemetryValue<float> _dcMGUKDeployFixed;
        public TelemetryValue<float> DCMGUKDeployFixed => _dcMGUKDeployFixed ??= new(Sdk, "dcMGUKDeployFixed");

        private TelemetryValue<float> _dcMGUKRegenGain;
        public TelemetryValue<float> DCMGUKRegenGain => _dcMGUKRegenGain ??= new(Sdk, "dcMGUKRegenGain");

        private TelemetryValue<float> _energyBatteryToMGU;
        public TelemetryValue<float> EnergyBatteryToMGU => _energyBatteryToMGU ??= new(Sdk, "EnergyBatteryToMGU_KLap");

        private TelemetryValue<float> _energyBudgetBattToMGU;
        public TelemetryValue<float> EnergyBudgetBattToMGU => _energyBudgetBattToMGU ??= new(Sdk, "EnergyBudgetBattToMGU_KLap");

        private TelemetryValue<float> _energyERSBattery;
        public TelemetryValue<float> EnergyERSBattery => _energyERSBattery ??= new(Sdk, "EnergyERSBattery");

        private TelemetryValue<float> _powerMGUH;
        public TelemetryValue<float> PowerMGUH => _powerMGUH ??= new(Sdk, "PowerMGU_H");

        private TelemetryValue<float> _powerMGUK;
        public TelemetryValue<float> PowerMGUK => _powerMGUK ??= new(Sdk, "PowerMGU_K");

        private TelemetryValue<float> _torqueMGUK;
        public TelemetryValue<float> TorqueMGUK => _torqueMGUK ??= new(Sdk, "TorqueMGU_K");

        private TelemetryValue<int> _lapCompleted;
        /// <summary>
        /// The number of laps you have completed. Note: on Nordschleife Tourist layout, you can complete a lap without starting a new one!
        /// </summary>
        public TelemetryValue<int> LapCompleted => _lapCompleted ??= new(Sdk, "LapCompleted");

        private TelemetryValue<double> _sessionTime;
        /// <summary>
        /// Seconds since session start. Unit: s
        /// </summary>
        public TelemetryValue<double> SessionTime => _sessionTime ??= new(Sdk, "SessionTime");

        private TelemetryValue<int> _sessionNum;
        /// <summary>
        /// Session number. 
        /// </summary>
        public TelemetryValue<int> SessionNum => _sessionNum ??= new(Sdk, "SessionNum");

        private TelemetryValue<IRacingSessionStates> _sessionState;
        /// <summary>
        /// Session state. Unit: irsdk_SessionState
        /// </summary>
        public TelemetryValue<IRacingSessionStates> SessionState => _sessionState ??= new(Sdk, "SessionState");

        private TelemetryValue<int> _sessionUniqueID;
        /// <summary>
        /// Session ID. 
        /// </summary>
        public TelemetryValue<int> SessionUniqueID => _sessionUniqueID ??= new(Sdk, "SessionUniqueID");

        private TelemetryValue<PitServiceFlag> _pitServiceFlags;
        /// <summary>
        /// Pit service flags. Unit: irsdk_PitSvFlags
        /// </summary>
        public TelemetryValue<PitServiceFlag> PitServiceFlags => _pitServiceFlags ??= new(Sdk, "PitSvFlags");

        private TelemetryValue<IRacingPitSvStatuses> _pitSvStatuses;
        /// <summary>
        /// Pit service status flags. Unit: irsdk_PitSvStatus
        /// </summary>
        public TelemetryValue<IRacingPitSvStatuses> PitSvStatuses => _pitSvStatuses ??= new(Sdk, "PlayerCarPitSvStatus");

        private TelemetryValue<SessionFlag> _sessionFlags;
        /// <summary>
        /// Session flags. Unit: irsdk_Flags
        /// </summary>
        public TelemetryValue<SessionFlag> SessionFlags => _sessionFlags ??= new(Sdk, "SessionFlags");

        private TelemetryValue<IRacingCarLeftRights> _carLeftRights;
        /// <summary>
        /// Car Left Right. Unit: irsdk_CarLeftRight
        /// </summary>
        public TelemetryValue<IRacingCarLeftRights> CarLeftRights => _carLeftRights ??= new(Sdk, "CarLeftRight");

        private TelemetryValue<bool> _driverMarker;
        /// <summary>
        /// Driver activated flag. 
        /// </summary>
        public TelemetryValue<bool> DriverMarker => _driverMarker ??= new(Sdk, "DriverMarker");

        private TelemetryValue<bool> _isReplayPlaying;
        /// <summary>
        /// 0=replay not playing  1=replay playing. 
        /// </summary>
        public TelemetryValue<bool> IsReplayPlaying => _isReplayPlaying ??= new(Sdk, "IsReplayPlaying");

        private TelemetryValue<int> _replayFrameNum;
        /// <summary>
        /// Integer replay frame number (60 per second). 
        /// </summary>
        public TelemetryValue<int> ReplayFrameNum => _replayFrameNum ??= new(Sdk, "ReplayFrameNum");

        private TelemetryValue<int[]> _carIdxLap;
        /// <summary>
        /// Current lap number by car index
        /// </summary>
        public TelemetryValue<int[]> CarIdxLap => _carIdxLap ??= new(Sdk, "CarIdxLap");

        private TelemetryValue<int[]> _carIdxLapCompleted;
        /// <summary>
        /// Current number of completed laps by car index. Note: On Nordschleife Tourist layout, cars can complete a lap without starting a new lap!
        /// </summary>
        public TelemetryValue<int[]> CarIdxLapCompleted => _carIdxLapCompleted ??= new(Sdk, "CarIdxLapCompleted");

        private TelemetryValue<float[]> _carIdxLapDistPct;
        /// <summary>
        /// Percentage distance around lap by car index. Unit: %
        /// </summary>
        public TelemetryValue<float[]> CarIdxLapDistPct => _carIdxLapDistPct ??= new(Sdk, "CarIdxLapDistPct");

        private TelemetryValue<IRacingTrackSurfaces[]> _carIdxTrackSurface;
        /// <summary>
        /// Track surface type by car index. Unit: irsdk_TrkLoc
        /// </summary>
        public TelemetryValue<IRacingTrackSurfaces[]> CarIdxTrackSurface => _carIdxTrackSurface ??= new(Sdk, "CarIdxTrackSurface");

        private TelemetryValue<IRacingTrackSurfaceMaterials[]> _carIdxTrackSurfaceMaterial;
        /// <summary>
        /// Track surface material type by car index. Unit: irsdk_TrkSurf
        /// </summary>
        public TelemetryValue<IRacingTrackSurfaceMaterials[]> CarIdxTrackSurfaceMaterial => _carIdxTrackSurfaceMaterial ??= new(Sdk, "CarIdxTrackSurfaceMaterial");

        private TelemetryValue<float[]> _carIdxSteer;
        /// <summary>
        /// Steering wheel angle by car index. Unit: rad
        /// </summary>
        public TelemetryValue<float[]> CarIdxSteer => _carIdxSteer ??= new(Sdk, "CarIdxSteer");

        private TelemetryValue<float[]> _carIdxRPM;
        /// <summary>
        /// Engine rpm by car index. Unit: revs/min
        /// </summary>
        public TelemetryValue<float[]> CarIdxRPM => _carIdxRPM ??= new(Sdk, "CarIdxRPM");

        private TelemetryValue<int[]> _carIdxGear;
        /// <summary>
        /// -1=reverse  0=neutral  1..n=current gear by car index. 
        /// </summary>
        public TelemetryValue<int[]> CarIdxGear => _carIdxGear ??= new(Sdk, "CarIdxGear");

        private TelemetryValue<int[]> _carIdxFastRepairsUsed;
        /// <summary>
        /// Number of fast repairs used by car index. Stays 0 in practice and qual 
        /// </summary>
        public TelemetryValue<int[]> CarIdxFastRepairsUsed => _carIdxFastRepairsUsed ??= new(Sdk, "CarIdxFastRepairsUsed");

        private TelemetryValue<IRacingSessionFlags[]> _carIdxSessionFlags;
        /// <summary>
        /// Session flags by car index
        /// </summary>
        public TelemetryValue<IRacingSessionFlags[]> CarIdxSessionFlags => _carIdxSessionFlags ??= new(Sdk, "CarIdxSessionFlags");

        private TelemetryValue<float[]> _carIdxF2Time;
        /// <summary>
        /// Race time behind leader or fastest lap time otherwise
        /// </summary>
        public TelemetryValue<float[]> CarIdxF2Time => _carIdxF2Time ??= new(Sdk, "CarIdxF2Time");

        private TelemetryValue<float[]> _carIdxEstTime;
        /// <summary>
        /// Estimated time to reach current location on track
        /// </summary>
        public TelemetryValue<float[]> CarIdxEstTime => _carIdxEstTime ??= new(Sdk, "CarIdxEstTime");

        private TelemetryValue<bool[]> _carIdxOnPitRoad;
        /// <summary>
        /// On pit road between the cones by car index
        /// </summary>
        public TelemetryValue<bool[]> CarIdxOnPitRoad => _carIdxOnPitRoad ??= new(Sdk, "CarIdxOnPitRoad");

        private TelemetryValue<int[]> _carIdxPosition;
        /// <summary>
        /// Cars position in race by car index
        /// </summary>
        public TelemetryValue<int[]> CarIdxPosition => _carIdxPosition ??= new(Sdk, "CarIdxPosition");

        private TelemetryValue<int[]> _carIdxClass;
        /// <summary>
        /// Cars class index in race by car index
        /// </summary>
        public TelemetryValue<int[]> CarIdxClass => _carIdxClass ??= new(Sdk, "CarIdxClass");

        private TelemetryValue<int[]> _carIdxClassPosition;
        /// <summary>
        /// Cars class position in race by car index
        /// </summary>
        public TelemetryValue<int[]> CarIdxClassPosition => _carIdxClassPosition ??= new(Sdk, "CarIdxClassPosition");

        private TelemetryValue<int[]> _carIdxBestLapNum;
        /// <summary>
        /// Cars best lap number
        /// </summary>
        public TelemetryValue<int[]> CarIdxBestLapNum => _carIdxBestLapNum ??= new(Sdk, "CarIdxBestLapNum");

        private TelemetryValue<int[]> _carIdxTireCompound;
        /// <summary>
        /// -1: No Selection/Unknown
        /// 0: Soft ?
        /// 1: Medium ?
        /// 2: Hard ?
        /// </summary>
        public TelemetryValue<int[]> CarIdxTireCompound => _carIdxTireCompound ??= new(Sdk, "CarIdxTireCompound");

        private TelemetryValue<int[]> _carIdxQualTireCompound;
        /// <summary>
        /// Tire compound index with which each team has set their best time in Qual - it will be -1 if none has been locked in so far
        /// -1: Not locked
        /// 0: Soft ?
        /// 1: Medium ?
        /// 2: Hard ?
        /// </summary>
        public TelemetryValue<int[]> CarIdxQualTireCompound => _carIdxQualTireCompound ??= new(Sdk, "CarIdxQualTireCompound");

        private TelemetryValue<bool[]> _carIdxQualTireCompoundLocked;
        /// <summary>
        ///  Indicates whether or not Race Control has officially locked that team in to that compound choice for the start of the race
        /// -1: Not locked
        /// 0: Soft ?
        /// 1: Medium ?
        /// 2: Hard ?
        /// </summary>
        public TelemetryValue<bool[]> CarIdxQualTireCompoundLocked => _carIdxQualTireCompoundLocked ??= new(Sdk, "CarIdxQualTireCompoundLocked");

        private TelemetryValue<float[]> _carIdxBestLapTime;
        /// <summary>
        /// Cars best lap time
        /// </summary>
        public TelemetryValue<float[]> CarIdxBestLapTime => _carIdxBestLapTime ??= new(Sdk, "CarIdxBestLapTime");

        private TelemetryValue<float[]> _carIdxLastLapTime;
        /// <summary>
        /// Cars last lap time
        /// </summary>
        public TelemetryValue<float[]> CarIdxLastLapTime => _carIdxLastLapTime ??= new(Sdk, "CarIdxLastLapTime");

        private TelemetryValue<int[]> _carIdxP2PCount;
        /// <summary>
        /// Push2Pass count of usage (or remaining in Race)
        /// </summary>
        public TelemetryValue<int[]> CarIdxP2PCount => _carIdxP2PCount ??= new(Sdk, "CarIdxP2P_Count");

        private TelemetryValue<bool[]> _carIdxP2PStatus;
        /// <summary>
        /// Push2Pass active or not
        /// </summary>
        public TelemetryValue<bool[]> CarIdxP2PStatus => _carIdxP2PStatus ??= new(Sdk, "CarIdxP2P_Status");

        private TelemetryValue<float> _steeringWheelAngle;
        /// <summary>
        /// Steering wheel angle. Unit: rad
        /// </summary>
        public TelemetryValue<float> SteeringWheelAngle => _steeringWheelAngle ??= new(Sdk, "SteeringWheelAngle");

        private TelemetryValue<float> _throttle;
        /// <summary>
        /// 0=off throttle to 1=full throttle. Unit: %
        /// </summary>
        public TelemetryValue<float> Throttle => _throttle ??= new(Sdk, "Throttle");

        private TelemetryValue<float> _brake;
        /// <summary>
        /// 0=brake released to 1=max pedal force. Unit: %
        /// </summary>
        public TelemetryValue<float> Brake => _brake ??= new(Sdk, "Brake");

        private TelemetryValue<float> _clutch;
        /// <summary>
        /// 0=disengaged to 1=fully engaged. Unit: %
        /// </summary>
        public TelemetryValue<float> Clutch => _clutch ??= new(Sdk, "Clutch");

        private TelemetryValue<int> _gear;
        /// <summary>
        /// -1=reverse  0=neutral  1..n=current gear. 
        /// </summary>
        public TelemetryValue<int> Gear => _gear ??= new(Sdk, "Gear");

        private TelemetryValue<float> _rpm;
        /// <summary>
        /// Engine rpm. Unit: revs/min
        /// </summary>
        public TelemetryValue<float> RPM => _rpm ??= new(Sdk, "RPM");

        private TelemetryValue<int> _lap;
        /// <summary>
        /// Lap count. 
        /// </summary>
        public TelemetryValue<int> Lap => _lap ??= new(Sdk, "Lap");

        private TelemetryValue<float> _lapDist;
        /// <summary>
        /// Meters traveled from S/F this lap. Unit: m
        /// </summary>
        public TelemetryValue<float> LapDist => _lapDist ??= new(Sdk, "LapDist");

        private TelemetryValue<float> _lapDistPct;
        /// <summary>
        /// Percentage distance around lap. Unit: %
        /// </summary>
        public TelemetryValue<float> LapDistPct => _lapDistPct ??= new(Sdk, "LapDistPct");

        private TelemetryValue<int> _raceLaps;
        /// <summary>
        /// Laps completed in race. 
        /// </summary>
        public TelemetryValue<int> RaceLaps => _raceLaps ??= new(Sdk, "RaceLaps");

        private TelemetryValue<float> _longAccel;
        /// <summary>
        /// Longitudinal acceleration (including gravity). Unit: m/s^2
        /// </summary>
        public TelemetryValue<float> LongAccel => _longAccel ??= new(Sdk, "LongAccel");

        private TelemetryValue<float> _latAccel;
        /// <summary>
        /// Lateral acceleration (including gravity). Unit: m/s^2
        /// </summary>
        public TelemetryValue<float> LatAccel => _latAccel ??= new(Sdk, "LatAccel");

        private TelemetryValue<float> _vertAccel;
        /// <summary>
        /// Vertical acceleration (including gravity). Unit: m/s^2
        /// </summary>
        public TelemetryValue<float> VertAccel => _vertAccel ??= new(Sdk, "VertAccel");

        private TelemetryValue<float> _rollRate;
        /// <summary>
        /// Roll rate. Unit: rad/s
        /// </summary>
        public TelemetryValue<float> RollRate => _rollRate ??= new(Sdk, "RollRate");

        private TelemetryValue<float> _pitchRate;
        /// <summary>
        /// Pitch rate. Unit: rad/s
        /// </summary>
        public TelemetryValue<float> PitchRate => _pitchRate ??= new(Sdk, "PitchRate");

        private TelemetryValue<float> _yawRate;
        /// <summary>
        /// Yaw rate. Unit: rad/s
        /// </summary>
        public TelemetryValue<float> YawRate => _yawRate ??= new(Sdk, "YawRate");

        private TelemetryValue<float> _speed;
        /// <summary>
        /// GPS vehicle speed. Unit: m/s
        /// </summary>
        public TelemetryValue<float> Speed => _speed ??= new(Sdk, "Speed");

        private TelemetryValue<float> _velocityX;
        /// <summary>
        /// X velocity. Unit: m/s
        /// </summary>
        public TelemetryValue<float> VelocityX => _velocityX ??= new(Sdk, "VelocityX");

        private TelemetryValue<float> _velocityY;
        /// <summary>
        /// Y velocity. Unit: m/s
        /// </summary>
        public TelemetryValue<float> VelocityY => _velocityY ??= new(Sdk,

 "VelocityY");

        private TelemetryValue<float> _velocityZ;
        /// <summary>
        /// Z velocity. Unit: m/s
        /// </summary>
        public TelemetryValue<float> VelocityZ => _velocityZ ??= new(Sdk, "VelocityZ");

        private TelemetryValue<float> _yaw;
        /// <summary>
        /// Yaw orientation. Unit: rad
        /// </summary>
        public TelemetryValue<float> Yaw => _yaw ??= new(Sdk, "Yaw");

        private TelemetryValue<float> _yawNorth;
        /// <summary>
        /// Yaw orientation relative to north. Unit: rad
        /// </summary>
        public TelemetryValue<float> YawNorth => _yawNorth ??= new(Sdk, "YawNorth");

        private TelemetryValue<float> _pitch;
        /// <summary>
        /// Pitch orientation. Unit: rad
        /// </summary>
        public TelemetryValue<float> Pitch => _pitch ??= new(Sdk, "Pitch");

        private TelemetryValue<float> _roll;
        /// <summary>
        /// Roll orientation. Unit: rad
        /// </summary>
        public TelemetryValue<float> Roll => _roll ??= new(Sdk, "Roll");

        private TelemetryValue<int> _camCarIdx;
        /// <summary>
        /// Active camera's focus car index. 
        /// </summary>
        public TelemetryValue<int> CamCarIdx => _camCarIdx ??= new(Sdk, "CamCarIdx");

        private TelemetryValue<int> _camCameraNumber;
        /// <summary>
        /// Active camera number. 
        /// </summary>
        public TelemetryValue<int> CamCameraNumber => _camCameraNumber ??= new(Sdk, "CamCameraNumber");

        private TelemetryValue<int> _camGroupNumber;
        /// <summary>
        /// Active camera group number. 
        /// </summary>
        public TelemetryValue<int> CamGroupNumber => _camGroupNumber ??= new(Sdk, "CamGroupNumber");

        private TelemetryValue<CameraState> _camCameraState;
        /// <summary>
        /// State of camera system. Unit: irsdk_CameraState
        /// </summary>
        public TelemetryValue<CameraState> CamCameraState => _camCameraState ??= new(Sdk, "CamCameraState");

        private TelemetryValue<bool> _isOnTrack;
        /// <summary>
        /// 1=Car on track physics running. 
        /// </summary>
        public TelemetryValue<bool> IsOnTrack => _isOnTrack ??= new(Sdk, "IsOnTrack");

        private TelemetryValue<bool> _isInGarage;
        /// <summary>
        /// 1=Car in garage physics running. 
        /// </summary>
        public TelemetryValue<bool> IsInGarage => _isInGarage ??= new(Sdk, "IsInGarage");

        private TelemetryValue<float> _steeringWheelTorque;
        /// <summary>
        /// Output torque on steering shaft. Unit: N*m
        /// </summary>
        public TelemetryValue<float> SteeringWheelTorque => _steeringWheelTorque ??= new(Sdk, "SteeringWheelTorque");

        private TelemetryValue<float> _steeringWheelPctTorque;
        /// <summary>
        /// Force feedback % max torque on steering shaft. Unit: %
        /// </summary>
        public TelemetryValue<float> SteeringWheelPctTorque => _steeringWheelPctTorque ??= new(Sdk, "SteeringWheelPctTorque");

        private TelemetryValue<float> _shiftIndicatorPct;
        /// <summary>
        /// Percent of shift indicator to light up. Unit: %
        /// </summary>
        public TelemetryValue<float> ShiftIndicatorPct => _shiftIndicatorPct ??= new(Sdk, "ShiftIndicatorPct");

        private TelemetryValue<EngineWarning> _engineWarnings;
        /// <summary>
        /// Bitfield for warning lights. Unit: irsdk_EngineWarnings
        /// </summary>
        public TelemetryValue<EngineWarning> EngineWarnings => _engineWarnings ??= new(Sdk, "EngineWarnings");

        private TelemetryValue<float> _fuelLevel;
        /// <summary>
        /// Liters/Kilowatt-hours of fuel remaining. Unit: l or kWh
        /// </summary>
        public TelemetryValue<float> FuelLevel => _fuelLevel ??= new(Sdk, "FuelLevel");

        private TelemetryValue<float> _fuelLevelPct;
        /// <summary>
        /// Percent fuel remaining. Unit: %
        /// </summary>
        public TelemetryValue<float> FuelLevelPct => _fuelLevelPct ??= new(Sdk, "FuelLevelPct");

        private TelemetryValue<int> _replayPlaySpeed;
        /// <summary>
        /// Replay playback speed. 
        /// </summary>
        public TelemetryValue<int> ReplayPlaySpeed => _replayPlaySpeed ??= new(Sdk, "ReplayPlaySpeed");

        private TelemetryValue<bool> _replayPlaySlowMotion;
        /// <summary>
        /// 0=not slow motion  1=replay is in slow motion. 
        /// </summary>
        public TelemetryValue<bool> ReplayPlaySlowMotion => _replayPlaySlowMotion ??= new(Sdk, "ReplayPlaySlowMotion");

        private TelemetryValue<double> _replaySessionTime;
        /// <summary>
        /// Seconds since replay session start. Unit: s
        /// </summary>
        public TelemetryValue<double> ReplaySessionTime => _replaySessionTime ??= new(Sdk, "ReplaySessionTime");

        private TelemetryValue<int> _replaySessionNum;
        /// <summary>
        /// Replay session number. 
        /// </summary>
        public TelemetryValue<int> ReplaySessionNum => _replaySessionNum ??= new(Sdk, "ReplaySessionNum");

        private TelemetryValue<float> _waterTemp;
        /// <summary>
        /// Engine coolant temp. Unit: C
        /// </summary>
        public TelemetryValue<float> WaterTemp => _waterTemp ??= new(Sdk, "WaterTemp");

        private TelemetryValue<float> _waterLevel;
        /// <summary>
        /// Engine coolant level. Unit: l
        /// </summary>
        public TelemetryValue<float> WaterLevel => _waterLevel ??= new(Sdk, "WaterLevel");

        private TelemetryValue<float> _manifoldPress;
        public TelemetryValue<float> ManifoldPress => _manifoldPress ??= new(Sdk, "ManifoldPress");

        private TelemetryValue<float> _fuelPress;
        /// <summary>
        /// Engine fuel pressure. Unit: bar
        /// </summary>
        public TelemetryValue<float> FuelPress => _fuelPress ??= new(Sdk, "FuelPress");

        private TelemetryValue<float> _oilTemp;
        /// <summary>
        /// Engine oil temperature. Unit: C
        /// </summary>
        public TelemetryValue<float> OilTemp => _oilTemp ??= new(Sdk, "OilTemp");

        private TelemetryValue<float> _oilPress;
        /// <summary>
        /// Engine oil pressure. Unit: bar
        /// </summary>
        public TelemetryValue<float> OilPress => _oilPress ??= new(Sdk, "OilPress");

        private TelemetryValue<float> _oilLevel;
        /// <summary>
        /// Engine oil level. Unit: l
        /// </summary>
        public TelemetryValue<float> OilLevel => _oilLevel ??= new(Sdk, "OilLevel");

        private TelemetryValue<float> _voltage;
        /// <summary>
        /// Engine voltage. Unit: V
        /// </summary>
        public TelemetryValue<float> Voltage => _voltage ??= new(Sdk, "Voltage");

        private TelemetryValue<double> _sessionTimeRemain;
        public TelemetryValue<double> SessionTimeRemain => _sessionTimeRemain ??= new(Sdk, "SessionTimeRemain");

        private TelemetryValue<int> _replayFrameNumEnd;
        public TelemetryValue<int> ReplayFrameNumEnd => _replayFrameNumEnd ??= new(Sdk, "ReplayFrameNumEnd");

        private TelemetryValue<float> _airDensity;
        public TelemetryValue<float> AirDensity => _airDensity ??= new(Sdk, "AirDensity");

        private TelemetryValue<float> _airPressure;
        public TelemetryValue<float> AirPressure => _airPressure ??= new(Sdk, "AirPressure");

        private TelemetryValue<float> _airTemp;
        public TelemetryValue<float> AirTemp => _airTemp ??= new(Sdk, "AirTemp");

        private TelemetryValue<float> _fogLevel;
        public TelemetryValue<float> FogLevel => _fogLevel ??= new(Sdk, "FogLevel");

        private TelemetryValue<int> _skies;
        public TelemetryValue<int> Skies => _skies ??= new(Sdk, "Skies");

        private TelemetryValue<float> _trackTemp;
        public TelemetryValue<float> TrackTemp => _trackTemp ??= new(Sdk, "TrackTemp");

        private TelemetryValue<float> _trackTempCrew;
        public TelemetryValue<float> TrackTempCrew => _trackTempCrew ??= new(Sdk, "TrackTempCrew");

        private TelemetryValue<float> _relativeHumidity;
        public TelemetryValue<float> RelativeHumidity => _relativeHumidity ??= new(Sdk, "RelativeHumidity");

        private TelemetryValue<float> _precipitation;
        public TelemetryValue<float> Precipitation => _precipitation ??=

 new(Sdk, "Precipitation");

        private TelemetryValue<float> _windDir;
        public TelemetryValue<float> WindDir => _windDir ??= new(Sdk, "WindDir");

        private TelemetryValue<float> _windVel;
        public TelemetryValue<float> WindVel => _windVel ??= new(Sdk, "WindVel");

        private TelemetryValue<int> _playerCarTeamIncidentCount;
        public TelemetryValue<int> PlayerCarTeamIncidentCount => _playerCarTeamIncidentCount ??= new(Sdk, "PlayerCarTeamIncidentCount");

        private TelemetryValue<int> _playerCarMyIncidentCount;
        public TelemetryValue<int> PlayerCarMyIncidentCount => _playerCarMyIncidentCount ??= new(Sdk, "PlayerCarMyIncidentCount");

        private TelemetryValue<int> _playerCarDriverIncidentCount;
        public TelemetryValue<int> PlayerCarDriverIncidentCount => _playerCarDriverIncidentCount ??= new(Sdk, "PlayerCarDriverIncidentCount");

        private TelemetryValue<IRacingTrackSurfaces> _playerTrackSurface;
        public TelemetryValue<IRacingTrackSurfaces> PlayerTrackSurface => _playerTrackSurface ??= new(Sdk, "PlayerTrackSurface");

        private TelemetryValue<int> _playerCarIdx;
        public TelemetryValue<int> PlayerCarIdx => _playerCarIdx ??= new(Sdk, "PlayerCarIdx");

        private TelemetryValue<bool> _weatherDeclaredWet;
        public TelemetryValue<bool> WeatherDeclaredWet => _weatherDeclaredWet ??= new(Sdk, "WeatherDeclaredWet");

        private TelemetryValue<IRacingTrackWetness> _trackWetness;
        public TelemetryValue<IRacingTrackWetness> TrackWetness => _trackWetness ??= new(Sdk, "TrackWetness");

        private TelemetryValue<float> _lfwearL;
        public TelemetryValue<float> LFwearL => _lfwearL ??= new(Sdk, "LFwearL");

        private TelemetryValue<float> _lfwearM;
        public TelemetryValue<float> LFwearM => _lfwearM ??= new(Sdk, "LFwearM");

        private TelemetryValue<float> _lfwearR;
        public TelemetryValue<float> LFwearR => _lfwearR ??= new(Sdk, "LFwearR");

        private TelemetryValue<float> _rfwearL;
        public TelemetryValue<float> RFwearL => _rfwearL ??= new(Sdk, "RFwearL");

        private TelemetryValue<float> _rfwearM;
        public TelemetryValue<float> RFwearM => _rfwearM ??= new(Sdk, "RFwearM");

        private TelemetryValue<float> _rfwearR;
        public TelemetryValue<float> RFwearR => _rfwearR ??= new(Sdk, "RFwearR");

        private TelemetryValue<float> _lrwearL;
        public TelemetryValue<float> LRwearL => _lrwearL ??= new(Sdk, "LRwearL");

        private TelemetryValue<float> _lrwearM;
        public TelemetryValue<float> LRwearM => _lrwearM ??= new(Sdk, "LRwearM");

        private TelemetryValue<float> _lrwearR;
        public TelemetryValue<float> LRwearR => _lrwearR ??= new(Sdk, "LRwearR");

        private TelemetryValue<float> _rrwearL;
        public TelemetryValue<float> RRwearL => _rrwearL ??= new(Sdk, "RRwearL");

        private TelemetryValue<float> _rrwearM;
        public TelemetryValue<float> RRwearM => _rrwearM ??= new(Sdk, "RRwearM");

        private TelemetryValue<float> _rrwearR;
        public TelemetryValue<float> RRwearR => _rrwearR ??= new(Sdk, "RRwearR");

        private TelemetryValue<float> _lfcoldPressure;
        public TelemetryValue<float> LFcoldPressure => _lfcoldPressure ??= new(Sdk, "LFcoldPressure");

        private TelemetryValue<float> _rfcoldPressure;
        public TelemetryValue<float> RFcoldPressure => _rfcoldPressure ??= new(Sdk, "RFcoldPressure");

        private TelemetryValue<float> _lrcoldPressure;
        public TelemetryValue<float> LRcoldPressure => _lrcoldPressure ??= new(Sdk, "LRcoldPressure");

        private TelemetryValue<float> _rrcoldPressure;
        public TelemetryValue<float> RRcoldPressure => _rrcoldPressure ??= new(Sdk, "RRcoldPressure");

        private TelemetryValue<float> _dpLFTireColdPress;
        public TelemetryValue<float> DPLFTireColdPress => _dpLFTireColdPress ??= new(Sdk, "dpLFTireColdPress");

        private TelemetryValue<float> _dpRFTireColdPress;
        public TelemetryValue<float> DPRFTireColdPress => _dpRFTireColdPress ??= new(Sdk, "dpRFTireColdPress");

        private TelemetryValue<float> _dpLRTireColdPress;
        public TelemetryValue<float> DPLRTireColdPress => _dpLRTireColdPress ??= new(Sdk, "dpLRTireColdPress");

        private TelemetryValue<float> _dpRRTireColdPress;
        public TelemetryValue<float> DPRRTireColdPress => _dpRRTireColdPress ??= new(Sdk, "dpRRTireColdPress");

        private TelemetryValue<float> _dpTireChange;
        public TelemetryValue<float> DPTireChange => _dpTireChange ??= new(Sdk, "dpTireChange");

        private TelemetryValue<float> _dpLFTireChange;
        public TelemetryValue<float> DPLFTireChange => _dpLFTireChange ??= new(Sdk, "dpLFTireChange");

        private TelemetryValue<float> _dpRFTireChange;
        public TelemetryValue<float> DPRFTireChange => _dpRFTireChange ??= new(Sdk, "dpRFTireChange");

        private TelemetryValue<float> _dpLRTireChange;
        public TelemetryValue<float> DPLRTireChange => _dpLRTireChange ??= new(Sdk, "dpLRTireChange");

        private TelemetryValue<float> _dpRRTireChange;
        public TelemetryValue<float> DPRRTireChange => _dpRRTireChange ??= new(Sdk, "dpRRTireChange");

        private TelemetryValue<float> _pitSvLFP;
        public TelemetryValue<float> PitSvLFP => _pitSvLFP ??= new(Sdk, "PitSvLFP");

        private TelemetryValue<float> _pitSvRFP;
        public TelemetryValue<float> PitSvRFP => _pitSvRFP ??= new(Sdk, "PitSvRFP");

        private TelemetryValue<float> _pitSvLRP;
        public TelemetryValue<float> PitSvLRP => _pitSvLRP ??= new(Sdk, "PitSvLRP");

        private TelemetryValue<float> _pitSvRRP;
        public TelemetryValue<float> PitSvRRP => _pitSvRRP ??= new(Sdk, "PitSvRRP");

        private TelemetryValue<float> _dpFuelAddKg;
        public TelemetryValue<float> DPFuelAddKg => _dpFuelAddKg ??= new(Sdk, "dpFuelAddKg");

        private TelemetryValue<float> _dpFuelFill;
        public TelemetryValue<float> DPFuelFill => _dpFuelFill ??= new(Sdk, "dpFuelFill");

        private TelemetryValue<float> _dpWindshieldTearoff;
        public TelemetryValue<float> DPWindshieldTearoff => _dpWindshieldTearoff ??= new(Sdk, "dpWindshieldTearoff");

        private TelemetryValue<float> _dpFastRepair;
        public TelemetryValue<float> DPFastRepair => _dpFastRepair ??= new(Sdk, "dpFastRepair");

        private TelemetryValue<float> _pitOptRepairLeft;
        public TelemetryValue<float> PitOptRepairLeft => _pitOptRepairLeft ??= new(Sdk, "PitOptRepairLeft");

        private TelemetryValue<float> _pitRepairLeft;
        public TelemetryValue<float> PitRepairLeft => _pitRepairLeft ??= new(Sdk, "PitRepairLeft");

        private TelemetryValue<float> _pitSvFuel;
        public TelemetryValue<float> PitSvFuel => _pitSvFuel ??= new(Sdk, "PitSvFuel");

        private TelemetryValue<float> _dcBrakeBias;
        public TelemetryValue<float> DCBrakeBias => _dcBrakeBias ??= new(Sdk, "dcBrakeBias");

        private TelemetryValue<float> _dcTractionControl;
        public TelemetryValue<float> DCTractionControl => _dcTractionControl ??= new(Sdk, "dcTractionControl");

        private TelemetryValue<float> _dcABS;
        public TelemetryValue<float> DCABS => _dcABS ??= new(Sdk, "dcABS");

        private TelemetryValue<float> _sessionTimeOfDay;
        public TelemetryValue<float> SessionTimeOfDay => _sessionTimeOfDay ??= new(Sdk, "SessionTimeOfDay");

        private TelemetryValue<float> _lapDeltaToSessionLastlLap;
        public TelemetryValue<float> LapDeltaToSessionLastlLap => _lapDeltaToSessionLastlLap ??= new(Sdk, "LapDeltaToSessionLastlLap");

        private TelemetryValue<float> _lapDeltaToSessionBestLap;
        public TelemetryValue<float> LapDeltaToSessionBestLap => _lapDeltaToSessionBestLap ??= new(Sdk, "LapDeltaToSessionBestLap");

        private TelemetryValue<bool> _lapDeltaToSessionBestLapOK;
        public TelemetryValue<bool> LapDeltaToSessionBestLapOK => _lapDeltaToSessionBestLapOK ??= new(Sdk, "LapDeltaToSessionBestLap_OK");

        private TelemetryValue<float> _lapBestLapTime;
        public TelemetryValue<float> LapBestLapTime => _lapBestLapTime ??= new(Sdk, "LapBestLapTime");

        private TelemetryValue<float> _lapLastLapTime;
        public TelemetryValue<float> LapLastLapTime => _lapLastLapTime ??= new(Sdk, "LapLastLapTime");

        private TelemetryValue<int> _playerCarDryTireSetLimit;
        public TelemetryValue<int> PlayerCarDryTireSetLimit => _playerCarDryTireSetLimit ??= new(Sdk, "PlayerCarDryTireSetLimit");

        private TelemetryValue<int> _pitSvTireCompound;
        public TelemetryValue<int> PitSvTireCompound => _pitSvTireCompound ??= new(Sdk, "PitSvTireCompound");

        private TelemetryValue<int> _drsStatus;
        /// <summary>
        /// Current DRS status. 0 = inactive, 1 = can be activated in next DRS zone, 2 = can be activated now, 3 = active.
        /// </summary>
        public TelemetryValue<int> DRSStatus => _drsStatus ??= new(Sdk, "DRS_Status");

        private TelemetryValue<int> _drsCount;
        public TelemetryValue<int> DRSCount => _drsCount ??= new(Sdk, "DRS_Count");

        private TelemetryValue<int> _enterExitReset;
        public TelemetryValue<int> EnterExitReset => _enterExitReset ??= new(Sdk, "EnterExitReset");

        private TelemetryValue<int> _fastRepairUsed;
        public TelemetryValue<int> FastRepairUsed => _fastRepairUsed ??= new(Sdk, "FastRepairUsed");

        private TelemetryValue<int> _fastRepairAvailable;
        public TelemetryValue<int> FastRepairAvailable => _fastRepairAvailable ??= new(Sdk, "FastRepairAvailable");

        private TelemetryValue<int> _lfTiresAvailable;
        public TelemetryValue<int> LFTiresAvailable => _lfTiresAvailable ??= new(Sdk, "LFTiresAvailable");

        private TelemetryValue<int> _rfTiresAvailable;
        public TelemetryValue<int> RFTiresAvailable => _rfTiresAvailable ??= new(Sdk, "RFTiresAvailable");

        private TelemetryValue<int> _lrTiresAvailable;
        public TelemetryValue<int> LRTiresAvailable => _lrTiresAvailable ??= new(Sdk, "LRTiresAvailable");

        private TelemetryValue<int> _rrTiresAvailable;
        public TelemetryValue<int> RRTiresAvailable => _rrTiresAvailable ??= new(Sdk, "RRTiresAvailable");

        private TelemetryValue<int> _lfTiresUsed;
        public TelemetryValue<int> LFTiresUsed => _lfTiresUsed ??= new(Sdk, "LFTiresUsed");

        private TelemetryValue<int> _rfTiresUsed;
        public TelemetryValue<int> RFTiresUsed => _rfTiresUsed ??= new(Sdk, "RFTiresUsed");

        private TelemetryValue<int> _lrTiresUsed;
        public TelemetryValue<int> LRTiresUsed => _lrTiresUsed ??= new(Sdk, "LRTiresUsed");

        private TelemetryValue<int> _rrTiresUsed;
        public TelemetryValue<int> RRTiresUsed => _rrTiresUsed ??= new(Sdk, "RRTiresUsed");

        private TelemetryValue<int> _leftTireSetsUsed;
        public TelemetryValue<int> LeftTireSetsUsed => _leftTireSetsUsed ??= new(Sdk, "LeftTireSetsUsed");

        private TelemetryValue<int> _rightTireSetsUsed;
        public TelemetryValue<int> RightTireSetsUsed => _rightTireSetsUsed ??= new(Sdk, "RightTireSetsUsed");

        private TelemetryValue<int> _frontTireSetsUsed;
        public TelemetryValue<int> FrontTireSetsUsed => _frontTireSetsUsed ??= new(Sdk, "FrontTireSetsUsed");

        private TelemetryValue<int> _rearTireSetsUsed;
        public TelemetryValue<int> RearTireSetsUsed => _rearTireSetsUsed ??= new(Sdk, "RearTireSetsUsed");

        private TelemetryValue<int> _tireSetsUsed;
        public TelemetryValue<int> TireSetsUsed => _tireSetsUsed ??= new(Sdk, "TireSetsUsed");

        private TelemetryValue<int> _tireSetsAvailable;
        public TelemetryValue<int> TireSetsAvailable => _tireSetsAvailable ??= new(Sdk, "TireSetsAvailable");

        private TelemetryValue<int> _leftTireSetsAvailable;
        public TelemetryValue<int> LeftTireSetsAvailable => _leftTireSetsAvailable ??= new(Sdk, "LeftTireSetsAvailable");

        private TelemetryValue<int> _rightTireSetsAvailable;
        public TelemetryValue<int> RightTireSetsAvailable => _rightTireSetsAvailable ??= new(Sdk, "RightTireSetsAvailable");

        private TelemetryValue<int> _frontTireSetsAvailable;
        public TelemetryValue<int> FrontTireSetsAvailable => _frontTireSetsAvailable ??= new(Sdk, "FrontTireSetsAvailable");

        private TelemetryValue<int> _rearTireSetsAvailable;
        public TelemetryValue<int> RearTireSetsAvailable => _rearTireSetsAvailable ??= new(Sdk, "RearTireSetsAvailable");

        private TelemetryValue<int> _displayUnits;
        public TelemetryValue<int> DisplayUnits => _displayUnits ??= new(Sdk, "DisplayUnits");

        private TelemetryValue<int> _sessionLapsRemainEx;
        public TelemetryValue<int> SessionLapsRemainEx => _sessionLapsRemainEx ??= new(Sdk, "SessionLapsRemainEx");

        private TelemetryValue<int> _playerCarPosition;
        public TelemetryValue<int> PlayerCarPosition => _playerCarPosition ??= new(Sdk, "PlayerCarPosition");

        private TelemetryValue<int> _playerCarClassPosition;
        public TelemetryValue<int> PlayerCarClassPosition => _playerCarClassPosition ??= new(Sdk, "PlayerCarClassPosition");

        private TelemetryValue<int> _lapBestLap;
        public TelemetryValue<int> LapBestLap => _lapBestLap ??= new(Sdk, "LapBestLap");

        private TelemetryValue<int> _radioTransmitCarIdx;
        public TelemetryValue<int> RadioTransmitCarIdx => _radioTransmitCarIdx ??= new(Sdk, "RadioTransmitCarIdx");

        private TelemetryValue<int> _radioTransmitRadioIdx;
        public TelemetryValue<int> RadioTransmitRadioIdx => _radioTransmitRadioIdx ??= new(Sdk, "RadioTransmitRadioIdx");

        private TelemetryValue<int> _radioTransmitFrequencyIdx;
        public TelemetryValue<int> RadioTransmitFrequencyIdx => _radioTransmitFrequencyIdx ??= new(Sdk, "RadioTransmitFrequencyIdx");

        private TelemetryValue<bool> _pitsOpen;
        public TelemetryValue<bool> PitsOpen => _pitsOpen ??= new(Sdk, "PitsOpen");

        private TelemetryValue<bool> _brakeABSactive;
        public TelemetryValue<bool> BrakeABSactive => _brakeABSactive ??= new(Sdk, "BrakeABSactive");

        private TelemetryValue<bool> _dcTriggerWindshieldWipers;
        public TelemetryValue<bool> DCTriggerWindshieldWipers => _dcTriggerWindshieldWipers ??= new(Sdk, "dcTriggerWindshieldWipers");

        private TelemetryValue<bool> _dcToggleWindshieldWipers;
        public TelemetryValue<bool> DCToggleWindshieldWipers => _dcToggleWindshieldWipers ??= new(Sdk, "dcToggleWindshieldWipers");

        private TelemetryValue<bool> _isDiskLoggingActive;
        public TelemetryValue<bool> IsDiskLoggingActive => _isDiskLoggingActive ??= new(Sdk, "IsDiskLoggingActive");

        private TelemetryValue<bool> _isDiskLoggingEnabled;
        public TelemetryValue<bool> IsDiskLoggingEnabled => _isDiskLoggingEnabled ??= new(Sdk, "IsDiskLoggingEnabled");

        private TelemetryValue<bool> _pitstopActive;
        public TelemetryValue<bool> PitstopActive => _pitstopActive ??= new(Sdk, "PitstopActive");

        private TelemetryValue<bool> _playerCarInPitStall;
        public TelemetryValue<bool> PlayerCarInPitStall => _playerCarInPitStall ??= new(Sdk, "PlayerCarInPitStall");

        private TelemetryValue<bool> _isOnTrackCar;
        public TelemetryValue<bool> IsOnTrackCar => _isOnTrackCar ??= new(Sdk, "IsOnTrackCar");

        private TelemetryValue<bool> _onPitRoad;
        public TelemetryValue<bool> OnPitRoad => _onPitRoad ??= new(Sdk, "OnPitRoad");
    }
}