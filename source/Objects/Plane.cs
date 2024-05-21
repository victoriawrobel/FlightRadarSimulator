
namespace OOD_24L_01180686.source.Objects
{
    public class Plane : Entity
    {
        public string SerialNr { get; protected set; }
        public string CountryISO { get; protected set; }
        public string Model { get; protected set; }

        public Plane(ulong ID, string serialNr, string countryISO, string model) : base(ID)
        {
            this.SerialNr = serialNr;
            this.CountryISO = countryISO;
            this.Model = model;

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