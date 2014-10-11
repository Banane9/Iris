using Iris.Irc.Messages.Server;
using System;
using System.Collections.Generic;
using System.Linq;

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

            connection.SendLine(ClientStringMessageType.Password + " " + Config.Password);
            connection.SendLine(ClientStringMessageType.Nickname + " " + Config.Nickname);
            connection.SendLine(ClientStringMessageType.User + " " + Config.Nickname + " " + (int)Config.UserMode + " * :" + Config.Username);

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
        /// Sends a line through the connection.
        /// </summary>
        /// <param name="line">The line to be send.</param>
        public void SendLine(string line)
        {
            connection.SendLine(line);
        }

        /// <summary>
        /// Stops the client.
        /// </summary>
        public void Stop()
        {
            running = false;
        }

        /// <summary>
        /// Fires the JoinMessage event.
        /// </summary>
        /// <param name="joinMessage">The join message.</param>
        protected void onJoinMessage(JoinMessage joinMessage)
        {
            if (JoinMessage != null)
                JoinMessage(this, joinMessage);
        }

        /// <summary>
        /// Fires the Message event.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void onMessage(Message message)
        {
            if (Message != null)
                Message(this, message);
        }

        /// <summary>
        /// Fires the NickMessage event.
        /// </summary>
        /// <param name="nickMessage">The nick message.</param>
        protected void onNickMessage(NickMessage nickMessage)
        {
            if (NickMessage != null)
                NickMessage(this, nickMessage);
        }

        /// <summary>
        /// Fires the Notice event.
        /// </summary>
        /// <param name="notice">The notice.</param>
        protected void onNotice(Notice notice)
        {
            if (Notice != null)
                Notice(this, notice);
        }

        /// <summary>
        /// Fires the NumericalMessage event.
        /// </summary>
        /// <param name="numericalMessage">The numerical message.</param>
        protected void onNumericalMessage(NumericalMessage numericalMessage)
        {
            if (NumericalMessage != null)
                NumericalMessage(this, numericalMessage);
        }

        /// <summary>
        /// Fires the PartMessage event.
        /// </summary>
        /// <param name="partMessage">The part message.</param>
        protected void onPartMessage(PartMessage partMessage)
        {
            if (PartMessage != null)
                PartMessage(this, partMessage);
        }

        /// <summary>
        /// Fires the PrivateMessage event.
        /// </summary>
        /// <param name="privateMessage">The private message.</param>
        protected void onPrivateMessage(PrivateMessage privateMessage)
        {
            if (PrivateMessage != null)
                PrivateMessage(this, privateMessage);
        }

        /// <summary>
        /// Fires the QuitMessage event.
        /// </summary>
        /// <param name="quitMessage">The quit message.</param>
        protected void onQuitMessage(QuitMessage quitMessage)
        {
            if (QuitMessage != null)
                QuitMessage(this, quitMessage);
        }

        /// <summary>
        /// Dispatches the right events for the line.
        /// </summary>
        /// <param name="line">The line.</param>
        private void dispatchEventsFor(string line)
        {
            if (Messages.Server.Message.IsCorrectFormat(line))
            {
                //Tryed to order them by how often they appear.
                if (Messages.Server.PrivateMessage.IsCorrectFormat(line))
                {
                    var privateMessage = new Messages.Server.PrivateMessage(line);
                    onPrivateMessage(privateMessage);
                    onMessage(privateMessage);
                }
                else if (Messages.Server.NumericalMessage.IsCorrectFormat(line))
                {
                    var numericalMessage = new Messages.Server.NumericalMessage(line);
                    onNumericalMessage(numericalMessage);
                    onMessage(numericalMessage);
                }
                else if (Messages.Server.Notice.IsCorrectFormat(line))
                {
                    var notice = new Messages.Server.Notice(line);
                    onNotice(notice);
                    onMessage(notice);
                }
                else if (Messages.Server.NickMessage.IsCorrectFormat(line))
                {
                    var nickMessage = new Messages.Server.NickMessage(line);
                    onNickMessage(nickMessage);
                    onMessage(nickMessage);
                }
                else if (Messages.Server.JoinMessage.IsCorrectFormat(line))
                {
                    var joinMessage = new Messages.Server.JoinMessage(line);
                    onJoinMessage(joinMessage);
                    onMessage(joinMessage);
                }
                else if (Messages.Server.PartMessage.IsCorrectFormat(line))
                {
                    var partMessage = new Messages.Server.PartMessage(line);
                    onPartMessage(partMessage);
                    onMessage(partMessage);
                }
                else if (Messages.Server.QuitMessage.IsCorrectFormat(line))
                {
                    var quitMessage = new Messages.Server.QuitMessage(line);
                    onQuitMessage(quitMessage);
                    onMessage(quitMessage);
                }
            }
        }

        /// <summary>
        /// Event Handler for incoming Messages.
        /// </summary>
        /// <typeparam name="TMessage">The type of message.</typeparam>
        /// <param name="sender">The <see cref="Client"/> sending the Message.</param>
        /// <param name="message">The incoming message.</param>
        public delegate void MessageEventHandler<TMessage>(Client sender, TMessage message) where TMessage : Message;

        /// <summary>
        /// Fires for JOINs.
        /// </summary>
        public event MessageEventHandler<JoinMessage> JoinMessage;

        /// <summary>
        /// Fires for any message.
        /// </summary>
        public event MessageEventHandler<Message> Message;

        /// <summary>
        /// Fires for NICKs.
        /// </summary>
        public event MessageEventHandler<NickMessage> NickMessage;

        /// <summary>
        /// Fires for NOTICEs.
        /// </summary>
        public event MessageEventHandler<Notice> Notice;

        /// <summary>
        /// Fires for numerical messages.
        /// </summary>
        public event MessageEventHandler<NumericalMessage> NumericalMessage;

        /// <summary>
        /// Fires for PARTs.
        /// </summary>
        public event MessageEventHandler<PartMessage> PartMessage;

        /// <summary>
        /// Fires for PRIVMSGs.
        /// </summary>
        public event MessageEventHandler<PrivateMessage> PrivateMessage;

        /// <summary>
        /// Fires for QUITs.
        /// </summary>
        public event MessageEventHandler<QuitMessage> QuitMessage;
    }
}