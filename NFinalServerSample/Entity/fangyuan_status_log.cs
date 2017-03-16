using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// fangyuan_status_log
    ///</summary>
    public class fangyuan_status_log
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
        /// userid
        ///</summary>
        public System.Int32? userid { get; set; }
        /// <summary>
        /// logtime
        ///</summary>
        public System.DateTime? logtime { get; set; }
        /// <summary>
        /// module_type
        ///</summary>
        public System.Int32? module_type { get; set; }
    }
}