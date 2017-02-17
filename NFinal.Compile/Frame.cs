//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename :Frame.cs
//        Description :总框架
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using RazorEngine;
using RazorEngine.Templating;

namespace NFinal.Compile
{
    /// <summary>
    /// 全局生成类
    /// </summary>
    public class Frame
    {
        public static ReWriteData reWriteData = new ReWriteData();
        /// <summary>
        /// 项目根目录
        /// </summary>
        public static string appRoot;
        /// <summary>
        /// 模块名
        /// </summary>
        public static string AssemblyTitle;
        /// <summary>
        /// 本模块下的连接字符串
        /// </summary>
        public static System.Collections.Generic.List<DB.ConnectionString> ConnectionStrings = new System.Collections.Generic.List<DB.ConnectionString>();

        /// <summary>
        /// 生成类初始化
        /// </summary>
        /// <param name="appRoot">项目根目录</param>
        public Frame(string appRoot)
        {
            var config = new RazorEngine.Configuration.TemplateServiceConfiguration();
            config.Debug = false;
            config.Language = Language.CSharp;
            config.EncodedStringFactory = new RazorEngine.Text.RawStringFactory();
            var service = RazorEngine.Templating.RazorEngineService.Create(config);
            RazorEngine.Engine.Razor = service;

            reWriteData.rewriteDirectoryList = new List<RewriteDirectory>();
            reWriteData.rewriteFileList = new List<RewriteFile>();
            Frame.appRoot = appRoot;
            string[] fileNames = Directory.GetFiles(appRoot, "*.csproj");
            if (fileNames.Length > 0)
            {
                AssemblyTitle = Path.GetFileNameWithoutExtension(fileNames[0]);
            }
            else
            {
                string temp;
                temp = appRoot.Trim('\\');
                AssemblyTitle = temp.Substring(temp.LastIndexOf('\\') + 1);
            }
        }

        /// <summary>
        /// 把基于网站根目录的绝对路径改为相对路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MapPath(string url)
        {
            return appRoot + url.Trim('/').Replace('/', '\\');
        }
        /// <summary>
        /// 释放数据库资源
        /// </summary>
        public void ClearDB()
        {
            if (DB.Coding.DbCache.DbStore.Count > 0)
            {
                foreach (KeyValuePair<string, DB.Coding.DataUtility> da in DB.Coding.DbCache.DbStore)
                {
                    da.Value.con.Close();
                }
                DB.Coding.DbCache.DbStore.Clear();
            }
        }
        /// <summary>
        /// 获取数据库相关数据
        /// </summary>
        /// <param name="config"></param>
        public void GetDB(Config config)
        {
            //获取WebConfig中的连接字符串信息
            Frame.ConnectionStrings.Clear();
            if (config.connectionStrings.Count > 0)
            {
                foreach (DB.ConnectionString connectionString in config.connectionStrings)
                {
                    if (connectionString.provider.ToLower().IndexOf("mysql") > -1)
                    {
                        connectionString.type = DB.DBType.MySql;
                    }
                    else if (connectionString.provider.ToLower().IndexOf("sqlclient") > -1)
                    {
                        connectionString.type = DB.DBType.SqlServer;
                    }
                    else if (connectionString.provider.ToLower().IndexOf("sqlite") > -1)
                    {
                        connectionString.type = DB.DBType.Sqlite;
                    }
                    else if (connectionString.provider.ToLower().IndexOf("oracle") > -1)
                    {
                        connectionString.type = DB.DBType.Oracle;
                    }
                    else if (connectionString.provider.ToLower().IndexOf("npgsql") > -1)
                    {
                        connectionString.type = DB.DBType.PostgreSql;
                    }
                    Frame.ConnectionStrings.Add(connectionString);
                }
            }
            //读取数据库信息
            DB.Coding.DataUtility dataUtility = null;
            if (Frame.ConnectionStrings.Count > 0)
            {
                DB.ConnectionString conStr;
                DB.Coding.DbCache.DbStore.Clear();
                for (int i = 0; i < Frame.ConnectionStrings.Count; i++)
                {
                    conStr = Frame.ConnectionStrings[i];
                    if (conStr.type == DB.DBType.MySql)
                    {
                        dataUtility = new DB.Coding.MySQLDataUtility(conStr.value);
                        dataUtility.GetAllTables(dataUtility.con.Database);
                        DB.Coding.DbCache.DbStore.Add(conStr.name, dataUtility);
                    }
                    else if (conStr.type == DB.DBType.Sqlite)
                    {
                        dataUtility = new DB.Coding.SQLiteDataUtility(conStr.value);
                        dataUtility.GetAllTables(dataUtility.con.Database);
                        DB.Coding.DbCache.DbStore.Add(conStr.name, dataUtility);
                    }
                    else if (conStr.type == DB.DBType.SqlServer)
                    {
                        dataUtility = new DB.Coding.SQLDataUtility(conStr.value);
                        dataUtility.GetAllTables(dataUtility.con.Database);
                        DB.Coding.DbCache.DbStore.Add(conStr.name, dataUtility);
                    }
                    else if (conStr.type == DB.DBType.Oracle)
                    {
                        dataUtility = new DB.Coding.OracleDataUtility(conStr.value);
                        dataUtility.GetAllTables(dataUtility.con.Database);
                        DB.Coding.DbCache.DbStore.Add(conStr.name, dataUtility);
                    }
                    else if (conStr.type == DB.DBType.PostgreSql)
                    {
                        dataUtility = new DB.Coding.PostgreSqlDataUtility(conStr.value);
                        dataUtility.GetAllTables(dataUtility.con.Database);
                        DB.Coding.DbCache.DbStore.Add(conStr.name, dataUtility);
                    }
                }
            }
        }

