//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : SessionState.cs
//        Description :Session配置
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Config.Plug
{
    /// <summary>
    /// Session模式
    /// </summary>
    public enum SessionStateMode
    {
        /// <summary>
        /// 关闭
        /// </summary>
        Off,
        /// <summary>
        /// Redis缓存模式
        /// </summary>
        Redis,
        /// <summary>
        /// 内存模式
        /// </summary>
        InProc
    }
    /// <summary>
    /// Session配置
    /// </summary>
    public class SessionState
    {
        /// <summary>
        /// Session_Id的Cookie名称
        /// </summary>
        public string cookieName;
        /// <summary>
        /// Session的模式
        /// </summary>
        public SessionStateMode mode;
        /// <summary>
        /// Session服务器配置
        /// </summary>
        public string stateConnectionString;
        /// <summary>
        /// 超时时间
        /// </summary>
        public int timeout;
        /// <summary>
        /// Session前缀
        /// </summary>
        public string prefix;
    }
}
