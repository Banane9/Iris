using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.Irc.Messages
{
    public class NickMessage : Message
    {
        public string OldNick { get; private set; }

        public string NewNick { get; private set; }

        public override MessageTypes Type
        {
            get { return MessageTypes.Nick; }
        }

        public NickMessage(string line)
            : base(line)
        {
            string[] split = line.Split(' ');

            if (split.Length < 3)
                throw new FormatException("Not enough parts in message.");

            if (split[1].ToUpper() != IrcClientMessageTypes.Nickname)
                throw new FormatException("Not a NICK message.");

            OldNick = split[0].Remove(0, 1);
            NewNick = split[2];
        }
    }
}