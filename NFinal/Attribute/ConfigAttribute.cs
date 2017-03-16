using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal
{
    /// <summary>
    /// 配置文件加载类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigAttribute: System.Attribute
    {
        public ConfigAttribute()
        {

        }
    }
}
