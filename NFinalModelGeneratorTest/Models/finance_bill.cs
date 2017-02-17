using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalModelGeneratorTest.Models
{
    /// <summary>
    /// finance_bill
    ///</summary>
    public class finance_bill
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// hotelid
        ///</summary>
        public System.Int32? hotelid { get; set; }
        /// <summary>
        /// orderid
        ///</summary>
        public System.String orderid { get; set; }
        /// <summary>
        /// year
        ///</summary>
        public System.Int32? year { get; set; }
        /// <summary>
        /// month
        ///</summary>
        public System.Int32? month { get; set; }
        /// <summary>
        /// commission
        ///</summary>
        public System.Single? commission { get; set; }
        /// <summary>
        /// total_fee
        ///</summary>
        public System.Single? total_fee { get; set; }
        /// <summary>
        /// type
        ///</summary>
        public System.Int32? type { get; set; }
        /// <summary>
        /// status
        ///</summary>
        public System.Int32? status { get; set; }
        /// <summary>
        /// createtime
        ///</summary>
        public System.DateTime? createtime { get; set; }
        /// <summary>
        /// last_modify_time
        ///</summary>
        public System.DateTime? last_modify_time { get; set; }
        /// <summary>
        /// last_modifier
        ///</summary>
        public System.Int32? last_modifier { get; set; }
    }
}