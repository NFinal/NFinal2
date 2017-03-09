using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// orders_discount_by_day
    ///</summary>
    public class orders_discount_by_day
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// fid
        ///</summary>
        public System.Int32? fid { get; set; }
        /// <summary>
        /// orderid
        ///</summary>
        public System.Int32? orderid { get; set; }
        /// <summary>
        /// name
        ///</summary>
        public System.String name { get; set; }
        /// <summary>
        /// day_num
        ///</summary>
        public System.Int32? day_num { get; set; }
        /// <summary>
        /// memo
        ///</summary>
        public System.String memo { get; set; }
        /// <summary>
        /// discount
        ///</summary>
        public System.Single? discount { get; set; }
        /// <summary>
        /// discountid
        ///</summary>
        public System.Int32? discountid { get; set; }
    }
}