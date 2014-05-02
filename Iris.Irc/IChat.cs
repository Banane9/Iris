using Iris.Irc.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.Irc
{
    public interface IChat
    {
        /// <summary>
        /// Gets the Name of the chat. That is, where PRIVMSGs have to be send to.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the messages that have been sent in the chat.
        /// </summary>
        IList<Message> Messages { get; }
    }
}