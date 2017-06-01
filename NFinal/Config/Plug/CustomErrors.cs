//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : CustomErrors.cs
//        Description :自义定错误设置
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
    /// 自定义错误模式
    /// </summary>
    public enum CustomErrorsMode
    {
        /// <summary>
        /// 只有远程显示错误
        /// </summary>
        RemoteOnly,
        /// <summary>
        /// 始终显示错误
        /// </summary>
        On,
        /// <summary>
        /// 不显示错误
        /// </summary>
        Off
    }
    /// <summary>
    /// 自定义错误
    /// </summary>
    public class Error
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int statusCode;
        /// <summary>
        /// 跳转页面
        /// </summary>
        public string redirect;
    }
    /// <summary>
    /// 自定义错误配置
    /// </summary>
    public class CustomErrors
    {
        /// <summary>
        /// 默认跳转页面
        /// </summary>
        public string defaultRedirect;
        /// <summary>
        /// 自定义错误模式
        /// </summary>
        public CustomErrorsMode mode;
        /// <summary>
        /// 自定义错误数组
        /// </summary>
        public Error[] errors;
    }
}
