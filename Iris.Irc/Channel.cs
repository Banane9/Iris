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
        public const string GlobalChannelPrefix = "#";

        public const string LocalChannelPrefix = "&";

        /// <summary>
        /// Gets or sets the modes of the channel.
        /// </summary>
        public ChannelModes ChannelModes { get; set; }

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
        /// Gets the Name of the chat. That is, where PRIVMSGs have to be send to.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a List of Users in the Channel.
        /// </summary>
        public IList<User> Users { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Iris.Irc.Channel"/> class with the given channel name.
        /// </summary>
        /// <param name="name">The channel's name.</param>
        public Channel(string name, ChannelModes channelModes = 0)
        {
            if (!IsChannel(name))
                throw new FormatException("Channel must start with '" + LocalChannelPrefix + "' (local) or '" + GlobalChannelPrefix + "' (network wide).");

            Name = name;
            ChannelModes = channelModes;

            Messages = new List<Message>();
            Users = new List<User>();
        }

        public static bool IsChannel(string identifier)
        {
            return identifier.StartsWith(LocalChannelPrefix, StringComparison.OrdinalIgnoreCase) || identifier.StartsWith(GlobalChannelPrefix, StringComparison.OrdinalIgnoreCase);
        }
    }
}