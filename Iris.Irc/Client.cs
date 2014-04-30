using Iris.Irc.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Iris.Irc
{
    public class Client
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
                    dispatchEventsFor(line);
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

        private void dispatchEventsFor(string line)
        {
            if (ServerMessages.Message.IsCorrectFormat(line))
            {
                onMessage(new ServerMessages.Message(line));

                //Tryed to order them by how often they appear.
                if (ServerMessages.PrivateMessage.IsCorrectFormat(line))
                {
                    onPrivateMessage(new ServerMessages.PrivateMessage(line));
                }
                else if (ServerMessages.NumericalMessage.IsCorrectFormat(line))
                {
                    onNumericalMessage(new ServerMessages.NumericalMessage(line));
                }
                else if (ServerMessages.Notice.IsCorrectFormat(line))
                {
                    onNotice(new ServerMessages.Notice(line));
                }
                else if (ServerMessages.NickMessage.IsCorrectFormat(line))
                {
                    onNickMessage(new ServerMessages.NickMessage(line));
                }
                else if (ServerMessages.JoinMessage.IsCorrectFormat(line))
                {
                    onJoinMessage(new ServerMessages.JoinMessage(line));
                }
                else if (ServerMessages.PartMessage.IsCorrectFormat(line))
                {
                    onPartMessage(new ServerMessages.PartMessage(line));
                }
                else if (ServerMessages.QuitMessage.IsCorrectFormat(line))
                {
                    onQuitMessage(new ServerMessages.QuitMessage(line));
                }
            }
        }

        public delegate void MessageEventHandler(Client sender, Message message);

        public event MessageEventHandler Message;

        protected void onMessage(Message message)
        {
            if (Message != null)
                Message(this, message);
        }

        public delegate void NoticeEventHandler(Client sender, Notice notice);

        public event NoticeEventHandler Notice;

        protected void onNotice(Notice notice)
        {
            if (Notice != null)
                Notice(this, notice);
        }

        public delegate void NumericalMessageEventHandler(Client sender, NumericalMessage numericalMessage);

        public event NumericalMessageEventHandler NumericalMessage;

        protected void onNumericalMessage(NumericalMessage numericalMessage)
        {
            if (NumericalMessage != null)
                NumericalMessage(this, numericalMessage);
        }

        public delegate void PrivateMessageEventHandler(Client sender, PrivateMessage privateMessage);

        public event PrivateMessageEventHandler PrivateMessage;

        protected void onPrivateMessage(PrivateMessage privateMessage)
        {
            if (PrivateMessage != null)
                PrivateMessage(this, privateMessage);
        }

        public delegate void NickMessageEventHandler(Client sender, NickMessage nickMessage);

        public event NickMessageEventHandler NickMessage;

        protected void onNickMessage(NickMessage nickMessage)
        {
            if (NickMessage != null)
                NickMessage(this, nickMessage);
        }

        public delegate void JoinMessageEventHandler(Client sender, JoinMessage joinMessage);

        public event JoinMessageEventHandler JoinMessage;

        protected void onJoinMessage(JoinMessage joinMessage)
        {
            if (JoinMessage != null)
                JoinMessage(this, joinMessage);
        }

        public delegate void PartMessageEventHandler(Client sender, PartMessage partMessage);

        public event PartMessageEventHandler PartMessage;

        protected void onPartMessage(PartMessage partMessage)
        {
            if (PartMessage != null)
                PartMessage(this, partMessage);
        }

        public delegate void QuitMessageEventHandler(Client sender, QuitMessage quitMessage);

        public event QuitMessageEventHandler QuitMessage;

        protected void onQuitMessage(QuitMessage quitMessage)
        {
            if (QuitMessage != null)
                QuitMessage(this, quitMessage);
        }
    }
}