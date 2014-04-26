using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.Irc.Messages
{
    public class NumericalMessage : Message
    {
        public NumericalMessageTypes NumericalType { get; private set; }

        public override MessageTypes Type
        {
            get { return MessageTypes.Numerical; }
        }

        public NumericalMessage(string line)
            : base(line)
        {
            string[] split = line.Split(' ');

            if (split.Length < 2)
                throw new FormatException("Not enough parts in message.");

            int number;
            if (!int.TryParse(split[1], out number))
                throw new FormatException("Not a numerical message.");

            try
            {
                NumericalType = (NumericalMessageTypes)number;
            }
            catch (Exception ex)
            {
                throw new FormatException("Not a valid number for a numerical message.", ex);
            }
        }
    }
}