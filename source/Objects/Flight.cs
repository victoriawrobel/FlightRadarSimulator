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
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float AMSL { get; set; }
        public ulong PlaneID { get; protected set; }
        public ulong[] CrewID { get; protected set; }
        public ulong[] LoadID { get; protected set; }

        private DateTime LastUpdated;

        public Flight(ulong ID, ulong originID, ulong targetID, string takeOffTime, string landingTime, float longitude,
            float latitude, float aMSL, ulong planeID, ulong[] crewID, ulong[] loadID) : base(ID)
        {
            this.OriginID = originID;
            this.TargetID = targetID;
            this.TakeOffTime = takeOffTime;
            this.LandingTime = landingTime;
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.AMSL = aMSL;
            this.PlaneID = planeID;
            this.CrewID = crewID;
            this.LoadID = loadID;
            LastUpdated = DateTime.Now;

            FieldMap.Add("OriginID", () => OriginID);
            FieldMap.Add("TargetID", () => TargetID);
            FieldMap.Add("TakeOffTime", () => TakeOffTime);
            FieldMap.Add("LandingTime", () => LandingTime);
            FieldMap.Add("Longitude", () => Longitude);
            FieldMap.Add("Latitude", () => Latitude);
            FieldMap.Add("AMSL", () => AMSL);
            FieldMap.Add("PlaneID", () => PlaneID);
            FieldMap.Add("CrewID", () => CrewID);
            FieldMap.Add("LoadID", () => LoadID);

            InitialUpdate();
        }

        public void InitialUpdate()
        {
            if ((Airport)EntitySearch.GetObject(OriginID) != null &&
                (Airport)EntitySearch.GetObject(TargetID) != null)
            {
                Airport origin = (Airport)EntitySearch.GetObject(OriginID);
                Airport target = (Airport)EntitySearch.GetObject(TargetID);
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


        public void UpdatePosition()
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan elapsedTime = currentTime - LastUpdated;
            LastUpdated = currentTime;
            if ((Airport)EntitySearch.GetObject(OriginID) != null &&
                (Airport)EntitySearch.GetObject(TargetID) != null)
            {
                Airport origin = (Airport)EntitySearch.GetObject(OriginID);
                Airport target = (Airport)EntitySearch.GetObject(TargetID);
                if (GetProgress() < 1 && GetProgress() > 0)
                {
                    Longitude += (target.Longitude - Longitude) / GetProgress() * (float)elapsedTime.TotalHours;
                    Latitude += (target.Latitude - Latitude) / GetProgress() * (float)elapsedTime.TotalHours;
                    AMSL += (target.AMSL - AMSL) / GetProgress() * (float)elapsedTime.TotalHours;
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
            if ((Airport)EntitySearch.GetObject(TargetID) != null)
            {
                Airport target = (Airport)EntitySearch.GetObject(TargetID);
                float x = target.Longitude - Longitude;
                float y = target.Latitude - Latitude;

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

        public override string GetTypeCustom()
        {
            return "Flight";
        }
    }
}