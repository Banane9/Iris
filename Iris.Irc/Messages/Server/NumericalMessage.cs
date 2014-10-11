using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc.Messages.Server
{
    public class NumericalMessage : Message
    {
        public NumericalMessageType NumericalType { get; private set; }

        public string Server { get; private set; }

        public NumericalMessage(string line)
            : base(line)
        {
            string[] split = line.Split(' ');

            if (split.Length < 2)
                throw new FormatException("Not enough parts in message.");

            NumericalMessageType numericalType;
            if (!Enum.TryParse<NumericalMessageType>(split[1], out numericalType))
                throw new FormatException("Not a valid number for a numerical message.");

            NumericalType = numericalType;
            Server = split[0].Remove(0, 1);
        }

        public static new bool IsCorrectFormat(string line)
        {
            string[] split = line.Split();

            if (split.Length < 2) return false;

            NumericalMessageType _;
            return Enum.TryParse<NumericalMessageType>(split[1], out _);
        }
    }
}