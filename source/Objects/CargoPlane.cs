using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180686.source.Objects
{
    public class CargoPlane : Plane
    {
        public float MaxLoad { get; set; }

        public CargoPlane(ulong ID, string serialNr, string countryISO, string model, float maxLoad) : base(ID, serialNr, countryISO, model)
        {
            this.MaxLoad = maxLoad;
        }

        public override string ToString()
        {
            return $"CargoPlane: {ID} {SerialNr} {CountryISO} {Model} {MaxLoad}";
        }
    }
}