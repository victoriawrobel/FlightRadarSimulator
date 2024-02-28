using OOD_24L_01180686.source.Readers;

namespace OOD_24L_01180686.source.Factories
{
    public interface IReaderFactory
    {
        IDataRead Create();
    }

    public class FTRReaderFactory : IReaderFactory
    {
        public IDataRead Create()
        {
            return new FTRReader();
        }
    }
}