//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ContextMiddleware.cs
//        Description :基于IOwinContext的中间件
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
#if (NET40 || NET451 || NET461)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Owin;
using NFinal.Owin;
using NFinal.Action;
using NFinal.Http;
using System.IO;

namespace NFinal.Middleware
{
    /// <summary>
    /// 基于IOwinContext的中间件
    /// </summary>
    public class ContextMiddleware : Middleware<IOwinContext,IOwinRequest>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="next"></param>
        public ContextMiddleware(InvokeDelegate<IOwinContext> next) : base(next)
        {
            
        }
        /// <summary>
        /// 获取默认控制器
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IAction<IOwinContext,IOwinRequest> GetAction(IOwinContext context)
        {
            ContextAction controller= new ContextAction();
            controller.BaseInitialization(context,null);
            return controller;
        }
        
        /// <summary>
        /// 获取Http请求参数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override NameValueCollection GetParameters(IOwinRequest request)
        {
            return GetParameters(request.Method, request.ContentType, request.QueryString.Value, request.Body);
        }
        
        /// <summary>
        /// 获取Http请求信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IOwinRequest GetRequest(IOwinContext context)
        {
            return context.Request;
        }
        /// <summary>
        /// 获取Http请求方法
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override string GetRequestMethod(IOwinContext context)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取Http请求路径
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override string GetRequestPath(IOwinContext context)
        {
            return context.Request.Path.Value;
        }
        /// <summary>
        /// 获取二级域名
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override string GetSubDomain(IOwinContext context)
        {
            throw new NotImplementedException();
        }
    }
}
#endif