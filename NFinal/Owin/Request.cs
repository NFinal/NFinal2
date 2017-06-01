//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : Request.cs
//        Description :基于Owin的请求信息。
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NFinal.Owin
{
    /// <summary>
    /// 从浏览器传过来的请求数据
    /// </summary>
    public class Request
    {
        /// <summary>
        /// 请求头
        /// </summary>
        public IDictionary<string,string[]> headers;
        /// <summary>
        /// 请求头中包含的Cookie信息
        /// </summary>
        public IDictionary<string,string> cookies;
        /// <summary>
        /// Http请求参数
        /// </summary>
        public NameValueCollection parameters;
        /// <summary>
        /// 请求中包含的内容，例如POST请求
        /// </summary>
        public Stream stream;
        /// <summary>
        /// 请求方法
        /// </summary>
        public NFinal.Http.MethodType methodType;
        /// <summary>
        /// Post请求中包含的文件流
        /// </summary>
        public IDictionary<string, NFinal.Http.HttpMultipart.HttpFile> files;
        /// <summary>
        /// 请求的绝对路径,例：/Index.html
        /// </summary>
        public string requestPath;
        /// <summary>
        /// 请求的queryString
        /// </summary>
        public string queryString;
        /// <summary>
        /// owin框架
        /// </summary>
        public IDictionary<string, object> environment;
        /// <summary>
        /// 请求的根路径，例：http://www.x.com
        /// </summary>
        public string requestRoot
        {
            get {
                return environment.GetRequestRoot(headers);
            }
        }

    }
}
