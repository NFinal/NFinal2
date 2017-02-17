//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename :Config.cs
//        Description :陪置
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Collections.Specialized;

namespace NFinal.Config
{
    /// <summary>
    /// 配置文件实体类
    /// </summary>
    public class Config
    {
        /// <summary>
        /// url前缀
        /// </summary>
        public string urlPrefix = "";
        /// <summary>
        /// url路径中默认扩展名
        /// </summary>
        public string urlExtension = "";
        /// <summary>
        /// cookie前缀
        /// </summary>
        public string cookiePrefix = "";
        /// <summary>
        /// session前缀
        /// </summary>
        public string sessionPrefix = "";
        /// <summary>
        /// 默认皮肤
        /// </summary>
        public string defaultStyle = "Default/";
        /// <summary>
        /// js，css等静态文件统一版本号
        /// </summary>
        public string version = "1.0";
        /// <summary>
        /// 自动在所有html,js与css等文件后加上v=1.0??，使其在微信端缓存失效
        /// </summary>
        public bool autoVersion = false;
        ///// <summary>
        ///// 配置文本对应的json实体类
        ///// </summary>
        //private JsonObject jsonData = null;
        //public JsonObject JsonData
        //{
        //    get {
        //        return jsonData;
        //    }
        //    set { jsonData = value; }
        //}
        /// <summary>
        /// url文件夹重写配置
        /// </summary>
        public List<RewriteDirectory> rewriteDirectoryList = new List<RewriteDirectory>();
        /// <summary>
        /// url文件重写配置
        /// </summary>
        public List<RewriteFile> rewriteFileList = new List<RewriteFile>();

        /// <summary>
        /// redis缓存配置
        /// </summary>
        public bool redisConfigAutoStart = false;
        public int redisConfigMaxReadPoolSize = 60;
        public int redisConfigMaxWritePoolSize = 60;
        public string[] redisReadWriteHosts = new string[] { "127.0.0.1:6379"};
        public string[] redisReadOnlyHosts = new string[] { "127.0.0.1:6379"};
        public System.Collections.Generic.List<ConnectionString> connectionStrings = null;
        /// <summary>
        /// url默认规则
        /// </summary>
        public int UrlMode = 1;
        /// <summary>
        /// 生成时是否压缩模板中的空格
        /// </summary>
        public bool CompressHTML = false;

        public Config()
        { }
       
    }
}