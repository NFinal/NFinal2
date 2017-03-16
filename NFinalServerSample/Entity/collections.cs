using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// collections
    ///</summary>
    public class collections
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// model_type
        ///</summary>
        public System.Int32? model_type { get; set; }
        /// <summary>
        /// fid
        ///</summary>
        public System.Int32? fid { get; set; }
        /// <summary>
        /// uid
        ///</summary>
        public System.Int32? uid { get; set; }
        /// <summary>
        /// zf_fy_id
        ///</summary>
        public System.Int32? zf_fy_id { get; set; }
        /// <summary>
        /// create_time
        ///</summary>
        public System.DateTime? create_time { get; set; }
    }
}