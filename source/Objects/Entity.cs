using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180686.src.Objects
{
    public class Entity
    {
        public ulong ID { get; set; }

        public Entity(ulong ID)
        {
            this.ID = ID;
        }

        public override string ToString()
        {
            return $"Entity: {ID}";
        }
    }

}
