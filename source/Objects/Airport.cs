using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OOD_24L_01180686.src.Objects
{
    public class Airport : Entity
    {
        public string Name;
        public string Code;
        public float Longitude;
        public float Latitude;
        public float AMSL;
        public string CountryISO;

        public Airport(ulong ID, string name, string code, float longitude, float latitude, float aMSL, string countryISO) : base(ID)
        {
            this.Name = name;
            this.Code = code;
            this.Longitude = longitude;
            this.Latitude = latitude;
            AMSL = aMSL;
            this.CountryISO = countryISO;
        }

        public override string ToString()
        {
            return $"Airport: {ID} {Name} {Code} {Longitude} {Latitude} {AMSL} {CountryISO}";
        }
    }
}
