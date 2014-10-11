using Iris.Irc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Iris.Tests
{
    [TestClass]
    public class OtherParsing
    {
        [TestMethod]
        public void UserNicknameChangeParses()
        {
            var complete = "Banane9!banane9@host.com";
            var user = new User(complete);
            user.Nickname = "Banane9|Other";

            Assert.AreNotEqual<string>(complete, user.Complete);
            Assert.AreEqual<string>("Banane9|Other", user.Nickname);
            Assert.AreEqual<string>("banane9", user.Username);
            Assert.AreEqual<string>("host.com", user.Host);
            Assert.IsTrue(user.HasIdent);
        }

        [TestMethod]
        public void UserParses()
        {
            var complete = "Banane9!banane9@host.com";
            var user = new User(complete);

            Assert.AreEqual<string>(complete, user.Complete);
            Assert.AreEqual<string>("Banane9", user.Nickname);
            Assert.AreEqual<string>("banane9", user.Username);
            Assert.AreEqual<string>("host.com", user.Host);
            Assert.IsTrue(user.HasIdent);
        }
    }
}