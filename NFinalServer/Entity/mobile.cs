using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// mobile
    ///</summary>
    public class mobile
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// mobilecode
        ///</summary>
        public System.Int32? mobilecode { get; set; }
        /// <summary>
        /// province
        ///</summary>
        public System.String province { get; set; }
        /// <summary>
        /// city
        ///</summary>
        public System.String city { get; set; }
        /// <summary>
        /// corp
        ///</summary>
        public System.String corp { get; set; }
        /// <summary>
        /// areacode
        ///</summary>
        public System.Int32? areacode { get; set; }
        /// <summary>
        /// postcode
        ///</summary>
        public System.Int32? postcode { get; set; }
    }
}