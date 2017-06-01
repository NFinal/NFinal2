//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ViewAttribute.cs
//        Description :标识某字段或属性需要添加到视图数据中的特性
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal
{
    /// <summary>
    /// 标识某字段或属性需要添加到视图数据中的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property,AllowMultiple =false,Inherited =true)]
    public class ViewBagMemberAttribute: System.Attribute
    {
        /// <summary>
        /// 该成员将会自动添加到ViewBag视图数据中
        /// </summary>
        public ViewBagMemberAttribute()
        {
        }
    }
}
