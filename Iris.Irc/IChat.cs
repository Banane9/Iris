using Iris.Irc.Messages.Server;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc
{
    public interface IChat
    {
        /// <summary>
        /// Gets the messages that have been sent in the chat.
        /// </summary>
        IList<Message> Messages { get; }

        /// <summary>
        /// Gets the Name of the chat. That is, where PRIVMSGs have to be send to.
        /// </summary>
        string Name { get; }
    }
}