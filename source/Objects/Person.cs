
namespace OOD_24L_01180686.source.Objects
{
    public class Person : Entity
    {
        public string Name { get; set; }
        public ulong Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Person(ulong ID, string name, ulong age, string phone, string email) : base(ID)
        {
            this.Name = name;
            this.Age = age;
            this.Phone = phone;
            this.Email = email;
        }

        public override string ToString()
        {
            return $"Person: {ID} {Name} {Age} {Phone} {Email}";
        }
    }
}