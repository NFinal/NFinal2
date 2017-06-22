//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : PlugConfig.cs
//        Description :插件配置
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
using Newtonsoft.Json;

namespace NFinal.Config.Plug
{
    /// <summary>
    /// 插件配置
    /// </summary>
    public class PlugConfig
    {
        /// <summary>
        /// plug全局字符串，数字等缓存类。
        /// </summary>
        [JsonIgnore]
        public NFinal.Collections.FastSearch.FastSearch<StringContainer> keyValueCache=null;
        /// <summary>
        /// plug附加对象
        /// </summary>
        [JsonIgnore]
        public object extraData;
        /// <summary>
        /// 类型的全路径
        /// </summary>
        [JsonIgnore]
        public NFinal.DependencyInjection.IServiceCollection serviceCollection=null;
        /// <summary>
        /// Json实体类，用于用户自定义配置
        /// </summary>
        [JsonIgnore]
        public SimpleJSON.JSONObject JsonObject;
        /// <summary>
        /// 插件信息
        /// </summary>
        public Plug plug;
        /// <summary>
        /// 自定义配置项
        /// </summary>
        public Dictionary<string, string> appSettings;
        /// <summary>
        /// 连接字符串
        /// </summary>
        public Dictionary<string, ConnectionString> connectionStrings;
        /// <summary>
        /// 可接受的请求方法
        /// </summary>
        public string[] verbs;
        /// <summary>
        /// Session配置
        /// </summary>
        public SessionState sessionState;
        /// <summary>
        /// Url配置
        /// </summary>
        public Url url;
        /// <summary>
        /// Cookie配置
        /// </summary>
        public Cookie cookie;
        /// <summary>
        /// 默认皮肤
        /// </summary>
        public string defaultSkin;
        /// <summary>
        /// 用户自定义错误
        /// </summary>
        public CustomErrors customErrors;
    }
}
