using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.Irc
{
    /// <summary>
    /// Lists all the numerical message types, as described here: http://tools.ietf.org/html/rfc1459#section-6 and here: http://tools.ietf.org/html/rfc2812#section-5.1
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
        /// Sent to a user who is either (a) Not on a channel which is mode +n or (b) not a chanop (or mode +v) on a channel
        /// which has mode +m set and is trying to send a PRIVMSG message to that channel.
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
        /// Returned to a client which is attempting to send a PRIVMSG/NOTICE using
        /// the user@host destination format and for a user@host which has several occurrences.
        /// <para>Returned to a client which trying to send a PRIVMSG/NOTICE to too many recipients.</para>
        /// <para>Returned to a client which is attempting to JOIN a safe channel using the
        /// shortname when there are more than one such channel.</para>
        /// <para>Format: "$target :$errorCode recipients. $abortMessage"</para>
        /// </summary>
        Error_TooManyTargets = 407,

        /// <summary>
        /// Returned to a client which is attempting to send a SQUERY to a service which does not exist.
        /// <para>Format: "$serviceName :No such service"</para>
        /// </summary>
        Error_NoSuchService = 408,

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
        /// Format: "$mask :Bad Server/host mask"
        /// </summary>
        Error_BadMask = 415,

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
        /// <para>Format: "$nick :Nickname collision KILL from $user@$host"</para>
        /// </summary>
        Error_NickCollision = 436,

        /// <summary>
        /// Returned by a server to a user trying to join a channel
        /// currently blocked by the channel delay mechanism.
        /// <para>Returned by a server to a user trying to change nickname when the
        /// desired nickname is blocked by the nick delay mechanism.</para>
        /// <para>Format: "$(nick|channel) :Nick/channel is temporarily unavailable"</para>
        /// </summary>
        Error_UnavailableResource = 437,

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
        Error_UsersDisabled = 446,

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
        /// Sent by a server to a user to inform that access to the server will soon be denied.
        /// </summary>
        Error_YouWillBeBanned = 466,

        /// <summary>
        /// Format: "$channel :Channel key already set"
        /// </summary>
        Error_KeySet = 467,

        /// <summary>
        /// Format: "$channel :Cannot join channel (+l)"
        /// </summary>
        Error_ChannelIsFull = 471,

        /// <summary>
        /// Format: "$char :is unknown mode char to me for $channel"
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
        /// Format: "$channel :Bad Channel Mask"
        /// </summary>
        Error_BadChannelMask = 476,

        /// <summary>
        /// Format: "$channel :Channel doesn't support modes"
        /// </summary>
        Error_NoChannelModes = 477,

        /// <summary>
        /// Format: "$channel $char :Channel list is full"
        /// </summary>
        Error_BanlistFull = 478,

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
        /// Sent by the server to a user upon connection to indicate
        /// the restricted nature of the connection (user mode "+r").
        /// <para>Format: ":Your connection is restricted!"</para>
        /// </summary>
        Error_Restricted = 484,

        /// <summary>
        /// Any MODE requiring "channel creator" privileges MUST return this error
        /// if the client making the attempt is not a chanop on the specified channel.
        /// <para>Format: ":You're not the original channel operator"</para>
        /// </summary>
        Error_UniqOpPrivilegesNeeded = 485,

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

        /// <summary>
        /// Format: ":Welcome to the Internet Relay Network $nick!$user@$host"
        /// </summary>
        Reply_Welcome = 001,

        /// <summary>
        /// Format: "Your host is $servername, running version $ver"
        /// </summary>
        Reply_YourHost = 002,

        /// <summary>
        /// Format: "This server was created $date"
        /// </summary>
        Reply_Created = 003,

        /// <summary>
        /// Format: "$servername $version $availableUserModes $availableChannelModes"
        /// </summary>
        Reply_MyInfo = 004,

        /// <summary>
        /// Sent by the server to a user to suggest an alternative server.
        /// This is often used when the connection is refused because the server is already full.
        /// <para>Format: "Try server $serverName, port $portNumber"</para>
        /// </summary>
        Reply_Bounce = 005,

        /// <summary>
        /// Dummy reply number. Not used.
        /// </summary>
        Reply_None = 300,

        /// <summary>
        ///Reply format used by USERHOST to list replies to the query list.
        ///The reply string is composed as follows:
        ///<para>Format: "$nick['*'] = (+|-)$hostname"</para>
        ///The '*' indicates whether the client has registered as an Operator.
        ///The '-' or '+' characters represent whether the client has set an
        ///AWAY message or not respectively.
        ///<para>Format: ":[$reply{'space'$reply...}]"</para>
        /// </summary>
        Reply_UserHost = 302,

        /// <summary>
        /// Reply format used by ISON to list replies to the query list.
        /// <para>Format: ":[$nick {'space'$nick...}]"</para>
        /// </summary>
        Reply_IsOn = 303,

        /// <summary>
        /// Sent to any client sending a PRIVMSG to a client which is away.
        /// <para>Format: "$nick :$awayMessage"</para>
        /// </summary>
        Reply_Away = 301,

        /// <summary>
        /// Sent when the client removes an AWAY message.
        /// <para>Format: ":You are no longer marked as being away"</para>
        /// </summary>
        Reply_Unaway = 305,

        /// <summary>
        /// Send when the client sets an AWAY message.
        /// <para>Format: ":You have been marked as being away"</para>
        /// </summary>
        Reply_NowAway = 306,

        /// <summary>
        /// Format: "$nick $user $host '*' :$realName"
        /// </summary>
        Reply_WhoisUser = 311,

        /// <summary>
        /// Format: "$nick $server :$serverInfo"
        /// </summary>
        Reply_WhoisServer = 312,

        /// <summary>
        /// Format: "$nick :is an IRC operator"
        /// </summary>
        Reply_WhoisOperator = 313,

        /// <summary>
        /// Format: "$nick $integer :seconds idle"
        /// </summary>
        Reply_WhoisIdle = 317,

        /// <summary>
        /// Marks the end of processing a WHOIS message.
        /// <para>Format: "$nick :End of /WHOIS list"</para>
        /// </summary>
        Reply_EndOfWhois = 318,

        /// <summary>
        /// May appear more than once in a WHOIS reply set. '@' and '+' characters next to the
        /// channel name indicate whether a client is a channel operator or has been granted permission
        /// to speak on a moderated channel.
        /// <para>Format: "$nick :[{@|+}$channel'space'...]"</para>
        /// </summary>
        Reply_WhoisChannels = 319,

        /// <summary>
        /// Format: "$nick $user $host '*' :$realName"
        /// </summary>
        Reply_WhoWasUser = 314,

        /// <summary>
        /// Marks the end of a WHOWAS reply batch. Appears even if there was only one message being an error.
        /// <para>Format: "$nick :End of WHOWAS"</para>
        /// </summary>
        Reply_EndOfWhoWas = 369,

        /// <summary>
        /// Marks the start of the LIST command's reply batch.
        /// <para>Format: Channel :Users'space''space'Name"</para>
        /// </summary>
        Reply_ListStart = 321,

        /// <summary>
        /// Format: "$channel $visibleUsers :$topic"
        /// </summary>
        Reply_List = 322,

        /// <summary>
        /// Marks the end of the LIST command's reply batch.
        /// <para>Format: ":End of /LIST"</para>
        /// </summary>
        Reply_ListEnd = 323,

        /// <summary>
        /// Format: "$channel $nickname"
        /// </summary>
        Reply_UniqOpIs = 325,

        /// <summary>
        /// Format: "$channel $mode $modeParams"
        /// </summary>
        Reply_ChannelModeIs = 324,

        /// <summary>
        /// Format: "$channel :No topic is set"
        /// </summary>
        Reply_NoTopic = 331,

        /// <summary>
        /// Format: "$channel :$topic"
        /// </summary>
        Reply_Topic = 332,

        /// <summary>
        /// Returned by the server to indicate that the attempted INVITE message
        /// was successful and is being passed onto the end client.
        /// <para>Format: "$channel $nick"</para>
        /// </summary>
        Reply_Inviting = 341,

        /// <summary>
        /// Returned by a server answering a SUMMON message to indicate that it is summoning that user.
        /// <para>Format: "$user :Summoning user to IRC"</para>
        /// </summary>
        Reply_Summoning = 342,

        /// <summary>
        /// Format: "$channel $invitemask"
        /// </summary>
        Reply_Invitelist = 346,

        /// <summary>
        /// Marks the end of the invitations list for a channel.
        /// <para>Format: "$channel :End of channel invite list"</para>
        /// </summary>
        Reply_EndOfInvitelist = 347,

        /// <summary>
        /// Format: "$channel $exceptionmask"
        /// </summary>
        Reply_Exceptlist = 348,

        /// <summary>
        /// Marks the end of the exception list for a channel.
        /// <para>Format: "$channel :End of channel exception list"</para>
        /// </summary>
        Reply_EndOfExceptlist = 349,

        /// <summary>
        /// Reply by the server showing its version details.
        /// The $version is the version of the software being used
        /// (including any patchlevel revisions) and the $debuglevel
        /// is used to indicate if the server is running in "debug mode".
        /// <para>Format: "$version.$debuglevel $server :$comments"</para>
        /// </summary>
        Reply_Version = 351,

        /// <summary>
        /// Reply to the WHO command. Only sent if there's an appropriate match to the request.
        /// <para>Format: "$channel $user $host $server $nick (H|G)['*'][@|+] :$hopcount $realName"</para>
        /// </summary>
        Reply_WhoReply = 352,

        /// <summary>
        /// Send after each WhoReply message.
        /// <para>Format: "$name :End of /WHO list"</para>
        /// </summary>
        Reply_EndOfWho = 315,

        /// <summary>
        /// @ for secret channels; '*' for private channels and = for others (public channels).
        /// <para>Format: "(=|'*'|@) $channel :[[@|+]$nick'space'...]]"</para>
        /// </summary>
        Reply_NameReply = 353,

        /// <summary>
        /// Marks the end of the reply batch for the NAMES command.
        /// <para>Format: "$channel :End of /NAMES list"</para>
        /// </summary>
        Reply_EndOfNames = 366,

        /// <summary>
        /// Format: "$mask $server :$hopcount $serverInfo"
        /// </summary>
        Reply_Links = 364,

        /// <summary>
        /// Marks the end of the reply batch from the LINKS command.
        /// <para>Format: "$mask :End of /LINKS list"</para>
        /// </summary>
        Reply_EndOfLinks = 365,

        /// <summary>
        /// Format: "$channel $banid"
        /// </summary>
        Reply_Banlist = 367,

        /// <summary>
        /// Marks the end of the reply batch from the ban list command (MODE $channel b).
        /// <para>Format: "$channel :End of channel ban list"</para>
        /// </summary>
        Reply_EndOfBanlist = 368,

        /// <summary>
        /// Format: ":$string"
        /// </summary>
        Reply_Info = 371,

        /// <summary>
        /// Marks the end of the INFO command's reply batch.
        /// <para>Format: ":End of /INFO list"</para>
        /// </summary>
        Reply_EndOfInfo = 374,

        /// <summary>
        /// Marks the start of the MOTD command's reply batch.
        /// <para>Format: ":- $server Message of the day - "</para>
        /// </summary>
        Reply_MotdStart = 375,

        /// <summary>
        /// Format: ":- $text"
        /// </summary>
        Reply_Motd = 372,

        /// <summary>
        /// Marks the end of the MOTD command's reply batch.
        /// <para>Format: ":End of /MOTD command"</para>
        /// </summary>
        Reply_EndOfMotd = 376,

        /// <summary>
        /// Sent back to a client which has just successfully issued
        /// an OPER message and gained operator status.
        /// <para>Format: ":You are now an IRC operator"</para>
        /// </summary>
        Reply_YoureOper = 381,

        /// <summary>
        /// Format: "$configFile :Rehashing"
        /// </summary>
        Reply_Rehashing = 382,

        /// <summary>
        /// Sent by the server to a service upon successful registration.
        /// <para>Format: ":You are service $servicename"</para>
        /// </summary>
        Reply_YoureService = 383,

        /// <summary>
        /// Format: "$server :$stringShowingServer'sLocalTime"
        /// </summary>
        Reply_Time = 391,

        /// <summary>
        /// Marks the start of the USERS command's reply batch.
        /// <para>Format: ":UserID'space''space''space'Terminal'space''space'Host"</para>
        /// </summary>
        Reply_UsersStart = 392,

        /// <summary>
        /// Format: ":%-8s %-9s %-8s"
        /// </summary>
        Reply_Users = 393,

        /// <summary>
        /// Marks the end of the USERS command's reply batch.
        /// <para>Format: ":End of users"</para>
        /// </summary>
        Reply_EndOfUsers = 394,

        /// <summary>
        /// Format: ":Nobody logged in"
        /// </summary>
        Reply_NoUsers = 395,

        /// <summary>
        /// Format: "Link $versionAndDebugLevel $destination $nextServer"
        /// </summary>
        Reply_TraceLink = 200,

        /// <summary>
        /// Format: "Try. $class $server"
        /// </summary>
        Reply_TraceConnecting = 201,

        /// <summary>
        /// Format: "H.S. $class $server"
        /// </summary>
        Reply_TraceHandshake = 202,

        /// <summary>
        /// Format: "???? $class [$clientIpAddressInDotForm]"
        /// </summary>
        Reply_TraceUnknow = 203,

        /// <summary>
        /// Format: "Oper $class $nick"
        /// </summary>
        Reply_TraceOperator = 204,

        /// <summary>
        /// Format: "User $class $nick"
        /// </summary>
        Reply_TraceUser = 205,

        /// <summary>
        /// Format: "Serv $class $int'S' $int'C' $server ($nick!$user|*!*)@($host|$server)"
        /// </summary>
        Reply_TraceServer = 206,

        /// <summary>
        /// Format: "Service $class $name $type $activeType"
        /// </summary>
        Reply_TraceService = 207,

        /// <summary>
        /// Format: "$newtype 0 $clientName"
        /// </summary>
        Reply_TraceNewType = 208,

        /// <summary>
        /// Format: "Class $class $count"
        /// </summary>
        Reply_TraceClass = 209,

        /// <summary>
        /// Unused.
        /// </summary>
        Reply_TraceReconnect = 210,

        /// <summary>
        /// Format: "File $logfile $debugLevel"
        /// </summary>
        Reply_TraceLog = 261,

        /// <summary>
        /// Format: "$linkname $sendq $sentMessages $sentKBytes $receivedMessages $receivedKBytes $timeOpen"
        /// </summary>
        Reply_StatsLinkInfo = 211,

        /// <summary>
        /// Reports statistics on commands usage.
        /// <para>Format: "$command $count $byteCount $remoteCount"</para>
        /// </summary>
        Reply_StatsCommands = 212,

        /// <summary>
        /// Format: "C $host '*' $name $port $class"
        /// </summary>
        Reply_StatsCLine = 213,

        /// <summary>
        /// Format: "N $host '*' $name $port $class"
        /// </summary>
        Reply_StatsNLine = 214,

        /// <summary>
        /// Format: "I $host '*' $host $port $class"
        /// </summary>
        Reply_StatsILine = 215,

        /// <summary>
        /// Format: "K $host '*' $username $port $class"
        /// </summary>
        Reply_StatsKLine = 216,

        /// <summary>
        /// Format: "Y $class $pingFrequency $connectFrequency $maxSendq>"
        /// </summary>
        Reply_StatsYLine = 218,

        /// <summary>
        /// Marks the end of that letter's STATS report.
        /// <para>Format: "$statsLetter :End of /STATS report"</para>
        /// </summary>
        Reply_EndOfStats = 219,

        /// <summary>
        /// Format: "L $hostmask '*' $servername maxdepth"
        /// </summary>
        Reply_StatsLLine = 241,

        /// <summary>
        /// Format: ":Server Up %d days %d:%02d:%02d"
        /// </summary>
        Reply_StatsUptime = 242,

        /// <summary>
        /// Format: "O $hostmask '*' $name"
        /// </summary>
        Reply_StatsOLine = 243,

        /// <summary>
        /// Format: "H $hostmask '*' $servername"
        /// </summary>
        Reply_StatsHLine = 244,

        /// <summary>
        /// The own user mode. (MODE $ownNick)
        /// <para>Format: "$userModeString"</para>
        /// </summary>
        Reply_UserModeIs = 221,

        /// <summary>
        /// Format: "$name $server $mask $type $hopcount $info"
        /// </summary>
        Reply_Servicelist = 234,

        /// <summary>
        /// Marks the end of the SERVLIST command's reply batch.
        /// <para>Format: "$mask $type :End of service listing"</para>
        /// </summary>
        Reply_ServicelistEnd = 235,

        /// <summary>
        /// ":There are $integer users and $integer invisible on $integer servers"
        /// </summary>
        Reply_LUserClient = 251,

        /// <summary>
        /// Format: "$integer :operator(s) online"
        /// </summary>
        Reply_LUserOperator = 252,

        /// <summary>
        /// Format: "$integer :unknown connection(s)"
        /// </summary>
        Reply_LUserUnknown = 253,

        /// <summary>
        /// Format: "$integer :channels formed"
        /// </summary>
        Reply_LUserChannels = 254,

        /// <summary>
        /// Format: ":I have $integer clients and $integer servers"
        /// </summary>
        Reply_LUserMe = 255,

        /// <summary>
        /// Format: "$server :Administrative info"
        /// </summary>
        Reply_AdminMe = 256,

        /// <summary>
        /// Format: ":$admin info"
        /// </summary>
        Reply_AdminLoc1 = 257,

        /// <summary>
        /// Format: ":$admin info"
        /// </summary>
        Reply_AdminLoc2 = 258,

        /// <summary>
        /// Format: ":$admin info"
        /// </summary>
        Reply_AdminEMail = 259,

        /// <summary>
        /// When a server drops a command without processing it, it MUST
        /// use the reply RPL_TRYAGAIN to inform the originating client.
        /// <para>Format: "$command :Please wait a while and try again."</para>
        /// </summary>
        Reply_TryAgain = 263
    }
}