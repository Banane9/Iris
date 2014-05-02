using Iris.Irc.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Iris.Irc
{
    /// <summary>
    /// The client that takes an <see cref="Iris.Irc.IConnection"/> and splits up the incoming messages.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Connection to the server.
        /// </summary>
        private IConnection connection;

        /// <summary>
        /// Whether the client is supposed to be running or not.
        /// </summary>
        private bool running;

        public ClientConfig Config { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Iris.Irc.Client"/> class.
        /// </summary>
        /// <param name="connection">Connection to the IRC Server.</param>
        /// <param name="config"></param>
        public Client(IConnection connection, ClientConfig config)
        {
            this.connection = connection;
            Config = config;
        }

        /// <summary>
        /// Run the client. Should be started in its own <see cref="System.Threading.Thread"/>.
        /// Delay function has to be passed in because the Portable Class Library doesn't support the Thread class.
        /// </summary>
        /// <param name="delay">Function that gets called to let the Thread sleep when there's no new lines to process.</param>
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

        /// <summary>
        /// Stops the client.
        /// </summary>
        public void Stop()
        {
            running = false;
        }

        /// <summary>
        /// Dispatches the right events for the line.
        /// </summary>
        /// <param name="line">The line.</param>
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

        /// <summary>
        /// Fires for any message.
        /// </summary>
        public event MessageEventHandler Message;

        /// <summary>
        /// Fires the Message event.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void onMessage(Message message)
        {
            if (Message != null)
                Message(this, message);
        }

        public delegate void NoticeEventHandler(Client sender, Notice notice);

        /// <summary>
        /// Fires for NOTICEs.
        /// </summary>
        public event NoticeEventHandler Notice;

        /// <summary>
        /// Fires the Notice event.
        /// </summary>
        /// <param name="notice">The notice.</param>
        protected void onNotice(Notice notice)
        {
            if (Notice != null)
                Notice(this, notice);
        }

        public delegate void NumericalMessageEventHandler(Client sender, NumericalMessage numericalMessage);

        /// <summary>
        /// Fires for numerical messages.
        /// </summary>
        public event NumericalMessageEventHandler NumericalMessage;

        /// <summary>
        /// Fires the NumericalMessage event.
        /// </summary>
        /// <param name="numericalMessage">The numerical message.</param>
        protected void onNumericalMessage(NumericalMessage numericalMessage)
        {
            if (NumericalMessage != null)
                NumericalMessage(this, numericalMessage);
        }

        public delegate void PrivateMessageEventHandler(Client sender, PrivateMessage privateMessage);

        /// <summary>
        /// Fires for PRIVMSGs.
        /// </summary>
        public event PrivateMessageEventHandler PrivateMessage;

        /// <summary>
        /// Fires the PrivateMessage event.
        /// </summary>
        /// <param name="privateMessage">The private message.</param>
        protected void onPrivateMessage(PrivateMessage privateMessage)
        {
            if (PrivateMessage != null)
                PrivateMessage(this, privateMessage);
        }

        public delegate void NickMessageEventHandler(Client sender, NickMessage nickMessage);

        /// <summary>
        /// Fires for NICKs.
        /// </summary>
        public event NickMessageEventHandler NickMessage;

        /// <summary>
        /// Fires the NickMessage event.
        /// </summary>
        /// <param name="nickMessage">The nick message.</param>
        protected void onNickMessage(NickMessage nickMessage)
        {
            if (NickMessage != null)
                NickMessage(this, nickMessage);
        }

        public delegate void JoinMessageEventHandler(Client sender, JoinMessage joinMessage);

        /// <summary>
        /// Fires for JOINs.
        /// </summary>
        public event JoinMessageEventHandler JoinMessage;

        /// <summary>
        /// Fires the JoinMessage event.
        /// </summary>
        /// <param name="joinMessage">The join message.</param>
        protected void onJoinMessage(JoinMessage joinMessage)
        {
            if (JoinMessage != null)
                JoinMessage(this, joinMessage);
        }

        public delegate void PartMessageEventHandler(Client sender, PartMessage partMessage);

        /// <summary>
        /// Fires for PARTs.
        /// </summary>
        public event PartMessageEventHandler PartMessage;

        /// <summary>
        /// Fires the PartMessage event.
        /// </summary>
        /// <param name="partMessage">The part message.</param>
        protected void onPartMessage(PartMessage partMessage)
        {
            if (PartMessage != null)
                PartMessage(this, partMessage);
        }

        public delegate void QuitMessageEventHandler(Client sender, QuitMessage quitMessage);

        /// <summary>
        /// Fires for QUITs.
        /// </summary>
        public event QuitMessageEventHandler QuitMessage;

        /// <summary>
        /// Fires the QuitMessage event.
        /// </summary>
        /// <param name="quitMessage">The quit message.</param>
        protected void onQuitMessage(QuitMessage quitMessage)
        {
            if (QuitMessage != null)
                QuitMessage(this, quitMessage);
        }
    }
}