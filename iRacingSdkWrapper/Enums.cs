namespace iRacingSdkWrapper
{
    public enum TrackSurfaces
    {
        NotInWorld = -1,
        OffTrack,
        InPitStall,
        AproachingPits,
        OnTrack
    }

    public enum SessionStates
    {
        Invalid,
        GetInCar,
        Warmup,
        ParadeLaps,
        Racing,
        Checkered,
        CoolDown
    }

    public enum CarLeftRights
    {
        LROff,
        LRClear, // no cars around us.
        LRCarLeft, // there is a car to our left. 
        LRCarRight, // there is a car to our right. 
        LRCarLeftRight, // there are cars on each side. 
        LR2CarsLeft, // there are two cars to our left. 
        LR2CarsRight // there are two cars to our right.
    }

    public enum PitSvStatuses
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
    };
}
