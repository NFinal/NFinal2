//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : PlugInfo.cs
//        Description :插件信息
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace NFinal.Plugs
{
    /// <summary>
    /// 插件信息
    /// </summary>
    public class PlugInfo
    {
        /// <summary>
        /// 加载是否成功
        /// </summary>
        public bool loadSuccess;
        /// <summary>
        /// 插件所在集序集
        /// </summary>
        public Assembly assembly;
        /// <summary>
        /// 是否开启插件
        /// </summary>
        public bool enable;
        /// <summary>
        /// 插件名称，唯一标识
        /// </summary>
        public string name;
        /// <summary>
        /// Url前缀
        /// </summary>
        public string urlPrefix;
        /// <summary>
        /// 插件描术
        /// </summary>
        public string description;
        /// <summary>
        /// 插件程序集路径
        /// </summary>
        public string assemblyPath;
        /// <summary>
        /// 插件配置文件路径
        /// </summary>
        public string configPath;
        /// <summary>
        /// 插件配置信息
        /// </summary>
        public NFinal.Config.Plug.PlugConfig config;
    }
}
