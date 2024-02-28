using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OOD_24L_01180686.source.Objects
{
    public class Passenger : Person
    {
        public string CabinClass { get; set; }
        public ulong Miles { get; set; }

        public Passenger(ulong ID, string name, ulong age, string phone, string email, string cabinClass, ulong miles) : base(ID, name, age, phone, email)
        {
            this.CabinClass = cabinClass;
            this.Miles = miles;
        }

        public override string ToString()
        {
            return $"Passenger: {ID} {Name} {Age} {Phone} {Email} {CabinClass} {Miles}";
        }
    }
}