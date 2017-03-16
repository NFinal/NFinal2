using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// orders_fangyuan_status
    ///</summary>
    public class orders_fangyuan_status
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
        /// fid
        ///</summary>
        public System.Int32? fid { get; set; }
        /// <summary>
        /// year
        ///</summary>
        public System.Int32? year { get; set; }
        /// <summary>
        /// month
        ///</summary>
        public System.Int32? month { get; set; }
        /// <summary>
        /// day
        ///</summary>
        public System.Int32? day { get; set; }
        /// <summary>
        /// ruzhu
        ///</summary>
        public System.Int32? ruzhu { get; set; }
        /// <summary>
        /// score
        ///</summary>
        public System.Int32? score { get; set; }
        /// <summary>
        /// price
        ///</summary>
        public System.Int32? price { get; set; }
        /// <summary>
        /// qty
        ///</summary>
        public System.Int32? qty { get; set; }
        /// <summary>
        /// total_qty
        ///</summary>
        public System.Int32? total_qty { get; set; }
        /// <summary>
        /// zhekou
        ///</summary>
        public System.Int32? zhekou { get; set; }
    }
}