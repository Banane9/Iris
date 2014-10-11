using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc.Messages.Server
{
    /// <summary>
    /// Represents a PRIVMSG message that a Client receives from the Server.
    /// </summary>
    public class PrivateMessage : Message
    {
        /// <summary>
        /// Gets the actual content of the message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the identifier of the Recipient of the message.
        /// </summary>
        public string Recipient { get; private set; }

        /// <summary>
        /// Gets the identifier of the Sender of the message.
        /// </summary>
        public string User { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="PrivateMessage"/> class with the given line.
        /// </summary>
        /// <param name="line">The complete text of the Message.</param>
        public PrivateMessage(string line)
            : base(line)
        {
            var split = line.Split(' ');

            if (split.Length < 4)
                throw new FormatException("Not enough parts in message.");

            if (!split[1].Equals(NamedMessageType.Private, StringComparison.OrdinalIgnoreCase))
                throw new FormatException("Not a " + NamedMessageType.Private + " message.");

            User = split[0].Remove(0, 1);
            Recipient = split[2];
            Message = string.Join(" ", split.Skip(3)).Remove(0, 1);
        }

        /// <summary>
        /// Checks if the given line has the correct format for this type of Message.
        /// </summary>
        /// <param name="line">The complete text of the Message.</param>
        /// <returns>Whether it has the correct format for this type of Message.</returns>
        public static new bool IsCorrectFormat(string line)
        {
            var split = line.Split(' ');

            return split.Length > 3 && split[1].Equals(NamedMessageType.Private, StringComparison.OrdinalIgnoreCase);
        }
    }
}