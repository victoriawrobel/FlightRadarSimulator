
namespace OOD_24L_01180686.source.Objects
{
    public class Entity : IType
    {
        public ulong ID { get; set; }

        public Dictionary<string, Func<object>> FieldMap = new Dictionary<string, Func<object>>();

        public Entity(ulong ID)
        {
            this.ID = ID;
            FieldMap.Add("ID", () => ID);
        }

        public override string ToString()
        {
            return $"Entity: {ID}";
        }

        public object GetFieldValue(string fieldName)
        {
            FieldMap.TryGetValue(fieldName, out var value);
            return value?.Invoke();
        }

        public virtual string GetTypeCustom()
        {
            return "Entity";
        }
    }
}