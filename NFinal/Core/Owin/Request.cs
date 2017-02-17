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
        public NameValueCollection parameters;
        /// <summary>
        /// 请求中包含的内容，例如POST请求
        /// </summary>
        public Stream stream;
        /// <summary>
        /// 请求方法
        /// </summary>
        public NFinal.MethodType methodType;
        /// <summary>
        /// Post请求中包含的文件流
        /// </summary>
        public IDictionary<string, NFinal.Owin.HttpMultipart.HttpFile> files;
        /// <summary>
        /// 请求的绝对路径,例：/Index.html
        /// </summary>
        public string requestPath;
        /// <summary>
        /// 请求中包含的参数
        /// </summary>
        //public NameValueCollection get;
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
        public Request()
        { }
        public Request(IDictionary<string, string> cookies, NameValueCollection parameters,string requestPath,string subdomain)
        {
            this.parameters = null;
            this.headers = null;
            this.cookies = cookies;
            this.environment = null;
            this.files = null;
            this.parameters = parameters;
            this.methodType = MethodType.GET;
            this.requestPath = requestPath;
            this.stream = null;
            this.queryString = null;
        }

    }
}
