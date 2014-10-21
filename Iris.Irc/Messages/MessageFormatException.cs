using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc.Messages
{
    /// <summary>
    /// Gets thrown when the line supposed to be parsed doesn't have the right format for the message type.
    /// </summary>
    public class MessageFormatException : Exception
    {
        public MessageFormatException()
        {
        }

        public MessageFormatException(string message)
            : base(message)
        {
        }

        public MessageFormatException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}