using Iris.Irc.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Iris.Irc
{
    public sealed class Client
    {
        private IConnection connection;
        private bool running;

        public ConnectionConfig Config { get; set; }

        public Client(IConnection connection, ConnectionConfig config)
        {
            this.connection = connection;
            Config = config;
        }

        public void Run(Action delay)
        {
            running = connection.Open();

            if (!running) return;

            connection.SendLine(ClientStringMessageTypes.Password + " " + Config.Password);
            connection.SendLine(ClientStringMessageTypes.Nickname + " " + Config.Nickname);
            connection.SendLine(ClientStringMessageTypes.User + " " + Config.Nickname + " " + (int)Config.UserMode + " * :" + Config.Username);

            string line;
            while (running)
            {
                if (connection.TryGetNextLine(out line))
                {
                    Line(this, line);
                }

                if (!connection.HasMoreLines)
                    delay();
            }

            connection.SendLine("QUIT"); //Add configurable message
            connection.Close();
        }

        public void Stop()
        {
            running = false;
        }

        public delegate void LineEventHandler(Client sender, string line);

        public event LineEventHandler Line;

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