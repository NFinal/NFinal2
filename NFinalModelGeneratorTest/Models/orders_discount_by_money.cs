using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalModelGeneratorTest.Models
{
    /// <summary>
    /// orders_discount_by_money
    ///</summary>
    public class orders_discount_by_money
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
        /// discountid
        ///</summary>
        public System.Int32? discountid { get; set; }
        /// <summary>
        /// name
        ///</summary>
        public System.String name { get; set; }
        /// <summary>
        /// money
        ///</summary>
        public System.Int32? money { get; set; }
        /// <summary>
        /// discount
        ///</summary>
        public System.Int32? discount { get; set; }
        /// <summary>
        /// memo
        ///</summary>
        public System.String memo { get; set; }
    }
}