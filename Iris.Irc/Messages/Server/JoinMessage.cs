using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc.Messages.Server
{
    /// <summary>
    /// Represents a JOIN message that a Client receives from the Server.
    /// </summary>
    public class JoinMessage : Message
    {
        /// <summary>
        /// Gets the identifier of the channel that the User joined.
        /// </summary>
        public string Channel { get; private set; }

        /// <summary>
        /// Gets the identifier of the User that joined the channel.
        /// </summary>
        public string User { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="JoinMessage"/> class with the given line.
        /// </summary>
        /// <param name="line">The complete text of the Message.</param>
        public JoinMessage(string line)
            : base(line)
        {
            var split = line.Split(' ');

            if (split.Length < 3)
                throw new FormatException("Not enough parts in message.");

            if (!split[1].Equals(NamedMessageType.Join, StringComparison.OrdinalIgnoreCase))
                throw new FormatException("Not a " + NamedMessageType.Join + " message.");

            User = split[0].Remove(0, 1);
            Channel = split[2];
        }

        /// <summary>
        /// Checks if the given line has the correct format for this type of Message.
        /// </summary>
        /// <param name="line">The complete text of the Message.</param>
        /// <returns>Whether it has the correct format for this type of Message.</returns>
        public static new bool IsCorrectFormat(string line)
        {
            var split = line.Split(' ');

            return split.Length > 2 && split[1].ToUpper() == NamedMessageType.Join;
        }
    }
}