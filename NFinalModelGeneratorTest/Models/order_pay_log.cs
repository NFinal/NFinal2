using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalModelGeneratorTest.Models
{
    /// <summary>
    /// order_pay_log
    ///</summary>
    public class order_pay_log
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
        /// order_status
        ///</summary>
        public System.Int32? order_status { get; set; }
        /// <summary>
        /// pay_opt_time
        ///</summary>
        public System.DateTime? pay_opt_time { get; set; }
        /// <summary>
        /// is_pay
        ///</summary>
        public System.Int32? is_pay { get; set; }
        /// <summary>
        /// trade_no
        ///</summary>
        public System.String trade_no { get; set; }
        /// <summary>
        /// buyer_email
        ///</summary>
        public System.String buyer_email { get; set; }
        /// <summary>
        /// buyer_id
        ///</summary>
        public System.String buyer_id { get; set; }
        /// <summary>
        /// from_user
        ///</summary>
        public System.Int32? from_user { get; set; }
        /// <summary>
        /// to_user
        ///</summary>
        public System.Int32? to_user { get; set; }
        /// <summary>
        /// pay_dir
        ///</summary>
        public System.Int32? pay_dir { get; set; }
        /// <summary>
        /// memo
        ///</summary>
        public System.String memo { get; set; }
        /// <summary>
        /// hotel_id
        ///</summary>
        public System.Int32? hotel_id { get; set; }
    }
}