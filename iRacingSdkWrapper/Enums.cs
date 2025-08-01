﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace iRacingSdkWrapper
{
    public enum IRacingTrackSurfaces
    {
        NotInWorld = -1,
        OffTrack,
        InPitStall,
        ApproachingPits,
        OnTrack
    }
    
    public enum IRacingTrackSurfaceMaterials
    {
        SurfaceNotInWorld = -1,
        Undefined = 0,

        Asphalt1,
        Asphalt2,
        Asphalt3,
        Asphalt4,
        Concrete1,
        Concrete2,
        RacingDirt1,
        RacingDirt2,
        Paint1,
        Paint2,
        Rumble1,
        Rumble2,
        Rumble3,
        Rumble4,

        Grass1,
        Grass2,
        Grass3,
        Grass4,
        Dirt1,
        Dirt2,
        Dirt3,
        Dirt4,
        Sand,
        Gravel1,
        Gravel2,
        Grasscrete,
        Astroturf,
    }

    public enum IRacingSessionStates
    {
        Invalid,
        GetInCar,
        Warmup,
        ParadeLaps,
        Racing,
        Checkered,
        CoolDown
    }

    public enum IRacingCarLeftRights
    {
        LROff,
        LRClear, // no cars around us.
        LRCarLeft, // there is a car to our left. 
        LRCarRight, // there is a car to our right. 
        LRCarLeftRight, // there are cars on each side. 
        LR2CarsLeft, // there are two cars to our left. 
        LR2CarsRight // there are two cars to our right.
    }

    public enum IRacingPitSvStatuses
    {
        // status 
        PitSvNone = 0,
        PitSvInProgress = 1,
        PitSvComplete = 2,

        // errors 
        PitSvTooFarLeft = 100,
        PitSvTooFarRight = 101,
        PitSvTooFarForward = 102,
        PitSvTooFarBack = 103,
        PitSvBadAngle = 104,
        PitSvCantFixThat = 105
    }
    
    [JsonConverter(typeof(StringEnumConverter))] 
    public enum IRacingTrackWetness
    
    {
        Unknown = 0, //irsdk_TrackWetness_UNKNOWN = 0,
        Dry, //irsdk_TrackWetness_Dry,
        SlightlyDamp, //irsdk_TrackWetness_MostlyDry,
        Damp, //irsdk_TrackWetness_VeryLightlyWet,
        SlightlyWet, //irsdk_TrackWetness_LightlyWet,
        Wet, //irsdk_TrackWetness_ModeratelyWet,
        VeryWet, //irsdk_TrackWetness_VeryWet,
        Flooded, //irsdk_TrackWetness_ExtremelyWet
    }
    
    // [JsonConverter(typeof(StringEnumConverter))] 
    // public enum PaceMode
    // {
    //     SingleFileStart = 0,
    //     DoubleFileStart,
    //     SingleFileRestart,
    //     DoubleFileRestart,
    //     NotPacing,
    // }
    //
    // [JsonConverter(typeof(StringEnumConverter))] 
    // [Flags]
    // public enum PaceFlags
    // {
    //     None = 0x00,
    //     EndOfLine = 0x01,
    //     FreePass = 0x02,
    //     WavedAround = 0x04,
    // }
}
