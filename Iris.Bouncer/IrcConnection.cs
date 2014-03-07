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
    public class IrcConnection : IConnection
    {
        private CancellationTokenSource cancellationTokenSource;
        private StreamReader reader;
        private Stream stream;
        private StreamWriter writer;

        public ConnectionConfig Config { get; private set; }

        public IrcConnection(ConnectionConfig config)
        {
            Config = config;
        }

        public event ConnectionClosedEventHandler ConnectionClosed;

        public event ConnectionClosingEventHandler ConnectionClosing;

        public event NewLineEventHandler NewLine;

        public void SendLine(string line)
        {
            writer.WriteLine(line);
            writer.FlushAsync();
        }

        public void Start()
        {
            cancellationTokenSource = new CancellationTokenSource();

            Task.Factory.StartNew(() =>
                {
                    while (!cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        try
                        {
                            string line = reader.ReadLine();

                            if (!String.IsNullOrEmpty(line))
                            {
                                if (NewLine != null) NewLine(this, line);
                            }
                        }
                        catch (IOException)
                        {
                            bool reconnected = reconnect();

                            if (!reconnected)
                            {
                                Stop();
                            }
                        }
                    }
                });
        }

        public void Stop()
        {
            if (ConnectionClosing != null) ConnectionClosing(this);
            cancellationTokenSource.Cancel();
            stream.Close();
            if (ConnectionClosed != null) ConnectionClosed(this);
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
            SendLine("USER " + Config.Nickname + "something something:" + Config.Username);

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