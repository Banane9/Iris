using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Irc
{
    public sealed class IrcNumericalMessageEventArgs : EventArgs
    {
        public string Arguments { get; set; }

        public IrcNumericalMessageTypes MessageType { get; set; }

        public string Sender { get; set; }
    }
}