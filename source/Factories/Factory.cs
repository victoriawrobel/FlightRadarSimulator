using OOD_24L_01180686.src.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180686.src.Factories
{
    public interface IFactory
    {
        IDataRead Create();
    }

    public abstract class FileReaderFactory : IFactory
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
