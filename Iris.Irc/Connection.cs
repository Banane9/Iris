using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Iris.Irc
{
    public delegate void ConnectionClosedEventHandler(IConnection sender);

    public delegate void ConnectionClosingEventHandler(IConnection sender);

    public delegate void NewLineEventHandler(IConnection sender, string line);

    public interface IConnection
    {
        event ConnectionClosedEventHandler ConnectionClosed;

        event ConnectionClosingEventHandler ConnectionClosing;

        event NewLineEventHandler NewLine;

        void SendLine(string line);

        void Start();

        void Stop();
    }
}