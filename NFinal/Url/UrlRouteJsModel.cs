//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : UrlRouteJsModel.cs
//        Description :生成包含Url函数的javascript文件所需的视图数据
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

namespace NFinal.Url
{
    /// <summary>
    /// 生成包含Url函数的javascript文件所需的视图数据
    /// </summary>
    public class UrlRouteJsModel
    {
        /// <summary>
        /// 包含Url解析后相关的信息，用于生成Js函数时使用
        /// </summary>
        public NFinal.Collections.FastDictionary<RuntimeTypeHandle, Dictionary<string, FormatData>> formatControllerDictionary;
    }
}
