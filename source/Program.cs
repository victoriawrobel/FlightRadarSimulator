using OOD_24L_01180686.src.Factories;
using OOD_24L_01180686.src.Objects;
using OOD_24L_01180686.src.Readers;


namespace OOD_24L_01180686.src
{
    class Program
    {
        public static void Main(string[] args)
        {
            FileReaderFactory fileReaderFactory = new FTRReaderFactory();
            var fileReader = fileReaderFactory.Create();
            var list = fileReader.ReadData(Directory.GetCurrentDirectory() + "..\\..\\..\\..\\DataFiles\\example1.ftr");
            foreach(var item in list)
            {
                Console.WriteLine(item.ToString());
            }

        }
    }
}