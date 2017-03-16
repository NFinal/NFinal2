using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal
{

    /// <summary>
    /// 二级域名
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class SubDomainAttribute : System.Attribute
    {
        public string Name { get; }
        public SubDomainAttribute(string name)
        {
            this.Name = name;
        }
    }
}