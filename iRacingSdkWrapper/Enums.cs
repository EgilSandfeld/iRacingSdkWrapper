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
    
    public enum TrackSurfaceMaterials
    {
        irsdk_SurfaceNotInWorld = -1,
        irsdk_UndefinedMaterial = 0,

        irsdk_Asphalt1Material,
        irsdk_Asphalt2Material,
        irsdk_Asphalt3Material,
        irsdk_Asphalt4Material,
        irsdk_Concrete1Material,
        irsdk_Concrete2Material,
        irsdk_RacingDirt1Material,
        irsdk_RacingDirt2Material,
        irsdk_Paint1Material,
        irsdk_Paint2Material,
        irsdk_Rumble1Material,
        irsdk_Rumble2Material,
        irsdk_Rumble3Material,
        irsdk_Rumble4Material,

        irsdk_Grass1Material,
        irsdk_Grass2Material,
        irsdk_Grass3Material,
        irsdk_Grass4Material,
        irsdk_Dirt1Material,
        irsdk_Dirt2Material,
        irsdk_Dirt3Material,
        irsdk_Dirt4Material,
        irsdk_SandMaterial,
        irsdk_Gravel1Material,
        irsdk_Gravel2Material,
        irsdk_GrasscreteMaterial,
        irsdk_AstroturfMaterial,
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
