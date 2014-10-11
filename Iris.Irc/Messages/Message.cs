using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc.Messages
{
    /// <summary>
    /// Abstrat base class for all Messages.
    /// </summary>
    public abstract class Message
    {
        /// <summary>
        /// Gets the complete text of the Message.
        /// </summary>
        public string Line { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Message"/> class with the given line.
        /// </summary>
        /// <param name="line">The complete text of the Message.</param>
        protected Message(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                throw new FormatException("Line must contain something other than whitespace and not be null.");

            Line = line;
        }

        /// <summary>
        /// Checks if the given line has the correct format for this type of Message.
        /// </summary>
        /// <param name="line">The complete text of the Message.</param>
        /// <returns>Whether it has the correct format for this type of Message.</returns>
        public static bool IsCorrectFormat(string line)
        {
            return !string.IsNullOrWhiteSpace(line);
        }

        /// <summary>
        /// Returns the Line. No real conversion happens.
        /// </summary>
        /// <returns>The Line.</returns>
        public override string ToString()
        {
            return Line;
        }
    }
}