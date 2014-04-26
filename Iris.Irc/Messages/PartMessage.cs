using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.Irc.Messages
{
    public class PartMessage : Message
    {
        public string Nick { get; private set; }

        public string Channel { get; private set; }

        public override MessageTypes Type
        {
            get { return MessageTypes.Part; }
        }

        public PartMessage(string line)
            : base(line)
        {
            string[] split = line.Split(' ');

            if (split.Length < 3)
                throw new FormatException("Not enough parts in message.");

            if (split[1].ToUpper() != IrcClientMessageTypes.Part)
                throw new FormatException("Not a PART message.");

            Nick = split[0].Remove(0, 1);
            Channel = split[2];
        }
    }
}