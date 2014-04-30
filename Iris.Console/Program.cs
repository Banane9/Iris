using Iris.Bouncer;
using Iris.Irc;
using Iris.Irc.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Iris.ConsoleTesting
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IrcConnection ircConnection = new IrcConnection(new Irc.ServerDetails() { Port = 6667, Address = "irc.esper.net", Name = "Esper.net" });
            Client client = new Client(ircConnection, new ConnectionConfig() { Nickname = "Iris", Password = "whatever", UserMode = 0, Username = "Iris" });
            client.Message += (sender, line) => Console.WriteLine(line);

            Thread clientThread = new Thread((ParameterizedThreadStart)((object delay) => client.Run((Action)delay)));
            clientThread.Name = "Cient - Esper.net";
            clientThread.IsBackground = true;
            clientThread.Start((Action)(() => Thread.Sleep(100))); //Needs the delay action passed in because Portable Class Libraries don't support Threads ...

            Console.WriteLine("Press enter to stop threads.");
            Console.ReadLine();

            client.Stop();

            Console.WriteLine("Threads should be stopping.");
            Console.ReadLine();
        }
    }
}