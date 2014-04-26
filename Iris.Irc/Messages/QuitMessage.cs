using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.Irc.Messages
{
    public class QuitMessage : Message
    {
        public string Message { get; private set; }

        public string Nick { get; private set; }

        public override MessageTypes Type
        {
            get { return MessageTypes.Quit; }
        }

        public QuitMessage(string line)
            : base(line)
        {
            string[] split = line.Split(' ');

            if (split.Length < 3)
                throw new FormatException("Not enough parts in message.");

            if (split[1].ToUpper() != IrcClientMessageTypes.Quit)
                throw new FormatException("Not a QUIT message.");

            Nick = split[0].Remove(0, 1);
            Message = split.Skip(2).Aggregate((left, right) => left + " " + right).Remove(0, 1);
        }
    }
}