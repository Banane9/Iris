using Iris.Irc;
using Iris.Irc.Messages.Client;
using Iris.Irc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Iris.Tests
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für ClientBehaviour
    /// </summary>
    [TestClass]
    public class ClientBehaviour
    {
        private Client client;
        private Queue<string> clientLines = new Queue<string>();
        private Thread clientThread;
        private FakeConnection connection;
        private string nickname = "Iris";
        private string password = "password";
        private Queue<string> serverLines = new Queue<string>();
        private UserModes usermode = UserModes.i;
        private string username = "Iris";

        public ClientBehaviour()
        {
            connection = new FakeConnection(line => clientLines.Enqueue(line));
            client = new Client(connection, new ClientConfig() { Nickname = nickname, Username = username, UserMode = usermode, Password = password });
            client.Message += (sender, message) => serverLines.Enqueue(message.Line);

            clientThread = new Thread((ParameterizedThreadStart)((object delay) => client.Run((Action)delay)));
            clientThread.IsBackground = true;
            clientThread.Start((Action)(() => Thread.Sleep(100)));
        }

        [TestMethod]
        public void Login()
        {
            Assert.AreEqual<string>(NamedMessageType.Password + " " + password, clientLines.Dequeue());
            Assert.AreEqual<string>(NamedMessageType.Nickname + " " + nickname, clientLines.Dequeue());
            Assert.AreEqual<string>(NamedMessageType.User + " " + nickname + " " + (int)usermode + " * :" + username, clientLines.Dequeue());
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            client.Stop();
        }
    }
}