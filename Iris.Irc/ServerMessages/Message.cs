using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Irc.ServerMessages
{
    public class Message
    {
        public string Line { get; private set; }

        public virtual MessageTypes Type
        {
            get { return MessageTypes.None; }
        }

        public static bool IsCorrectFormat(string line)
        {
            return !string.IsNullOrWhiteSpace(line);
        }

        public override string ToString()
        {
            return Line;
        }

        public Message(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                throw new FormatException("Line must contain something other than whitespace and not be null.");

            Line = line;
        }
    }
}