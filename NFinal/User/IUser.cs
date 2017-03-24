using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf.Meta;

namespace NFinal.User
{
    public interface IUser
    {
        int Role { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        string Password { get; set; }
        /// <summary>
        /// 帐号
        /// </summary>
        string Account { get; set; }
        StringContainer this[string key]
        {
            get;
            set;
        }
    }
    public abstract class AbstractUser : IUser
    {
        private Dictionary<string, StringContainer> m_dic = null;
        public bool IsNotRole(int role)
        {
            return (role | this.Role) == 0;
        }
        //public AbstractUser(string[] keys)
        //{
        //    if (m_dic == null)
        //    {
        //        m_dic = new Dictionary<string, StringContainer>()
        //        {
        //            { "Id",StringContainer.Empty},
        //            { "Name",StringContainer.Empty},
        //            { "Password",StringContainer.Empty},
        //            { "Account",StringContainer.Empty}
        //        };
        //        for (int i = 0; i < keys.Length; i++)
        //        {
        //            m_dic.Add(keys[i], StringContainer.Empty);
        //        }
        //    }
        //}
        public StringContainer this[string key]
        {
            get
            {
                StringContainer value;
                bool hasValue = m_dic.TryGetValue(key, out value);
                if (hasValue)
                {
                    return value;
                }
                else
                {
                    return StringContainer.Empty;
                }

            }
            set
            {
                if (m_dic.ContainsKey(key))
                {
                    m_dic[key] = value;
                }
                else
                {
                    m_dic.Add(key, value);
                }
            }
        }
        public abstract string Id { get; set; }
        public abstract string Name { get; set; }
        public abstract string Password { get; set; }
        public abstract string Account { get; set; }
        public int Role { get; set; }
    }
    [ProtoBuf.ProtoContract]
    public class User : AbstractUser
    {
        [ProtoBuf.ProtoMember(0)]
        public Dictionary<string, StringContainer> m_dic = new Dictionary<string, StringContainer>();
        //public User(string[] keys):base(keys)
        //{
        //    if (m_dic == null)
        //    {
        //        m_dic.Add("Id", this.Id);
        //        m_dic.Add("Name", this.Name);
        //        m_dic.Add("Password",this.Password);
        //        m_dic.Add("Account", this.Account);
        //    }
        //}
        public override string Id { get; set; }
        public override string Name { get; set; }
        public override string Password { get; set; }
        public override string Account { get; set; }
    }
}
