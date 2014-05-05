using Iris.Irc.ServerMessages;
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
        private List<User> users;

        public ReadOnlyCollection<User> Users { get; private set; }

        public IList<IChat> Chats { get; set; }

        private Client client;

        public ClientStateTracker(Client client)
        {
            this.client = client;

            this.client.Message += client_Message;
            this.client.PrivateMessage += client_PrivateMessage;
            this.client.Notice += client_Notice;
            this.client.NickMessage += client_NickMessage;
            this.client.JoinMessage += client_JoinMessage;
            this.client.PartMessage += client_PartMessage;
            this.client.QuitMessage += client_QuitMessage;
        }

        private void client_Message(Client sender, Message message)
        {
        }

        private void client_PrivateMessage(Client sender, PrivateMessage privateMessage)
        {
        }

        private void client_Notice(Client sender, Notice notice)
        {
        }

        private void client_NickMessage(Client sender, NickMessage nickMessage)
        {
        }

        private void client_JoinMessage(Client sender, JoinMessage joinMessage)
        {
        }

        private void client_PartMessage(Client sender, PartMessage partMessage)
        {
        }

        private void client_QuitMessage(Client sender, QuitMessage quitMessage)
        {
        }
    }
}