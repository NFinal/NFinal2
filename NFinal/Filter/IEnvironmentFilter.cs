//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : IEnvironmentFilter.cs
//        Description :Owin上下文过滤器接口
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
    /// Owin上下文过滤器接口
    /// </summary>
    public interface IEnvironmentFilter:IBaseFilter<IDictionary<string,object>>
    {
    }
}
