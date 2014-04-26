using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.Irc.Messages
{
    public class Notice : Message
    {
        public string Message { get; private set; }

        public string Nick { get; private set; }

        public string Recipient { get; private set; }

        public override MessageTypes Type
        {
            get { return MessageTypes.Notice; }
        }

        public Notice(string line)
            : base(line)
        {
            string[] split = line.Split(' ');

            if (split.Length < 4)
                throw new FormatException("Not enough parts in message.");

            if (split[1].ToUpper() != IrcClientMessageTypes.Notice)
                throw new FormatException("Not a NOTICE.");

            Nick = split[0].Remove(0, 1);
            Recipient = split[2];
            Message = split.Skip(3).Aggregate((left, right) => left + " " + right).Remove(0, 1);
        }
    }
}