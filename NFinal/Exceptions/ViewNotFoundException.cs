//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ViewNotFoundException.cs
//        Description :模板未找到异常。（通常是因为模板路径错误！或者未安装NFinalCompiler插件）
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
    /// 模板未找到异常。（通常是因为模板路径错误！或者未安装NFinalCompiler插件）
    /// </summary>
    public class ViewNotFoundException:System.Exception
    {
        /// <summary>
        /// 模板未找到异常。（通常是因为模板路径错误！或者未安装NFinalCompiler插件）
        /// </summary>
        /// <param name="url"></param>
        public ViewNotFoundException(string url)
            :base("模板未找到！请确认是否安装NFinalCompiler插件，并检查模板路径是否有误！所需模板路径为:" + url)
        {
        }
    }
}
