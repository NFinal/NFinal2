using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Entity
{
    /// <summary>
    /// fangyuan_details
    ///</summary>
    public class fangyuan_details
    {
        /// <summary>
        /// id
        ///</summary>
        public System.Int32 id { get; set; }
        /// <summary>
        /// fid
        ///</summary>
        public System.Int32? fid { get; set; }
        /// <summary>
        /// f_zip
        ///</summary>
        public System.Int32? f_zip { get; set; }
        /// <summary>
        /// ruzhuxuzhi
        ///</summary>
        public System.String ruzhuxuzhi { get; set; }
        /// <summary>
        /// jiaotongqingkuang
        ///</summary>
        public System.String jiaotongqingkuang { get; set; }
        /// <summary>
        /// zhoubianqingkuang
        ///</summary>
        public System.String zhoubianqingkuang { get; set; }
        /// <summary>
        /// miaoshu
        ///</summary>
        public System.String miaoshu { get; set; }
        /// <summary>
        /// xiaoquliangdian
        ///</summary>
        public System.String xiaoquliangdian { get; set; }
    }
}