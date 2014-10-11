using Iris.Irc;
using Iris.Irc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Iris.Tests
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für ClientBehaviour
    /// </summary>
    [TestClass]
    public class ClientBehaviour
    {
        private Queue<string> clientLines = new Queue<string>();
        private Queue<string> serverLines = new Queue<string>();

        private FakeConnection connection;
        private Client client;
        private Thread clientThread;

        private string nickname = "Iris";
        private string username = "Iris";
        private UserModes usermode = UserModes.i;
        private string password = "password";

        public ClientBehaviour()
        {
            connection = new FakeConnection(line => clientLines.Enqueue(line));
            client = new Client(connection, new ClientConfig() { Nickname = nickname, Username = username, UserMode = usermode, Password = password });
            client.Message += (sender, message) => serverLines.Enqueue(message.Line);

            clientThread = new Thread((ParameterizedThreadStart)((object delay) => client.Run((Action)delay)));
            clientThread.IsBackground = true;
            clientThread.Start((Action)(() => Thread.Sleep(100)));
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            client.Stop();
        }

        [TestMethod]
        public void Login()
        {
            Assert.AreEqual<string>(ClientStringMessageType.Password + " " + password, clientLines.Dequeue());
            Assert.AreEqual<string>(ClientStringMessageType.Nickname + " " + nickname, clientLines.Dequeue());
            Assert.AreEqual<string>(ClientStringMessageType.User + " " + nickname + " " + (int)usermode + " * :" + username, clientLines.Dequeue());
        }
    }
}