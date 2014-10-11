using System;
using System.Collections.Generic;
using System.Linq;

namespace Iris.Irc
{
    public static class ServerStringMessageType
    {
        public const string Join = "JOIN";
        public const string Nickname = "NICK";
        public const string Notice = "NOTICE";
        public const string Part = "PART";
        public const string Private = "PRIVMSG";
        public const string Quit = "QUIT";
    }
}