using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// 二级域名设置
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SubDomainAttribute:Attribute
    {
        /// <summary>
        /// 二级域名设置
        /// </summary>
        /// <param name="subdomainName">二级域名</param>
        public SubDomainAttribute(string subdomainName)
        {

        }
    }
}
