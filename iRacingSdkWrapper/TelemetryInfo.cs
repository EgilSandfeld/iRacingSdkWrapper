using System.Collections.Generic;
using iRSDKSharp;
using iRacingSdkWrapper.Bitfields;

namespace iRacingSdkWrapper
{
    /// <summary>
    /// Represents an object from which you can get Telemetry var headers by name
    /// </summary>
    public sealed class TelemetryInfo
    {
        public iRacingSDK Sdk;

        public TelemetryInfo(iRacingSDK sdk)
        {
            Sdk = sdk;
        }

        public IEnumerable<TelemetryValue> GetValues()
        {
            var values = new List<TelemetryValue>();
            values.AddRange(new TelemetryValue[]
                                {
                                    this.SessionTime,
                                    this.SessionNum,
                                    this.SessionState,
                                    this.SessionUniqueID,
                                    this.SessionFlags,
                                    this.CarLeftRights,
                                    this.PitServiceFlags,
                                    this.DriverMarker,
                                    this.IsReplayPlaying,
                                    this.ReplayFrameNum,
                                    this.CarIdxLap,
                                    this.CarIdxLapCompleted,
                                    this.CarIdxLapDistPct,
                                    this.CarIdxTrackSurface,
                                    this.CarIdxTrackSurfaceMaterial,
                                    this.CarIdxSteer,
                                    this.CarIdxRPM,
                                    this.CarIdxGear,
                                    this.CarIdxFastRepairsUsed,
                                    this.CarIdxSessionFlags,
                                    this.CarIdxF2Time,
                                    this.CarIdxEstTime,
                                    this.CarIdxOnPitRoad,
                                    this.CarIdxPosition,
                                    this.CarIdxClassPosition,
                                    this.SteeringWheelAngle,
                                    this.Throttle,
                                    this.Brake,
                                    this.Clutch,
                                    this.Gear,
                                    this.RPM,
                                    this.Lap,
                                    this.LapDist,
                                    this.LapDistPct,
                                    this.RaceLaps,
                                    this.LongAccel,
                                    this.LatAccel,
                                    this.VertAccel,
                                    this.RollRate,
                                    this.PitchRate,
                                    this.YawRate,
                                    this.Speed,
                                    this.VelocityX,
                                    this.VelocityY,
                                    this.VelocityZ,
                                    this.Yaw,
                                    this.Pitch,
                                    this.Roll,
                                    this.CamCarIdx,
                                    this.CamCameraNumber,
                                    this.CamCameraState,
                                    this.CamGroupNumber,
                                    this.IsOnTrack,
                                    this.IsInGarage,
                                    this.SteeringWheelTorque,
                                    this.SteeringWheelPctTorque,
                                    this.ShiftIndicatorPct,
                                    this.EngineWarnings,
                                    this.FuelLevel,
                                    this.FuelLevelPct,
                                    this.ReplayPlaySpeed,
                                    this.ReplaySessionTime,
                                    this.ReplaySessionNum,
                                    this.WaterTemp,
                                    this.WaterLevel,
                                    this.FuelPress,
                                    this.OilTemp,
                                    this.OilPress,
                                    this.OilLevel,
                                    this.Voltage,
                                    this.SessionTimeRemain,
                                    this.ReplayFrameNumEnd,
                                    this.AirDensity,
                                    this.AirPressure,
                                    this.AirTemp,
                                    this.FogLevel,
                                    this.Skies,
                                    this.TrackTemp,
                                    this.TrackTempCrew,
                                    this.RelativeHumidity,
                                    this.WeatherType,
                                    this.WindDir,
                                    this.WindVel,
                                    this.MGUKDeployAdapt,
                                    this.MGUKDeployFixed,
                                    this.MGUKRegenGain,
                                    this.EnergyBatteryToMGU,
                                    this.EnergyBudgetBattToMGU,
                                    this.EnergyERSBattery,
                                    this.PowerMGUH,
                                    this.PowerMGUK,
                                    this.TorqueMGUK,
                                    this.DrsStatus,
                                    this.LapCompleted,
                                    this.PlayerCarDriverIncidentCount,
                                    this.PlayerCarTeamIncidentCount,
                                    this.PlayerCarMyIncidentCount,
                                    this.PlayerTrackSurface,
                                    this.PlayerCarIdx
                                });
            return values;
        }

