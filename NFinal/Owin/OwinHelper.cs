//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : OwinHelper.cs
//        Description :Owin帮助类。
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;

namespace NFinal.Owin
{
    /// <summary>
    /// owin帮助类
    /// </summary>
    public struct OwinHelper
    {
        /// <summary>
        /// owin实体
        /// </summary>
        public IDictionary<string, object> Environment;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="environment"></param>
        public OwinHelper(IDictionary<string, object> environment)
        {
            this.Environment = environment;
        }
        /// <summary>
        /// 获取Cookie
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static NameValueCollection GetCookies(IDictionary<string, string[]> headers)
        {
            NameValueCollection cookie = new NameValueCollection();
            if (headers.ContainsKey("Cookie"))
            {
                string[] tempArray = headers["Cookie"][0].Split('&', '=');
                if ((tempArray.Length & 1) == 0)
                {
                    int len = tempArray.Length >> 1;
                    int i = 0;
                    
                    while (i < len)
                    {
                        cookie.Add(tempArray[i << 1], Uri.UnescapeDataString(tempArray[(i << 1) + 1]));
                    }
                }
            }
            return cookie;
        }
        /// <summary>
        /// A Stream with the request body, if any. Stream.Null MAY be used as a placeholder if there is no request body.
        /// </summary>
        public Stream RequestBody
        {
            get {
                return (Stream)Environment["owin.RequestBody"];
            }
        }
        /// <summary>
        /// <!--An IDictionary<string, string[]> of request headers.-->
        /// </summary>
        public IDictionary<string, string[]> RequestHeaders
        {
            get {
                return (IDictionary<string, string[]>)Environment["owin.RequestHeaders"];
            }
        }
        /// <summary>
        /// A string containing the HTTP request method of the request (e.g., "GET", "POST").
        /// </summary>
        public string RequestMethod
        {
            get
            {
                return (string)Environment["owin.RequestMethod"];
            }
        }
        /// <summary>
        /// A string containing the request path. The path MUST be relative to the "root" of the application delegate.
        /// </summary>
        public string RequestPath
        {
            get
            {
                return (string)Environment["owin.RequestPath"];
            }
        }
        /// <summary>
        /// A string containing the portion of the request path corresponding to the "root" of the application delegate;
        /// </summary>
        public string RequestPathBase
        {
            get
            {
                return (string)Environment["owin.RequestPathBase"];
            }
        }
        /// <summary>
        /// A string containing the protocol name and version (e.g. "HTTP/1.0" or "HTTP/1.1").
        /// </summary>
        public string RequestProtocol
        {
            get
            {
                return (string)Environment["owin.RequestProtocol"];
            }
        }
        /// <summary>
        /// A string containing the query string component of the HTTP request URI, without the leading "?" (e.g., "foo=bar&amp;baz=quux"). The value may be an empty string.
        /// </summary>
        public string RequestQueryString
        {
            get
            {
                return (string)Environment["owin.RequestQueryString"];
            }
        }
        /// <summary>
        /// A string containing the URI scheme used for the request (e.g., "http", "https"); 
        /// </summary>
        public string RequestScheme
        {
            get
            {
                return (string)Environment["owin.RequestScheme"];
            }
        }
        /// <summary>
        /// A Stream used to write out the response body, if any.
        /// </summary>
        public Stream ResponseBody
        {
            get
            {
                return (Stream)Environment["owin.ResponseBody"];
            }
        }
        /// <summary>
        /// <!--An IDictionary<string, string[]> of response headers.-->
        /// </summary>
        public IDictionary<string, string[]> ResponseHeaders
        {
            get
            {
                return (IDictionary<string,string[]>)Environment["owin.ResponseHeaders"];
            }
        }
        /// <summary>
        /// An optional int containing the HTTP response status code as defined in RFC 2616 section 6.1.1. The default is 200.
        /// </summary>
        public int ResponseStatusCode
        {
            set
            {
                Environment["owin.ResponseStatusCode"] = value;
            }
        }
        /// <summary>
        /// An optional string containing the reason phrase associated the given status code. If none is provided then the server SHOULD provide a default as described in RFC 2616 section 6.1.1
        /// </summary>
        public string ResponseReasonPhrase
        {
            set
            {
                Environment["owin.ResponseReasonPhrase"] = value;
            }
        }
        /// <summary>
        /// An optional string containing the protocol name and version (e.g. "HTTP/1.0" or "HTTP/1.1"). If none is provided then the "owin.RequestProtocol" key's value is the default.
        /// </summary>
        public string ResponseProtocol
        {
            set
            {
                Environment["owin.ResponseProtocol"] = value;
            }
        }
        /// <summary>
        /// A CancellationToken indicating if the request has been canceled/aborted. See [Request Lifetime][sec-req-lifetime].
        /// </summary>
        public CancellationToken CallCancelled
        {
            get
            {
                return (CancellationToken)Environment["owin.CallCancelled"];
            }
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version
        {
            get
            {
                return (string)Environment["owin.Version"];
            }
        }
    }
}
