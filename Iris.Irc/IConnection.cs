using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Iris.Irc
{
    /// <summary>
    /// Interface for a IRC Connection.
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// Trys to get the next line from the connection.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns>Whether it was successful or not.</returns>
        bool TryGetNextLine(out string line);

        /// <summary>
        /// Gets whether there are more lines waiting to be processed.
        /// </summary>
        bool HasMoreLines { get; }

        /// <summary>
        /// Gets whether the connection is alive.
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        /// Send a line through the connection.
        /// </summary>
        /// <param name="line">The line.</param>
        void SendLine(string line);

        /// <summary>
        /// Trys to establish a connection and starts the receiving thread.
        /// </summary>
        /// <returns>Whether it was succeessful or not.</returns>
        bool Open();

        /// <summary>
        /// Closes the connection and stops the receiving thread.
        /// </summary>
        void Close();

        /// <summary>
        /// Fires when the connection drops unexepectedly. That is, any time when it wasn't caused by the Close() method.
        /// </summary>
        event ConnectionDroppedUnexpectedlyEventHandler ConnectionDroppedUnexpectedly;
    }

    public delegate void ConnectionDroppedUnexpectedlyEventHandler(IConnection sender, Exception ex);
}