using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServerSample.Entity
{
    /// <summary>
    /// service_group_and_hotel_ref
    ///</summary>
    public class service_group_and_hotel_ref
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
        /// service_group_id
        ///</summary>
        public System.Int32? service_group_id { get; set; }
    }
}