//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ServerType.cs
//        Description :服务器类型，指核心实现方式
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

namespace NFinal.Http
{
    /// <summary>
    /// 服务器类型，指核心实现方式
    /// </summary>
    public enum ServerType
    {
        /// <summary>
        /// IIS传统服务器
        /// </summary>
        AspNET,
        /// <summary>
        /// vNext服务器
        /// </summary>
        MicrosoftOwin,
        /// <summary>
        /// NFinal实现的Owin服务器
        /// </summary>
        NFinalOwin,
        /// <summary>
        /// 无服务器，静态页生成
        /// </summary>
        IsStatic,
        /// <summary>
        /// 未知
        /// </summary>
        UnKnown
    }
}
