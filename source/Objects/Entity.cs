
namespace OOD_24L_01180686.source.Objects
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