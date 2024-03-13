

namespace OOD_24L_01180686.source.Readers
{
    public interface IDataRead
    {
        IEnumerable<object> ReadData(string filepath);
    }

    public class FTRReader : IDataRead
    {
        public IEnumerable<object> ReadData(string filepath)
        {
            if (!filepath.EndsWith(".ftr"))
                throw new ArgumentException("Wrong file type.");

            List<object> objects = new List<object>();
            using (var reader = new StreamReader(filepath))
            {
                while (!reader.EndOfStream)
                {
                    var attr = reader.ReadLine().Split(',');
                    objects.Add(LineParser(attr));
                }
            }

            return objects;
        }

        private object LineParser(string[] attr)
        {
            string objectType = attr[0];
            if (Reader.objectCreators.TryGetValue(objectType, out var creator))
            {
                return creator(attr);
            }

            throw new ArgumentException("Unrecognized object type.");
        }
    }
}