using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc.Messages.Server
{
    /// <summary>
    /// Represents a QUIT message that a Client receives from the Server.
    /// </summary>
    public class QuitMessage : Message
    {
        /// <summary>
        /// Gets the quit message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the identifier of the User that quit.
        /// </summary>
        public string User { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="QuitMessage"/> class with the given line.
        /// </summary>
        /// <param name="line">The complete text of the Message.</param>
        public QuitMessage(string line)
            : base(line)
        {
            var split = line.Split(' ');

            if (split.Length < 3)
                throw new FormatException("Not enough parts in message.");

            if (!split[1].Equals(NamedMessageType.Quit, StringComparison.OrdinalIgnoreCase))
                throw new FormatException("Not a " + NamedMessageType.Quit + " message.");

            User = split[0].Remove(0, 1);
            Message = string.Join(" ", split.Skip(2)).Remove(0, 1);
        }

        /// <summary>
        /// Checks if the given line has the correct format for this type of Message.
        /// </summary>
        /// <param name="line">The complete text of the Message.</param>
        /// <returns>Whether it has the correct format for this type of Message.</returns>
        public static new bool IsCorrectFormat(string line)
        {
            var split = line.Split(' ');

            return split.Length > 2 && split[1].Equals(NamedMessageType.Quit, StringComparison.OrdinalIgnoreCase);
        }
    }
}