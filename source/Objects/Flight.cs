
namespace OOD_24L_01180686.source.Objects
{
    public class Flight : Entity
    {
        public ulong OriginID { get; set; }
        public ulong TargetID { get; set; }
        public string TakeOffTime { get; set; }
        public string LandingTime { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float AMSL { get; set; }
        public ulong PlaneID { get; set; }
        public ulong[] CrewID { get; set; }
        public ulong[] LoadID { get; set; }

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