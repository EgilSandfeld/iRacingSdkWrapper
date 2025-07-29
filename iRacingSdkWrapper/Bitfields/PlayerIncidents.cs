using System;

namespace iRacingSdkWrapper.Bitfields
{
    public class PlayerIncidents : BitfieldBase<IRacingPlayerIncidents>
    {
        private uint IRSKD_INCIDENT_REP_MASK = 0x000000FF;
        private uint IRSKD_INCIDENT_PEN_MASK = 0x0000FF00;

        public PlayerIncidents(int value) : base(value)
        {
        }
        
        public (IRacingPlayerIncidents type, int penalty) Get()
        {
            uint val = Value;
            var incidentType = (IRacingPlayerIncidents)(val & IRSKD_INCIDENT_REP_MASK);
    
            // Correctly mask the value to get the penalty.
            //var penaltyValue = (int)(val & IRSKD_INCIDENT_PEN_MASK);
    
            // Determine the penalty multiplier
            int penaltyMultiplier;
            // switch (penaltyValue)
            // {
            //     case (int)IRacingPlayerIncidents.PenNoReport:
            //     case (int)IRacingPlayerIncidents.PenZeroX:
            //         penaltyMultiplier = 0;
            //         break;
            //     case (int)IRacingPlayerIncidents.PenOneX:
            //         penaltyMultiplier = 1;
            //         break;
            //     case (int)IRacingPlayerIncidents.PenTwoX:
            //         penaltyMultiplier = 2;
            //         break;
            //     case (int)IRacingPlayerIncidents.PenFourX:
            //         penaltyMultiplier = 4;
            //         break;
            //     default:
            //         penaltyMultiplier = 0; // Default to zero if unknown
            //         break;
            // }

            switch (incidentType)
            {
                case IRacingPlayerIncidents.RepNoReport:
                    penaltyMultiplier = 0;
                    break;
                case IRacingPlayerIncidents.RepOutOfControl:
                    penaltyMultiplier = 2;
                    break;
                case IRacingPlayerIncidents.RepOffTrack:
                case IRacingPlayerIncidents.RepOffTrackOngoing:
                    penaltyMultiplier = 1;
                    break;
                
                case IRacingPlayerIncidents.RepContactWithWorld:
                    penaltyMultiplier = 0;
                    break;
                case IRacingPlayerIncidents.RepCollisionWithWorld:
                case IRacingPlayerIncidents.RepCollisionWithWorldOngoing:
                    penaltyMultiplier = 2;
                    break;
                
                case IRacingPlayerIncidents.RepContactWithCar:
                    penaltyMultiplier = 0;
                    break;
                case IRacingPlayerIncidents.RepCollisionWithCar:
                    penaltyMultiplier = 4;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
    
            return (incidentType, penaltyMultiplier);
        }
    }

    [Flags]
    public enum IRacingPlayerIncidents : uint
    {
        // first byte is incident report flag
        // only one of these will be used

        RepNoReport					= 0x0000, // no penalty 
        RepOutOfControl				= 0x0001, // "Loss of Control (2x)"
        RepOffTrack					= 0x0002, // "Off Track (1x)"
        RepOffTrackOngoing			= 0x0003, // not currently sent
        RepContactWithWorld			= 0x0004, // "Contact (0x)"
        RepCollisionWithWorld		= 0x0005, // "Contact (2x)"
        RepCollisionWithWorldOngoing = 0x0006, // not currently sent
        RepContactWithCar			= 0x0007, // "Car Contact (0x)"
        RepCollisionWithCar			= 0x0008, // "Car Contact (4x)"


        // second byte is incident penalty
        // only one of these will be used

        PenNoReport					= 0x0000, // no penalty
        PenZeroX						= 0x0100, // 0x
        PenOneX						= 0x0200, // 1x
        PenTwoX						= 0x0300, // 2x
        PenFourX						= 0x0400, // 4x


        // not enums, used to seperate the above incident report field
        // from the incident penalty field
        
        //IRSKD_INCIDENT_REP_MASK						= 0x000000FF,
        //IRSKD_INCIDENT_PEN_MASK						= 0x0000FF00,
    }
}
