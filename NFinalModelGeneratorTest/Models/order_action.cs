using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalModelGeneratorTest.Models
{
    /// <summary>
    /// order_action
    ///</summary>
    public class order_action
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// order_id
        ///</summary>
        public System.Int32? order_id { get; set; }
        /// <summary>
        /// action_user
        ///</summary>
        public System.Int32? action_user { get; set; }
        /// <summary>
        /// order_status
        ///</summary>
        public System.Int32? order_status { get; set; }
        /// <summary>
        /// log_time
        ///</summary>
        public System.DateTime? log_time { get; set; }
        /// <summary>
        /// order_type
        ///</summary>
        public System.Int32? order_type { get; set; }
        /// <summary>
        /// action_memo
        ///</summary>
        public System.String action_memo { get; set; }
        /// <summary>
        /// is_effect
        ///</summary>
        public System.Int32? is_effect { get; set; }
    }
}