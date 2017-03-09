using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// user_card
    ///</summary>
    public class user_card
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// user_id
        ///</summary>
        public System.Int32? user_id { get; set; }
        /// <summary>
        /// user_real_name
        ///</summary>
        public System.String user_real_name { get; set; }
        /// <summary>
        /// user_card_no
        ///</summary>
        public System.String user_card_no { get; set; }
        /// <summary>
        /// front_img_url
        ///</summary>
        public System.String front_img_url { get; set; }
        /// <summary>
        /// back_img_url
        ///</summary>
        public System.String back_img_url { get; set; }
        /// <summary>
        /// last_modify_time
        ///</summary>
        public System.DateTime? last_modify_time { get; set; }
        /// <summary>
        /// last_modifier
        ///</summary>
        public System.Int32? last_modifier { get; set; }
        /// <summary>
        /// status
        ///</summary>
        public System.Int32? status { get; set; }
        /// <summary>
        /// fail_cause
        ///</summary>
        public System.String fail_cause { get; set; }
    }
}