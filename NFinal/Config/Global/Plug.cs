//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : Plug.cs
//        Description :插件配置信息
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Config.Global
{
    /// <summary>
    /// 插件配置信息
    /// </summary>
    public class Plug
    {
        /// <summary>
        /// 是否开启插件
        /// </summary>
        public bool enable;
        /// <summary>
        /// 插件名称
        /// </summary>
        public string name;
        /// <summary>
        /// 插件Url前缀
        /// </summary>
        public string urlPrefix;
        /// <summary>
        /// 插件描述
        /// </summary>
        public string description;
        /// <summary>
        /// 插件程序集所在路径
        /// </summary>
        public string assemblyPath;
        /// <summary>
        /// 插件配置信息路径
        /// </summary>
        public string configPath;
    }
}