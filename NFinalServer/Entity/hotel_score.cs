using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// hotel_score
    ///</summary>
    public class hotel_score
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
        /// hotel_name
        ///</summary>
        public System.String hotel_name { get; set; }
        /// <summary>
        /// total_score
        ///</summary>
        public System.Int32? total_score { get; set; }
        /// <summary>
        /// left_pay_score
        ///</summary>
        public System.Int32? left_pay_score { get; set; }
        /// <summary>
        /// left_sale_score
        ///</summary>
        public System.Int32? left_sale_score { get; set; }
    }
}