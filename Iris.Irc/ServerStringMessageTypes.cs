using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Irc
{
    public static class ServerStringMessageTypes
    {
        public const string Notice = "NOTICE";
        public const string Private = "PRIVMSG";
        public const string Nickname = "NICK";
        public const string Join = "JOIN";
        public const string Part = "PART";
        public const string Quit = "QUIT";
    }
}