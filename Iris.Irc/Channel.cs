using Iris.Irc.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc
{
    /// <summary>
    /// Represents a one-to-many chat (channel).
    /// </summary>
    public class Channel : IChat
    {
        /// <summary>
        /// The symbol that global Channels are prefixed with.
        /// </summary>
        public const string GlobalChannelPrefix = "#";

        /// <summary>
        /// The symbol that local Channels are prefixed with.
        /// </summary>
        public const string LocalChannelPrefix = "&";

        /// <summary>
        /// Gets whether the channel is network wide ('#'), or only on the server currently connected to ('&').
        /// </summary>
        public bool IsGlobal
        {
            get
            {
                return Name.StartsWith(GlobalChannelPrefix, StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// Gets whether the channel is only on the server currently connected to ('&'), or network wide ('#').
        /// </summary>
        public bool IsLocal
        {
            get
            {
                return Name.StartsWith(LocalChannelPrefix, StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// Gets a List of messages that have been sent in the chat.
        /// </summary>
        public IList<Message> Messages { get; private set; }

        /// <summary>
        /// Gets or sets the modes of the channel.
        /// </summary>
        public Flags Modes { get; set; }

        /// <summary>
        /// Gets the Name of the chat. That is, where PRIVMSGs have to be send to.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a List of Users in the Channel.
        /// </summary>
        public IList<User> Users { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Channel"/> class with the given channel name.
        /// </summary>
        /// <param name="name">The channel's name.</param>
        public Channel(string name, Flags channelModes = 0)
        {
            if (!IsChannel(name))
                throw new FormatException("Channel must start with '" + LocalChannelPrefix + "' (local) or '" + GlobalChannelPrefix + "' (network wide).");

            Name = name;
            Modes = channelModes;

            Messages = new List<Message>();
            Users = new List<User>();
        }

        /// <summary>
        /// Checks whether the given identifier is a channel or not.
        /// </summary>
        /// <param name="identifier">The identifier to check.</param>
        /// <returns>Whether it's a channel or not.</returns>
        public static bool IsChannel(string identifier)
        {
            return identifier.StartsWith(LocalChannelPrefix, StringComparison.OrdinalIgnoreCase) || identifier.StartsWith(GlobalChannelPrefix, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Enumeration of the different flags that a channel can have.
        /// </summary>
        [Flags]
        public enum Flags
        {
            /// <summary>
            /// No channel modes.
            /// </summary>
            None = 0,

            /// <summary>
            /// Give or take Channel operator privileges.
            /// </summary>
            o = 1,

            /// <summary>
            /// Channel is private.
            /// </summary>
            p = 1 << 1,

            /// <summary>
            /// Channel is secret.
            /// </summary>
            s = 1 << 2,

            /// <summary>
            /// Channel is invite only.
            /// </summary>
            i = 1 << 3,

            /// <summary>
            /// Topic only setable by Channel operators.
            /// </summary>
            t = 1 << 4,

            /// <summary>
            /// No messages to Channel from clients not in it.
            /// </summary>
            n = 1 << 5,

            /// <summary>
            /// Channel is moderated.
            /// </summary>
            m = 1 << 6,

            /// <summary>
            /// Set the user limit of the Channel.
            /// </summary>
            l = 1 << 7
        }
    }
}