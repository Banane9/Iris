using Iris.Irc.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Irc
{
    public sealed class Client
    {
        private IConnection connection;

        public Client(IConnection connection)
        {
            this.connection = connection;
        }

        public delegate void NoticeEventHandler(Client sender, Notice notice);

        public event NoticeEventHandler Notice;

        public delegate void NumericalMessageEventHandler(Client sender, NumericalMessage numericalMessage);

        public event NumericalMessageEventHandler NumericalMessage;

        public delegate void PrivateMessageEventHandler(Client sender, PrivateMessage privateMessage);

        public event PrivateMessageEventHandler PrivateMessage;

        public delegate void NickMessageEventHandler(Client sender, NickMessage nickMessage);

        public event NickMessageEventHandler NickMessage;

        public delegate void JoinMessageEventHandler(Client sender, JoinMessage joinMessage);

        public event JoinMessageEventHandler JoinMessage;

        public delegate void PartMessageEventHandler(Client sender, PartMessage partMessage);

        public event PartMessageEventHandler PartMessage;

        public delegate void QuitMessageEventHandler(Client sender, QuitMessage quitMessage);

        public event QuitMessageEventHandler QuitMessage;
    }
}