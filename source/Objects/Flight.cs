using FlightTrackerGUI;
using System;


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

        public void UpdatePosition()
        {
            if (EntitySearch.GetObject(OriginID) is Airport origin &&
                EntitySearch.GetObject(TargetID) is Airport target)
            {
                if (GetProgress() < 1 && GetProgress() > 0)
                {
                    Longitude = origin.Longitude + (target.Longitude - origin.Longitude) * GetProgress();
                    Latitude = origin.Latitude + (target.Latitude - origin.Latitude) * GetProgress();
                    AMSL = origin.AMSL + (target.AMSL - origin.AMSL) * GetProgress();
                }
                else if (GetProgress() <= 0)
                {
                    Longitude = origin.Longitude;
                    Latitude = origin.Latitude;
                    AMSL = origin.AMSL;
                }
                else if (GetProgress() >= 1)
                {
                    Longitude = target.Longitude;
                    Latitude = target.Latitude;
                    AMSL = target.AMSL;
                }
            }
            else
            {
                throw new KeyNotFoundException("Origin or target not found.");
            }
        }

        public float GetProgress()
        {
            DateTime currentTime = DateTime.Now;
            DateTime takeOff = DateTime.Parse(TakeOffTime);
            DateTime landing = DateTime.Parse(LandingTime);

            TimeSpan elapsedTime = currentTime - takeOff;
            TimeSpan totalDuration = landing - takeOff;

            if (elapsedTime < TimeSpan.Zero)
            {
                return 0.0f;
            }
            else if (elapsedTime >= totalDuration)
            {
                return 1.0f;
            }
            else
            {
                return (float)(elapsedTime.TotalSeconds / totalDuration.TotalSeconds);
            }
        }

        public float GetRotation()
        {
            if (EntitySearch.GetObject(OriginID) is Airport origin &&
                EntitySearch.GetObject(TargetID) is Airport target)
            {
                float x = target.Longitude - origin.Longitude;
                float y = target.Latitude - origin.Latitude;

                float rotation = (float)Math.Atan2(x, y);
                return rotation;
            }

            return 0.0f;
        }

        public override string ToString()
        {
            return
                $"Flight: {ID} {OriginID} {TargetID} {TakeOffTime} {LandingTime} {Longitude} {Latitude} {AMSL} {PlaneID} {CrewID} {LoadID}";
        }
    }
}