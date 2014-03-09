using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Iris.Irc
{
    public interface IIrcConnection
    {
        event EventHandler ConnectionClosed;

        event EventHandler ConnectionClosing;

        event EventHandler<IrcLineEventArgs> NewLine;

        void SendLine(string line);

        void Start();

        void Stop();
    }
}