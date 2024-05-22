
namespace OOD_24L_01180686.source.Objects
{
    public class Crew : Person
    {
        public ushort Practice { get; set; }
        public string Role { get; set; }

        public Crew() : base()
        {
            Practice = 0;
            Role = "Unknown";

            IntitializeFieldMap();
        }

        public Crew(ulong ID, string name, ulong age, string phone, string email, ushort practice, string role) : base(ID, name, age, phone, email)
        {
            this.Practice = practice;
            this.Role = role;

            IntitializeFieldMap();
        }

        private void IntitializeFieldMap()
        {
            FieldMap.Add("Practice", () => Practice);
            FieldMap.Add("Role", () => Role);
        }

        public override string ToString()
        {
            return $"Crew: {ID} {Name} {Age} {Phone} {Email} {Practice} {Role}";
        }

        public override string GetTypeCustom()
        {
            return "Crew";
        }
    }
}