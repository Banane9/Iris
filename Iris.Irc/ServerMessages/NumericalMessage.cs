using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.Irc.ServerMessages
{
    public class NumericalMessage : Message
    {
        public NumericalMessageTypes NumericalType { get; private set; }

        public string Server { get; private set; }

        public override MessageTypes Type
        {
            get { return MessageTypes.Numerical; }
        }

        public static new bool IsCorrectFormat(string line)
        {
            string[] split = line.Split();

            if (split.Length < 2) return false;

            NumericalMessageTypes _;
            return Enum.TryParse<NumericalMessageTypes>(split[1], out _);
        }

        public NumericalMessage(string line)
            : base(line)
        {
            string[] split = line.Split(' ');

            if (split.Length < 2)
                throw new FormatException("Not enough parts in message.");

            NumericalMessageTypes numericalType;
            if (!Enum.TryParse<NumericalMessageTypes>(split[1], out numericalType))
                throw new FormatException("Not a valid number for a numerical message.");

            NumericalType = numericalType;
            Server = split[0].Remove(0, 1);
        }
    }
}