using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServerSample.Entity
{
    /// <summary>
    /// ticket
    ///</summary>
    public class ticket
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// ticket_name
        ///</summary>
        public System.String ticket_name { get; set; }
        /// <summary>
        /// total_ticket_num
        ///</summary>
        public System.Int32? total_ticket_num { get; set; }
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
    }
}