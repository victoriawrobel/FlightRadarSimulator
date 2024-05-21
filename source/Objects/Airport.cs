
using OOD_24L_01180686.source.Reports;

namespace OOD_24L_01180686.source.Objects
{
    public class Airport : Entity, IReportable
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

            FieldMap.Add("Name", () => Name);
            FieldMap.Add("Code", () => Code);
            FieldMap.Add("Longitude", () => Longitude);
            FieldMap.Add("Latitude", () => Latitude);
            FieldMap.Add("AMSL", () => AMSL);
            FieldMap.Add("CountryISO", () => CountryISO);
        }

        public override string ToString()
        {
            return $"Airport: {ID} {Name} {Code} {Longitude} {Latitude} {AMSL} {CountryISO}";
        }

        public string Accept(Reporter reporter)
        {
            return reporter.Visit(this);
        }

        public string GetTypeCustom()
        {
            return "Airport";
        }
    }
}