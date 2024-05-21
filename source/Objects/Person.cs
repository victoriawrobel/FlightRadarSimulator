
namespace OOD_24L_01180686.source.Objects
{
    public class Person : Entity
    {
        public string Name { get; protected set; }
        public ulong Age { get; protected set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Person(ulong ID, string name, ulong age, string phone, string email) : base(ID)
        {
            this.Name = name;
            this.Age = age;
            this.Phone = phone;
            this.Email = email;

            FieldMap.Add("Name", () => Name);
            FieldMap.Add("Age", () => Age);
            FieldMap.Add("Phone", () => Phone);
            FieldMap.Add("Email", () => Email);
        }

        public override string ToString()
        {
            return $"Person: {ID} {Name} {Age} {Phone} {Email}";
        }

        public override string GetTypeCustom()
        {
            return "Person";
        }
    }
}