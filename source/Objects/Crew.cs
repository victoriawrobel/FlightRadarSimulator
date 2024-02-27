using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OOD_24L_01180686.src.Objects
{
    public class Crew : Person
    {
        public ushort Practice;
        public string Role;

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
