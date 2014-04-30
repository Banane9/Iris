using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.Irc.ServerMessages
{
    public class QuitMessage : Message
    {
        public string Message { get; private set; }

        public string User { get; private set; }

        public override MessageTypes Type
        {
            get { return MessageTypes.String; }
        }

        public override bool IsCorrectFormat(string line)
        {
            string[] split = line.Split(' ');

            return split.Length > 3 && split[1].ToUpper() == ServerStringMessageTypes.Quit;
        }

        public QuitMessage(string line)
            : base(line)
        {
            string[] split = line.Split(' ');

            if (split.Length < 3)
                throw new FormatException("Not enough parts in message.");

            if (split[1].ToUpper() != ServerStringMessageTypes.Quit)
                throw new FormatException("Not a QUIT message.");

            User = split[0].Remove(0, 1);
            Message = split.Skip(2).Aggregate((left, right) => left + " " + right).Remove(0, 1);
        }
    }
}