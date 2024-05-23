using OOD_24L_01180686.source.Reports;

namespace OOD_24L_01180686.source.Objects
{
    public class CargoPlane : Plane, IReportable
    {
        public float MaxLoad { get; set; }

        public CargoPlane() : base()
        {
            MaxLoad = 0.0f;

            FieldMap.Add("MaxLoad", () => MaxLoad);
        }

        public CargoPlane(ulong ID, string serialNr, string countryISO, string model, float maxLoad) : base(ID,
            serialNr, countryISO, model)
        {
            this.MaxLoad = maxLoad;

            FieldMap.Add("MaxLoad", () => MaxLoad);
        }

        public override string ToString()
        {
            return $"CargoPlane: {ID} {SerialNr} {CountryISO} {Model} {MaxLoad}";
        }

        public string Accept(Reporter reporter)
        {
            return reporter.Visit(this);
        }

        public override string GetTypeCustom()
        {
            return "CargoPlane";
        }
    }
}