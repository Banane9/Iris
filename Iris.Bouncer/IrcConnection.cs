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
        private StreamReader reader;
        private Stream stream;
        private StreamWriter writer;
        private Thread connectionThread;
        private ConcurrentQueue<string> lineQueue = new ConcurrentQueue<string>();

        public bool HasMoreLines
        {
            get { return !lineQueue.IsEmpty; }
        }

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

        public bool Open()
        {
            bool connected = connect();

            if (!connected) return false;

            connectionThread = new Thread(() =>
                {
                    while (true)
                    {
                        try
                        {
                            string line = reader.ReadLine();

                            if (!String.IsNullOrWhiteSpace(line))
                            {
                                //What does the client care about pings. Also the're might be too long of a delay due to backlog from message processing.
                                if (line.ToUpper().StartsWith("PING"))
                                {
                                    SendLine("PONG" + line.Remove(0, 4));
                                }
                                else
                                {
                                    lineQueue.Enqueue(line);
                                }
                            }
                        }
                        catch (ThreadAbortException) { } //If the thread gets aborted, it's not unexpected.
                        catch (Exception ex)
                        {
                            onConnectionDroppedUnexpectedly(ex);
                        }
                    }
                });
            connectionThread.Name = "Connection - " + Server.Name;
            connectionThread.IsBackground = true;
            connectionThread.Start();

            return true;
        }

        public void Close()
        {
            connectionThread.Abort();
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