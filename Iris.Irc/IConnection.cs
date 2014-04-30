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
        bool TryGetNextLine(out string line);

        bool HasMoreLines { get; }

        void SendLine(string line);

        bool Open();

        void Close();

        event ConnectionDroppedUnexpectedlyEventHandler ConnectionDroppedUnexpectedly;
    }

    public delegate void ConnectionDroppedUnexpectedlyEventHandler(IConnection sender, Exception ex);
}