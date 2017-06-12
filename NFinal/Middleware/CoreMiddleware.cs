//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : CoreMiddleware.cs
//        Description :基于.net core HttpContext的中间件
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
#if NETCORE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NFinal.Action;

namespace NFinal.Middleware
{
    /// <summary>
    /// 基于.net core HttpContext的中间件
    /// </summary>
    public class CoreMiddleware : Middleware<HttpContext, HttpRequest>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="next"></param>
        public CoreMiddleware(RequestDelegate next):base((context) => { return next.Invoke(context); })
        {
        }
        /// <summary>
        /// 获取基本控制器
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IAction<HttpContext, HttpRequest> GetAction(HttpContext context)
        {
            return new NFinal.CoreAction();
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override NameValueCollection GetParameters(HttpRequest request)
        {
            
            NameValueCollection nvc = new NameValueCollection();
            if (!string.IsNullOrEmpty(request.QueryString.Value))
            {
                var queryString = request.QueryString.Value.Split('&');
                string[] kv;
                foreach (var qs in queryString)
                {
                    kv = qs.Split('=');
                    nvc.Add(kv[0], kv[1]);
                }
            }
            if (request.HasFormContentType)
            {
                foreach (var form in request.Form)
                {
                    nvc.Add(form.Key, form.Value);
                }
            }
            return nvc;
        }
        /// <summary>
        /// 获取请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override HttpRequest GetRequest(HttpContext context)
        {
            return context.Request;
        }
        /// <summary>
        /// 获取请求方法
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override string GetRequestMethod(HttpContext context)
        {
            return context.Request.Method;
        }
        /// <summary>
        /// 获取请求路径
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override string GetRequestPath(HttpContext context)
        {
            return context.Request.Path;
        }
        /// <summary>
        /// 获取二级域名
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override string GetSubDomain(HttpContext context)
        {
            return context.Request.Host.Host==null?"www":context.Request.Host.Host.Split('.')[0];
        }
    }
}
#endif
