//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : GlobalConfig.cs
//        Description :全局配置信息
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NFinal.Config.Global
{
    /// <summary>
    /// 全局配置信息
    /// </summary>
    public class GlobalConfig
    {
        /// <summary>
        /// 全局扩展对象
        /// </summary>
        public NFinal.Collections.FastSearch.FastSearch<StringContainer> keyValueCache=null;
        [JsonIgnore]
        public object extraData;
        /// <summary>
        /// 调试信息
        /// </summary>
        public Debug debug;
        /// <summary>
        /// 服务器配置
        /// </summary>
        public Server server;
        /// <summary>
        /// 项目类型
        /// </summary>
        public ProjectType projectType;
        /// <summary>
        /// JSON实体类，用于用户自定义数据信息
        /// </summary>
        [JsonIgnore]
        public SimpleJSON.JSONObject JsonObject;
    }
}
