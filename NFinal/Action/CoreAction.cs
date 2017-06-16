//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename :CoreAction.cs
//        Description :HttpContext对应的控制器基类
//
//        created by Lucas at  2015-6-30
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
#if NETCORE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using NFinal.Http;
using System.Data;

namespace NFinal
{
    public class CoreAction : NFinal.Action.AbstractAction<HttpContext, HttpRequest>
    {
#region 初始化函数
        public CoreAction() { }
        public override void BaseInitialization(HttpContext context, string methodName, NFinal.Config.Plug.PlugConfig plugConfig)
        {
            base.BaseInitialization(context, methodName, plugConfig);
            this.request = context.Request;
            this.parameters = new NFinal.NameValueCollection();
            foreach (var query in request.Query)
            {
                this.parameters.Add(query.Key, query.Value);
            }
            if (context.Request.HasFormContentType)
            {
                foreach (var form in context.Request.Form)
                {
                    this.parameters.Add(form.Key,form.Value);
                }
            }
            IDictionary<string, string> requestCookie = new Dictionary<string, string>();
            foreach (var cookie in this.request.Cookies)
            {
                requestCookie.Add(cookie.Key, cookie.Value);
            }

            this.Cookie = new Cookie(requestCookie);
            this.Session = GetSession(Cookie.SessionId);
            this.outputStream = context.Response.Body;
        }
        /// <summary>
        /// 流输出初始化函数
        /// </summary>
        /// <param param name="plugConfig">插件配置</param>
        /// <param name="methodName">行为名称</param>
        /// <param name="context">Http上下文</param>
        /// <param name="outputStream">输出流</param>
        /// <param name="request"></param>
        /// <param name="compressMode"></param>
        public override void Initialization(HttpContext context, string methodName, Stream outputStream, HttpRequest request, CompressMode compressMode, NFinal.Config.Plug.PlugConfig plugConfig)
        {
            base.Initialization(context, methodName, outputStream, request, compressMode, plugConfig);
            this.parameters = new NFinal.NameValueCollection();
            foreach (var query in request.Query)
            {
                this.parameters.Add(query.Key, query.Value);
            }
            if (context.Request.HasFormContentType)
            {
                foreach (var form in context.Request.Form)
                {
                    this.parameters.Add(form.Key, form.Value);
                }
            }
            IDictionary<string, string> requestCookie = new Dictionary<string, string>();
            foreach (var cookie in this.request.Cookies)
            {
                requestCookie.Add(cookie.Key, cookie.Value);
            }

            this.Cookie = new Cookie(requestCookie);
            this.Session = GetSession(Cookie.SessionId);
            if (outputStream == null)
            {
                this.outputStream = context.Response.Body;
            }
            else
            {
                this.outputStream = outputStream;
            }
        }
#endregion
        public override void After()
        {
            
        }

        public override bool Before()
        {
            return true;
        }

        public override void Close()
        {
            if (_serverType != ServerType.IsStatic)
            {
            
                if (context.Response.Headers.ContainsKey(NFinal.Constant.HeaderContentType))
                {
                    context.Response.ContentType = contentType;
                }
                else
                {
                    context.Response.Headers.Add(NFinal.Constant.HeaderContentType, new string[] { this.contentType });
                }
                foreach (var responseCookie in Cookie.ResponseCookies)
                {
                    context.Response.Headers.Add(NFinal.Constant.HeaderSetCookie, responseCookie.Value);
                }
                foreach (var header in this.response.headers)
                {
                    context.Response.Headers.Add(header.Key, header.Value);
                }
            }
            this.writeStream.Dispose();
            this.response.stream.Seek(0, SeekOrigin.Begin);
            this.response.stream.CopyTo(this.outputStream);
            this.response.stream.Dispose();
        }
        public override void Dispose()
        {
            if (this.writeStream != null)
            {
                this.writeStream.Dispose();
            }
            if (this.response.stream != null)
            {
                this.response.stream.Dispose();
            }
            if (this.files != null)
            {
                foreach (var file in this.files)
                {
                    file.Value?.Value?.Dispose();
                }
            }
            if (this.con != null)
            {
                this.con.Close();
            }
        }
        public override string GetRemoteIpAddress()
        {
            return request.Host.Host;
        }

        public override string GetRequestHeader(string key)
        {
            return request.Headers[key];
        }

        public override string GetRequestPath()
        {
            return request.Path;
        }

        public override string GetSubDomain(HttpContext context)
        {
            return context.Request.Host.Host.Split('.')[0];
        }

        public override void SetResponseHeader(string key, string[] value)
        {
            response.headers[key] = value;
        }

        public override void SetResponseHeader(string key, string value)
        {
            response.headers[key] =new string[] { value };
        }

        public override void SetResponseStatusCode(int statusCode)
        {
            response.statusCode = statusCode;
        }

        public override Stream GetRequestBody()
        {
            return request.Body;
        }
    }
}
#endif