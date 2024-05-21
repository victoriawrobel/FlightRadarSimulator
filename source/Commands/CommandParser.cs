using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180686.source.Commands
{
    internal class CommandParser
    {
        private static readonly Dictionary<string, Func<string, Command>> commandParsers = new Dictionary<string, Func<string, Command>>
        {
            { "display", ParseDisplayCommand },
            { "add", ParseAddCommand },
            { "delete", ParseDeleteCommand },
            { "update", ParseUpdateCommand }
        };
        public static Command Parse(string input)
        {
            var parts = input.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return null;

            var commandString = parts[0].ToLower();
            if (!commandParsers.ContainsKey(commandString))
            {
                throw new ArgumentException("Invalid command.");
            }

            return commandParsers[commandString](parts.Length > 1 ? parts[1] : "");
        }

        private static DisplayCommand ParseDisplayCommand(string input)
        {
            var parts = input.Split(new string[] { " from " }, StringSplitOptions.RemoveEmptyEntries);
            var fieldsPart = parts[0];
            var classAndConditions = parts[1].Split(new string[] { " where " }, StringSplitOptions.RemoveEmptyEntries);

            var objectFields = fieldsPart.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var objectClass = classAndConditions[0];
            var conditions = classAndConditions.Length > 1 ? classAndConditions[1] : null;

            return new DisplayCommand(objectFields, objectClass, conditions);
        }

        private static AddCommand ParseAddCommand(string input)
        {
            var parts = input.Split(new string[] { " new (" }, StringSplitOptions.RemoveEmptyEntries);
            var objectClass = parts[0];
            var keyValueList = parts[1].TrimEnd(')');

            var keyValuePairs = keyValueList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var keyValueDictionary = new Dictionary<string, string>();

            foreach (var pair in keyValuePairs)
            {
                var keyValue = pair.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                keyValueDictionary[keyValue[0]] = keyValue[1];
            }

            return new AddCommand(objectClass, keyValueDictionary);
        }

        private static DeleteCommand ParseDeleteCommand(string input)
        {
            var parts = input.Split(new string[] { " where " }, StringSplitOptions.RemoveEmptyEntries);
            var objectClass = parts[0];
            var conditions = parts.Length > 1 ? parts[1] : null;

            return new DeleteCommand(objectClass, conditions);
        }

        private static UpdateCommand ParseUpdateCommand(string input)
        {
            var parts = input.Split(new string[] { " set (" }, StringSplitOptions.RemoveEmptyEntries);
            var objectClass = parts[0];
            var keyValueAndConditions = parts[1].Split(new string[] { ") where " }, StringSplitOptions.RemoveEmptyEntries);

            var keyValueList = keyValueAndConditions[0];
            var conditions = keyValueAndConditions.Length > 1 ? keyValueAndConditions[1] : null;

            var keyValuePairs = keyValueList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var keyValueDictionary = new Dictionary<string, string>();

            foreach (var pair in keyValuePairs)
            {
                var keyValue = pair.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                keyValueDictionary[keyValue[0]] = keyValue[1];
            }

            return new UpdateCommand(objectClass, keyValueDictionary, conditions);
        }
    }
}
