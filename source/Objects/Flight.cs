
namespace OOD_24L_01180686.source.Objects
{
    public class Flight : Entity
    {
        public ulong OriginID { get; protected set; }
        public ulong TargetID { get; protected set; }
        public string TakeOffTime { get; protected set; }
        public string LandingTime { get; protected set; }
        public float Longitude { get; protected set; }
        public float Latitude { get; protected set; }
        public float AMSL { get; protected set; }
        public ulong PlaneID { get; protected set; }
        public ulong[] CrewID { get; protected set; }
        public ulong[] LoadID { get; protected set; }

        public Flight(ulong ID, ulong originID, ulong targetID, string takeOffTime, string landingTime, float longitude,
            float latitude, float aMSL, ulong planeID, ulong[] crewID, ulong[] loadID) : base(ID)
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
            return
                $"Flight: {ID} {OriginID} {TargetID} {TakeOffTime} {LandingTime} {Longitude} {Latitude} {AMSL} {PlaneID} {CrewID} {LoadID}";
        }
    }
}