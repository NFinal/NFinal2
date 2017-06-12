//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : OwinMiddleware.cs
//        Description :基于Owin协议的中间件
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NFinal.Owin;
using NFinal.Action;

namespace NFinal.Middleware
{
    /// <summary>
    /// 基于Owin协议的中间件
    /// </summary>
    public class OwinMiddleware : Middleware<IDictionary<string, object>,Owin.Request>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="next"></param>
        public OwinMiddleware(InvokeDelegate<IDictionary<string, object>> next) : base(next)
        {
        }
        /// <summary>
        /// 获取默认控制器
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IAction<IDictionary<string, object>,Owin.Request> GetAction(IDictionary<string, object> context)
        {
            NFinal.Owin.Request request = context.GetRequest();
            NFinal.OwinAction controller = new OwinAction();
            controller.BaseInitialization(context,null);
            return controller;
        }
        /// <summary>
        /// 获取Http请求参数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override NameValueCollection GetParameters(Request request)
        {
            return request.parameters;
        }
        /// <summary>
        /// 获取Http请求信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Request GetRequest(IDictionary<string, object> context)
        {
            return context.GetRequest();
        }
        /// <summary>
        /// 获取Http请求方法
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override string GetRequestMethod(IDictionary<string, object> context)
        {
            return context.GetRequestMethod();
        }
        /// <summary>
        /// 获取请求路径
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override string GetRequestPath(IDictionary<string, object> context)
        {
            return context.GetRequestPath();
        }
        /// <summary>
        /// 获取二级域名
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override string GetSubDomain(IDictionary<string, object> context)
        {
            string subDomain = context.GetSubDomain();
            return subDomain;
        }
    }
}