        public TelemetryValue<float> MGUKDeployAdapt { get { return new TelemetryValue<float>(Sdk, "dcMGUKDeployAdapt"); } }

        public TelemetryValue<float> MGUKDeployFixed { get { return new TelemetryValue<float>(Sdk, "dcMGUKDeployFixed"); } }

        public TelemetryValue<float> MGUKRegenGain { get { return new TelemetryValue<float>(Sdk, "dcMGUKRegenGain"); } }

        public TelemetryValue<float> EnergyBatteryToMGU { get { return new TelemetryValue<float>(Sdk, "EnergyBatteryToMGU_KLap"); } }

        public TelemetryValue<float> EnergyBudgetBattToMGU { get { return new TelemetryValue<float>(Sdk, "EnergyBudgetBattToMGU_KLap"); } }

        public TelemetryValue<float> EnergyERSBattery { get { return new TelemetryValue<float>(Sdk, "EnergyERSBattery"); } }

        public TelemetryValue<float> PowerMGUH { get { return new TelemetryValue<float>(Sdk, "PowerMGU_H"); } }

        public TelemetryValue<float> PowerMGUK { get { return new TelemetryValue<float>(Sdk, "PowerMGU_K"); } }

        public TelemetryValue<float> TorqueMGUK { get { return new TelemetryValue<float>(Sdk, "TorqueMGU_K"); } }

        /// <summary>
        /// Current DRS status. 0 = inactive, 1 = can be activated in next DRS zone, 2 = can be activated now, 3 = active.
        /// </summary>
        public TelemetryValue<int> DrsStatus { get { return new TelemetryValue<int>(Sdk, "DRS_Status"); } }

        /// <summary>
        /// The number of laps you have completed. Note: on Nordschleife Tourist layout, you can complete a lap without starting a new one!
        /// </summary>
        public TelemetryValue<int> LapCompleted { get { return new TelemetryValue<int>(Sdk, "LapCompleted"); } }


        /// <summary>
        /// Seconds since session start. Unit: s
        /// </summary>
        public TelemetryValue<double> SessionTime { get { return new TelemetryValue<double>(Sdk, "SessionTime"); } }


        /// <summary>
        /// Session number. 
        /// </summary>
        public TelemetryValue<int> SessionNum { get { return new TelemetryValue<int>(Sdk, "SessionNum"); } }


        /// <summary>
        /// Session state. Unit: irsdk_SessionState
        /// </summary>
        public TelemetryValue<SessionStates> SessionState { get { return new TelemetryValue<SessionStates>(Sdk, "SessionState"); } }


        /// <summary>
        /// Session ID. 
        /// </summary>
        public TelemetryValue<int> SessionUniqueID { get { return new TelemetryValue<int>(Sdk, "SessionUniqueID"); } }


        /// <summary>
        /// Pit service flags. Unit: irsdk_PitSvFlags
        /// </summary>
        public TelemetryValue<PitServiceFlag> PitServiceFlags { get { return new TelemetryValue<PitServiceFlag>(Sdk, "PitSvFlags"); } }


        /// <summary>
        /// Pit service status flags. Unit: irsdk_PitSvStatus
        /// </summary>
        public TelemetryValue<PitSvStatuses> PitSvStatuses { get { return new TelemetryValue<PitSvStatuses>(Sdk, "PlayerCarPitSvStatus"); } }


        /// <summary>
        /// Session flags. Unit: irsdk_Flags
        /// </summary>
        public TelemetryValue<SessionFlag> SessionFlags { get { return new TelemetryValue<SessionFlag>(Sdk, "SessionFlags"); } }


        /// <summary>
        /// Car Left Right. Unit: irsdk_CarLeftRight
        /// </summary>
        public TelemetryValue<CarLeftRights> CarLeftRights { get { return new TelemetryValue<CarLeftRights>(Sdk, "CarLeftRight"); } }


