using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigAttribute:Attribute
    {
        public ConfigAttribute()
        {

        }
    }
}
