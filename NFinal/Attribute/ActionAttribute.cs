//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ActionAttribute.cs
//        Description :对控制器行为进行重命名的特性
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal
{
    /// <summary>
    /// 对控制器行为进行重命名的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false,Inherited =false)]
    public class ActionAttribute:System.Attribute
    {
        /// <summary>
        /// 控制器行为重命名
        /// </summary>
        /// <param name="name">控制器行为名称</param>
        public ActionAttribute(string name){
            this.Name = name;
        }
        /// <summary>
        /// 控制器行为名称
        /// </summary>
        public string Name { get; set; }
    }
    
}
