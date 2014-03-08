using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Irc
{
    public static class IrcClientMessageTypes
    {
        public const string Admin = "ADMIN";
        public const string Away = "AWAY";
        public const string Connect = "CONNECT";
        public const string Die = "DIE";
        public const string Info = "INFO";
        public const string Invite = "INVITE";
        public const string IsOn = "ISON";
        public const string Join = "JOIN";
        public const string Kick = "KICK";
        public const string Kill = "KILL";
        public const string Links = "LINKS";
        public const string List = "LIST";
        public const string Lusers = "LUSERS";
        public const string MessageOfTheDay = "MOTD";
        public const string Mode = "MODE";
        public const string Names = "NAMES";
        public const string Nickname = "NICK";
        public const string Notice = "NOTICE";
        public const string Oper = "OPER";
        public const string Part = "PART";
        public const string Password = "PASS";
        public const string Ping = "PING";
        public const string Pong = "PONG";
        public const string PrivateMessage = "PRIVMSG";
        public const string Quit = "QUIT";
        public const string Rehash = "REHASH";
        public const string Restart = "RESTART";
        public const string ServerList = "SERVLIST";
        public const string SQuery = "SQUERY";
        public const string SQuit = "SQUIT";
        public const string Statistics = "STATS";
        public const string Summon = "SUMMON";
        public const string Time = "TIME";
        public const string Topic = "TOPIC";
        public const string Trace = "TRACE";
        public const string User = "USER";
        public const string UserHost = "USERHOST";
        public const string Users = "USERS";
        public const string Version = "VERSION";
        public const string Who = "WHO";
        public const string WhoIs = "WHOIS";
        public const string WhoWas = "WHOWAS";
    }
}