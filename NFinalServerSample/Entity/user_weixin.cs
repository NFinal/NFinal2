using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServerSample.Entity
{
    /// <summary>
    /// user_weixin
    ///</summary>
    public class user_weixin
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
        /// certificate
        ///</summary>
        public System.String certificate { get; set; }
        /// <summary>
        /// activity_id
        ///</summary>
        public System.Int32? activity_id { get; set; }
        /// <summary>
        /// create_time
        ///</summary>
        public System.DateTime? create_time { get; set; }
        /// <summary>
        /// wx_name
        ///</summary>
        public System.String wx_name { get; set; }
        /// <summary>
        /// openid
        ///</summary>
        public System.String openid { get; set; }
        /// <summary>
        /// is_pay
        ///</summary>
        public System.Int32? is_pay { get; set; }
        /// <summary>
        /// book_num
        ///</summary>
        public System.Int32? book_num { get; set; }
    }
}