using Iris.Irc.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Irc
{
    /// <summary>
    /// Represents a one-to-one chat with another User.
    /// </summary>
    public class PrivateChat : IChat
    {
        /// <summary>
        /// Gets the Name of the chat. That is, where PRIVMSGs have to be send to.
        /// </summary>
        public string Name
        {
            get { return User.Nickname; }
        }

        /// <summary>
        /// Gets a List of messages that have been sent in the chat.
        /// </summary>
        public IList<Message> Messages { get; private set; }

        /// <summary>
        /// Gets the User that this private chat is with.
        /// </summary>
        public User User { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Iris.Irc.PrivateChat"/> class, with the given User.
        /// </summary>
        /// <param name="user">The User that the chat is with.</param>
        public PrivateChat(User user)
        {
            User = user;
            Messages = new List<Message>();
        }
    }
}