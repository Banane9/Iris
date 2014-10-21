using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc.Messages.Client
{
    /// <summary>
    /// Represents a NICK message that a Client sends to the Server.
    /// </summary>
    public sealed class NickMessage : Message
    {
        /// <summary>
        /// Gets the new nickname.
        /// </summary>
        public string Nick { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="NickMessage"/> class with the given line.
        /// </summary>
        /// <param name="line">The complete text of the message.</param>
        public NickMessage(string line)
            : base(line)
        {
            var split = line.Split(' ');

            if (split.Length < 2)
                throw new FormatException("Not enough parts in message.");

            if (!split[0].Equals(NamedMessageType.Nickname, StringComparison.OrdinalIgnoreCase))
                throw new FormatException("Not a " + NamedMessageType.Nickname + " message.");

            Nick = split[1];
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