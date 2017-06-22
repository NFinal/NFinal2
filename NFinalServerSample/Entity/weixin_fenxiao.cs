using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServerSample.Entity
{
    /// <summary>
    /// weixin_fenxiao
    ///</summary>
    public class weixin_fenxiao
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// nickname
        ///</summary>
        public System.String nickname { get; set; }
        /// <summary>
        /// openid
        ///</summary>
        public System.String openid { get; set; }
        /// <summary>
        /// groupid
        ///</summary>
        public System.Int32? groupid { get; set; }
        /// <summary>
        /// phone
        ///</summary>
        public System.String phone { get; set; }
    }
}