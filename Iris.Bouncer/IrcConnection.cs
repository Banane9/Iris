using Iris.Irc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Iris.Bouncer
{
    public sealed class IrcConnection : IConnection, IDisposable
    {
        private CancellationTokenSource cancellationTokenSource;
        private StreamReader reader;
        private Stream stream;
        private StreamWriter writer;
        private ConcurrentQueue<string> lineQueue = new ConcurrentQueue<string>();

        public ServerDetails Server { get; set; }

        public IrcConnection(ServerDetails server)
        {
            Server = server;
        }

        public void SendLine(string line)
        {
            writer.WriteLine(line);
            writer.Flush();
        }

        public bool Start()
        {
            bool connected = connect();

            if (!connected) return false;

            cancellationTokenSource = new CancellationTokenSource();

            Task.Factory.StartNew(() =>
                {
                    //Essentially while(true) since it will almost certainly be in the ReadLine() when the cancelation is requested.
                    while (!cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        try
                        {
                            string line = reader.ReadLine();

                            if (!String.IsNullOrWhiteSpace(line))
                            {
                                lineQueue.Enqueue(line);
                            }
                        }
                        catch (Exception ex)
                        {
                            if (!cancellationTokenSource.Token.IsCancellationRequested)
                                onConnectionDroppedUnexpectedly(ex);
                        }
                    }
                });

            return true;
        }

        public void Stop()
        {
            cancellationTokenSource.Cancel();
            stream.Close();
        }

        private bool connect()
        {
            try
            {
                stream = new TcpClient(Server.Address, (int)Server.Port).GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            reader.Dispose();
            writer.Dispose();
            stream.Dispose();
        }

        public bool TryGetNextLine(out string line)
        {
            return lineQueue.TryDequeue(out line);
        }

        private void onConnectionDroppedUnexpectedly(Exception ex)
        {
            if (ConnectionDroppedUnexpectedly != null)
                ConnectionDroppedUnexpectedly(this, ex);
        }

        public event ConnectionDroppedUnexpectedlyEventHandler ConnectionDroppedUnexpectedly;
    }
}