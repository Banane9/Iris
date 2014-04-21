using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Irc
{
    public sealed class Client
    {
        private IConnection connection;

        public Client(IConnection connection)
        {
            this.connection = connection;
        }

        public event EventHandler<IrcMessageEventArgs> Notice;

        public event EventHandler<IrcNumericalMessageEventArgs> NumericalMessage;

        public event EventHandler<IrcMessageEventArgs> PrivateMessage;
    }
}