using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServerSample.Entity
{
    /// <summary>
    /// sys_channel_new
    ///</summary>
    public class sys_channel_new
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// channel_group_no
        ///</summary>
        public System.Int32? channel_group_no { get; set; }
        /// <summary>
        /// channel_no
        ///</summary>
        public System.Int32? channel_no { get; set; }
        /// <summary>
        /// channel_name
        ///</summary>
        public System.String channel_name { get; set; }
        /// <summary>
        /// url
        ///</summary>
        public System.String url { get; set; }
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