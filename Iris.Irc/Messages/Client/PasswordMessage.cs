using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc.Messages.Client
{
    /// <summary>
    /// Represents a PASS message that a Client sends to the Server.
    /// </summary>
    public sealed class PasswordMessage : Message
    {
        /// <summary>
        /// Gets the "connection password".
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Message"/> class with the given line.
        /// </summary>
        /// <param name="line">The complete text of the Message.</param>
        public PasswordMessage(string line)
            : base(line)
        {
            var split = line.Split(' ');

            if (split.Length < 2)
                throw new MessageFormatException("Not enough parts in message.");

            if (!split[0].Equals(NamedMessageType.Password, StringComparison.OrdinalIgnoreCase))
                throw new MessageTypeException(split[0], NamedMessageType.Password);

            Password = split[1];
        }
    }
}