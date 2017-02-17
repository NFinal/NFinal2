using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalModelGeneratorTest.Models
{
    /// <summary>
    /// feedback
    ///</summary>
    public class feedback
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// problem
        ///</summary>
        public System.String problem { get; set; }
        /// <summary>
        /// userid
        ///</summary>
        public System.Int32? userid { get; set; }
        /// <summary>
        /// createtime
        ///</summary>
        public System.DateTime? createtime { get; set; }
        /// <summary>
        /// reply_content
        ///</summary>
        public System.String reply_content { get; set; }
        /// <summary>
        /// is_anonymity
        ///</summary>
        public System.Int32? is_anonymity { get; set; }
        /// <summary>
        ///  replyer
        ///</summary>
        public System.Int32?  replyer { get; set; }
        /// <summary>
        /// replytime
        ///</summary>
        public System.DateTime? replytime { get; set; }
        /// <summary>
        /// status
        ///</summary>
        public System.Int32? status { get; set; }
    }
}