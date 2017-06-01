//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ILogger.cs
//        Description :日志接口
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Logs
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 输出根踪信息
        /// </summary>
        /// <param name="message"></param>
        void Trace(string message);
        /// <summary>
        /// 输出根踪信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Trace(string message, params object[] args);
        /// <summary>
        /// 输出调试信息
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message);
        /// <summary>
        /// 输出调试信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Debug(string message, params object[] args);
        /// <summary>
        /// 输出一般性信息
        /// </summary>
        /// <param name="message"></param>
        void Info(string message);
        /// <summary>
        /// 输出一般性信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Info(string message, params object[] args);
        /// <summary>
        /// 输出警告信息
        /// </summary>
        /// <param name="message"></param>
        void Warn(string message);
        /// <summary>
        /// 输出警告信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Warn(string message, params object[] args);
        /// <summary>
        /// 输出错误信息
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);
        /// <summary>
        /// 输出错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Error(string message, params object[] args);
        /// <summary>
        /// 输出致命错误信息
        /// </summary>
        /// <param name="message"></param>
        void Fatal(string message);
        /// <summary>
        /// 输出致命错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Fatal(string message, params object[] args);
    }
}
