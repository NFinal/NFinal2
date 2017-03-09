using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// order_pay_log_new
    ///</summary>
    public class order_pay_log_new
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// order_id
        ///</summary>
        public System.Int32? order_id { get; set; }
        /// <summary>
        /// order_type
        ///</summary>
        public System.Int32? order_type { get; set; }
        /// <summary>
        /// order_pay_type
        ///</summary>
        public System.Int32? order_pay_type { get; set; }
        /// <summary>
        /// total_fee
        ///</summary>
        public System.Single? total_fee { get; set; }
        /// <summary>
        /// total_score
        ///</summary>
        public System.Int32? total_score { get; set; }
        /// <summary>
        /// user_id
        ///</summary>
        public System.Int32? user_id { get; set; }
        /// <summary>
        /// hotel_id
        ///</summary>
        public System.Int32? hotel_id { get; set; }
        /// <summary>
        /// order_status
        ///</summary>
        public System.Int32? order_status { get; set; }
        /// <summary>
        /// opt_id
        ///</summary>
        public System.Int32? opt_id { get; set; }
        /// <summary>
        /// pay_opt_time
        ///</summary>
        public System.DateTime? pay_opt_time { get; set; }
        /// <summary>
        /// pay_dir
        ///</summary>
        public System.Int32? pay_dir { get; set; }
        /// <summary>
        /// memo
        ///</summary>
        public System.String memo { get; set; }
        /// <summary>
        /// order_detail_type
        ///</summary>
        public System.Int32? order_detail_type { get; set; }
        /// <summary>
        /// real_price
        ///</summary>
        public System.Single? real_price { get; set; }
        /// <summary>
        /// zhekou
        ///</summary>
        public System.Int32? zhekou { get; set; }
        /// <summary>
        /// is_tixian
        ///</summary>
        public System.Int32? is_tixian { get; set; }
    }
}