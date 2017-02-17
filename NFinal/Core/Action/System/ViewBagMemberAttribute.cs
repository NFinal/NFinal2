using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// 该成员将会自动添加到ViewBag中
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property,AllowMultiple =false,Inherited =true)]
    public class ViewBagMemberAttribute:Attribute
    {
        public ViewBagMemberAttribute()
        {
        }
    }
}
