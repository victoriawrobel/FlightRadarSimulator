using OOD_24L_01180686.source.Factories;
using OOD_24L_01180686.source.Objects;
using OOD_24L_01180686.source.Readers;


namespace OOD_24L_01180686.source
{
    class Program
    {
        public static void Main(string[] args)
        {
            FileReaderFactory fileReaderFactory = new FTRReaderFactory();
            var fileReader = fileReaderFactory.Create();
            var list = fileReader.ReadData(Directory.GetCurrentDirectory() + "..\\..\\..\\..\\DataFiles\\example1.ftr");

            FileWriterFactory fileWriterFactory = new JSONWriterFactory();
            var fileWriter = fileWriterFactory.Create();
            fileWriter.WriteData(list,
                Directory.GetCurrentDirectory() + "..\\..\\..\\..\\DataFiles\\example1serializer.json");
        }
    }
}