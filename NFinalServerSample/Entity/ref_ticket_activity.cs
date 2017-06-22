using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServerSample.Entity
{
    /// <summary>
    /// ref_ticket_activity
    ///</summary>
    public class ref_ticket_activity
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// ticket_id
        ///</summary>
        public System.Int32? ticket_id { get; set; }
        /// <summary>
        /// num
        ///</summary>
        public System.Int32? num { get; set; }
        /// <summary>
        /// activity_id
        ///</summary>
        public System.Int32? activity_id { get; set; }
    }
}