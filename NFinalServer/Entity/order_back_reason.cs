using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// order_back_reason
    ///</summary>
    public class order_back_reason
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// reason_id
        ///</summary>
        public System.Int32 reason_id { get; set; }
        /// <summary>
        /// reason
        ///</summary>
        public System.String reason { get; set; }
        /// <summary>
        /// isstop
        ///</summary>
        public System.Int32? isstop { get; set; }
    }
}