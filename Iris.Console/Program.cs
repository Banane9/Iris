using Iris.Bouncer;
using Iris.Irc;
using Iris.Irc.Messages.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Iris.ConsoleTesting
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IrcConnection ircConnection = new IrcConnection(new ServerDetails() { Port = 6667, Address = "irc.esper.net", Name = "Esper.net" });
            Client client = new Client(ircConnection, new ClientConfig() { Nickname = "Iris", Password = "whatever", UserMode = 0, Username = "Iris" });
            client.Message += (sender, message) =>
                {
                    if (message is PrivateMessage)
                    {
                        var privmsg = (PrivateMessage)message;
                        Console.WriteLine(privmsg.User + ": " + privmsg.Message);
                    }
                    else
                        Console.WriteLine(message.Line.Remove(0, message.Line.IndexOf(':', 2) + 1));
                };

            Thread clientThread = new Thread((ParameterizedThreadStart)((object delay) => client.Run((Action)delay)));
            clientThread.Name = "Client - Esper.net";
            clientThread.IsBackground = true;
            clientThread.Start((Action)(() => Thread.Sleep(100))); //Needs the delay action passed in because Portable Class Libraries don't support Threads ...

            Console.WriteLine("Write exit to ... well ... exit.");

            string readLine = "";
            while (true)
            {
                readLine = Console.ReadLine();
                if (readLine.ToLower() == "exit") break;
                ircConnection.SendLine(readLine);
            }

            client.Stop();

            Console.WriteLine("Threads should be stopping.");
            Console.ReadLine();
        }
    }
}