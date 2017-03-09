using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// city
    ///</summary>
    public class city
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// code
        ///</summary>
        public System.String code { get; set; }
        /// <summary>
        /// name
        ///</summary>
        public System.String name { get; set; }
        /// <summary>
        /// provincecode
        ///</summary>
        public System.String provincecode { get; set; }
        /// <summary>
        /// isprice
        ///</summary>
        public System.Int32? isprice { get; set; }
        /// <summary>
        /// isscore
        ///</summary>
        public System.Int32? isscore { get; set; }
        /// <summary>
        /// firstletter
        ///</summary>
        public System.String firstletter { get; set; }
        /// <summary>
        /// ishotcity
        ///</summary>
        public System.Int32? ishotcity { get; set; }
    }
}