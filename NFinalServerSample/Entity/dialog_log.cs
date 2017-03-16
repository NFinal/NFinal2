using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// dialog_log
    ///</summary>
    public class dialog_log
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
        /// content
        ///</summary>
        public System.String content { get; set; }
        /// <summary>
        /// status
        ///</summary>
        public System.Int32? status { get; set; }
        /// <summary>
        /// log_date
        ///</summary>
        public System.DateTime? log_date { get; set; }
        /// <summary>
        /// fid
        ///</summary>
        public System.Int32? fid { get; set; }
        /// <summary>
        /// from_user_name
        ///</summary>
        public System.String from_user_name { get; set; }
        /// <summary>
        /// to_user_name
        ///</summary>
        public System.String to_user_name { get; set; }
        /// <summary>
        /// fname
        ///</summary>
        public System.String fname { get; set; }
    }
}