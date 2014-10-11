using Iris.Irc;
using Iris.Irc.Messages.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Iris.Tests
{
    [TestClass]
    public class ServerMessageParsing
    {
        [TestMethod]
        public void ChannelParses()
        {
            var globalChannel = new Channel("#global");
            var localChannel = new Channel("&local");

            Assert.IsTrue(globalChannel.IsGlobal);
            Assert.IsFalse(globalChannel.IsLocal);

            Assert.IsFalse(localChannel.IsGlobal);
            Assert.IsTrue(localChannel.IsLocal);
        }

        [TestMethod]
        public void JoinMessageParses()
        {
            var message = ":Banane9 JOIN #banane9";

            Assert.IsTrue(JoinMessage.IsCorrectFormat(message));

            var joinMessage = new JoinMessage(message);

            Assert.AreEqual<string>("Banane9", joinMessage.User);
            Assert.AreEqual<string>("#banane9", joinMessage.Channel);
        }

        [TestMethod]
        public void NickMessageParses()
        {
            var message = ":Banane9Derp NICK Banane9";

            Assert.IsTrue(NickMessage.IsCorrectFormat(message));

            var nickMessage = new NickMessage(message);

            Assert.AreEqual<string>("Banane9Derp", nickMessage.OldNick);
            Assert.AreEqual<string>("Banane9", nickMessage.NewNick);
        }

        [TestMethod]
        public void NoticeParses()
        {
            var message = ":Banane9 NOTICE #banane9 :Notice to channel!";

            Assert.IsTrue(Notice.IsCorrectFormat(message));

            var notice = new Notice(message);

            Assert.AreEqual<string>("Banane9", notice.User);
            Assert.AreEqual<string>("#banane9", notice.Recipient);
            Assert.AreEqual<string>("Notice to channel!", notice.Message);
        }

        [TestMethod]
        public void NumericalMessageParses()
        {
            var message = ":Server 401 Banane9 :No such nick/channel";

            Assert.IsTrue(NumericalMessage.IsCorrectFormat(message));

            var numericalMessage = new NumericalMessage(message);

            Assert.AreEqual<string>("Server", numericalMessage.Server);
            Assert.AreEqual<NumericalMessageType>(NumericalMessageType.Error_NoSuchNick, numericalMessage.NumericalType);
        }

        [TestMethod]
        public void PartMessageParses()
        {
            var message = ":Banane9 PART #banane9";

            Assert.IsTrue(PartMessage.IsCorrectFormat(message));

            var partMessage = new PartMessage(message);

            Assert.AreEqual<string>("Banane9", partMessage.User);
            Assert.AreEqual<string>("#banane9", partMessage.Channel);
        }

        [TestMethod]
        public void PrivateMessageParses()
        {
            var message = ":Banane9 PRIVMSG #banane9 :Hello, channel!";

            Assert.IsTrue(PrivateMessage.IsCorrectFormat(message));

            var privateMessage = new PrivateMessage(message);

            Assert.AreEqual<string>("Banane9", privateMessage.User);
            Assert.AreEqual<string>("#banane9", privateMessage.Recipient);
            Assert.AreEqual<string>("Hello, channel!", privateMessage.Message);
        }

        [TestMethod]
        public void QuitMessageParses()
        {
            var message = ":Banane9 QUIT :Bye cruel world!";

            Assert.IsTrue(QuitMessage.IsCorrectFormat(message));

            var quitMessage = new QuitMessage(message);

            Assert.AreEqual<string>("Banane9", quitMessage.User);
            Assert.AreEqual<string>("Bye cruel world!", quitMessage.Message);
        }
    }
}