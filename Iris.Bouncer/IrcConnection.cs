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
    /// <summary>
    /// Implementation of the <see cref="Iris.Irc.IConnection"/> interface for desktop applications.
    /// </summary>
    public sealed class IrcConnection : IConnection, IDisposable
    {
        public ServerDetails Server { get; set; }

        private StreamReader reader;
        private Stream stream;
        private StreamWriter writer;
        private Thread connectionThread;
        private ConcurrentQueue<string> lineQueue = new ConcurrentQueue<string>();

        /// <summary>
        /// Trys to get the next line from the connection.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns>Whether it was successful or not.</returns>
        public bool TryGetNextLine(out string line)
        {
            return lineQueue.TryDequeue(out line);
        }

        /// <summary>
        /// Gets whether there are more lines waiting to be processed.
        /// </summary>
        public bool HasMoreLines
        {
            get { return !lineQueue.IsEmpty; }
        }

        /// <summary>
        /// Gets whether the connection is alive.
        /// </summary>
        public bool IsAlive
        {
            get { return connectionThread.IsAlive; }
        }

        /// <summary>
        /// Send a line through the connection.
        /// </summary>
        /// <param name="line">The line.</param>
        public void SendLine(string line)
        {
            writer.WriteLine(line);
            writer.Flush();
        }

        /// <summary>
        /// Trys to establish a connection and starts the receiving thread.
        /// </summary>
        /// <returns>Whether it was succeessful or not.</returns>
        public bool Open()
        {
            bool connected = connect();

            if (!connected) return false;

            connectionThread = new Thread(() =>
                {
                    bool alive = true;
                    while (alive)
                    {
                        try
                        {
                            string line = reader.ReadLine();

                            if (!String.IsNullOrWhiteSpace(line))
                            {
                                //What does the client care about pings? Also the're might be too long of a delay due to backlog from message processing.
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
                        catch (ThreadAbortException) { alive = false; } //If the thread gets aborted, it's not unexpected.
                        catch (Exception ex)
                        {
                            onConnectionDroppedUnexpectedly(ex);
                            alive = false;
                        }
                    }
                });
            connectionThread.Name = "Connection - " + Server.Name;
            connectionThread.IsBackground = true;
            connectionThread.Start();

            return true;
        }

        /// <summary>
        /// Closes the connection and stops the receiving thread.
        /// </summary>
        public void Close()
        {
            connectionThread.Abort();
            stream.Close();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Iris.Bouncer.IrcConnection"/> class for the server.
        /// </summary>
        /// <param name="server">Server details of the connection.</param>
        public IrcConnection(ServerDetails server)
        {
            Server = server;
        }

        /// <summary>
        /// Trys to connect to the server.
        /// </summary>
        /// <returns>Whether it was successful or not.</returns>
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

        /// <summary>
        /// Fires the ConnectionDroppedUnexpectedly event.
        /// </summary>
        /// <param name="ex">The Exception caused by the drop.</param>
        private void onConnectionDroppedUnexpectedly(Exception ex)
        {
            if (ConnectionDroppedUnexpectedly != null)
                ConnectionDroppedUnexpectedly(this, ex);
        }

        public event ConnectionDroppedUnexpectedlyEventHandler ConnectionDroppedUnexpectedly;
    }
}