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
        event NewLineEventHandler NewLine;

        void SendLine(string line);

        void Open();

        void Close();
    }

    public delegate void NewLineEventHandler(IConnection sender, string line);
}