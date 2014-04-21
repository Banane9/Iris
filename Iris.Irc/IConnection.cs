using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Iris.Irc
{
    public interface IConnection
    {
        event EventHandler ConnectionClosed;

        event EventHandler<IrcLineEventArgs> NewLine;

        void SendLine(string line);

        void Open();

        void Close();
    }
}