        /// <summary>
        /// Driver activated flag. 
        /// </summary>
        public TelemetryValue<bool> DriverMarker { get { return new TelemetryValue<bool>(Sdk, "DriverMarker"); } }


        /// <summary>
        /// 0=replay not playing  1=replay playing. 
        /// </summary>
        public TelemetryValue<bool> IsReplayPlaying { get { return new TelemetryValue<bool>(Sdk, "IsReplayPlaying"); } }


        /// <summary>
        /// Integer replay frame number (60 per second). 
        /// </summary>
        public TelemetryValue<int> ReplayFrameNum { get { return new TelemetryValue<int>(Sdk, "ReplayFrameNum"); } }


        /// <summary>
        /// Current lap number by car index
        /// </summary>
        public TelemetryValue<int[]> CarIdxLap { get { return new TelemetryValue<int[]>(Sdk, "CarIdxLap"); } }

        /// <summary>
        /// Current number of completed laps by car index. Note: On Nordschleife Tourist layout, cars can complete a lap without starting a new lap!
        /// </summary>
        public TelemetryValue<int[]> CarIdxLapCompleted { get { return new TelemetryValue<int[]>(Sdk, "CarIdxLapCompleted"); } }

        /// <summary>
        /// Percentage distance around lap by car index. Unit: %
        /// </summary>
        public TelemetryValue<float[]> CarIdxLapDistPct { get { return new TelemetryValue<float[]>(Sdk, "CarIdxLapDistPct"); } }


        /// <summary>
        /// Track surface type by car index. Unit: irsdk_TrkLoc
        /// </summary>
        public TelemetryValue<TrackSurfaces[]> CarIdxTrackSurface { get { return new TelemetryValue<TrackSurfaces[]>(Sdk, "CarIdxTrackSurface"); } }
        
        
        /// <summary>
        /// Track surface material type by car index. Unit: irsdk_TrkSurf
        /// </summary>
        public TelemetryValue<TrackSurfaceMaterials[]> CarIdxTrackSurfaceMaterial { get { return new TelemetryValue<TrackSurfaceMaterials[]>(Sdk, "CarIdxTrackSurfaceMaterial"); } }


        /// <summary>
        /// Steering wheel angle by car index. Unit: rad
        /// </summary>
        public TelemetryValue<float[]> CarIdxSteer { get { return new TelemetryValue<float[]>(Sdk, "CarIdxSteer"); } }


        /// <summary>
        /// Engine rpm by car index. Unit: revs/min
        /// </summary>
        public TelemetryValue<float[]> CarIdxRPM { get { return new TelemetryValue<float[]>(Sdk, "CarIdxRPM"); } }


        /// <summary>
        /// -1=reverse  0=neutral  1..n=current gear by car index. 
        /// </summary>
        public TelemetryValue<int[]> CarIdxGear { get { return new TelemetryValue<int[]>(Sdk, "CarIdxGear"); } }


        /// <summary>
        /// Number of fast repairs used by car index. Stays 0 in practice and qual 
        /// </summary>
        public TelemetryValue<int[]> CarIdxFastRepairsUsed { get { return new TelemetryValue<int[]>(Sdk, "CarIdxFastRepairsUsed"); } }


        /// <summary>
        /// Session flags by car index
        /// </summary>
        public TelemetryValue<SessionFlags[]> CarIdxSessionFlags { get { return new TelemetryValue<SessionFlags[]>(Sdk, "CarIdxSessionFlags"); } }

        /// <summary>
        /// Race time behind leader or fastest lap time otherwise
        /// </summary>
        public TelemetryValue<float[]> CarIdxF2Time { get { return new TelemetryValue<float[]>(Sdk, "CarIdxF2Time"); } }

        /// <summary>
        /// Estimated time to reach current location on track
        /// </summary>
        public TelemetryValue<float[]> CarIdxEstTime { get { return new TelemetryValue<float[]>(Sdk, "CarIdxEstTime"); } }

        
        /// <summary>
        /// On pit road between the cones by car index
        /// </summary>
        public TelemetryValue<bool[]> CarIdxOnPitRoad { get { return new TelemetryValue<bool[]>(Sdk, "CarIdxOnPitRoad"); } }

