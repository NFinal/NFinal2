//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : OwinKeys.cs
//        Description :Owin字典常量。
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

namespace NFinal.Owin
{
    /// <summary>
    /// Owin字典常量
    /// </summary>
    public struct OwinKeys
    {
        /// <summary>
        /// 请示头
        /// </summary>
        public const string RequestHeaders = "owin.RequestHeaders";
        /// <summary>
        /// 请求方法
        /// </summary>
        public const string RequestMethod = "owin.RequestMethod";
        /// <summary>
        /// 请求路径
        /// </summary>
        public const string RequestPath = "owin.RequestPath";
        /// <summary>
        /// 请求基础路径，默认为空
        /// </summary>
        public const string RequestPathBase = "owin.RequestPathBase";
        /// <summary>
        /// Http协议
        /// </summary>
        public const string RequestProtocol = "owin.RequestProtocol";
        /// <summary>
        /// QueryString
        /// </summary>
        public const string RequestQueryString = "owin.RequestQueryString";
        /// <summary>
        /// Http:// Https://
        /// </summary>
        public const string RequestScheme = "owin.RequestScheme";
        /// <summary>
        /// 请求内容
        /// </summary>
        public const string RequestBody = "owin.RequestBody";
        /// <summary>
        /// 响应头
        /// </summary>
        public const string ResponseHeaders = "owin.ResponseHeaders";
        /// <summary>
        /// 响应代码
        /// </summary>
        public const string ResponseStatusCode = "owin.ResponseStatusCode";
        /// <summary>
        /// 响应内容
        /// </summary>
        public const string ResponseBody = "owin.ResponseBody";
        /// <summary>
        /// 响应代码描述语句
        /// </summary>
        public const string ResponseReasonPhrase = "owin.ResponseReasonPhrase";
        /// <summary>
        /// 响应代码协议(HTTP/1.0)
        /// </summary>
        public const string ResponseProtocol = "owin.ResponseProtocol";
        /// <summary>
        /// 响应终止回调代理
        /// </summary>
        public const string CallCancelled = "owin.CallCancelled";
        /// <summary>
        /// Owin版本
        /// </summary>
        public const string Version = "owin.Version";
        /// <summary>
        /// 客户IP
        /// </summary>
        public const string RemoteIpAddress = "server.RemoteIpAddress";
        /// <summary>
        /// 客户端口
        /// </summary>
        public const string RemotePort = "server.RemotePort";
        /// <summary>
        /// 服务器IP
        /// </summary>
        public const string LocalIpAddress="server.LocalIpAddress";
        /// <summary>
        /// 服务器端口
        /// </summary>
        public const string LocalPort = "server.LocalPort";
        /// <summary>
        /// 请求是否是本地请求
        /// </summary>
        public const string IsLocal = "server.IsLocal";
    }
}
