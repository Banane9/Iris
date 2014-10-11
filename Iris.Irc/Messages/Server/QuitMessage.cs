using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc.Messages.Server
{
    public class QuitMessage : Message
    {
        public string Message { get; private set; }

        public string User { get; private set; }

        public QuitMessage(string line)
            : base(line)
        {
            string[] split = line.Split(' ');

            if (split.Length < 3)
                throw new FormatException("Not enough parts in message.");

            if (!split[1].Equals(ServerStringMessageType.Quit, StringComparison.OrdinalIgnoreCase))
                throw new FormatException("Not a QUIT message.");

            User = split[0].Remove(0, 1);
            Message = string.Join(" ", split.Skip(2)).Remove(0, 1);
        }

        public static new bool IsCorrectFormat(string line)
        {
            string[] split = line.Split(' ');

            return split.Length > 2 && split[1].Equals(ServerStringMessageType.Quit, StringComparison.OrdinalIgnoreCase);
        }
    }
}