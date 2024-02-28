using OOD_24L_01180686.source.Readers;

namespace OOD_24L_01180686.source.Factories
{
    public interface IReaderFactory
    {
        IDataRead Create();
    }

    public abstract class FileReaderFactory : IReaderFactory
    {
        virtual public IDataRead Create()
        {
            return null;
        }
    }

    public class FTRReaderFactory : FileReaderFactory
    {
        public override IDataRead Create()
        {
            return new FTRReader();
        }
    }
}