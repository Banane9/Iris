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
            string message = ":Banane9 PRIVMSG #banane9 :Hello, channel!";

            Assert.IsTrue(Iris.Irc.ServerMessages.PrivateMessage.IsCorrectFormat(message));

            PrivateMessage privateMessage = new PrivateMessage(message);

            Assert.AreEqual<string>("Banane9", privateMessage.User);
            Assert.AreEqual<string>("#banane9", privateMessage.Recipient);
            Assert.AreEqual<string>("Hello, channel!", privateMessage.Message);
        }

        [TestMethod]
        public void Notice()
        {
            string message = ":Banane9 NOTICE #banane9 :Notice to channel!";

            Assert.IsTrue(Iris.Irc.ServerMessages.Notice.IsCorrectFormat(message));

            Notice notice = new Notice(message);

            Assert.AreEqual<string>("Banane9", notice.User);
            Assert.AreEqual<string>("#banane9", notice.Recipient);
            Assert.AreEqual<string>("Notice to channel!", notice.Message);
        }

        [TestMethod]
        public void NickMessage()
        {
            string message = ":Banane9Derp NICK Banane9";

            Assert.IsTrue(Iris.Irc.ServerMessages.NickMessage.IsCorrectFormat(message));

            NickMessage nickMessage = new NickMessage(message);

            Assert.AreEqual<string>("Banane9Derp", nickMessage.OldNick);
            Assert.AreEqual<string>("Banane9", nickMessage.NewNick);
        }

        [TestMethod]
        public void JoinMessage()
        {
            string message = ":Banane9 JOIN #banane9";

            Assert.IsTrue(Iris.Irc.ServerMessages.JoinMessage.IsCorrectFormat(message));

            JoinMessage joinMessage = new JoinMessage(message);

            Assert.AreEqual<string>("Banane9", joinMessage.User);
            Assert.AreEqual<string>("#banane9", joinMessage.Channel);
        }

        [TestMethod]
        public void PartMessage()
        {
            string message = ":Banane9 PART #banane9";

            Assert.IsTrue(Iris.Irc.ServerMessages.PartMessage.IsCorrectFormat(message));

            PartMessage partMessage = new PartMessage(message);

            Assert.AreEqual<string>("Banane9", partMessage.User);
            Assert.AreEqual<string>("#banane9", partMessage.Channel);
        }

        [TestMethod]
        public void QuitMessage()
        {
            string message = ":Banane9 QUIT :Bye cruel world!";

            Assert.IsTrue(Iris.Irc.ServerMessages.QuitMessage.IsCorrectFormat(message));

            QuitMessage quitMessage = new QuitMessage(message);

            Assert.AreEqual<string>("Banane9", quitMessage.User);
            Assert.AreEqual<string>("Bye cruel world!", quitMessage.Message);
        }

        [TestMethod]
        public void NumericalMessage()
        {
            string message = ":Server 401 Banane9 :No such nick/channel";

            Assert.IsTrue(Iris.Irc.ServerMessages.NumericalMessage.IsCorrectFormat(message));

            NumericalMessage numericalMessage = new NumericalMessage(message);

            Assert.AreEqual<string>("Server", numericalMessage.Server);
            Assert.AreEqual<NumericalMessageTypes>(NumericalMessageTypes.Error_NoSuchNick, numericalMessage.NumericalType);
        }
    }
}