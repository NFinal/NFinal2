//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ModelNotFoundException.cs
//        Description :视图数据类型未生成异常，通常是没有安装NFinalCompiler插件
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
    /// 视图数据类型未生成异常，通常是没有安装NFinalCompiler插件
    /// </summary>
    public class ModelNotFoundException: System.Exception
    {
        /// <summary>
        /// 视图数据类型未生成异常，通常是没有安装NFinalCompiler插件
        /// </summary>
        /// <param name="fullName"></param>
        public ModelNotFoundException(string fullName)
            :base("ViewBag实体类未找到！请确认是否安装了NFinalCompiler插件。\r\n实体类型为:" + fullName)
        {
        }
    }
}
