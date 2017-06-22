using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServerSample.Entity
{
    /// <summary>
    /// service_type
    ///</summary>
    public class service_type
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// service_type_name
        ///</summary>
        public System.String service_type_name { get; set; }
        /// <summary>
        /// service_type_no
        ///</summary>
        public System.Int32? service_type_no { get; set; }
        /// <summary>
        /// duty
        ///</summary>
        public System.String duty { get; set; }
    }
}