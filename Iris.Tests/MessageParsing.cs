using Iris.Irc;
using Iris.Irc.ServerMessages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Iris.Tests
{
    [TestClass]
    public class MessageParsing
    {
        [TestMethod]
        public void PrivateMessage()
        {
            PrivateMessage privateMessage = new PrivateMessage(":Banane9 PRIVMSG #banane9 :Hello, channel!");

            Assert.AreEqual<string>("Banane9", privateMessage.User);
            Assert.AreEqual<string>("#banane9", privateMessage.Recipient);
            Assert.AreEqual<string>("Hello, channel!", privateMessage.Message);
        }

        [TestMethod]
        public void Notice()
        {
            Notice notice = new Notice(":Banane9 NOTICE #banane9 :Notice to channel!");

            Assert.AreEqual<string>("Banane9", notice.User);
            Assert.AreEqual<string>("#banane9", notice.Recipient);
            Assert.AreEqual<string>("Notice to channel!", notice.Message);
        }

        [TestMethod]
        public void NickMessage()
        {
            NickMessage nickMessage = new NickMessage(":Banane9Derp NICK Banane9");

            Assert.AreEqual<string>("Banane9Derp", nickMessage.OldNick);
            Assert.AreEqual<string>("Banane9", nickMessage.NewNick);
        }

        [TestMethod]
        public void JoinMessage()
        {
            JoinMessage joinMessage = new JoinMessage(":Banane9 JOIN #banane9");

            Assert.AreEqual<string>("Banane9", joinMessage.User);
            Assert.AreEqual<string>("#banane9", joinMessage.Channel);
        }

        [TestMethod]
        public void PartMessage()
        {
            PartMessage partMessage = new PartMessage(":Banane9 PART #banane9");

            Assert.AreEqual<string>("Banane9", partMessage.User);
            Assert.AreEqual<string>("#banane9", partMessage.Channel);
        }

        [TestMethod]
        public void QuitMessage()
        {
            QuitMessage quitMessage = new QuitMessage(":Banane9 QUIT :Bye cruel world!");

            Assert.AreEqual<string>("Banane9", quitMessage.User);
            Assert.AreEqual<string>("Bye cruel world!", quitMessage.Message);
        }

        [TestMethod]
        public void NumericalMessage()
        {
            NumericalMessage numericalMessage = new NumericalMessage(":Server 401 Banane9 :No such nick/channel");

            Assert.AreEqual<string>("Server", numericalMessage.Server);
            Assert.AreEqual<NumericalMessageTypes>(NumericalMessageTypes.Error_NoSuchNick, numericalMessage.NumericalType);
        }
    }
}