        /// <summary>
        /// Cars position in race by car index
        /// </summary>
        public TelemetryValue<int[]> CarIdxPosition { get { return new TelemetryValue<int[]>(Sdk, "CarIdxPosition"); } }

        
        /// <summary>
        /// Cars class index in race by car index
        /// </summary>
        public TelemetryValue<int[]> CarIdxClass { get { return new TelemetryValue<int[]>(Sdk, "CarIdxClass"); } }
        
        /// <summary>
        /// Cars class position in race by car index
        /// </summary>
        public TelemetryValue<int[]> CarIdxClassPosition { get { return new TelemetryValue<int[]>(Sdk, "CarIdxClassPosition"); } }
        
        /// <summary>
        /// Cars best lap number
        /// </summary>
        public TelemetryValue<int[]> CarIdxBestLapNum { get { return new TelemetryValue<int[]>(Sdk, "CarIdxBestLapNum"); } }
        
        
        /// <summary>
        /// -1: No Selection/Unknown
        /// 0: Soft ?
        /// 1: Medium ?
        /// 2: Hard ?
        /// </summary>
        public TelemetryValue<int[]> CarIdxTireCompound { get { return new TelemetryValue<int[]>(Sdk, "CarIdxTireCompound"); } }
        
        
        /// <summary>
        /// Tire compound index with which each team has set their best time in Qual - it will be -1 if none has been locked in so far
        /// -1: Not locked
        /// 0: Soft ?
        /// 1: Medium ?
        /// 2: Hard ?
        /// </summary>
        public TelemetryValue<int[]> CarIdxQualTireCompound { get { return new TelemetryValue<int[]>(Sdk, "CarIdxQualTireCompound"); } }
        
        
        /// <summary>
        ///  Indicates whether or not Race Control has officially locked that team in to that compound choice for the start of the race
        /// -1: Not locked
        /// 0: Soft ?
        /// 1: Medium ?
        /// 2: Hard ?
        /// </summary>
        public TelemetryValue<bool[]> CarIdxQualTireCompoundLocked { get { return new TelemetryValue<bool[]>(Sdk, "CarIdxQualTireCompoundLocked"); } }
        
        /// <summary>
        /// Cars best lap time
        /// </summary>
        public TelemetryValue<float[]> CarIdxBestLapTime { get { return new TelemetryValue<float[]>(Sdk, "CarIdxBestLapTime"); } }
        
        /// <summary>
        /// Cars last lap time
        /// </summary>
        public TelemetryValue<float[]> CarIdxLastLapTime { get { return new TelemetryValue<float[]>(Sdk, "CarIdxLastLapTime"); } }
        
        /// <summary>
        /// Push2Pass count of usage (or remaining in Race)
        /// </summary>
        public TelemetryValue<float[]> CarIdxP2P_Count { get { return new TelemetryValue<float[]>(Sdk, "CarIdxP2P_Count"); } }
        
        /// <summary>
        /// Push2Pass active or not
        /// </summary>
        public TelemetryValue<bool[]> CarIdxP2P_Status { get { return new TelemetryValue<bool[]>(Sdk, "CarIdxP2P_Status"); } }


        /// <summary>
        /// Steering wheel angle. Unit: rad
        /// </summary>
        public TelemetryValue<float> SteeringWheelAngle { get { return new TelemetryValue<float>(Sdk, "SteeringWheelAngle"); } }


        /// <summary>
        /// 0=off throttle to 1=full throttle. Unit: %
        /// </summary>
        public TelemetryValue<float> Throttle { get { return new TelemetryValue<float>(Sdk, "Throttle"); } }


        /// <summary>
        /// 0=brake released to 1=max pedal force. Unit: %
        /// </summary>
        public TelemetryValue<float> Brake { get { return new TelemetryValue<float>(Sdk, "Brake"); } }


