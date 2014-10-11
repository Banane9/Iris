using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc.Messages.Server
{
    public class Notice : Message
    {
        public string Message { get; private set; }

        public string Recipient { get; private set; }

        public string User { get; private set; }

        public Notice(string line)
            : base(line)
        {
            string[] split = line.Split(' ');

            if (split.Length < 4)
                throw new FormatException("Not enough parts in message.");

            if (!split[1].Equals(ServerStringMessageType.Notice, StringComparison.OrdinalIgnoreCase))
                throw new FormatException("Not a NOTICE.");

            User = split[0].Remove(0, 1);
            Recipient = split[2];
            Message = string.Join(" ", split.Skip(3)).Remove(0, 1);
        }

        public static new bool IsCorrectFormat(string line)
        {
            string[] split = line.Split(' ');

            return split.Length > 3 && split[1].Equals(ServerStringMessageType.Notice, StringComparison.OrdinalIgnoreCase);
        }
    }
}