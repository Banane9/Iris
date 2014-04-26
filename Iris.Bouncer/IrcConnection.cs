using Iris.Irc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Iris.Bouncer
{
    public sealed class IrcConnection : IConnection
    {
        private CancellationTokenSource cancellationTokenSource;
        private StreamReader reader;
        private Stream stream;
        private StreamWriter writer;

        public IrcConnectionConfig Config { get; private set; }

        public IrcConnection(IrcConnectionConfig config)
        {
            Config = config;
        }

        ~IrcConnection()
        {
            reader.Dispose();
            writer.Dispose();
            stream.Dispose();
        }

        public event EventHandler ConnectionClosed;

        public event EventHandler ConnectionClosing;

        public event NewLineEventHandler NewLine;

        public void SendLine(string line)
        {
            writer.WriteLine(line);
            writer.FlushAsync();
        }

        public void Open()
        {
            cancellationTokenSource = new CancellationTokenSource();

            Task.Factory.StartNew(() =>
                {
                    reconnect();

                    while (!cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        try
                        {
                            string line = reader.ReadLine();

                            if (!String.IsNullOrEmpty(line))
                            {
                                OnNewLine(line);
                            }
                        }
                        catch (IOException)
                        {
                            bool reconnected = reconnect();

                            if (!reconnected)
                            {
                                Close();
                            }
                        }
                    }
                });
        }

        public void Close()
        {
            OnConnectionClosing();
            cancellationTokenSource.Cancel();
            stream.Close();
            OnConnectionClosed();
        }

        protected void OnConnectionClosed()
        {
            if (ConnectionClosed != null) ConnectionClosed(this, EventArgs.Empty);
        }

        protected void OnConnectionClosing()
        {
            if (ConnectionClosing != null) ConnectionClosing(this, EventArgs.Empty);
        }

        protected void OnNewLine(string line)
        {
            if (NewLine != null) NewLine(this, line);
        }

        private bool connect()
        {
            try
            {
                stream = new TcpClient(Config.Server.Address, (int)Config.Server.Port).GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
            }
            catch
            {
                return false;
            }

            SendLine("PASS " + Config.Password);
            SendLine("NICK " + Config.Nickname);
            SendLine("USER " + Config.Nickname + " " + (int)Config.UserMode + " * :" + Config.Username);

            return true;
        }

        private bool reconnect()
        {
            uint reconnectionAttempts = 0;
            bool reconnected = false;

            while (reconnectionAttempts < Config.ReconnectionAttempts)
            {
                reconnected = connect();
                if (reconnected) break;
                reconnectionAttempts++;
            }

            return reconnected;
        }
    }
}