using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// message_log
    ///</summary>
    public class message_log
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// from_user
        ///</summary>
        public System.Int32? from_user { get; set; }
        /// <summary>
        /// to_user
        ///</summary>
        public System.Int32? to_user { get; set; }
        /// <summary>
        /// log_type
        ///</summary>
        public System.Int32? log_type { get; set; }
        /// <summary>
        /// level
        ///</summary>
        public System.Int32? level { get; set; }
        /// <summary>
        /// status
        ///</summary>
        public System.Int32? status { get; set; }
        /// <summary>
        /// content
        ///</summary>
        public System.String content { get; set; }
        /// <summary>
        /// orderid
        ///</summary>
        public System.Int32? orderid { get; set; }
        /// <summary>
        /// log_date
        ///</summary>
        public System.DateTime? log_date { get; set; }
        /// <summary>
        /// url_type
        ///</summary>
        public System.Int32? url_type { get; set; }
        /// <summary>
        /// order_type
        ///</summary>
        public System.Int32? order_type { get; set; }
        /// <summary>
        /// to_user_name
        ///</summary>
        public System.String to_user_name { get; set; }
        /// <summary>
        /// out_trade_no
        ///</summary>
        public System.String out_trade_no { get; set; }
        /// <summary>
        /// order_status
        ///</summary>
        public System.Int32? order_status { get; set; }
    }
}