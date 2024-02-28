using OOD_24L_01180686.source.Writers;

namespace OOD_24L_01180686.source.Factories
{
    public interface IWriterFactory
    {
        IDataWrite Create();
    }

    public class JSONWriterFactory : IWriterFactory
    {
        public IDataWrite Create()
        {
            return new JSONWriter();
        }
    }
}