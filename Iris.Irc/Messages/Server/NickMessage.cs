using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc.Messages.Server
{
    public class NickMessage : Message
    {
        public string NewNick { get; private set; }

        public string OldNick { get; private set; }

        public NickMessage(string line)
            : base(line)
        {
            string[] split = line.Split(' ');

            if (split.Length < 3)
                throw new FormatException("Not enough parts in message.");

            if (!split[1].Equals(ServerStringMessageType.Nickname, StringComparison.OrdinalIgnoreCase))
                throw new FormatException("Not a NICK message.");

            OldNick = split[0].Remove(0, 1);
            NewNick = split[2];
        }

        public static new bool IsCorrectFormat(string line)
        {
            string[] split = line.Split(' ');

            return split.Length > 2 && split[1].Equals(ServerStringMessageType.Nickname, StringComparison.OrdinalIgnoreCase);
        }
    }
}