        /// <summary>
        /// 获取所有的应用模块
        /// </summary>
        /// <returns>模块名数组</returns>
        public string[] GetApps()
        {
            string[] dirs = Directory.GetDirectories(MapPath("/"));
            string configJsonFileName = string.Empty;
            System.Collections.Generic.List<string> apps = new System.Collections.Generic.List<string>();
            for (int i = 0; i < dirs.Length; i++)
            {
                configJsonFileName = System.IO.Path.Combine(dirs[i], "config.json");
                if (File.Exists(configJsonFileName))
                {
                    apps.Add(System.IO.Path.GetFileName(dirs[i]));
                }
            }
            return apps.ToArray();
        }
        /// <summary>
        /// 获取所有的控制器
        /// </summary>
        /// <returns>所有的控制器</returns>
        public System.Collections.Generic.List<ControllerFileData> GetAllControllers()
        {
            string[] apps = GetApps();
            System.Collections.Generic.List<ControllerFileData> fileDataList = new System.Collections.Generic.List<ControllerFileData>();
            ControllerAnalyse com = new ControllerAnalyse();
            ControllerFileData fileData = null;
            string[] fileNames = null;
            string controllerPath = string.Empty;
            Config config = null;
            for (int i = 0; i < apps.Length; i++)
            {
                controllerPath = Frame.MapPath("/" + apps[i].Trim('/') + "/Controllers");
                if (!Directory.Exists(controllerPath))
                {
                    continue;
                }
                fileNames = Directory.GetFiles(controllerPath, "*.cs", SearchOption.AllDirectories);
                if (fileNames == null || fileNames.Length < 1)
                {
                    continue;
                }
                config = ConfigurationManager.GetConfig(apps[i], Frame.appRoot);
                for (int j = 0; j < fileNames.Length; j++)
                {
                    fileData = com.GetFileData(Frame.appRoot, apps[i], fileNames[j], System.Text.Encoding.UTF8, config);
                    fileDataList.Add(fileData);
                }
            }
            return fileDataList;
        }
        /// <summary>
        /// 找到ActionURL
        /// </summary>
        /// <param name="controllerFileDataList">控制器实体类</param>
        /// <param name="rewriteData">重写配置数据</param>
        public void CreateActionSearch(System.Collections.Generic.List<ControllerFileData> controllerFileDataList, ReWriteData rewriteData)
        {
            StreamWriter sw = File.CreateText(MapPath("/App_Start/NFinal/ActionSearch.cs"));
            var ActionSearchModel = new NFinal.Compile.Template.Startup.NFinal.ActionSearchModel
            { controllerFileDataList = controllerFileDataList, rewriteData = rewriteData };
            var template = new NFinal.Compile.Template.Startup.NFinal.ActionSearch();
            template.Model = ActionSearchModel;
            sw.Write(template.TransformText());
            //RazorEngine.Engine.Razor.RunCompile(
            //    System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.NFinal.NFinal.ActionSearch),
            //    "/App_Start/NFinal/ActionSearch.cs",
            //    sw,
            //    ActionSearchModel.GetType(),
            //    ActionSearchModel);
            sw.Dispose();
        }
        /// <summary>
        /// 创建middleware中间件，用于移值。
        /// </summary>
        /// <param name="controllerFileDataList">控制器实体类</param>
        /// <param name="rewriteData">重写配置数据</param>
        public void CreateOwinMiddlewareAndRouter(System.Collections.Generic.List<ControllerFileData> controllerFileDataList, ReWriteData rewriteData)
        {
            StreamWriter sw = File.CreateText(MapPath("/App_Start/NFinal/Middleware.cs"));
            var MiddlewareModel = new NFinal.Compile.Template.Startup.NFinal.MiddlewareModel
            { project = Frame.AssemblyTitle };
            var MiddlewareTemplate = new NFinal.Compile.Template.Startup.NFinal.Middleware();
            MiddlewareTemplate.Model = MiddlewareModel;
            sw.Write(MiddlewareTemplate.TransformText());
            //RazorEngine.Engine.Razor.RunCompile(
            //    System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.NFinal.NFinal.Middleware),
            //    "/App_Start/NFinal/Middleware.cs",
            //    sw,
            //    MiddlewareModel.GetType(),
            //    MiddlewareModel);
            sw.Dispose();

            sw = File.CreateText(MapPath("/App_Start/OwinRouter.cs"));
            var OwinRouterModel = new NFinal.Compile.Template.Startup.OwinRouterModel
            { project = Frame.AssemblyTitle, controllerFileDataList = controllerFileDataList, rewriteData = rewriteData };
            var OwinRouterTemplate = new NFinal.Compile.Template.Startup.OwinRouter();
            OwinRouterTemplate.Model = OwinRouterModel;
            sw.Write(OwinRouterTemplate.TransformText());
            //RazorEngine.Engine.Razor.RunCompile(
            //    System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.Startup.OwinRouter),
            //    "/App_Start/OwinRouter.cs",
            //    sw,
            //    OwinRouterModel.GetType(),
            //    OwinRouterModel);
            sw.Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerFileDataList"></param>
        /// <param name="rewriteData"></param>
        public void CreateNFinalOwinMiddlewareAndRouter(System.Collections.Generic.List<ControllerFileData> controllerFileDataList, ReWriteData rewriteData)
        {
            StreamWriter sw = File.CreateText(MapPath("/App_Start/NFinal/NFinalOwinMiddleware.cs"));
            var NFinalOwinMiddlewareModel = new NFinal.Compile.Template.Startup.NFinal.NFinalOwinMiddlewareModel
            { project = Frame.AssemblyTitle };
            var NFinalOwinMiddlewareTemplate = new NFinal.Compile.Template.Startup.NFinal.NFinalOwinMiddleware();
            NFinalOwinMiddlewareTemplate.Model = NFinalOwinMiddlewareModel;
            sw.Write(NFinalOwinMiddlewareTemplate.TransformText());
            //RazorEngine.Engine.Razor.RunCompile(
            //    System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.NFinal.NFinal.NFinalOwinMiddleware),
            //    "/App_Start/NFinal/NFinalOwinMiddleware.cs",
            //    sw,
            //    NFinalOwinMiddlewareModel.GetType(),
            //    NFinalOwinMiddlewareModel);
            sw.Dispose();

            sw = File.CreateText(MapPath("/App_Start/NFinalOwinRouter.cs"));
            var NFinalOwinRouterModel = new NFinal.Compile.Template.Startup.NFinalOwinRouterModel
            { project = Frame.AssemblyTitle, controllerFileDataList = controllerFileDataList, rewriteData = rewriteData };
            var NFinalOwinRouterTemplate = new NFinal.Compile.Template.Startup.NFinalOwinRouter();
            NFinalOwinRouterTemplate.Model = NFinalOwinRouterModel;
            sw.Write(NFinalOwinRouterTemplate.TransformText());
            //RazorEngine.Engine.Razor.RunCompile(
            //    System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.Startup.NFinalOwinRouter),
            //    "/App_Start/NFinalOwinRouter.cs",
            //    sw,
            //    NFinalOwinRouterModel.GetType(),
            //    NFinalOwinRouterModel);
            sw.Dispose();
        }
        /// <summary>
        /// URL解析
        /// </summary>
        /// <param name="controllerFileDataList">控制器实体类</param>
        /// <param name="rewriteData">重写配置数据</param>
        public void CreateModuleAndRouter(System.Collections.Generic.List<ControllerFileData> controllerFileDataList,ReWriteData rewriteData)
        {
            StreamWriter sw = File.CreateText(MapPath("/App_Start/NFinal/Module.cs"));
            var ModuleModel = new NFinal.Compile.Template.Startup.NFinal.ModuleModel
            { controllerFileDataList = controllerFileDataList, rewriteData = rewriteData };
            var ModuleTemplate = new NFinal.Compile.Template.Startup.NFinal.Module();
            ModuleTemplate.Model = ModuleModel;
            sw.Write(ModuleTemplate.TransformText());
            //RazorEngine.Engine.Razor.RunCompile(
            //    System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.NFinal.NFinal.Module),
            //    "/App_Start/NFinal/Module.cs",
            //    sw,
            //    ModuleModel.GetType(),
            //    ModuleModel);
            sw.Dispose();

            sw = File.CreateText(MapPath("/App_Start/NFinal/Handler.cs"));
            var HandlerModel = new NFinal.Compile.Template.Startup.NFinal.HandlerModel
            { project = Frame.AssemblyTitle };
            var HandlerTemplate = new NFinal.Compile.Template.Startup.NFinal.Handler();
            HandlerTemplate.Model = HandlerModel;
            sw.Write(HandlerTemplate.TransformText());
            //RazorEngine.Engine.Razor.RunCompile(
            //    System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.NFinal.NFinal.Handler),
            //    "/App_Start/NFinal/Handler.cs",
            //    sw,
            //    HandlerModel.GetType(),
            //    HandlerModel);
            sw.Dispose();

            sw = File.CreateText(MapPath("/App_Start/Router.cs"));
            var RouterModel = new NFinal.Compile.Template.Startup.RouterModel
            { project = Frame.AssemblyTitle, controllerFileDataList = controllerFileDataList, rewriteData = rewriteData };
            var RouterTemplate = new NFinal.Compile.Template.Startup.Router();
            RouterTemplate.Model = RouterModel;
            sw.Write(RouterTemplate.TransformText());
            //RazorEngine.Engine.Razor.RunCompile(
            //    System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.Startup.Router),
            //    "/App_Start/Router.cs",
            //    sw,
            //    RouterModel.GetType(),
            //    RouterModel);
            sw.Dispose();
        }
        /// <summary>
        /// URL生成
        /// </summary>
        /// <param name="controllerFileDataList">控制器实体类</param>
        /// <param name="rewriteData">重写配置数据</param>
        public void CreateURLHelper(System.Collections.Generic.List<ControllerFileData> controllerFileDataList, ReWriteData rewriteData)
        {
            StreamWriter sw = File.CreateText(MapPath("/App_Start/Url.cs"));
            var UrlModel = new NFinal.Compile.Template.Startup.UrlModel
            { controllerFileDataList = controllerFileDataList, rewriteData = rewriteData, project = Frame.AssemblyTitle };
            var UrlTemplate = new NFinal.Compile.Template.Startup.Url();
            UrlTemplate.Model = UrlModel;
            sw.Write(UrlTemplate.TransformText());
            //RazorEngine.Engine.Razor.RunCompile(
            //    System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.Startup.Url),
            //    "/App_Start/Url.cs",
            //    sw,
            //    UrlModel.GetType(),
            //    UrlModel);
            sw.Dispose();

            sw = File.CreateText(MapPath("/Scripts/Url.js"));
            var Url_JsModel = new NFinal.Compile.Template.Scripts.Url_JsModel
            { controllerFileDataList = controllerFileDataList, rewriteData = rewriteData, project = Frame.AssemblyTitle };
            var Url_JsTemplate = new NFinal.Compile.Template.Scripts.Url_Js();
            Url_JsTemplate.Model = Url_JsModel;
            sw.Write(Url_JsTemplate.TransformText());
            //RazorEngine.Engine.Razor.RunCompile(
            //    System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Scripts.Scripts.Url_Js),
            //    "/Scripts/Url.js",
            //    sw,
            //    Url_JsModel.GetType(),
            //    Url_JsModel);
            sw.Dispose();
        }
    }

}