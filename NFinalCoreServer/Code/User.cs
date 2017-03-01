using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace NFinalCoreServer.Code
{
    /// <summary>
    /// 用于缓存的用户数据
    /// </summary>
    [ProtoContract]
    public class User
    {
        /// <summary>
        /// protobuf专用属性
        /// </summary>
        [ProtoMember(1)]
        public int userId;
        /// <summary>
        /// protobuf专用属性
        /// </summary>
        [ProtoMember(2)]
        public string userName;
    }
}
