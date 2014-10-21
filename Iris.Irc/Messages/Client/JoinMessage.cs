using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc.Messages.Client
{
    /// <summary>
    /// Represents a JOIN message that a Client sends to the Server.
    /// </summary>
    public sealed class JoinMessage : Message
    {
        /// <summary>
        /// Gets the names of the channels that the Client wants to join.
        /// </summary>
        public IEnumerable<string> Channels { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Message"/> class with the given line.
        /// </summary>
        /// <param name="line">The complete text of the Message.</param>
        public JoinMessage(string line)
            : base(line)
        {
            var split = line.Split(' ');

            if (split.Length < 2)
                throw new FormatException("Not enough parts in message.");

            if (!split[0].Equals(NamedMessageType.Join, StringComparison.OrdinalIgnoreCase))
                throw new FormatException("Not a " + NamedMessageType.Join + " message.");
        }

        /// <summary>
        /// Checks if the given line has the correct format for this type of Message.
        /// </summary>
        /// <param name="line">The complete text of the Message.</param>
        /// <returns>Whether it has the correct format for this type of Message.</returns>
        public static new bool IsCorrectFormat(string line)
        {
            var split = line.Split(' ');

            return split.Length > 1 && split[0].Equals(NamedMessageType.Nickname, StringComparison.OrdinalIgnoreCase);
        }
    }
}