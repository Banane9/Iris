using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Irc
{
    public class ClientStateTracker
    {
        private List<IChat> chats;

        public ReadOnlyCollection<Channel> Chats { get; private set; }

        private List<User> users;

        public ReadOnlyCollection<User> Users { get; private set; }
    }
}