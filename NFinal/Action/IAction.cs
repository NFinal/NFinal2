//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename :IAction.cs
//        Description :控制器接口
//
//        created by Lucas at  2015-6-30
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.IO;
using System.Threading.Tasks;
using NFinal.Http;

namespace NFinal.Action
{
    /// <summary>
    /// Action接口类
    /// </summary>
    /// <typeparam name="TContext">上下文IOwinContext,Enviroment,Context</typeparam>
    /// <typeparam name="TRequest">Request</typeparam>
    public interface IAction<TContext, TRequest> : IO.IWriter,IDisposable
    {
        /// <summary>
        /// 获取Session对象
        /// </summary>
        /// <param name="sessionId">存储在sessionId</param>
        /// <param name="userKey">用户key</param>
        /// <returns></returns>
        NFinal.Http.ISession GetSession(string sessionId);
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <param name="methodName">请求方法</param>
        void BaseInitialization(TContext context,string methodName);
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <param name="methodName">请求方法</param>
        /// <param name="outputStream">响应内容</param>
        /// <param name="request">请求信息</param>
        /// <param name="compressMode">压缩模式</param>
        void Initialization(TContext context,string methodName, Stream outputStream, TRequest request, CompressMode compressMode);
        /// <summary>
        /// Http上下文
        /// </summary>
        TContext context { get; set; }
        /// <summary>
        /// Http请求信息
        /// </summary>
        TRequest request { get; set; }
        /// <summary>
        /// Http响应信息
        /// </summary>
        Owin.Response response { get; set; }
        /// <summary>
        /// Http响应内容
        /// </summary>
        Stream outputStream { get; set; }
        /// <summary>
        /// 压缩模式
        /// </summary>
        CompressMode compressMode { get; set; }
        /// <summary>
        /// 当前请求方法
        /// </summary>
        string methodName { get; }
        /// <summary>
        /// 获取请求URL
        /// </summary>
        /// <returns></returns>
        string GetRequestPath();
        /// <summary>
        /// 获取客户端的IP地址
        /// </summary>
        /// <returns></returns>
        string GetRemoteIpAddress();
        /// <summary>
        /// 获取请求头
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>value</returns>
        string GetRequestHeader(string key);
        /// <summary>
        /// 获取请求内容
        /// </summary>
        /// <returns></returns>
        Stream GetRequestBody();
        /// <summary>
        /// Http重定向
        /// </summary>
        /// <param name="url"></param>
        void Redirect(string url);
        /// <summary>
        /// 获取二级域名
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        string GetSubDomain(TContext context);
        /// <summary>
        /// 设置请求头
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetResponseHeader(string key,string value);
        /// <summary>
        /// 设置请求头
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetResponseHeader(string key, string[] value);
        /// <summary>
        /// 设置状态码
        /// </summary>
        /// <param name="statusCode"></param>
        void SetResponseStatusCode(int statusCode);
        /// <summary>
        /// 模板渲染前函数，用于子类重写
        /// </summary>
        bool Before();
        /// <summary>
        /// 模板渲染后函数，用于子类重写
        /// </summary>
        void After();
        /// <summary>
        /// 输出并关闭相应资源
        /// </summary>
        void Close();
    }
}
