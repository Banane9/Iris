using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc.Messages.Server
{
    /// <summary>
    /// Represents a PART message that a Client receives from the Server.
    /// </summary>
    public class PartMessage : Message
    {
        /// <summary>
        /// The identifier of the channel that the User left.
        /// </summary>
        public string Channel { get; private set; }

        /// <summary>
        /// The identifier of the User that left the channel.
        /// </summary>
        public string User { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="PartMessage"/> class with the given line.
        /// </summary>
        /// <param name="line">The complete text of the Message.</param>
        public PartMessage(string line)
            : base(line)
        {
            var split = line.Split(' ');

            if (split.Length < 3)
                throw new FormatException("Not enough parts in message.");

            if (!split[1].Equals(NamedMessageType.Part, StringComparison.OrdinalIgnoreCase))
                throw new FormatException("Not a " + NamedMessageType.Part + " message.");

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

            return split.Length > 2 && split[1].Equals(NamedMessageType.Part, StringComparison.OrdinalIgnoreCase);
        }
    }
}