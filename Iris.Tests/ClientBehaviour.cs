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
            client = new Client(connection, new ConnectionConfig() { Nickname = nickname, Username = username, UserMode = usermode, Password = password });
            client.Line += (sender, line) => serverLines.Enqueue(line);

            clientThread = new Thread((ParameterizedThreadStart)((object delay) => client.Run((Action)delay)));
            clientThread.IsBackground = true;
            clientThread.Start((Action)(() => Thread.Sleep(100)));
        }

        #region Zusätzliche Testattribute

        //
        // Sie können beim Schreiben der Tests folgende zusätzliche Attribute verwenden:
        //
        // Verwenden Sie ClassInitialize, um vor Ausführung des ersten Tests in der Klasse Code auszuführen.
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Verwenden Sie ClassCleanup, um nach Ausführung aller Tests in einer Klasse Code auszuführen.
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Mit TestInitialize können Sie vor jedem einzelnen Test Code ausführen.
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        //Mit TestCleanup können Sie nach jedem einzelnen Test Code ausführen.
        [TestCleanup()]
        public void MyTestCleanup()
        {
            client.Stop();
        }

        #endregion Zusätzliche Testattribute

        [TestMethod]
        public void Login()
        {
            Assert.AreEqual<string>(ClientMessageTypes.Password + " " + password, clientLines.Dequeue());
            Assert.AreEqual<string>(ClientMessageTypes.Nickname + " " + nickname, clientLines.Dequeue());
            Assert.AreEqual<string>(ClientMessageTypes.User + " " + nickname + " " + (int)usermode + " * :" + username, clientLines.Dequeue());
        }
    }
}