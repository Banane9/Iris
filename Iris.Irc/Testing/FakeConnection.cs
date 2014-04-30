using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Irc.Testing
{
    public class FakeConnection : IConnection
    {
        /// <summary>
        /// Gets or sets the queue of lines that are waiting to be processed by the client.
        /// </summary>
        public Queue<string> LineQueue { get; set; }

        /// <summary>
        /// Gets or sets the function that is executed when the client sends a line.
        /// </summary>
        public Action<string> SendLineFunction { get; set; }

        /// <summary>
        /// Trys to get the next line from the "connection".
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns>Whether it was successful or not.</returns>
        public bool TryGetNextLine(out string line)
        {
            line = LineQueue.Dequeue();
            return line != null;
        }

        /// <summary>
        /// Gets whether there are more lines waiting to be processed.
        /// </summary>
        public bool HasMoreLines
        {
            get { return LineQueue.Count > 0; }
        }

        /// <summary>
        /// Gets or sets whether the "connection" is alive.
        /// </summary>
        public bool IsAlive { get; set; }

        /// <summary>
        /// Send a line through the "connection".
        /// </summary>
        /// <param name="line">The line.</param>
        public void SendLine(string line)
        {
            SendLineFunction(line);
        }

        /// <summary>
        /// Sets the IsAlive Property to true.
        /// </summary>
        /// <returns>Always true.</returns>
        public bool Open()
        {
            //Just dummy.
            IsAlive = true;
            return true;
        }

        /// <summary>
        /// Sets the IsAlive Property to false.
        /// </summary>
        public void Close()
        {
            //Just dummy.
            IsAlive = false;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Iris.Irc.Testing.FakeConnection"/> class with an empty SendLineFunction.
        /// </summary>
        public FakeConnection()
        {
            SendLineFunction = (line) => { };
            LineQueue = new Queue<string>();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Iris.Irc.Testing.FakeConnection"/> class with the SendLineFunction.
        /// </summary>
        /// <param name="sendLineFunction">The function executed when the client sends a line.</param>
        public FakeConnection(Action<string> sendLineFunction)
        {
            this.SendLineFunction = sendLineFunction;
            LineQueue = new Queue<string>();
        }

        /// <summary>
        /// Fire an ConnectionDroppedUnexpectedly event.
        /// </summary>
        /// <param name="ex">The Exception caused by the drop.</param>
        public void DropConnectionUnexpectedly(Exception ex)
        {
            onConnectionDroppedUnexpectedly(ex);
        }

        /// <summary>
        /// Fires the ConnectionDroppedUnexpectedly event.
        /// </summary>
        /// <param name="ex">The Exception caused by the drop.</param>
        protected void onConnectionDroppedUnexpectedly(Exception ex)
        {
            if (ConnectionDroppedUnexpectedly != null)
                ConnectionDroppedUnexpectedly(this, ex);
        }

        /// <summary>
        /// Fires when the "connection" drops unexpectedly.
        /// </summary>
        public event ConnectionDroppedUnexpectedlyEventHandler ConnectionDroppedUnexpectedly;
    }
}