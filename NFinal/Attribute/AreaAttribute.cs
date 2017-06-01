//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : AreaAttribute.cs
//        Description :控制器区域设置特性
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
    /// 控制器区域设置
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AreaAttribute : System.Attribute
    {
        /// <summary>
        /// 控制器区域名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 控制器区域设置
        /// </summary>
        /// <param name="name"></param>
        public AreaAttribute(string name)
        {
            this.Name = name;
        }
    }
}
