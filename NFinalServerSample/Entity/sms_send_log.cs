using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// sms_send_log
    ///</summary>
    public class sms_send_log
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// sms_type
        ///</summary>
        public System.Int32? sms_type { get; set; }
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
        /// sms_send_json
        ///</summary>
        public System.String sms_send_json { get; set; }
        /// <summary>
        /// sms_result_json
        ///</summary>
        public System.String sms_result_json { get; set; }
    }
}