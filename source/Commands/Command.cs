using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180686.source.Commands
{
    internal abstract class Command
    {
        public static readonly Dictionary<string, Func<IComparable, IComparable, bool>> Operators = new Dictionary<string, Func<IComparable, IComparable, bool>>
        {
            { "==", (a, b) => a.Equals(b) },
            { "!=", (a, b) => !a.Equals(b) },
            { ">", (a, b) => a.CompareTo(b) > 0 },
            { "<", (a, b) =>a.CompareTo(b) < 0 },
            { ">=", (a, b) => a.CompareTo(b) >= 0 },
            { "<=", (a, b) => a.CompareTo(b) <= 0 }
        };
        public Command() { }
        public Command(string objectClass)
        {
            ObjectClass = objectClass;
        }

        protected string ObjectClass { get; set; }
        public abstract void Execute();

        protected IComparable ParseValue(string value)
        {
            if (ulong.TryParse(value, out var intValue))
            {
                return intValue;
            }

            if (float.TryParse(value, out var doubleValue))
            {
                return doubleValue;
            }
            return value;
        }
    }
}
