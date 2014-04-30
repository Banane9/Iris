using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Irc
{
    [Flags]
    public enum UserModes
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// User receives server notices
        /// </summary>
        s = 1,

        /// <summary>
        /// User receives Wallops
        /// </summary>
        w = 1 << 1,

        /// <summary>
        /// User is invisible
        /// </summary>
        i = 1 << 2,

        /// <summary>
        /// User is operator
        /// </summary>
        o = 1 << 3
    }
}