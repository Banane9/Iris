using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Iris.Irc
{
    public delegate void IrcConnectionClosedEventHandler(IIrcConnection sender);

    public delegate void IrcConnectionClosingEventHandler(IIrcConnection sender);

    public delegate void NewIrcLineEventHandler(IIrcConnection sender, string line);

    public interface IIrcConnection
    {
        event IrcConnectionClosedEventHandler ConnectionClosed;

        event IrcConnectionClosingEventHandler ConnectionClosing;

        event NewIrcLineEventHandler NewLine;

        void SendLine(string line);

        void Start();

        void Stop();
    }
}