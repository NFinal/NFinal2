using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServerSample.Entity
{
    /// <summary>
    /// fenxiaoshang_weixin
    ///</summary>
    public class fenxiaoshang_weixin
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// name
        ///</summary>
        public System.String name { get; set; }
        /// <summary>
        /// phone
        ///</summary>
        public System.String phone { get; set; }
        /// <summary>
        /// pwd
        ///</summary>
        public System.String pwd { get; set; }
        /// <summary>
        /// create_time
        ///</summary>
        public System.DateTime? create_time { get; set; }
        /// <summary>
        /// address
        ///</summary>
        public System.String address { get; set; }
    }
}