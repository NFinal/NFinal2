//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ViewModelTypeUnMatchedException.cs
//        Description :模板输入类型与所需类型不匹配。（通常是因为模板路径错误！）
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
    /// 模板输入类型与所需类型不匹配。（通常是因为模板路径错误！）
    /// </summary>
    public class ViewModelTypeUnMatchedException : System.Exception
    {
        /// <summary>
        /// 模板输入类型与所需类型不匹配。（通常是因为模板路径错误！）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="input"></param>
        /// <param name="need"></param>
        public ViewModelTypeUnMatchedException(string url,Type input,Type need)
            :base("模板输入类型与所需类型不匹配！请检查模板路径是否有误。\r\n模板路径为:" + url + "\r\n" +
                "输入类型为:" + input.FullName + "\r\n" +
                "所需类型为:" + need.FullName)
        {
        }
    }
}
