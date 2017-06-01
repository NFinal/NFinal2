//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ConsoleLogger.cs
//        Description :基于Console的日志记录类
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
    /// 基于Console的日志记录类
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        /// <summary>
        /// 输出根踪信息
        /// </summary>
        /// <param name="message"></param>
        public void Trace(string message)
        {
            Console.WriteLine("[TRACE] " + message);
        }
        /// <summary>
        /// 输出根踪信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Trace(string message, params object[] args)
        {
            Console.WriteLine("[TRACE] " + message, args);
        }
        /// <summary>
        /// 输出调试信息
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
        {
            Console.WriteLine("[DEBUG] " + message);
        }
        /// <summary>
        /// 输出调试信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Debug(string message, params object[] args)
        {
            Console.WriteLine("[DEBUG] " + message, args);
        }
        /// <summary>
        /// 输出一般性信息
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            Console.WriteLine("[INFO] " + message);
        }
        /// <summary>
        /// 输出一般性信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Info(string message, params object[] args)
        {
            Console.WriteLine("[INFO] " + message, args);
        }
        /// <summary>
        /// 输出警告信息
        /// </summary>
        /// <param name="message"></param>
        public void Warn(string message)
        {
            Console.WriteLine("[WARN] " + message);
        }
        /// <summary>
        /// 输出警告信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Warn(string message, params object[] args)
        {
            Console.WriteLine("[WARN] " + message, args);
        }
        /// <summary>
        /// 输出错误信息
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            Console.WriteLine("[ERROR] " + message);
        }
        /// <summary>
        /// 输出错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Error(string message, params object[] args)
        {
            Console.WriteLine("[ERROR] " + message, args);
        }
        /// <summary>
        /// 输出致命错误信息
        /// </summary>
        /// <param name="message"></param>
        public void Fatal(string message)
        {
            Console.WriteLine("[FATAL] " + message);
        }
        /// <summary>
        /// 输出致命错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Fatal(string message, params object[] args)
        {
            Console.WriteLine("[FATAL] " + message, args);
        }
    }
}
