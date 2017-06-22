using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServerSample.Entity
{
    /// <summary>
    /// activity
    ///</summary>
    public class activity
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// name
        ///</summary>
        public System.String name { get; set; }
        /// <summary>
        /// ticket_title
        ///</summary>
        public System.String ticket_title { get; set; }
        /// <summary>
        /// fx_id
        ///</summary>
        public System.Int32? fx_id { get; set; }
        /// <summary>
        /// price
        ///</summary>
        public System.Single? price { get; set; }
        /// <summary>
        /// memo
        ///</summary>
        public System.String memo { get; set; }
        /// <summary>
        /// status
        ///</summary>
        public System.Int32? status { get; set; }
        /// <summary>
        /// create_time
        ///</summary>
        public System.DateTime? create_time { get; set; }
        /// <summary>
        /// modify_time
        ///</summary>
        public System.DateTime? modify_time { get; set; }
        /// <summary>
        /// start_date
        ///</summary>
        public System.DateTime? start_date { get; set; }
        /// <summary>
        /// end_date
        ///</summary>
        public System.DateTime? end_date { get; set; }
        /// <summary>
        /// date_flag
        ///</summary>
        public System.Int32? date_flag { get; set; }
        /// <summary>
        /// book_num
        ///</summary>
        public System.Int32? book_num { get; set; }
        /// <summary>
        /// pay_num
        ///</summary>
        public System.Int32? pay_num { get; set; }
    }
}