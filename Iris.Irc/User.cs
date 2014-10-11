using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc
{
    /// <summary>
    /// Represents an IRC User.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the complete identifier of the User. identifier = nickname!username@host
        /// </summary>
        public string Complete { get; set; }

        /// <summary>
        /// Gets whether the user has an ident.
        /// </summary>
        public bool HasIdent
        {
            get
            {
                return !Complete.Contains("!~") && Complete.Contains("!");
            }
        }

        /// <summary>
        /// Gets the Host of the User. nickname!username@host
        /// </summary>
        public string Host
        {
            get
            {
                return Complete.Remove(0, Complete.IndexOf('@') + 1);
            }
        }

        /// <summary>
        /// Gets or sets whether the User is identified with NickServ. Null if it wasn't requested yet.
        /// </summary>
        public bool? IsNickServIdentified { get; set; }

        /// <summary>
        /// Gets or sets whether the User is online currently.
        /// </summary>
        public bool IsOnline { get; set; }

        /// <summary>
        /// Gets or sets the modes that the user has.
        /// </summary>
        public UserModes Modes { get; set; }

        /// <summary>
        /// Gets or sets the nickname of the User. nickname!username@host
        /// </summary>
        public string Nickname
        {
            get
            {
                return Complete.Remove(Complete.IndexOf('!'));
            }
            set
            {
                Complete = value + Complete.Remove(0, Complete.IndexOf('!'));
            }
        }

        /// <summary>
        /// Gets or sets the DateTime at which the identification was requested from NickServ.
        /// </summary>
        public DateTime NickServIdentificationRequested { get; set; }

        /// <summary>
        /// Gets the username of the User. nickname!username@host
        /// </summary>
        public string Username
        {
            get
            {
                return Complete.Remove(Complete.IndexOf('@')).Remove(0, Complete.IndexOf('!') + 1);
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Iris.Irc.User"/> class with the given complete identifier.
        /// </summary>
        /// <param name="complete">The complete identifier. nickname!username@host</param>
        /// <param name="isOnline">Whether the User is online at the moment.</param>
        /// <param name="isNickServIdentified">Whether the User is identified with NickServ. Null if it wasn't requested yet.</param>
        /// <param name="userModes">Current UserMode of the User.</param>
        public User(string complete, bool isOnline = true, bool? isNickServIdentified = null, UserModes userModes = 0)
        {
            if (!(complete.Contains("!") && complete.Contains("@")))
                throw new FormatException("Complete identifier doesn't have the correct format of nickname!username@host.");

            Complete = complete;
            IsOnline = isOnline;
            IsNickServIdentified = isNickServIdentified;
            Modes = userModes;
        }

        /// <summary>
        /// Sends a authentication request for the current User to NickServ.
        /// </summary>
        /// <param name="client">The client used for sending the message.</param>
        public void RequestNickServAuthentication(Client client)
        {
            NickServIdentificationRequested = DateTime.Now;
            client.SendLine(Messages.Client.NamedMessageType.Private + " NickServ :ACC " + Nickname);
        }
    }
}