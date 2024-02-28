using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180686.source.Objects
{
    public class PassengerPlane : Plane
    {
        public ushort FirstClassSize { get; set; }
        public ushort BusinessClassSize { get; set; }
        public ushort EconomyClassSize { get; set; }

        public PassengerPlane(ulong ID, string serialNr, string countryISO, string model, ushort firstClassSize, ushort businessClassSize, ushort economyClassSize) : 
            base(ID, serialNr, countryISO, model)
        {
            this.FirstClassSize = firstClassSize;
            this.BusinessClassSize = businessClassSize;
            this.EconomyClassSize = economyClassSize;
        }

        public override string ToString()
        {
            return
                $"PassengerPlane: {ID} {SerialNr} {CountryISO} {Model} {FirstClassSize} {BusinessClassSize} {EconomyClassSize}";
        }
    }
}