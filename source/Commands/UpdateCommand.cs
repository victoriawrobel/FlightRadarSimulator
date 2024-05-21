using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180686.source.Commands
{
    internal class UpdateCommand : Command
    {
        public Dictionary<string, string> KeyValuePairs { get; }
        public string Conditions { get; }

        public UpdateCommand(string objectClass, Dictionary<string, string> keyValuePairs, string conditions) : base(objectClass)
        {
            KeyValuePairs = keyValuePairs;
            Conditions = conditions;
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
