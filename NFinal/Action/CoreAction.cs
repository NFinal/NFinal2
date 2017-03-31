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
    public class CoreAction<TUser> : NFinal.Action.AbstractAction<HttpContext, HttpRequest, TUser> where TUser: NFinal.User.AbstractUser
    {
#region 初始化函数
        public CoreAction() { }
        public override void BaseInitialization(HttpContext context, string methodName)
        {
            base.BaseInitialization(context, methodName);
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
            this.Session = new NFinal.Http.Session(this.Cookie.SessionId, new Cache.SimpleCache(30));
            this.outputStream = context.Response.Body;
        }
        /// <summary>
        /// 流输出初始化函数
        /// </summary>
        /// <param name="enviroment">Owin中间件</param>
        /// <param name="outputStream">输出流</param>
        /// <param name="request"></param>
        /// <param name="compressMode"></param>
        public override void Initialization(HttpContext context, string methodName, Stream outputStream, HttpRequest request, CompressMode compressMode)
        {
            base.Initialization(context, methodName, outputStream, request, compressMode);
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
            this.Session = new NFinal.Http.Session(this.Cookie.SessionId, new Cache.SimpleCache(30));
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
            this.writeStream.Flush();
            this.writeStream.Dispose();
            this.response.stream.Seek(0,SeekOrigin.Begin);
            this.response.stream.CopyTo(this.outputStream);
            this.response.stream.Dispose();
            this.outputStream.Dispose();
            this.Dispose();
        }

        public override void Dispose()
        {
            this.writeStream?.Dispose();
            this.response.stream?.Dispose();
            this.outputStream?.Dispose();
            this.request.Body?.Dispose();
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