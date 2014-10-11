using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc.Messages.Server
{
    /// <summary>
    /// Represents a numerical message that a Client receives from the Server.
    /// </summary>
    public class NumericalMessage : Message
    {
        /// <summary>
        /// Gets the numerical type of the Message.
        /// </summary>
        public NumericalMessageType NumericalType { get; private set; }

        /// <summary>
        /// Gets the identifier/address of the Server sending the Message.
        /// </summary>
        public string Server { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="NumericalMessage"/> class with the given line.
        /// </summary>
        /// <param name="line">The complete text of the Message.</param>
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

        /// <summary>
        /// Checks if the given line has the correct format for this type of Message.
        /// </summary>
        /// <param name="line">The complete text of the Message.</param>
        /// <returns>Whether it has the correct format for this type of Message.</returns>
        public static new bool IsCorrectFormat(string line)
        {
            var split = line.Split();

            if (split.Length < 2) return false;

            NumericalMessageType _;
            return Enum.TryParse<NumericalMessageType>(split[1], out _);
        }
    }
}