//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : InvalidControllerNameException.cs
//        Description :控制器名称错误（必须为Controller后缀）
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

namespace NFinal.Exceptions
{
    /// <summary>
    /// 控制器名称错误（必须为Controller后缀）
    /// </summary>
    public class InvalidControllerNameException : System.Exception
    {
        /// <summary>
        /// 控制器名称错误
        /// </summary>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="name">名称</param>
        public InvalidControllerNameException(string nameSpace, string name)
            :base("控制器名称错误！必须为Controller后缀。当前名称为：" + name + ",所在命名空间：" + nameSpace)
        {
        }
    }
}
