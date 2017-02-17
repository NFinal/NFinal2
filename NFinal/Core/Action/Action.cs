using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NFinal
{
    /// <summary>
    /// Action抽像类,负责公用变量声明
    /// </summary>
    public abstract class Action : IAction
    {
        #region 参数声明
        public Owin.Request request;
        public Owin.Response response;
        public ServerType _serverType = ServerType.UnKnown;
        public string app;
        #endregion
        /// <summary>
        /// 模板渲染前函数，用于子类重写
        /// </summary>
        public abstract bool Before();
        /// <summary>
        /// 模板渲染后函数，用于子类重写
        /// </summary>
        public abstract void After();
        /// <summary>
        /// 停止输出
        /// </summary>
        public abstract void Close();
        /// <summary>
        /// 输出字节流
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="start"></param>
        /// <param name="cout"></param>
        public abstract void Write(byte[] buffer, int start, int cout);
        /// <summary>
        /// 设置响应头
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public abstract void SetResponseHeader(string key, string value);
        /// <summary>
        /// 设置响应头
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public abstract void SetResponseHeader(string key, string[] value);
        /// <summary>
        /// 读取请求头
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract string GetRequestHeader(string key);
        /// <summary>
        /// 设置状态码
        /// </summary>
        /// <param name="statusCode"></param>
        public abstract void SetResponseStatusCode(int statusCode);

        public abstract string GetRemoteIpAddress();
    }
}
