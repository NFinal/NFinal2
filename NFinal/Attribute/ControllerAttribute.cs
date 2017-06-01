//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ControllerAttribute.cs
//        Description :控制器名称设置特性
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
    /// Controller控制器名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false,Inherited =true)]
    public class ControllerAttribute : System.Attribute
    {
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 控制器名称设置特性
        /// </summary>
        /// <param name="name">控制器名称</param>
        public ControllerAttribute(string name)
        {
            this.Name = name;
        }
    }

}
