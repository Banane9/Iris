using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.Irc
{
    /// <summary>
    /// Represents an IRC User.
    /// </summary>
    public class User
    {
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
        /// Gets or sets whether the User is online currently.
        /// </summary>
        public bool IsOnline { get; set; }

        /// <summary>
        /// Gets or sets wheter the User is identified with NickServ.
        /// </summary>
        public bool IsNickServIdentified { get; set; }

        /// <summary>
        /// Gets or sets the modes that the user has.
        /// </summary>
        public UserModes Modes { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Iris.Irc.User"/> class with the given complete identifier.
        /// </summary>
        /// <param name="complete">The complete identifier. nickname!username@host</param>
        public User(string complete, bool isOnline = true, bool isNickServIdentified = false, UserModes userModes = 0)
        {
            if (!(complete.Contains("!") && complete.Contains("@")))
                throw new FormatException("Complete identifier doesn't have the correct format of nickname!username@host.");

            Complete = complete;
            IsOnline = isOnline;
            IsNickServIdentified = isNickServIdentified;
            Modes = userModes;
        }
    }
}