using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180686.source.Commands
{
    internal class AddCommand : Command
    {
        public Dictionary<string, string> KeyValuePairs { get; }

        public AddCommand(string objectClass, Dictionary<string, string> keyValuePairs) : base(objectClass)
        {
            KeyValuePairs = keyValuePairs;
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
