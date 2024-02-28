using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace OOD_24L_01180686.source.Writers
{
    public interface IDataWrite
    {
        void WriteData(IEnumerable<object> objects, string filepath);
    }

    public class JSONWriter : IDataWrite
    {
        public void WriteData(IEnumerable<object> objects, string filepath)
        {
            string jsonString = JsonSerializer.Serialize(objects, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filepath, jsonString);
        }
    }
}