using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using NFinal.Http;

namespace NFinal
{
    /// <summary>
    /// NFinal的控制器基类，MasterPage为母模板，ViewBag为视图数据,User为用户数据
    /// </summary>
    public class OwinAction<TUser> : NFinal.Action.AbstractAction<IDictionary<string,object>,Owin.Request,TUser> where TUser : NFinal.User.AbstractUser
    {
        #region 初始化函数
        public OwinAction() { }
        public override void BaseInitialization(IDictionary<string, object> enviroment,string methodName) {
            base.BaseInitialization(enviroment, methodName);
            this.request = enviroment.GetRequest();
            this.parameters = request.parameters;
            this.Cookie = new Cookie(this.request.cookies);
            this.Session = new NFinal.Http.Session(this.Cookie.SessionId, new Cache.SimpleCache(30));
            this.outputStream = enviroment.GetResponseBody();
        }
        /// <summary>
        /// 流输出初始化函数
        /// </summary>
        /// <param name="enviroment">Owin中间件</param>
        /// <param name="outputStream">输出流</param>
        /// <param name="request"></param>
        /// <param name="compressMode"></param>
        public override void Initialization(IDictionary<string, object> enviroment,string methodName, Stream outputStream, Owin.Request request, CompressMode compressMode)
        {
            base.Initialization(enviroment, methodName, outputStream, request, compressMode);
            this.parameters = request.parameters;
            this.Cookie = new Cookie(this.request.cookies);
            this.Session = new NFinal.Http.Session(this.Cookie.SessionId, new Cache.SimpleCache(30));
            if (outputStream == null)
            {
                this.outputStream = enviroment.GetResponseBody();
            }
            else
            {
                this.outputStream = outputStream;
            }
        }
        #endregion
        #region 输出函数
        public override string GetRemoteIpAddress()
        {
            return context.GetRemoteIpAddress();
        }
        public override string GetRequestPath()
        {
            return this.GetRequestPath();
        }
        /// <summary>
        /// 获取请求头
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override string GetRequestHeader(string key)
        {
            if (request.headers.ContainsKey(key))
            {
                return request.headers[key][0];
            }
            return null;
        }
        /// <summary>
        /// 设置输出头
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public override void SetResponseHeader(string key, string value)
        {
            this.response.headers.AddValue(key, new string[] { value });
        }
        public override void SetResponseHeader(string key, string[] value)
        {
            this.response.headers.AddValue(key, value);
        }
        /// <summary>
        /// 设置状态码
        /// </summary>
        /// <param name="statusCode"></param>
        public override void SetResponseStatusCode(int statusCode)
        {
            this.response.statusCode = statusCode;
            if (this._serverType != ServerType.IsStatic)
            {
                this.context[Owin.OwinKeys.ResponseStatusCode] = this.response.statusCode;
            }
        }
        /// <summary>
        /// 模板渲染前函数，用于子类重写
        /// </summary>
        public override bool Before()
        {
            return true;
        }
        /// <summary>
        /// 模板渲染后函数，用于子类重写
        /// </summary>
        public override void After(){}
        /// <summary>
        /// 完成输出并释放相关对象
        /// </summary>
        public override void Close()
        {
            if (_serverType != ServerType.IsStatic)
            {
                IDictionary<string, string[]> headers = (IDictionary<string, string[]>)context[Owin.OwinKeys.ResponseHeaders];
                if (headers.ContainsKey(NFinal.Constant.HeaderContentType))
                {
                    headers[NFinal.Constant.HeaderContentType] = new string[] { this.contentType };
                }
                else
                {
                    headers.Add(NFinal.Constant.HeaderContentType, new string[] { this.contentType });
                }
                foreach (var responseCookie in Cookie.ResponseCookies)
                {
                    headers.Add(NFinal.Constant.HeaderSetCookie,new string[] { responseCookie.Value });
                }
                foreach (var header in this.response.headers)
                {
                    headers.AddValue(header.Key, header.Value);
                }
            }
            this.writeStream.Flush();
            this.writeStream.Dispose();
            this.response.stream.Seek(0, SeekOrigin.Begin);
            this.response.stream.CopyTo(this.outputStream);
            this.Dispose();
        }
        /// <summary>
        /// 释放相关对象
        /// </summary>
        public override void Dispose()
        {
            this.writeStream?.Dispose();
            this.response.stream?.Dispose();
            if (this.request?.files != null)
            {
                foreach (var file in this.request.files)
                {
                    file.Value.Value.Dispose();
                }
            }
        }
        /// <summary>
        /// 获取二级域名
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override string GetSubDomain(IDictionary<string, object> context)
        {
            string host= ((IDictionary<string,string[]>)(context[Owin.OwinKeys.RequestHeaders]))[NFinal.Constant.HeaderHost][0];
            int dotQty = 0;
            int firstDotIndex = 0;
            for (int i = 0; i < host.Length; i++)
            {
                if (host[i] == '.')
                {
                    if (firstDotIndex == 0)
                    {
                        firstDotIndex = i;
                    }
                    dotQty++;
                }
            }
            if (dotQty == 3)
            {
                return host.Substring(0, firstDotIndex);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取请求流
        /// </summary>
        /// <returns></returns>
        public override Stream GetRequestBody()
        {
            return context.GetRequestBody();
        }

        #endregion
    }
}