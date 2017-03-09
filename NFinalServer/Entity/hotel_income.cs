using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// hotel_income
    ///</summary>
    public class hotel_income
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// hotel_id
        ///</summary>
        public System.Int32? hotel_id { get; set; }
        /// <summary>
        /// year
        ///</summary>
        public System.Int32? year { get; set; }
        /// <summary>
        /// month
        ///</summary>
        public System.Int32? month { get; set; }
        /// <summary>
        /// total_price
        ///</summary>
        public System.Single? total_price { get; set; }
        /// <summary>
        /// status
        ///</summary>
        public System.Int32? status { get; set; }
        /// <summary>
        /// create_time
        ///</summary>
        public System.DateTime? create_time { get; set; }
        /// <summary>
        /// enter_user
        ///</summary>
        public System.Int32? enter_user { get; set; }
        /// <summary>
        /// enter_time
        ///</summary>
        public System.DateTime? enter_time { get; set; }
        /// <summary>
        /// pay_user
        ///</summary>
        public System.Int32? pay_user { get; set; }
        /// <summary>
        /// pay_time
        ///</summary>
        public System.DateTime? pay_time { get; set; }
        /// <summary>
        /// manage_type
        ///</summary>
        public System.Int32? manage_type { get; set; }
    }
}