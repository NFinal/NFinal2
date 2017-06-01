//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : IResponseFilter.cs
//        Description :Http响应信息过滤器接口
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

namespace NFinal.Filter
{
    /// <summary>
    /// Http响应信息过滤器接口
    /// </summary>
    public interface IResponseFilter
    {
        /// <summary>
        /// 返回过滤器
        /// </summary>
        /// <param name="response">响应数据</param>
        /// <returns>是否中断输出</returns>
        bool ResponseFilter(NFinal.Owin.Response response);
    }
}
