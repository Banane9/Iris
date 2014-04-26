using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Irc.Messages
{
    public class Message
    {
        public string Line { get; private set; }

        public virtual MessageTypes Type
        {
            get { return MessageTypes.None; }
        }

        public override string ToString()
        {
            return Line;
        }

        public Message(string line)
        {
            Line = line;
        }
    }
}