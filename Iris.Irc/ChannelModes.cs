using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.Irc
{
    [Flags]
    public enum ChannelModes
    {
        None = 0,

        //[]
        o = 1, // - give/take channel operator privileges;

        p = 1 << 1,// - private channel flag;
        s = 1 << 2,// - secret channel flag;
        i = 1 << 3, //- invite-only channel flag;
        t = 1 << 4, //- topic settable by channel operator only flag;
        n = 1 << 5,// - no messages to channel from clients on the outside;
        m = 1 << 6,// - moderated channel;
        l = 1 << 7 // - set the user limit to channel;
    }
}