        /// <summary>
        /// 0=disengaged to 1=fully engaged. Unit: %
        /// </summary>
        public TelemetryValue<float> Clutch { get { return new TelemetryValue<float>(Sdk, "Clutch"); } }


        /// <summary>
        /// -1=reverse  0=neutral  1..n=current gear. 
        /// </summary>
        public TelemetryValue<int> Gear { get { return new TelemetryValue<int>(Sdk, "Gear"); } }


        /// <summary>
        /// Engine rpm. Unit: revs/min
        /// </summary>
        public TelemetryValue<float> RPM { get { return new TelemetryValue<float>(Sdk, "RPM"); } }


        /// <summary>
        /// Lap count. 
        /// </summary>
        public TelemetryValue<int> Lap { get { return new TelemetryValue<int>(Sdk, "Lap"); } }


        /// <summary>
        /// Meters traveled from S/F this lap. Unit: m
        /// </summary>
        public TelemetryValue<float> LapDist { get { return new TelemetryValue<float>(Sdk, "LapDist"); } }


        /// <summary>
        /// Percentage distance around lap. Unit: %
        /// </summary>
        public TelemetryValue<float> LapDistPct { get { return new TelemetryValue<float>(Sdk, "LapDistPct"); } }


        /// <summary>
        /// Laps completed in race. 
        /// </summary>
        public TelemetryValue<int> RaceLaps { get { return new TelemetryValue<int>(Sdk, "RaceLaps"); } }


        /// <summary>
        /// Longitudinal acceleration (including gravity). Unit: m/s^2
        /// </summary>
        public TelemetryValue<float> LongAccel { get { return new TelemetryValue<float>(Sdk, "LongAccel"); } }


        /// <summary>
        /// Lateral acceleration (including gravity). Unit: m/s^2
        /// </summary>
        public TelemetryValue<float> LatAccel { get { return new TelemetryValue<float>(Sdk, "LatAccel"); } }


        /// <summary>
        /// Vertical acceleration (including gravity). Unit: m/s^2
        /// </summary>
        public TelemetryValue<float> VertAccel { get { return new TelemetryValue<float>(Sdk, "VertAccel"); } }


        /// <summary>
        /// Roll rate. Unit: rad/s
        /// </summary>
        public TelemetryValue<float> RollRate { get { return new TelemetryValue<float>(Sdk, "RollRate"); } }


        /// <summary>
        /// Pitch rate. Unit: rad/s
        /// </summary>
        public TelemetryValue<float> PitchRate { get { return new TelemetryValue<float>(Sdk, "PitchRate"); } }


        /// <summary>
        /// Yaw rate. Unit: rad/s
        /// </summary>
        public TelemetryValue<float> YawRate { get { return new TelemetryValue<float>(Sdk, "YawRate"); } }


        /// <summary>
        /// GPS vehicle speed. Unit: m/s
        /// </summary>
        public TelemetryValue<float> Speed { get { return new TelemetryValue<float>(Sdk, "Speed"); } }


        /// <summary>
        /// X velocity. Unit: m/s
        /// </summary>
        public TelemetryValue<float> VelocityX { get { return new TelemetryValue<float>(Sdk, "VelocityX"); } }


        /// <summary>
        /// Y velocity. Unit: m/s
        /// </summary>
        public TelemetryValue<float> VelocityY { get { return new TelemetryValue<float>(Sdk, "VelocityY"); } }


        /// <summary>
        /// Z velocity. Unit: m/s
        /// </summary>
        public TelemetryValue<float> VelocityZ { get { return new TelemetryValue<float>(Sdk, "VelocityZ"); } }


        /// <summary>
        /// Yaw orientation. Unit: rad
        /// </summary>
        public TelemetryValue<float> Yaw { get { return new TelemetryValue<float>(Sdk, "Yaw"); } }


        /// <summary>
        /// Yaw orientation relative to north. Unit: rad
        /// </summary>
        public TelemetryValue<float> YawNorth { get { return new TelemetryValue<float>(Sdk, "YawNorth"); } }


