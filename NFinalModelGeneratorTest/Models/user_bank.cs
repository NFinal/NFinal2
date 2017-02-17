using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalModelGeneratorTest.Models
{
    /// <summary>
    /// user_bank
    ///</summary>
    public class user_bank
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// bank_card
        ///</summary>
        public System.String bank_card { get; set; }
        /// <summary>
        /// bank_type
        ///</summary>
        public System.String bank_type { get; set; }
        /// <summary>
        /// bank_user
        ///</summary>
        public System.String bank_user { get; set; }
        /// <summary>
        /// bank_address
        ///</summary>
        public System.String bank_address { get; set; }
        /// <summary>
        /// user_id
        ///</summary>
        public System.Int32? user_id { get; set; }
        /// <summary>
        /// bank_key
        ///</summary>
        public System.String bank_key { get; set; }
        /// <summary>
        /// bank_name
        ///</summary>
        public System.String bank_name { get; set; }
        /// <summary>
        /// creator
        ///</summary>
        public System.Int32? creator { get; set; }
        /// <summary>
        /// create_time
        ///</summary>
        public System.DateTime? create_time { get; set; }
        /// <summary>
        /// last_modifier
        ///</summary>
        public System.Int32? last_modifier { get; set; }
        /// <summary>
        /// last_modifytime
        ///</summary>
        public System.DateTime? last_modifytime { get; set; }
        /// <summary>
        /// default_bank
        ///</summary>
        public System.Int32? default_bank { get; set; }
    }
}