using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// common_orders_user
    ///</summary>
    public class common_orders_user
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// userid
        ///</summary>
        public System.Int32? userid { get; set; }
        /// <summary>
        /// name
        ///</summary>
        public System.String name { get; set; }
        /// <summary>
        /// id_card
        ///</summary>
        public System.String id_card { get; set; }
        /// <summary>
        /// sex
        ///</summary>
        public System.Int32? sex { get; set; }
        /// <summary>
        /// birthday
        ///</summary>
        public System.String birthday { get; set; }
        /// <summary>
        /// telphone
        ///</summary>
        public System.String telphone { get; set; }
    }
}