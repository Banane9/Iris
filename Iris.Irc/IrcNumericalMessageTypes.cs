using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.Irc
{
    /// <summary>
    /// Lists all the numerical message types, as described here: http://tools.ietf.org/html/rfc1459#section-6
    /// </summary>
    public enum IrcNumericalMessageTypes
    {
        /// <summary>
        /// Used to indicate the nickname parameter supplied to a command is currently unused.
        /// <para>Format: "$nickname :No such nick/channel"</para>
        /// </summary>
        Error_NoSuchNick = 401,

        /// <summary>
        /// Used to indicate the server name given currently doesn't exist.
        /// <para>Format: "$serverName :No such server"</para>
        /// </summary>
        Error_NoSuchServer = 402,

        /// <summary>
        /// Used to indicate the given channel name is invalid.
        /// <para>Format: "$channelName :No such channel"</para>
        /// </summary>
        Error_NoSuchChannel = 403,

        /// <summary>
        /// Sent to a user who is either
        /// <list type="bullet">
        /// <item>
        /// <description>Not on a channel which is mode +n or</description>
        /// </item>
        /// <item>
        /// <description>not a chanop (or mode +v) on a channel which has mode +m set</description>
        /// </item>
        /// </list>
        /// and is trying to send a PRIVMSG message to that channel.
        /// <para>Format: "$channelName :Cannot send to channel"</para>
        /// </summary>
        Error_CanNotSendToChannel = 404,

        /// <summary>
        /// Sent to a user when they have joined the maximum number of allowed channels
        /// and they try to join another channel.
        ///<para>Format: "$channelName :You have joined too many channels"</para>
        /// </summary>
        Error_TooManyChannels = 405,

        /// <summary>
        /// Returned by WHOWAS to indicate there is no history information for that nickname.
        /// <para>Format: "$nickname :There was no such nickname"</para>
        /// </summary>
        Error_WasNoSuchNick = 406,

        /// <summary>
        /// Returned to a client which is attempting to send a PRIVMSG/NOTICE using the
        /// user@host destination format and for a user@host which has several occurrences.
        /// <para>Format: "$target :Duplicate recipients. No message delivered"</para>
        /// </summary>
        Error_TooManyTargets = 407,

        /// <summary>
        /// PING or PONG message missing the originator parameter which is
        /// required since these commands must work without valid prefixes.
        /// <para>Format: ":No origin specified"</para>
        /// </summary>
        Error_NoOrigin = 409,

        /// <summary>
        /// Format: ":No recipient given ($command)"
        /// </summary>
        Error_NoRecipient = 411,

        /// <summary>
        /// Returned by PRIVMSG to indicate that the message wasn't delivered for some reason.
        /// <para>Format: ":No text to send"</para>
        /// </summary>
        Error_NoTextToSend = 412,

        /// <summary>
        /// Returned by PRIVMSG to indicate that the message wasn't delivered for some reason.
        /// <para>
        /// Error that is returned when an invalid use of "PRIVMSG $$server" or "PRIVMSG #$host" is attempted.
        /// </para>
        /// <para>Format: "$mask :No toplevel domain specified"</para>
        /// </summary>
        Error_NoTopLevel = 413,

        /// <summary>
        /// Returned by PRIVMSG to indicate that the message wasn't delivered for some reason.
        /// <para>
        /// Error that is returned when an invalid use of "PRIVMSG $$server" or "PRIVMSG #$host" is attempted.
        /// </para>
        /// <para>Format: "$mask :Wildcard in toplevel domain"</para>
        /// </summary>
        Error_WildTopLevel = 414,

        /// <summary>
        ///Returned to a registered client to indicate that the command sent is unknown by the server.
        /// <para>Format: "$command :Unknown command"</para>
        /// </summary>
        Error_UnknowCommand = 421,

        /// <summary>
        /// Server's MOTD file could not be opened by the server.
        /// <para>Format: ":MOTD File is missing"</para>
        /// </summary>
        Error_NoMotd = 422,

        /// <summary>
        /// Returned by a server in response to an ADMIN message when there is an error
        /// in finding the appropriate information.
        /// <para>Format: "$server :No administrative info available"</para>
        /// </summary>
        Error_NoAdminInfo = 423,

        /// <summary>
        /// Generic error message used to report a failed file operation during the processing of a message.
        /// <para>Format: ":File error doing $fileOp on $file"</para>
        /// </summary>
        Error_FileError = 424,

        /// <summary>
        /// Returned when a nickname parameter expected for a command and isn't found.
        /// <para>Format: ":No nickname given"</para>
        /// </summary>
        Error_NoNicknameGiven = 431,

        /// <summary>
        /// Returned after receiving a NICK message which contains
        /// characters which do not fall in the defined set.
        /// <para>Format: "$nick :Erroneus nickname"</para>
        /// </summary>
        Error_ErroneusNickname = 432,

        /// <summary>
        /// Returned when a NICK message is processed that results in an attempt
        /// to change to a currently existing nickname.
        /// <para>Format: "$nick :Nickname is already in use"</para>
        /// </summary>
        Error_NickNameInUse = 433,

        /// <summary>
        /// Returned by a server to a client when it detects a nickname collision
        /// (registered of a NICK that already exists by another server).
        /// <para>Format: "$nick :Nickname collision KILL"</para>
        /// </summary>
        Error_NickCollision = 436,

        /// <summary>
        /// Returned by the server to indicate that the target
        /// user of the command is not on the given channel.
        /// <para>Format: "$nick $channel :They aren't on that channel"</para>
        /// </summary>
        Error_UserNotInChannel = 441,

        /// <summary>
        /// Returned by the server whenever a client tries to perform
        /// a channel effecting command for which the client isn't a member.
        /// <para>Format: "$channel :You're not on that channel"</para>
        /// </summary>
        Error_NotOnChannel = 442,

        /// <summary>
        /// Returned when a client tries to invite a user to a channel they are already on.
        /// <para>Format: "$user $channel :is already on channel"</para>
        /// </summary>
        Error_UserOnChannel = 443,

        /// <summary>
        /// Returned by the summon after a SUMMON command for a user
        /// was unable to be performed since they were not logged in.
        /// <para>Format: "$user :User not logged in"</para>
        /// </summary>
        Error_NoLogin = 444,

        /// <summary>
        /// Returned as a response to the SUMMON command.  Must be
        /// returned by any server which does not implement it.
        /// <para>Format: ":SUMMON has been disabled"</para>
        /// </summary>
        Error_SummonDisabled = 445,

        /// <summary>
        /// Returned as a response to the USERS command.  Must be
        /// returned by any server which does not implement it.
        /// <para>Format: ":USERS has been disabled"</para>
        /// </summary>
        Error_UserDisabled = 446,

        /// <summary>
        /// Returned by the server to indicate that the client must be
        /// registered before the server will allow it to be parsed in detail.
        /// <para>":You have not registered"</para>
        /// </summary>
        Error_NotRegistered = 451,

        /// <summary>
        /// Returned by the server by numerous commands to indicate
        /// to the client that it didn't supply enough parameters.
        /// <para>Format: "$command :Not enough parameters"</para>
        /// </summary>
        Error_NeedMoreParameters = 461,

        /// <summary>
        /// Returned by the server to any link which tries to change part of the
        /// registered details (such as password or user details from second USER message).
        /// <para>Format: ":You may not reregister"</para>
        /// </summary>
        Error_AlreadyRegistered = 462,

        /// <summary>
        /// Returned to a client which attempts to register with a server which does not been
        /// setup to allow connections from the host the attempted connection is tried.
        /// <para>Format: ":Your host isn't among the privileged"</para>
        /// </summary>
        Error_NoPermissionForHost = 463,

        /// <summary>
        /// Returned to indicate a failed attempt at registering a connection for which a
        /// password was required and was either not given or incorrect.
        /// <para>Format: ":Password incorrect"</para>
        /// </summary>
        Error_PasswordMismatch = 464,

        /// <summary>
        /// Returned after an attempt to connect and register yourself with a server which
        /// has been setup to explicitly deny connections to you.
        /// <para>Format: ":You are banned from this server"</para>
        /// </summary>
        Error_YoureBannedCreep = 465,

        /// <summary>
        /// Format: "$channel :Channel key already set"
        /// </summary>
        Error_KeySet = 467,

        /// <summary>
        /// Format: "$channel :Cannot join channel (+l)"
        /// </summary>
        Error_ChannelIsFull = 471,

        /// <summary>
        /// Format: "$char :is unknown mode char to me"
        /// </summary>
        Error_UnknowMode = 472,

        /// <summary>
        /// Format: "$channel :Cannot join channel (+i)"
        /// </summary>
        Error_InviteOnlyChannel = 473,

        /// <summary>
        /// Format: "$channel :Cannot join channel (+b)"
        /// </summary>
        Error_BannedFromChannel = 474,

        /// <summary>
        /// Format: "$channel :Cannot join channel (+k)"
        /// </summary>
        Error_BadChannelKey = 475,

        /// <summary>
        /// Any command requiring operator privileges to operate returns
        /// this error to indicate the attempt was unsuccessful.
        /// <para>Format: ":Permission Denied- You're not an IRC operator"</para>
        /// </summary>
        Error_NoPrivileges = 481,

        /// <summary>
        /// Any command requiring 'chanop' privileges (such as MODE messages) returns this
        /// error if the client making the attempt is not a chanop on the specified channel.
        /// <para>Format: "$channel :You're not channel operator"</para>
        /// </summary>
        Error_ChannelOpPrivilegesNeeded = 482,

        /// <summary>
        /// Any attempts to use the KILL command on a server are to be
        /// refused and this error returned directly to the client.
        /// <para>Format: ":You cant kill a server!"</para>
        /// </summary>
        Error_CantKillServer = 483,

        /// <summary>
        /// If a client sends an OPER message and the server has not been configured to allow
        /// connections from the client's host as an operator, this error is returned.
        /// <para>Format: ":No O-lines for your host"</para>
        /// </summary>
        Error_NoOperHost = 491,

        /// <summary>
        /// Returned by the server to indicate that a MODE message was sent with a nickname
        /// parameter and that the a mode flag sent was not recognized.
        /// <para>Format: ":Unknown MODE flag"</para>
        /// </summary>
        Error_UserModeUnknownFlag = 501,

        /// <summary>
        /// Error sent to any user trying to view or change the user mode for a user other than themselves.
        /// <para>Format: ":Cant change mode for other users"</para>
        /// </summary>
        Error_UsersDontMatch = 502,

        Reply_None = 300,
        Reply_UserHost = 302,
        Reply_Ison = 303,
        Reply_Away = 301,
        Reply_Unaway = 305,
        Reply_NowAway = 306,
        Reply_WhoisUser = 311,
        Reply_WhoisServer = 312,
        Reply_WhoisOperator = 313,
        Reply_WhoisIdle = 317,
        Reply_EndOfWhois = 318,
        Reply_WhoisChannels = 319,
        Reply_WhoWasUser = 314,
        Reply_EndOfWhoWas = 369,
        Reply_ListStart = 321,
        Reply_List = 322,
        Reply_ListEnd = 323,
        Reply_ChannelModeIs = 324,
        Reply_NoTopic = 331,
        Reply_Topic = 332,
        Reply_Inviting = 341,
        Reply_Summoning = 342,
        Reply_Version = 351,
        Reply_WhoReply = 352,
        Reply_EndOfWho = 315,
        Reply_NameReply = 353,
        Reply_EndOfNames = 366,
        Reply_Links = 364,
        Reply_EndOfLinks = 365,
        Reply_Banlist = 367,
        Reply_EndOfBanlist = 368,
        Reply_Info = 371,
        Reply_EndOfInfo = 374,
        Reply_MotdStart = 375,
        Reply_Motd = 372,
        Reply_EndOfMotd = 376,
        Reply_YoureOper = 381,
        Reply_Rehashing = 382,
        Reply_Time = 391,
        Reply_UsersStart = 392,
        Reply_Users = 393,
        Reply_EndOfUsers = 394,
        Reply_NoUsers = 395,
        Reply_TraceLink = 200,
        Reply_TraceConnecting = 201,
        Reply_TraceHandshake = 202,
        Reply_TraceUnknow = 203,
        Reply_TraceOperator = 204,
        Reply_TraceUser = 205,
        Reply_TraceServer = 206,
        Reply_TraceNewType = 208,
        Reply_TraceLog = 261,
        Reply_StatsLinkInfo = 211,
        Reply_StatsCommands = 212,
        Reply_StatsCLine = 213,
        Reply_StatsNLine = 214,
        Reply_StatsILine = 215,
        Reply_StatsKLine = 216,
        Reply_StatsYLine = 218,
        Reply_EndOfStats = 219,
        Reply_StatsLLine = 241,
        Reply_StatsUptime = 242,
        Reply_StatsOLine = 243,
        Reply_StatsHLine = 244,
        Reply_UserModeIs = 221,
        Reply_LUserClient = 251,
        Reply_LUserOperator = 252,
        Reply_LUserUnknown = 253,
        Reply_LUserChannels = 254,
        Reply_LUserMe = 255,
        Reply_AdminMe = 256,
        Reply_AdminLoc1 = 257,
        Reply_AdminLoc2 = 258,
        Reply_AdminEMail = 259
    }
}