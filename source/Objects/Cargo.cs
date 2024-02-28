using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180686.source.Objects
{
    public class Cargo : Entity
    {
        public float Weight { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Cargo(ulong ID, float weight, string code, string description) : base(ID)
        {
            this.Weight = weight;
            this.Code = code;
            this.Description = description;
        }

        public override string ToString()
        {
            return $"Cargo: {ID} {Weight} {Code} {Description}";
        }
    }
}