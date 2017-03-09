using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// finance_bill_log
    ///</summary>
    public class finance_bill_log
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// financeid
        ///</summary>
        public System.Int32? financeid { get; set; }
        /// <summary>
        /// status
        ///</summary>
        public System.Int32? status { get; set; }
        /// <summary>
        /// opt_date
        ///</summary>
        public System.DateTime? opt_date { get; set; }
        /// <summary>
        /// opt_user
        ///</summary>
        public System.Int32? opt_user { get; set; }
        /// <summary>
        /// memo
        ///</summary>
        public System.String memo { get; set; }
    }
}