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

namespace NFinal.Compile
{
    /// <summary>
    /// 配置文件config.json实体类
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 模块路径
        /// </summary>
        public string APP = "/";
        /// <summary>
        /// 控制器后缀
        /// </summary>
        public string Controller = "Controllers/";
        /// <summary>
        /// 代码目录
        /// </summary>
        public string Code = "Code/";
        /// <summary>
        /// 视图目录
        /// </summary>
        public string Views = "Views/";
        /// <summary>
        /// 数据库实体类目录
        /// </summary>
        public string Models = "Models/";
        /// <summary>
        /// 数据库逻辑层目录
        /// </summary>
        public string BLL = "BLL/";
        /// <summary>
        /// 数据库操作层目录
        /// </summary>
        public string DAL = "DAL/";
        /// <summary>
        /// 网站运行目录
        /// </summary>
        public string Web = "Web/";
        /// <summary>
        /// 静态文件目录
        /// </summary>
        public string Content = "Content/";
        /// <summary>
        /// js目录
        /// </summary>
        public string ContentJS = "js/";
        /// <summary>
        /// css目录
        /// </summary>
        public string ContentCss = "css/";
        /// <summary>
        /// 图片目录
        /// </summary>
        public string ContentImages = "images/";

        /// <summary>
        /// url前缀
        /// </summary>
        public string urlPrefix = "";
        /// <summary>
        /// url默认扩展名
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
        /// 默认风格目录，即模板目录
        /// </summary>
        public string defaultStyle = "Default/";

        /// <summary>
        /// redis是否自动运行
        /// </summary>
        public bool redisConfigAutoStart = false;
        /// <summary>
        /// redisConfigMaxReadPoolSize
        /// </summary>
        public int redisConfigMaxReadPoolSize = 60;
        /// <summary>
        /// redisConfigMaxWritePoolSize
        /// </summary>
        public int redisConfigMaxWritePoolSize = 60;
        /// <summary>
        /// redisReadWriteHosts
        /// </summary>
        public string[] redisReadWriteHosts = new string[] { "127.0.0.1:6379"};
        /// <summary>
        /// redisReadOnlyHosts
        /// </summary>
        public string[] redisReadOnlyHosts = new string[] { "127.0.0.1:6379"};
        /// <summary>
        /// url目录重写配置
        /// </summary>
        public List<NFinal.Compile.RewriteDirectory> rewriteDirectoryList = new List<NFinal.Compile.RewriteDirectory>();
        /// <summary>
        /// url文件重写配置
        /// </summary>
        public List<NFinal.Compile.RewriteFile> rewriteFileList = new List<NFinal.Compile.RewriteFile>();

        /// <summary>
        /// url模式
        /// </summary>
        public int UrlMode = 1;
        /// <summary>
        /// 数据库配置
        /// </summary>
        public System.Collections.Generic.List<DB.ConnectionString> connectionStrings =null;
        /// <summary>
        /// 是否压缩模板中的空格
        /// </summary>
        public bool CompressHTML = false;
        /// <summary>
        /// 控制器的后缀名
        /// </summary>
        public string ControllerSuffix = "Controller";

        /// <summary>
        /// 初始化函数
        /// </summary>
        public Config()
        { }

        /// <summary>
        /// 获取命名空间
        /// </summary>
        /// <param name="projectName">项目名</param>
        /// <param name="name">类名字</param>
        /// <returns></returns>
        public string GetFullName(string projectName,string name)
        {
            name = projectName + name;
            return name.Trim('/').Replace('/','.');
        }
        /// <summary>
        /// 把全类名中的控制器名和工程名替换掉
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="controllerFullName"></param>
        /// <returns></returns>
        public string ChangeControllerName(string projectName, string controllerFullName)
        {
            string controllerName = Frame.AssemblyTitle + Controller;
            return projectName + controllerFullName.Substring(controllerName.Length,
                controllerFullName.Length - controllerName.Length);
        }
        /// <summary>
        /// 把全类名中的BLL名替换掉
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="BLLFullName"></param>
        /// <returns></returns>
        public string ChangeBLLName(string projectName, string BLLFullName)
        {
            string BLLName = (Frame.AssemblyTitle + BLL).TrimEnd('/');
            return projectName + BLLFullName.Substring(BLLName.Length,BLLFullName.Length-BLLName.Length);
        }
        /// <summary>
        /// 返回绝对路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string MapPath(string url)
        {
            return Frame.MapPath(url);
        }
       
    }
}