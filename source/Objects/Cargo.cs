
namespace OOD_24L_01180686.source.Objects
{
    public class Cargo : Entity
    {
        public float Weight { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Cargo() : base()
        {
            Weight = 0.0f;
            Code = "N/A";
            Description = "No description available";
        }

        public Cargo(ulong ID, float weight, string code, string description) : base(ID)
        {
            this.Weight = weight;
            this.Code = code;
            this.Description = description;

            FieldMap.Add("Weight", () => Weight);
            FieldMap.Add("Code", () => Code);
            FieldMap.Add("Description", () => Description);
        }

        public override string ToString()
        {
            return $"Cargo: {ID} {Weight} {Code} {Description}";
        }

        public override string GetTypeCustom()
        {
            return "Cargo";
        }
    }
}