
namespace OOD_24L_01180686.source.Objects
{
    public class Airport : Entity
    {
        public string Name { get; protected set; }
        public string Code { get; protected set; }
        public float Longitude { get; protected set; }
        public float Latitude { get; protected set; }
        public float AMSL { get; protected set; }
        public string CountryISO { get; protected set; }

        public Airport(ulong ID, string name, string code, float longitude, float latitude, float aMSL,
            string countryISO) : base(ID)
        {
            this.Name = name;
            this.Code = code;
            this.Longitude = longitude;
            this.Latitude = latitude;
            AMSL = aMSL;
            this.CountryISO = countryISO;
        }

        public override string ToString()
        {
            return $"Airport: {ID} {Name} {Code} {Longitude} {Latitude} {AMSL} {CountryISO}";
        }
    }
}