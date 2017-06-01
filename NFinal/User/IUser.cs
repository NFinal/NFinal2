//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : IUser.cs
//        Description :用户接口，用于存储用户的基本信息
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf.Meta;

namespace NFinal.User
{
    /// <summary>
    /// 用户接口，用于存储用户的基本信息
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// 角色
        /// </summary>
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
        /// <summary>
        /// 属性索引
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        StringContainer this[string key]
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 用户抽象类
    /// </summary>
    public abstract class AbstractUser : IUser
    {
        private Dictionary<string, StringContainer> m_dic = null;
        /// <summary>
        /// 判断用户是否具有某个角色权限
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 自定义属性
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Id
        /// </summary>
        public abstract string Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public abstract string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public abstract string Password { get; set; }
        /// <summary>
        /// 帐号
        /// </summary>
        public abstract string Account { get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>
        public int Role { get; set; }
    }
    /// <summary>
    /// 用户信息
    /// </summary>
    [ProtoBuf.ProtoContract]
    public class User : AbstractUser
    {
        /// <summary>
        /// 用户自定义属性缓存
        /// </summary>
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
        /// <summary>
        /// 用户Id
        /// </summary>
        public override string Id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public override string Name { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public override string Password { get; set; }
        /// <summary>
        /// 用户帐号
        /// </summary>
        public override string Account { get; set; }
    }
}
