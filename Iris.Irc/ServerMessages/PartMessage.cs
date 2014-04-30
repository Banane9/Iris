using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.Irc.ServerMessages
{
    public class PartMessage : Message
    {
        public string User { get; private set; }

        public string Channel { get; private set; }

        public override MessageTypes Type
        {
            get { return MessageTypes.String; }
        }

        public static new bool IsCorrectFormat(string line)
        {
            string[] split = line.Split(' ');

            return split.Length > 3 && split[1].ToUpper() == ServerStringMessageTypes.Part;
        }

        public PartMessage(string line)
            : base(line)
        {
            string[] split = line.Split(' ');

            if (split.Length < 3)
                throw new FormatException("Not enough parts in message.");

            if (split[1].ToUpper() != ServerStringMessageTypes.Part)
                throw new FormatException("Not a PART message.");

            User = split[0].Remove(0, 1);
            Channel = split[2];
        }
    }
}