        /// <summary>
        /// Pitch orientation. Unit: rad
        /// </summary>
        public TelemetryValue<float> Pitch { get { return new TelemetryValue<float>(Sdk, "Pitch"); } }


        /// <summary>
        /// Roll orientation. Unit: rad
        /// </summary>
        public TelemetryValue<float> Roll { get { return new TelemetryValue<float>(Sdk, "Roll"); } }


        /// <summary>
        /// Active camera's focus car index. 
        /// </summary>
        public TelemetryValue<int> CamCarIdx { get { return new TelemetryValue<int>(Sdk, "CamCarIdx"); } }


        /// <summary>
        /// Active camera number. 
        /// </summary>
        public TelemetryValue<int> CamCameraNumber { get { return new TelemetryValue<int>(Sdk, "CamCameraNumber"); } }


        /// <summary>
        /// Active camera group number. 
        /// </summary>
        public TelemetryValue<int> CamGroupNumber { get { return new TelemetryValue<int>(Sdk, "CamGroupNumber"); } }


        /// <summary>
        /// State of camera system. Unit: irsdk_CameraState
        /// </summary>
        public TelemetryValue<CameraState> CamCameraState { get { return new TelemetryValue<CameraState>(Sdk, "CamCameraState"); } }


        /// <summary>
        /// 1=Car on track physics running. 
        /// </summary>
        public TelemetryValue<bool> IsOnTrack { get { return new TelemetryValue<bool>(Sdk, "IsOnTrack"); } }


        /// <summary>
        /// 1=Car in garage physics running. 
        /// </summary>
        public TelemetryValue<bool> IsInGarage { get { return new TelemetryValue<bool>(Sdk, "IsInGarage"); } }


        /// <summary>
        /// Output torque on steering shaft. Unit: N*m
        /// </summary>
        public TelemetryValue<float> SteeringWheelTorque { get { return new TelemetryValue<float>(Sdk, "SteeringWheelTorque"); } }


        /// <summary>
        /// Force feedback % max torque on steering shaft. Unit: %
        /// </summary>
        public TelemetryValue<float> SteeringWheelPctTorque { get { return new TelemetryValue<float>(Sdk, "SteeringWheelPctTorque"); } }


        /// <summary>
        /// Percent of shift indicator to light up. Unit: %
        /// </summary>
        public TelemetryValue<float> ShiftIndicatorPct { get { return new TelemetryValue<float>(Sdk, "ShiftIndicatorPct"); } }


        /// <summary>
        /// Bitfield for warning lights. Unit: irsdk_EngineWarnings
        /// </summary>
        public TelemetryValue<EngineWarning> EngineWarnings { get { return new TelemetryValue<EngineWarning>(Sdk, "EngineWarnings"); } }


        /// <summary>
        /// Liters/Kilowatt-hours of fuel remaining. Unit: l or kWh
        /// </summary>
        public TelemetryValue<float> FuelLevel { get { return new TelemetryValue<float>(Sdk, "FuelLevel"); } }


        /// <summary>
        /// Percent fuel remaining. Unit: %
        /// </summary>
        public TelemetryValue<float> FuelLevelPct { get { return new TelemetryValue<float>(Sdk, "FuelLevelPct"); } }


        /// <summary>
        /// Replay playback speed. 
        /// </summary>
        public TelemetryValue<int> ReplayPlaySpeed { get { return new TelemetryValue<int>(Sdk, "ReplayPlaySpeed"); } }


        /// <summary>
        /// 0=not slow motion  1=replay is in slow motion. 
        /// </summary>
        public TelemetryValue<bool> ReplayPlaySlowMotion { get { return new TelemetryValue<bool>(Sdk, "ReplayPlaySlowMotion"); } }


        /// <summary>
        /// Seconds since replay session start. Unit: s
        /// </summary>
        public TelemetryValue<double> ReplaySessionTime { get { return new TelemetryValue<double>(Sdk, "ReplaySessionTime"); } }


        /// <summary>
        /// Replay session number. 
        /// </summary>
        public TelemetryValue<int> ReplaySessionNum { get { return new TelemetryValue<int>(Sdk, "ReplaySessionNum"); } }


