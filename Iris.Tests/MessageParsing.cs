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

            Assert.AreEqual<MessageTypes>(MessageTypes.Private, privateMessage.Type);
            Assert.AreEqual<string>("Banane9", privateMessage.User);
            Assert.AreEqual<string>("#banane9", privateMessage.Recipient);
            Assert.AreEqual<string>("Hello, channel!", privateMessage.Message);
        }

        [TestMethod]
        public void Notice()
        {
            Notice notice = new Notice(":Banane9 NOTICE #banane9 :Notice to channel!");

            Assert.AreEqual<MessageTypes>(MessageTypes.Notice, notice.Type);
            Assert.AreEqual<string>("Banane9", notice.User);
            Assert.AreEqual<string>("#banane9", notice.Recipient);
            Assert.AreEqual<string>("Notice to channel!", notice.Message);
        }

        [TestMethod]
        public void NickMessage()
        {
            NickMessage nickMessage = new NickMessage(":Banane9Derp NICK Banane9");

            Assert.AreEqual<MessageTypes>(MessageTypes.Nick, nickMessage.Type);
            Assert.AreEqual<string>("Banane9Derp", nickMessage.OldNick);
            Assert.AreEqual<string>("Banane9", nickMessage.NewNick);
        }

        [TestMethod]
        public void JoinMessage()
        {
            JoinMessage joinMessage = new JoinMessage(":Banane9 JOIN #banane9");

            Assert.AreEqual<MessageTypes>(MessageTypes.Join, joinMessage.Type);
            Assert.AreEqual<string>("Banane9", joinMessage.User);
            Assert.AreEqual<string>("#banane9", joinMessage.Channel);
        }

        [TestMethod]
        public void PartMessage()
        {
            PartMessage partMessage = new PartMessage(":Banane9 PART #banane9");

            Assert.AreEqual<MessageTypes>(MessageTypes.Part, partMessage.Type);
            Assert.AreEqual<string>("Banane9", partMessage.User);
            Assert.AreEqual<string>("#banane9", partMessage.Channel);
        }

        [TestMethod]
        public void QuitMessage()
        {
            QuitMessage quitMessage = new QuitMessage(":Banane9 QUIT :Bye cruel world!");

            Assert.AreEqual<MessageTypes>(MessageTypes.Quit, quitMessage.Type);
            Assert.AreEqual<string>("Banane9", quitMessage.User);
            Assert.AreEqual<string>("Bye cruel world!", quitMessage.Message);
        }

        [TestMethod]
        public void NumericalMessage()
        {
            NumericalMessage numericalMessage = new NumericalMessage(":Server 401 Banane9 :No such nick/channel");

            Assert.AreEqual<MessageTypes>(MessageTypes.Numerical, numericalMessage.Type);
            Assert.AreEqual<Irc.NumericalMessageTypes>(Irc.NumericalMessageTypes.Error_NoSuchNick, numericalMessage.NumericalType);
        }
    }
}