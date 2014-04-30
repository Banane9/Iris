using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Irc.Messages
{
    public class JoinMessage : Message
    {
        public string Nick { get; private set; }

        public string Channel { get; private set; }

        public override MessageTypes Type
        {
            get { return MessageTypes.Join; }
        }

        public JoinMessage(string line)
            : base(line)
        {
            string[] split = line.Split(' ');

            if (split.Length < 3)
                throw new FormatException("Not enough parts in message.");

            if (split[1].ToUpper() != ClientMessageTypes.Join)
                throw new FormatException("Not a JOIN message.");

            Nick = split[0].Remove(0, 1);
            Channel = split[2];
        }
    }
}