using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServerSample.Entity
{
    /// <summary>
    /// message_log_new
    ///</summary>
    public class message_log_new
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// to_user
        ///</summary>
        public System.Int32? to_user { get; set; }
        /// <summary>
        /// log_type
        ///</summary>
        public System.Int32? log_type { get; set; }
        /// <summary>
        /// log_status
        ///</summary>
        public System.Int32? log_status { get; set; }
        /// <summary>
        /// log_level
        ///</summary>
        public System.Int32? log_level { get; set; }
        /// <summary>
        /// title
        ///</summary>
        public System.String title { get; set; }
        /// <summary>
        /// content
        ///</summary>
        public System.String content { get; set; }
        /// <summary>
        /// img_url
        ///</summary>
        public System.String img_url { get; set; }
        /// <summary>
        /// url_type
        ///</summary>
        public System.Int32? url_type { get; set; }
        /// <summary>
        /// log_date
        ///</summary>
        public System.DateTime? log_date { get; set; }
        /// <summary>
        /// orderid
        ///</summary>
        public System.Int32? orderid { get; set; }
        /// <summary>
        /// order_type
        ///</summary>
        public System.Int32? order_type { get; set; }
        /// <summary>
        /// order_status
        ///</summary>
        public System.Int32? order_status { get; set; }
        /// <summary>
        /// mess_type
        ///</summary>
        public System.Int32? mess_type { get; set; }
    }
}