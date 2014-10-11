using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc.Messages.Server
{
    /// <summary>
    /// Represents a NICK message that a Client receives from the Server.
    /// </summary>
    public class NickMessage : Message
    {
        public string NewNick { get; private set; }

        public string OldNick { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="NickMessage"/> class with the given line.
        /// </summary>
        /// <param name="line">The complete text of the Message.</param>
        public NickMessage(string line)
            : base(line)
        {
            var split = line.Split(' ');

            if (split.Length < 3)
                throw new FormatException("Not enough parts in message.");

            if (!split[1].Equals(NamedMessageType.Nickname, StringComparison.OrdinalIgnoreCase))
                throw new FormatException("Not a " + NamedMessageType.Nickname + " message.");

            OldNick = split[0].Remove(0, 1);
            NewNick = split[2];
        }

        /// <summary>
        /// Checks if the given line has the correct format for this type of Message.
        /// </summary>
        /// <param name="line">The complete text of the Message.</param>
        /// <returns>Whether it has the correct format for this type of Message.</returns>
        public static new bool IsCorrectFormat(string line)
        {
            var split = line.Split(' ');

            return split.Length > 2 && split[1].Equals(NamedMessageType.Nickname, StringComparison.OrdinalIgnoreCase);
        }
    }
}