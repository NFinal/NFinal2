using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace NFinalServerSample.Code
{
    /// <summary>
    /// 用于缓存的用户数据
    /// </summary>
    [ProtoContract]
    public class User : NFinal.User.AbstractUser
    {
        [ProtoMember(1)]
        public override string Id { get; set; }
        [ProtoMember(2)]
        public override string Name { get; set; }
        [ProtoMember(3)]
        public override string Password { get; set; }
        [ProtoMember(4)]
        public override string Account { get; set; }
    }
}
