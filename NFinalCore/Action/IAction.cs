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
    public interface IAction<TContext, TRequest> :IO.IWriter,IDisposable
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context"></param>
        void BaseInitialization(TContext context,string methodName);
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context"></param>
        /// <param name="outputStream"></param>
        /// <param name="request"></param>
        /// <param name="compressMode"></param>
        void Initialization(TContext context,string methodName, Stream outputStream, TRequest request, CompressMode compressMode);
        /// <summary>
        /// 当前上下文
        /// </summary>
        TContext context { get; set; }
        TRequest request { get; set; }
        Owin.Response response { get; set; }
        Stream outputStream { get; set; }
        CompressMode compressMode { get; set; }
        string methodName { get; }
        string GetRequestPath();
        /// <summary>
        /// 获取客户端的IP地址
        /// </summary>
        /// <returns></returns>
        string GetRemoteIpAddress();
        /// <summary>
        /// 获取请求头
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetRequestHeader(string key);
        Stream GetRequestBody();
        void Redirect(string url);
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
