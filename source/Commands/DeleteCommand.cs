using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180686.source.Commands
{
    internal class DeleteCommand : Command
    {
        public string ObjectClass { get; }
        public string Conditions { get; }

        public DeleteCommand(string objectClass, string conditions) : base(objectClass)
        {
            ObjectClass = objectClass;
            Conditions = conditions;
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
