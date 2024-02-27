using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180686.src.Objects
{
    public class Cargo : Entity
    {
        public float Weight;
        public string Code;
        public string Description;

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