        /// <summary>
        /// Engine coolant temp. Unit: C
        /// </summary>
        public TelemetryValue<float> WaterTemp { get { return new TelemetryValue<float>(Sdk, "WaterTemp"); } }


        /// <summary>
        /// Engine coolant level. Unit: l
        /// </summary>
        public TelemetryValue<float> WaterLevel { get { return new TelemetryValue<float>(Sdk, "WaterLevel"); } }


        /// <summary>
        /// Engine fuel pressure. Unit: bar
        /// </summary>
        public TelemetryValue<float> FuelPress { get { return new TelemetryValue<float>(Sdk, "FuelPress"); } }


        /// <summary>
        /// Engine oil temperature. Unit: C
        /// </summary>
        public TelemetryValue<float> OilTemp { get { return new TelemetryValue<float>(Sdk, "OilTemp"); } }


        /// <summary>
        /// Engine oil pressure. Unit: bar
        /// </summary>
        public TelemetryValue<float> OilPress { get { return new TelemetryValue<float>(Sdk, "OilPress"); } }


        /// <summary>
        /// Engine oil level. Unit: l
        /// </summary>
        public TelemetryValue<float> OilLevel { get { return new TelemetryValue<float>(Sdk, "OilLevel"); } }


        /// <summary>
        /// Engine voltage. Unit: V
        /// </summary>
        public TelemetryValue<float> Voltage { get { return new TelemetryValue<float>(Sdk, "Voltage"); } }

        public TelemetryValue<double> SessionTimeRemain { get { return new TelemetryValue<double>(Sdk, "SessionTimeRemain"); } }

        public TelemetryValue<int> ReplayFrameNumEnd { get { return new TelemetryValue<int>(Sdk, "ReplayFrameNumEnd"); } }

        public TelemetryValue<float> AirDensity { get { return new TelemetryValue<float>(Sdk, "AirDensity"); } }

        public TelemetryValue<float> AirPressure { get { return new TelemetryValue<float>(Sdk, "AirPressure"); } }

        public TelemetryValue<float> AirTemp { get { return new TelemetryValue<float>(Sdk, "AirTemp"); } }

        public TelemetryValue<float> FogLevel { get { return new TelemetryValue<float>(Sdk, "FogLevel"); } }

        public TelemetryValue<int> Skies { get { return new TelemetryValue<int>(Sdk, "Skies"); } }

        public TelemetryValue<float> TrackTemp { get { return new TelemetryValue<float>(Sdk, "TrackTemp"); } }

        public TelemetryValue<float> TrackTempCrew { get { return new TelemetryValue<float>(Sdk, "TrackTempCrew"); } }

        public TelemetryValue<float> RelativeHumidity { get { return new TelemetryValue<float>(Sdk, "RelativeHumidity"); } }

        public TelemetryValue<int> WeatherType { get { return new TelemetryValue<int>(Sdk, "WeatherType"); } }

        public TelemetryValue<float> WindDir { get { return new TelemetryValue<float>(Sdk, "WindDir"); } }

        public TelemetryValue<float> WindVel { get { return new TelemetryValue<float>(Sdk, "WindVel"); } }

        public TelemetryValue<int> PlayerCarTeamIncidentCount { get { return new TelemetryValue<int>(Sdk, "PlayerCarTeamIncidentCount"); } }

        public TelemetryValue<int> PlayerCarMyIncidentCount { get { return new TelemetryValue<int>(Sdk, "PlayerCarMyIncidentCount"); } }

        public TelemetryValue<int> PlayerCarDriverIncidentCount { get { return new TelemetryValue<int>(Sdk, "PlayerCarDriverIncidentCount"); } }

        public TelemetryValue<TrackSurfaces> PlayerTrackSurface { get { return new TelemetryValue<TrackSurfaces>(Sdk, "PlayerTrackSurface"); } }

        public TelemetryValue<int> PlayerCarIdx { get { return new TelemetryValue<int>(Sdk, "PlayerCarIdx"); } }
    }
}
