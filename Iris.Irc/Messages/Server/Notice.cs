using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc.Messages.Server
{
    /// <summary>
    /// Represents a NOTICE that a Client receives from the Server.
    /// </summary>
    public class Notice : Message
    {
        /// <summary>
        /// Gets the actual content of the Notice.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the identifier of the Recipient of the Notice.
        /// </summary>
        public string Recipient { get; private set; }

        /// <summary>
        /// Gets the identifier of the Sender of the Notice.
        /// </summary>
        public string User { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Notice"/> class with the given line.
        /// </summary>
        /// <param name="line">The complete text of the Message.</param>
        public Notice(string line)
            : base(line)
        {
            string[] split = line.Split(' ');

            if (split.Length < 4)
                throw new FormatException("Not enough parts in message.");

            if (!split[1].Equals(NamedMessageType.Notice, StringComparison.OrdinalIgnoreCase))
                throw new FormatException("Not a " + NamedMessageType.Notice + ".");

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
            string[] split = line.Split(' ');

            return split.Length > 3 && split[1].Equals(NamedMessageType.Notice, StringComparison.OrdinalIgnoreCase);
        }
    }
}