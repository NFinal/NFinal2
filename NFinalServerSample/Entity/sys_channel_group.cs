using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// sys_channel_group
    ///</summary>
    public class sys_channel_group
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// channel_group_no
        ///</summary>
        public System.Int32 channel_group_no { get; set; }
        /// <summary>
        /// channel_group_name
        ///</summary>
        public System.String channel_group_name { get; set; }
        /// <summary>
        /// icon_show
        ///</summary>
        public System.String icon_show { get; set; }
        /// <summary>
        /// idx
        ///</summary>
        public System.Int32? idx { get; set; }
    }
}