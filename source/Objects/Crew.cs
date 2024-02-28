
namespace OOD_24L_01180686.source.Objects
{
    public class Crew : Person
    {
        public ushort Practice { get; protected set; }
        public string Role { get; protected set; }

        public Crew(ulong ID, string name, ulong age, string phone, string email, ushort practice, string role) : base(ID, name, age, phone, email)
        {
            this.Practice = practice;
            this.Role = role;
        }

        public override string ToString()
        {
            return $"Crew: {ID} {Name} {Age} {Phone} {Email} {Practice} {Role}";
        }
    }
}