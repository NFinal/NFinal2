using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// order_evaluate
    ///</summary>
    public class order_evaluate
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// user_id
        ///</summary>
        public System.String user_id { get; set; }
        /// <summary>
        /// fangyuanname
        ///</summary>
        public System.String fangyuanname { get; set; }
        /// <summary>
        /// fid
        ///</summary>
        public System.Int32? fid { get; set; }
        /// <summary>
        /// orderid
        ///</summary>
        public System.String orderid { get; set; }
        /// <summary>
        /// log_date
        ///</summary>
        public System.DateTime? log_date { get; set; }
        /// <summary>
        /// zbhj_score
        ///</summary>
        public System.Int32? zbhj_score { get; set; }
        /// <summary>
        /// futd_score
        ///</summary>
        public System.Int32? futd_score { get; set; }
        /// <summary>
        /// zhss_score
        ///</summary>
        public System.Int32? zhss_score { get; set; }
        /// <summary>
        /// tpzsd_score
        ///</summary>
        public System.Int32? tpzsd_score { get; set; }
        /// <summary>
        /// myd_score
        ///</summary>
        public System.Int32? myd_score { get; set; }
        /// <summary>
        /// fkyx_label
        ///</summary>
        public System.String fkyx_label { get; set; }
        /// <summary>
        /// pj_content
        ///</summary>
        public System.String pj_content { get; set; }
        /// <summary>
        /// img_url
        ///</summary>
        public System.String img_url { get; set; }
        /// <summary>
        /// is_hide_name
        ///</summary>
        public System.Int32? is_hide_name { get; set; }
        /// <summary>
        /// sale_pj_content
        ///</summary>
        public System.String sale_pj_content { get; set; }
        /// <summary>
        /// sale_Pj_date
        ///</summary>
        public System.DateTime? sale_Pj_date { get; set; }
        /// <summary>
        /// buy_add_content_1
        ///</summary>
        public System.String buy_add_content_1 { get; set; }
        /// <summary>
        /// buy_add_date_1
        ///</summary>
        public System.DateTime? buy_add_date_1 { get; set; }
        /// <summary>
        /// reply_content
        ///</summary>
        public System.String reply_content { get; set; }
        /// <summary>
        /// reply_date
        ///</summary>
        public System.DateTime? reply_date { get; set; }
        /// <summary>
        /// reply_name
        ///</summary>
        public System.String reply_name { get; set; }
        /// <summary>
        /// is_reply
        ///</summary>
        public System.Int32? is_reply { get; set; }
    }
}