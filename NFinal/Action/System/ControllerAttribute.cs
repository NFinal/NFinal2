using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// Controller控制器名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false,Inherited =true)]
    public class ControllerAttribute : Attribute
    {
        public string Name { get; set; }
        public ControllerAttribute()
        {
            this.Name = string.Empty;
        }
        public ControllerAttribute(string name)
        {
            this.Name = name;
        }
    }
    /// <summary>
    /// 区域
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AreaAttribute : Attribute
    {
        public string Name { get; set; }
        public AreaAttribute(string name)
        {
            this.Name = name;
        }
    }
    /// <summary>
    /// 二级域名
    /// </summary>
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple =false,Inherited =true)]
    public class SubDomainAttribute : Attribute
    {
        public string Name { get;  }
        public SubDomainAttribute(string name)
        {
            this.Name = name;
        }
    }
}
