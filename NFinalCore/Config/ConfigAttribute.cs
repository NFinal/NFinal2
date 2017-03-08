using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// 配置文件加载类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigAttribute:Attribute
    {
        public ConfigAttribute()
        {

        }
    }
}
