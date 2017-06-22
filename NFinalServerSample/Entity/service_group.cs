using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServerSample.Entity
{
    /// <summary>
    /// service_group
    ///</summary>
    public class service_group
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// group_name
        ///</summary>
        public System.String group_name { get; set; }
        /// <summary>
        /// service_type_no
        ///</summary>
        public System.Int32? service_type_no { get; set; }
        /// <summary>
        /// managerid
        ///</summary>
        public System.Int32? managerid { get; set; }
    }
}