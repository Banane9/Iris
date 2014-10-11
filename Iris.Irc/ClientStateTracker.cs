using Iris.Irc.Messages;
using Iris.Irc.Messages.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Iris.Irc
{
    public class ClientStateTracker
    {
        private Client client;
        private List<User> users = new List<User>();

        public IList<IChat> Chats { get; set; }

        public ReadOnlyCollection<User> Users { get; private set; }

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

        private void client_JoinMessage(Client sender, JoinMessage joinMessage)
        {
        }

        private void client_Message(Client sender, Message message)
        {
        }

        private void client_NickMessage(Client sender, NickMessage nickMessage)
        {
        }

        private void client_Notice(Client sender, Notice notice)
        {
        }

        private void client_PartMessage(Client sender, PartMessage partMessage)
        {
        }

        private void client_PrivateMessage(Client sender, PrivateMessage privateMessage)
        {
            if (Channel.IsChannel(privateMessage.Recipient))
            {
                Channel channel = Chats.SingleOrDefault(chat => chat.Name == privateMessage.Recipient) as Channel;
                if (channel == null)
                {
                    channel = new Channel(privateMessage.Recipient);
                    Chats.Add(channel);
                }

                channel.Messages.Add(privateMessage);

                User user = users.SingleOrDefault(usr => usr.Complete == privateMessage.User);
                if (user == null)
                {
                    user = new User(privateMessage.User, true);
                    users.Add(user);
                }

                if (user.IsNickServIdentified == null)
                {
                    user.RequestNickServAuthentication(client);
                }
            }
            else
            {
            }
        }

        private void client_QuitMessage(Client sender, QuitMessage quitMessage)
        {
        }
    }
}