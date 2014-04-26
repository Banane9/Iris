using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.Irc.Messages
{
    public enum MessageTypes
    {
        None,
        Numerical,
        Notice,
        Private,
        Nick,
        Join,
        Part,
        Quit
    }
}