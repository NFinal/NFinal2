using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// fangyuan_check_log
    ///</summary>
    public class fangyuan_check_log
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
        /// fid
        ///</summary>
        public System.Int32? fid { get; set; }
        /// <summary>
        /// status
        ///</summary>
        public System.Int32? status { get; set; }
        /// <summary>
        /// modifier_id
        ///</summary>
        public System.Int32? modifier_id { get; set; }
        /// <summary>
        /// modifier_time
        ///</summary>
        public System.DateTime? modifier_time { get; set; }
    }
}