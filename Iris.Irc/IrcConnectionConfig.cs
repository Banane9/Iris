using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.Irc
{
    public class IrcConnectionConfig
    {
        #region User

        public string Nickname { get; set; }

        public string Password { get; set; }

        public IrcUserModes UserMode { get; set; }

        public string Username { get; set; }

        #endregion User

        public uint ReconnectionAttempts { get; set; }

        public IrcServer Server { get; set; }
    }
}