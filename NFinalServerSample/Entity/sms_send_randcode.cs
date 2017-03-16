using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// sms_send_randcode
    ///</summary>
    public class sms_send_randcode
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// sms_page_type
        ///</summary>
        public System.Int32? sms_page_type { get; set; }
        /// <summary>
        /// sms_phone_num
        ///</summary>
        public System.String sms_phone_num { get; set; }
        /// <summary>
        /// sms_send_time
        ///</summary>
        public System.DateTime? sms_send_time { get; set; }
        /// <summary>
        /// sms_status
        ///</summary>
        public System.Int32? sms_status { get; set; }
        /// <summary>
        /// sms_rand_code
        ///</summary>
        public System.String sms_rand_code { get; set; }
        /// <summary>
        /// sms_identifier
        ///</summary>
        public System.String sms_identifier { get; set; }
    }
}