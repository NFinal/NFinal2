using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// cards
    ///</summary>
    public class cards
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// card_no
        ///</summary>
        public System.String card_no { get; set; }
        /// <summary>
        /// owner_id
        ///</summary>
        public System.Int32? owner_id { get; set; }
        /// <summary>
        /// user_id
        ///</summary>
        public System.Int32? user_id { get; set; }
        /// <summary>
        /// agent_id
        ///</summary>
        public System.Int32? agent_id { get; set; }
        /// <summary>
        /// create_time
        ///</summary>
        public System.DateTime? create_time { get; set; }
        /// <summary>
        /// creator
        ///</summary>
        public System.Int32? creator { get; set; }
        /// <summary>
        /// status
        ///</summary>
        public System.Int32? status { get; set; }
        /// <summary>
        /// zhekou
        ///</summary>
        public System.Int32? zhekou { get; set; }
        /// <summary>
        /// lastmodifier
        ///</summary>
        public System.Int32? lastmodifier { get; set; }
        /// <summary>
        /// lastmodify_time
        ///</summary>
        public System.DateTime? lastmodify_time { get; set; }
        /// <summary>
        /// hotel_id
        ///</summary>
        public System.Int32? hotel_id { get; set; }
    }
}