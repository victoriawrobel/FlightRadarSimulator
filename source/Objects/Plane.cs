using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180686.src.Objects
{
    public class Plane : Entity
    {
        public string SerialNr;
        public string CountryISO;
        public string Model;

        public Plane(ulong ID, string serialNr, string countryISO, string model) : base(ID)
        {
            this.SerialNr = serialNr;
            this.CountryISO = countryISO;
            this.Model = model;
        }

        public override string ToString()
        {
            return $"Plane: {ID} {SerialNr} {CountryISO} {Model}";
        }
    }
}
