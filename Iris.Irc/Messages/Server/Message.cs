using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc.Messages.Server
{
    public abstract class Message
    {
        public string Line { get; private set; }

        protected Message(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                throw new FormatException("Line must contain something other than whitespace and not be null.");

            Line = line;
        }

        public static bool IsCorrectFormat(string line)
        {
            return !string.IsNullOrWhiteSpace(line);
        }

        public override string ToString()
        {
            return Line;
        }
    }
}