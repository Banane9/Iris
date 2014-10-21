using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc.Messages
{
    /// <summary>
    /// Gets thrown when the line supposed to be parsed doesn't have the right message type.
    /// </summary>
    public sealed class MessageTypeException : Exception
    {
        public MessageTypeException()
        {
        }

        public MessageTypeException(string got, string expected)
            : base(makeMessage(got, expected))
        {
        }

        public MessageTypeException(string got, string expected, Exception inner)
            : base(makeMessage(got, expected), inner)
        {
        }

        private static string makeMessage(string got, string expected)
        {
            return string.Format("Got message of type [{0}], but expected [{1}].", got, expected);
        }
    }
}