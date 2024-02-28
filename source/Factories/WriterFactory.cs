using OOD_24L_01180686.source.Writers;

namespace OOD_24L_01180686.source.Factories
{
    public interface IWriterFactory
    {
        IDataWrite Create();
    }

    public abstract class FileWriterFactory : IWriterFactory
    {
        public virtual IDataWrite Create()
        {
            return null;
        }
    }

    public class JSONWriterFactory : FileWriterFactory
    {
        public override IDataWrite Create()
        {
            return new JSONWriter();
        }
    }
}