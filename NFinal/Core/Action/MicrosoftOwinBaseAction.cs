//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename :MagicViewBag.cs
//        Description :控制器父类
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.IO;

namespace NFinal
{
#if MicrosoftOwin
    /// <summary>
    /// NFinal的控制器基类，核心类，类似于asp.net中的page类
    /// </summary>
    public class MicrosoftOwinBaseAction : Action
    {
        /// <summary>
        /// 集成模式对应的OwinContext对象
        /// </summary>
        public Microsoft.Owin.IOwinContext _webContext = null;
#region 初始化函数
        /// <summary>
        /// 初始化函数
        /// </summary>
        public MicrosoftOwinBaseAction() : base() { }
        /// <summary>
        /// 基于文本输出对象的初始化函数
        /// </summary>
        /// <param name="tw">文本输出对象</param>
        public MicrosoftOwinBaseAction(System.IO.TextWriter tw) : base(tw) { }
        /// <summary>
        /// 静态html页输出初始化函数
        /// </summary>
        /// <param name="fileName"></param>
        public MicrosoftOwinBaseAction(string fileName) : base(fileName) { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="action"></param>
        public MicrosoftOwinBaseAction(MicrosoftOwinBaseAction action) : base(action)
        {
            action._webContext = this._webContext;
        }
#endregion
#region 输出函数
        /// <summary>
        /// 请求的根路径，例：http://www.x.com
        /// </summary>
        public override string RequestRoot
        {
            get
            {
                if (this._serverType == ServerType.MicrosoftOwin)
                {
                    return _webContext.Request.Uri.Scheme + Constant.SchemeDelimiter + _webContext.Request.Uri.Host;
                }
                return null;
            }
        }
        /// <summary>
        /// 获取请求头
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override string GetRequestHeader(string key)
        {
            if (this._serverType == ServerType.MicrosoftOwin)
            {
                return _webContext.Request.Headers[key];
            }
            return null;
        }
        /// <summary>
        /// 请求时的流
        /// </summary>
        public override System.IO.Stream InputStream
        {
            get
            {
                if (this._serverType == ServerType.MicrosoftOwin)
                {
                    return this._webContext.Request.Body;
                }
                return null;
            }
        }
        /// <summary>
        /// 设置输出头
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public override void SetResponseHeader(string key, string value)
        {
            if (this._serverType == ServerType.MicrosoftOwin)
            {
                _webContext.Response.Headers.Set(key, value);
            }
        }
        /// <summary>
        /// 设置输出头
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public override void SetResponseHeader(string key, string[] value)
        {
            if (this._serverType == ServerType.MicrosoftOwin)
            {
                _webContext.Response.Headers.SetValues(key,value);
            }
        }
        /// <summary>
        /// 设置状态码
        /// </summary>
        /// <param name="statusCode"></param>
        public override void SetResponseStatusCode(int statusCode)
        {
            _webContext.Response.StatusCode = statusCode;
        }
        /// <summary>
        /// 模板渲染前函数，用于子类重写
        /// </summary>
        public override void Before() { }
        /// <summary>
        /// 输出字节流，用于输出二进制流，如图象，文件等。
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public override void Write(byte[] buffer,int offset,int count)
        {
            if (this._serverType == ServerType.MicrosoftOwin)
            {
                _webContext.Response.Write(buffer);
            }
            else if (this._serverType == ServerType.IsStatic)
            {
                _tw.Write(Constant.encoding.GetString(buffer));
            }
        }
        /// <summary>
        /// 模板渲染后函数，用于子类重写
        /// </summary>
        public override void After() { }
        /// <summary>
        /// 关闭资源
        /// </summary>
        public override void Close()
        {
            if (this._tw != null)
            {
                this._tw.Close();
            }
        }
#endregion
    }
#endif
}