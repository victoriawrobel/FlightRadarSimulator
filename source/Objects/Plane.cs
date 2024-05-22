
namespace OOD_24L_01180686.source.Objects
{
    public class Plane : Entity
    {
        public string SerialNr { get; set; }
        public string CountryISO { get; set; }
        public string Model { get; set; }

        public Plane() : base()
        {
            SerialNr = "N/A";
            CountryISO = "UNK";
            Model = "Unknown";

            InitializeFieldMap();
        }

        public Plane(ulong ID, string serialNr, string countryISO, string model) : base(ID)
        {
            this.SerialNr = serialNr;
            this.CountryISO = countryISO;
            this.Model = model;

            InitializeFieldMap();
        }

        public void InitializeFieldMap()
        {
            FieldMap.Add("SerialNr", () => SerialNr);
            FieldMap.Add("CountryISO", () => CountryISO);
            FieldMap.Add("Model", () => Model);
        }

        public override string ToString()
        {
            return $"Plane: {ID} {SerialNr} {CountryISO} {Model}";
        }

        public override string GetTypeCustom()
        {
            return "Person";
        }
    }
}