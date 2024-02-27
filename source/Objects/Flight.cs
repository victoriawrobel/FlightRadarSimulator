using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180686.src.Objects
{
    public class Flight : Entity
    {
        public ulong OriginID;
        public ulong TargetID;
        public string TakeOffTime;
        public string LandingTime;
        public float Longitude;
        public float Latitude;
        public float AMSL;
        public ulong PlaneID;
        public ulong[] CrewID;
        public ulong[] LoadID;

        public Flight(ulong ID, ulong originID, ulong targetID, string takeOffTime, string landingTime, float longitude, float latitude, float aMSL, ulong planeID, ulong[] crewID, ulong[] loadID) : base(ID)
        {
            this.OriginID = originID;
            this.TargetID = targetID;
            this.TakeOffTime = takeOffTime;
            this.LandingTime = landingTime;
            this.Longitude = longitude;
            this.Latitude = latitude;
            AMSL = aMSL;
            this.PlaneID = planeID;
            this.CrewID = crewID;
            this.LoadID = loadID;
        }

        public override string ToString()
        {
            return $"Flight: {ID} {OriginID} {TargetID} {TakeOffTime} {LandingTime} {Longitude} {Latitude} {AMSL} {PlaneID} {CrewID} {LoadID}";
        }
    }
}
