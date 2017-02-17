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
using System.Web;
using System.Collections.Generic;
using System.IO;

namespace NFinal
{
#if AspNET
    /// <summary>
    /// NFinal的控制器基类，核心类，类似于asp.net中的page类
    /// </summary>
    public class AspNETBaseAction : Action
    {
        #region 初始化函数
        /// <summary>
        /// 经典模式对应的context对象
        /// </summary>
        public HttpContext _context = null;
        /// <summary>
        /// 初始化
        /// </summary>
        public AspNETBaseAction():base() { }
        /// <summary>
        /// 基于文本输出对象的初始化函数
        /// </summary>
        /// <param name="tw">文本输出对象</param>
        public AspNETBaseAction(System.IO.TextWriter tw) : base(tw) { }
        /// <summary>
        /// 静态html页输出初始化函数
        /// </summary>
        /// <param name="fileName"></param>
        public AspNETBaseAction(string fileName) : base(fileName) { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="action"></param>
        public AspNETBaseAction(AspNETBaseAction action):base(action)
        {
            action._context = this._context;
        }
        #endregion
        #region 输出函数
        /// <summary>
        /// 请求的根路径，例：http://www.x.com
        /// </summary>
        public override string RequestRoot
        {
            get {
                if (this._serverType == ServerType.AspNET)
                {
                    return this._context.Request.Url.Scheme + Constant.SchemeDelimiter + _context.Request.Url.Host;
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
            return _context.Request.Headers[key];
        }
        /// <summary>
        /// 请求内容
        /// </summary>
        public override System.IO.Stream InputStream
        {
            get { return _context.Request.InputStream; }
        }
        /// <summary>
        /// 设置响应头
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public override void SetResponseHeader(string key, string value)
        {
            _context.Response.Headers.Set(key, value);
        }
        /// <summary>
        /// 设置响应头
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public override void SetResponseHeader(string key, string[] value)
        {
            foreach (var v in value)
            {
                _context.Response.Headers.Remove(key);
                _context.Response.Headers.Add(key, v);
            }
        }
        /// <summary>
        /// 设置状态码
        /// </summary>
        /// <param name="statusCode"></param>
        public override void SetResponseStatusCode(int statusCode)
        {
            _context.Response.StatusCode = statusCode;
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
            if (this._serverType == ServerType.AspNET)
            {
                _context.Response.OutputStream.Write(buffer, offset, count);
            }
            else if (this._serverType == ServerType.IsStatic)
            {
                _tw.Write(Constant.encoding.GetString(buffer,offset,count));
            }
        }
        /// <summary>
        /// 模板渲染后函数，用于子类重写
        /// </summary>
        public override void After() { }
        /// <summary>
        /// 关闭输出
        /// </summary>
        public override void Close()
        {
            _context.Response.End();
        }
    #endregion
    }
#endif
}