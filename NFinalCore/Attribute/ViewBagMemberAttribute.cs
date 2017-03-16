using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal
{
    /// <summary>
    /// 该成员将会自动添加到ViewBag中
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property,AllowMultiple =false,Inherited =true)]
    public class ViewBagMemberAttribute: System.Attribute
    {
        public ViewBagMemberAttribute()
        {
        }
    }
}
