using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using RazorEngine;
using RazorEngine.Templating;
//using System.DirectoryServices;

namespace NFinal.AutoConfig
{
    /// <summary>
    /// 模块初始化时自动配置类
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 项目根目录
        /// </summary>
        public static string appRoot;
        /// <summary>
        /// 项目名称
        /// </summary>
        public static string AssemblyTitle;
        /// <summary>
        /// 自动生成
        /// </summary>
        public static bool autoGeneration = false;
        /// <summary>
        /// IIS版本
        /// </summary>
        public enum IISVersion {
            IIS6, IIS7, IIS8, Unknown
        }
        /// <summary>
        /// 自动配置初始化类
        /// </summary>
        /// <param name="appRoot">项目根目录</param>
        public Config(string appRoot)
        {
            Config.appRoot = appRoot;
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
        /// 设置config
        /// </summary>
        /// <returns></returns>
        //public static IISVersion GetIISVersion()
        //{
        //    DirectoryEntry getEntity = new DirectoryEntry("IIS://localhost/W3SVC/INFO");
        //    try
        //    {
        //        double Version = Convert.ToDouble(getEntity.Properties["MajorIISVersionNumber"].Value);
        //        Console.WriteLine("IIS:" + Version.ToString());
        //        if (Version < 7)
        //        {
        //            return IISVersion.IIS6;
        //        }
        //        if (Version >= 7 && Version < 8)
        //        {
        //            return IISVersion.IIS7;
        //        }
        //        else if (Version >= 8)
        //        {
        //            return IISVersion.IIS8;
        //        }
        //        else
        //        {
        //            return IISVersion.Unknown;
        //        }
        //    }
        //    catch
        //    {
        //        return IISVersion.Unknown;
        //    }
        //}
        /// <summary>
        /// 修改MVC默认路由配置
        /// </summary>
        /// <param name="apps"></param>
        public void ModifyGlobal(string[] apps)
        {
            string registRouterPattern = @"RouteConfig\s*.\s*RegisterRoutes\s*\(";
            string globalFileName = MapPath("/Global.asax.cs");
            if (File.Exists(globalFileName))
            {
                StreamReader globalReader = new StreamReader(globalFileName);
                string globalString = globalReader.ReadToEnd();
                globalReader.Close();
                Regex globalReg = new Regex(registRouterPattern);
                Match globalMat = globalReg.Match(globalString);
                if (globalMat.Success)
                {
                    string ignoreRoute = string.Empty;
                    for (int i = 0; i < apps.Length; i++)
                    {
                        if (globalString.IndexOf(string.Format("\"{0}/{{*pathInfo}}\"", apps[i])) < 0)
                        {
                            ignoreRoute += string.Format("RouteTable.Routes.Add(new Route(\"{0}/{{*pathInfo}}\",new StopRoutingHandler()));\r\n\t\t\t", apps[i]);
                        }
                    }
                    globalString = globalString.Insert(globalMat.Index, ignoreRoute);
                    StreamWriter globalWriter = new StreamWriter(globalFileName);
                    globalWriter.Write(globalString);
                    globalWriter.Close();
                }
            }
        }
        /// <summary>
        /// 初始化handler.cs,Module.cs,router.cs等相关类
        /// </summary>
        /// <param name="app">模块名</param>
        public void InitHanderAndModuleAndRouter(string app)
        {
            StreamWriter sw = File.CreateText(getfileName(MapPath("/App_Start/NFinal/Handler.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.NFinal.NFinal.Handler_Init), 
                "/App_Start/NFinal/Handler.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/App_Start/NFinal/Module.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.NFinal.NFinal.Module_Init),
                "/App_Start/NFinal/Module.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/App_Start/Router.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.Startup.Router_Init),
                "/App_Start/Router.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();
        }
        /// <summary>
        /// 初始化url.cs,url.js等路径生成类
        /// </summary>
        /// <param name="app"></param>
        public void InitUrlFunction(string app)
        {
            StreamWriter sw = File.CreateText(getfileName(MapPath("/App_Start/Url.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.Startup.Url_Init),
                "/App_Start/Url.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/Scripts/Url.js")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Scripts.Scripts.Url_Js_Init),
                "/Scripts/Url.js",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();
        }
        /// <summary>
        /// 初始化默认配置config.json
        /// </summary>
        /// <param name="app"></param>
        public void InitConfigJson(string app)
        {
            StreamWriter sw = File.CreateText(getfileName(MapPath("/" + app + "/config.json")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.App.config_json),
                "/config.json",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();
        }
        /// <summary>
        /// 初始化 Extend.cs类
        /// </summary>
        /// <param name="app"></param>
        public void InitExtend(string app)
        {
            StreamWriter sw = File.CreateText(getfileName(MapPath("/" + app + "/Extend.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.App.Extension),
                "/Extend.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();
        }
        /// <summary>
        /// 获取文件路径时，并创建所有目录
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        public string getfileName(string fileName)
        {
            string dir = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return fileName;
        }
        /// <summary>
        /// 初始化模块下的数据文件,Common.db等
        /// </summary>
        /// <param name="app">模块名</param>
        public void InitAppData(string app)
        {
            string appDataFolder = MapPath("/" + app + "/Data");
            if (!Directory.Exists(appDataFolder))
            {
                Directory.CreateDirectory(appDataFolder);
                string dir = MapPath("/NFinal/Resource/App_Data");
                if (Directory.Exists(dir))
                {
                    CopyDirectory(dir, appDataFolder);
                }
            }
        }
        /// <summary>
        /// 初始化首页及在线编辑器
        /// </summary>
        /// <param name="app">模块名</param>
        public void InitIndexHTML(string app)
        {

            if (!File.Exists(MapPath("/IDE.html")))
            {
                StreamWriter sw = new StreamWriter(MapPath("/IDE.html"), false, System.Text.Encoding.UTF8);
                sw.Write(NFinal.Compile.Template.Template.IDE);
                sw.Close();
                sw.Dispose();
            }
            if (!File.Exists(MapPath("/" + app + "/Index.html")))
            {
                StreamWriter sw = new StreamWriter(MapPath("/" + app + "/Index.html"), false, System.Text.Encoding.UTF8);
                sw.Write(NFinal.Compile.Template.Template.Index);
                sw.Close();
                sw.Dispose();
            }
        }
        /// <summary>
        /// 初始化模块通用代码，CookieInfo.cs,CookieManager.cs,SessionInfo.cs
        /// SessionManager.cs,Navigator.cs,Vcode.cs等代码文件
        /// </summary>
        /// <param name="app">模块名</param>
        public void InitCode(string app)
        {
            StreamWriter sw = File.CreateText(getfileName(MapPath("/" + app + "/Code/Data/CookieInfo.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Code.Data.Data.CookieInfo),
                "/Code/Data/CookieInfo.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Code/Data/CookieManager.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Code.Data.Data.CookieManager_Init),
                "/Code/Data/CookieManager.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Code/Data/SessionInfo.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Code.Data.Data.SessionInfo),
                "/Code/Data/SessionInfo.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Code/Data/SessionManager.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Code.Data.Data.SessionManager_Init),
                "/Code/Data/SessionManager.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Code/UserControl/Navigator.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Code.UserControl.UserControl.Navigator_cs),
                "/Code/UserControl/Navigator.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Code/UserControl/Vcode.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Code.UserControl.UserControl.Vcode_cs),
                "/Code/UserControl/Vcode.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();
        }
        /// <summary>
        /// 初始化代码生成器
        /// </summary>
        /// <param name="app">模块名</param>
        public void InitWebCompiler(string app)
        {
            StreamWriter sw = File.CreateText(getfileName(MapPath("/" + app + "/WebCompiler.aspx")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.App.WebCompiler_aspx),
                "/WebCompiler.aspx",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/WebCompiler.aspx.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.App.WebCompiler_aspx_cs),
                "/WebCompiler.aspx.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();
        }
        public void CreateAppFolders(string app, string[] folders)
        {
            for (int i = 0; i < folders.Length; i++)
            {
                if (!Directory.Exists(MapPath("/" + app + folders[i])))
                {
                    Directory.CreateDirectory(MapPath("/" + app + folders[i]));
                }
            }
        }
        /// <summary>
        /// 初始化模块下的目录
        /// </summary>
        /// <param name="app">模块名</param>
        public void InitFolder(string app)
        {
            string[] folders = new string[]{"/Code",
            "/Content","/Controllers","/Data","/Models","/Models/BLL","/Models/DAL","/Models/Tips","/Models/Entity","/Views","/Web"};
            CreateAppFolders(app, folders);
        }
        /// <summary>
        /// 初始化全局目录
        /// </summary>
        public void InitFolder()
        {
            string[] folders = new string[] { "/Scripts", "/App_Start", "/App_Start/NFinal", "/App_Data" };
            CreateFolders(folders);
        }
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="folders">目录数组</param>
        public void CreateFolders(string[] folders)
        {
            string dir = null;
            for (int i = 0; i < folders.Length; i++)
            {
                dir = MapPath(folders[i]);
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
            }
        }
        /// <summary>
        /// 初始化全局js
        /// </summary>
        /// <param name="app"></param>
        public void InitScripts(string app)
        {
            CopyDirectory(MapPath("/NFinal/Resource/Scripts"), MapPath("/Scripts"));
            StreamWriter sw = File.CreateText(getfileName(MapPath("/Scripts/Url.js")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Scripts.Scripts.Url_Js_Init),
                "/Scripts/Url.js",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();
        }
        /// <summary>
        /// 初始化Startup.cs,OwinRouter.cs,ActionSearch.cs,Middleware.cs
        /// </summary>
        /// <param name="app"></param>
        public void InitAppStart(string app)
        {
            StreamWriter sw = File.CreateText(getfileName(MapPath("/App_Start/Startup.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.Startup.Startup_cs_Init),
                "/App_Start/Startup.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/App_Start/Adapter.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.Startup.Adapter),
                "/App_Start/Adapter.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/App_Start/OwinRouter.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.Startup.OwinRouter_Init),
                "/App_Start/OwinRouter.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/App_Start/NFinalOwinRouter.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.Startup.NFinalOwinRouter_Init),
                "/App_Start/NFinalOwinRouter.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/App_Start/NFinal/ActionSearch.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.NFinal.NFinal.ActionSearch_Init),
                "/App_Start/NFinal/ActionSearch.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/App_Start/NFinal/Middleware.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.NFinal.NFinal.Middleware_Init),
                "/App_Start/NFinal/Middleware.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/App_Start/NFinal/NFinalMiddleware.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Startup.NFinal.NFinal.NFinalOwinMiddelware_Init),
                "/App_Start/NFinal/NFinalMiddleware.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();
        }
        /// <summary>
        /// 初始化数据库自动提示类
        /// </summary>
        /// <param name="app"></param>
        public void InitModels(string app)
        {
            StreamWriter sw = File.CreateText(getfileName(MapPath("/" + app + "/Models/Common.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Models.Models.Common_cs),
                "/Models/Common.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();
        }
        /// <summary>
        /// 初始化通用模板
        /// </summary>
        /// <param name="app"></param>
        public void InitViews(string app)
        {
            UTF8Encoding encoding = new UTF8Encoding(false);
            StreamWriter sw = new StreamWriter(MapPath("/" + app + "/Views/Web.config"), false, encoding);
            sw.Write( 
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Views.Views.Web_config));
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Views/Default/Common/Public/Error.aspx")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Views.Default.Common.Public.Public.Error_aspx),
                "/Views/Default/Common/Public/Error.aspx",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Views/Default/Common/Public/Error.aspx.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Views.Default.Common.Public.Public.Error_aspx_cs),
                "/Views/Default/Common/Public/Error.aspx.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Views/Default/Common/Public/Footer.ascx")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Views.Default.Common.Public.Public.Footer_ascx),
                "/Views/Default/Common/Public/Footer.ascx",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Views/Default/Common/Public/Footer.ascx.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Views.Default.Common.Public.Public.Footer_ascx_cs),
                "/Views/Default/Common/Public/Footer.ascx.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Views/Default/Common/Public/Header.ascx")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Views.Default.Common.Public.Public.Header_ascx),
                "/Views/Default/Common/Public/Header.ascx",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Views/Default/Common/Public/Header.ascx.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Views.Default.Common.Public.Public.Header_ascx_cs),
                "/Views/Default/Common/Public/Header.ascx.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Views/Default/Common/Public/KindEditorLibrary.ascx")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Views.Default.Common.Public.Public.KindEditorLibrary_ascx),
                "/Views/Default/Common/Public/KindEditorLibrary.ascx",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Views/Default/Common/Public/KindEditorLibrary.ascx.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Views.Default.Common.Public.Public.KindEditorLibrary_ascx_cs),
                "/Views/Default/Common/Public/KindEditorLibrary.ascx.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Views/Default/Common/Public/Navigator.ascx")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Views.Default.Common.Public.Public.Navigator_ascx),
                "/Views/Default/Common/Public/Navigator.ascx",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Views/Default/Common/Public/Navigator.ascx.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Views.Default.Common.Public.Public.Navigator_ascx_cs),
                "/Views/Default/Common/Public/Navigator.ascx.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Views/Default/Common/Public/Success.aspx")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Views.Default.Common.Public.Public.Success_aspx),
                "/Views/Default/Common/Public/Success.aspx",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Views/Default/Common/Public/Success.aspx.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Views.Default.Common.Public.Public.Success_aspx_cs),
                "/Views/Default/Common/Public/Success.aspx.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Views/Default/IndexController/Index.aspx")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Views.Default.IndexController.Index.Index_aspx),
                "/Views/Default/IndexController/Index.aspx",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Views/Default/IndexController/Index.aspx.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Views.Default.IndexController.Index.Index_aspx_cs),
                "/Views/Default/IndexController/Index.aspx.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();
        }
        /// <summary>
        /// 初始化Web层
        /// </summary>
        /// <param name="app"></param>
        public void InitWeb(string app)
        {
            StreamWriter sw = File.CreateText(getfileName(MapPath("/" + app + "/Web/Default/Common/Public/Error.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Web.Default.Common.Public.Public.Error_cs),
                "/Web/Default/Common/Public/Error.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Web/Default/Common/Public/Error.html")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Web.Default.Common.Public.Public.Error_html),
                "/Web/Default/Common/Public/Error.html",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Web/Default/Common/Public/Footer.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Web.Default.Common.Public.Public.Footer_cs),
                "/Web/Default/Common/Public/Footer.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Web/Default/Common/Public/Footer.html")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Web.Default.Common.Public.Public.Footer_html),
                "/Web/Default/Common/Public/Footer.html",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Web/Default/Common/Public/Header.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Web.Default.Common.Public.Public.Header_cs),
                "/Web/Default/Common/Public/Header.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Web/Default/Common/Public/Header.html")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Web.Default.Common.Public.Public.Header_html),
                "/Web/Default/Common/Public/Header.html",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Web/Default/Common/Public/KindEditor.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Web.Default.Common.Public.Public.KindEditor_cs),
                "/Web/Default/Common/Public/KindEditor.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Web/Default/Common/Public/KindEditor.html")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Web.Default.Common.Public.Public.KindEditor_html),
                "/Web/Default/Common/Public/KindEditor.html",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Web/Default/Common/Public/Navigator.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Web.Default.Common.Public.Public.Navigator_cs),
                "/Web/Default/Common/Public/Navigator.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Web/Default/Common/Public/Navigator.html")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Web.Default.Common.Public.Public.Navigator_html),
                "/Web/Default/Common/Public/Navigator.html",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Web/Default/Common/Public/Success.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Web.Default.Common.Public.Public.Success_cs),
                "/Web/Default/Common/Public/Success.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Web/Default/Common/Public/Success.html")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Web.Default.Common.Public.Public.Success_html),
                "/Web/Default/Common/Public/Success.html",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Web/Default/Common/VCode/GetVerifyImage.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Web.Default.Common.VCode.VCode.GetVerifyImage_cs),
                "/Web/Default/Common/VCode/GetVerifyImage.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Web/Default/Common/VCode/GetVerifyImage.html")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Web.Default.Common.VCode.VCode.GetVerifyImage_html),
                "/Web/Default/Common/VCode/GetVerifyImage.html",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Web/Default/Common/VCode/VCheck.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Web.Default.Common.VCode.VCode.VCheck_cs),
                "/Web/Default/Common/VCode/VCheck.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Web/Default/Common/VCode/VCheck.html")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Web.Default.Common.VCode.VCode.VCheck_html),
                "/Web/Default/Common/VCode/VCheck.html",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Web/Default/IndexController/Index.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Web.Default.IndexController.IndexController.Index_cs),
                "/Web/Default/IndexController/Index.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Web/Default/IndexController/Index.html")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Web.Default.IndexController.IndexController.Index_html),
                "/Web/Default/IndexController/Index.html",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();
        }
        /// <summary>
        /// 复制目录
        /// </summary>
        /// <param name="SourcePath"></param>
        /// <param name="DestinationPath"></param>
        void CopyDirectory(string SourcePath,string DestinationPath)
        {
　　        //创建所有目录
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));
            }
　　        //复制所有文件
           foreach (string newPath in Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories))
           {
               if (!File.Exists(newPath.Replace(SourcePath, DestinationPath)))
               {
                   File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath));
               }
           }
        }
        /// <summary>
        /// 初始化静态文件目录
        /// </summary>
        /// <param name="app"></param>
        public void InitContent(string app)
        {
            CopyDirectory(MapPath("/NFinal/Content"),MapPath("/"+app+"/Content"));
        }
        /// <summary>
        /// 初始化基础控制器类Controller.cs
        /// </summary>
        /// <param name="app"></param>
        public void InitControllerBaseClass(string app)
        {
            StreamWriter sw = File.CreateText(getfileName(MapPath("/" + app + "/Controller.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.App.Controller),
                "/Controller.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();
        }
        /// <summary>
        /// 初始化通用控制器类，IndexController.cs等
        /// </summary>
        /// <param name="app"></param>
        public void InitControllers(string app)
        {
            StreamWriter sw = File.CreateText(getfileName(MapPath("/" + app + "/Controllers/Common/Public.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Controllers.Common.Common.Public_cs),
                "/Controllers/Common/Public.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Controllers/Common/VCode.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Controllers.Common.Common.VCode_cs),
                "/Controllers/Common/VCode.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();

            sw = File.CreateText(getfileName(MapPath("/" + app + "/Controllers/IndexController.cs")));
            RazorEngine.Engine.Razor.RunCompile(
                System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Controllers.Controllers.IndexController_cs),
                "/Controllers/IndexController.cs",
                sw,
                null,
                new { app = app, project = AssemblyTitle });
            sw.Dispose();
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
        /// 配置自动生成
        /// </summary>
        /// <param name="autoGen"></param>
        public static void SetAutoCreateConfig(bool autoGen)
        {
            string projectFileName = MapPath("/" + AssemblyTitle + ".csproj");
            bool changed = false;

            if (File.Exists(projectFileName))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(projectFileName);
                XmlNamespaceManager xmanager = new XmlNamespaceManager(doc.NameTable);
                string nameSpace = doc.DocumentElement.Attributes["xmlns"].Value;
                xmanager.AddNamespace("x", nameSpace);

                XmlNode nodePostBuildEvent = doc.SelectSingleNode("/x:Project/x:PropertyGroup/x:PostBuildEvent", xmanager);
                if (autoGen)
                {
                    if (nodePostBuildEvent == null)
                    {
                        changed = true;
                        nodePostBuildEvent = doc.CreateElement("PostBuildEvent", nameSpace);
                        nodePostBuildEvent.InnerText = "$(ProjectDir)NFinal\\WebCompiler.exe";
                        XmlNode nodePropertyGroup = doc.CreateElement("PropertyGroup", nameSpace);
                        nodePropertyGroup.AppendChild(nodePostBuildEvent);
                        doc.DocumentElement.AppendChild(nodePropertyGroup);
                    }
                    else
                    {
                        if (nodePostBuildEvent.InnerText == string.Empty)
                        {
                            changed = true;
                            nodePostBuildEvent.InnerText = "$(ProjectDir)NFinal\\WebCompiler.exe";
                        }
                    }
                }
                else
                {
                    if (nodePostBuildEvent != null)
                    {
                        changed = true;
                        nodePostBuildEvent.ParentNode.RemoveChild(nodePostBuildEvent);
                    }
                }
                if (changed)
                {
                    doc.Save(projectFileName);
                }
            }
        }
        /// <summary>
        /// 根据xmlpath创建xml节点
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static XmlNode CreateXMLPath(XmlDocument doc, string root)
        {
            string[] pathes = root.Trim('/').Split('/');
            string oldXpath = "", newXpath = "";
            if (pathes.Length > 1)
            {
                newXpath = pathes[0];
                XmlNode newNode = null, oldNode = null;
                for (int i = 1; i < pathes.Length; i++)
                {
                    oldXpath = newXpath;
                    newXpath += "/" + pathes[i];
                    newNode = doc.SelectSingleNode(newXpath);

                    oldNode = doc.SelectSingleNode(oldXpath);
                    if (newNode == null)
                    {
                        newNode = doc.CreateElement(pathes[i]);
                        oldNode.AppendChild(newNode);
                    }
                }
                return newNode;
            }
            return doc.DocumentElement;
        }


        /// <summary>
        /// 获取所有的应用模块
        /// </summary>
        /// <returns>项目根目录</returns>
        public static string[] GetApps(string appRoot)
        {
            string[] dirs = Directory.GetDirectories(appRoot);
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
        /// 自动配置WebConfig
        /// </summary>
        /// <param name="version">IIS版本号</param>
        /// <param name="appRoot">项目根目录</param>
        /// <param name="Apps">模块名数组</param>
        public static void SetWebConfig(IISVersion version, string appRoot,string[] Apps)
        {
            Config.appRoot = appRoot;
            Config.AssemblyTitle = new DirectoryInfo(Config.appRoot).Name;
            //文档是否变动
            bool changed = false;
            
            //配置.net信息
            string webConfig = MapPath("/Web.config");
            if (!File.Exists(webConfig))
            {
                changed = true;
                StreamWriter sw = new StreamWriter(MapPath("/Web.config"), false, System.Text.Encoding.UTF8);
                sw.Write(NFinal.Compile.Template.Template.Web_config);
                sw.Close();
                sw.Dispose();
                sw = new StreamWriter(MapPath("/Web.Debug.config"), false, System.Text.Encoding.UTF8);
                sw.Write(NFinal.Compile.Template.Template.Web_config_Debug);
                sw.Close();
                sw.Dispose();
                sw = new StreamWriter(MapPath("/Web.Release.config"), false, System.Text.Encoding.UTF8);
                sw.Write(NFinal.Compile.Template.Template.Web_config_Release);
                sw.Close();
                sw.Dispose();
            }


            //根据环境配置webconfig
            System.Xml.XmlDocument doc = new XmlDocument();
            doc.Load(webConfig);
            XmlNode root = doc.DocumentElement;
            if (root == null)
            {
                return;
            }
            //禁用VS Browser Link功能
            XmlNode enableBrowserLinkNode= doc.DocumentElement.SelectSingleNode("configuration/appSettings/add[@key='vs:EnableBrowserLink']");
            if (enableBrowserLinkNode == null)
            {
                enableBrowserLinkNode = CreateXMLPath(doc, "configuration/appSettings/add");
            }
            if (enableBrowserLinkNode.Attributes["key"] != null)
            {
                enableBrowserLinkNode.Attributes["key"].Value = "vs:EnableBrowserLink";
            }
            else
            {
                XmlAttribute enableBrowserLinkAttrKey = doc.CreateAttribute("key");
                enableBrowserLinkAttrKey.Value = "vs:EnableBrowserLink";
                enableBrowserLinkNode.Attributes.Append(enableBrowserLinkAttrKey);
            }
            if (enableBrowserLinkNode.Attributes["value"] != null)
            {
                enableBrowserLinkNode.Attributes["value"].Value = "false";
            }
            else
            {
                XmlAttribute enableBrowserLinkAttrValue = doc.CreateAttribute("value");
                enableBrowserLinkAttrValue.Value = "false";
                enableBrowserLinkNode.Attributes.Append(enableBrowserLinkAttrValue);
            }
            //< !--删除http协议中无用的version头-- >
            XmlNode httpRuntimeNode = doc.DocumentElement.SelectSingleNode("configuration/system.web/httpRuntime");
            if (httpRuntimeNode == null)
            {
                httpRuntimeNode = CreateXMLPath(doc, "configuration/system.web/httpRuntime");
            }
            if (httpRuntimeNode.Attributes["enableVersionHeader"] != null)
            {
                httpRuntimeNode.Attributes["enableVersionHeader"].Value = "false";
            }
            else
            {
                XmlAttribute httpRuntimeEnableVersionHeaderAttr = doc.CreateAttribute("enableVersionHeader");
                httpRuntimeEnableVersionHeaderAttr.Value = "false";
                httpRuntimeNode.Attributes.Append(httpRuntimeEnableVersionHeaderAttr);
            }
            //< !--session重命名-- >
            XmlNode sessionStateNode = doc.DocumentElement.SelectSingleNode("configuration/system.web/sessionState");
            if (sessionStateNode == null)
            {
                sessionStateNode = CreateXMLPath(doc, "configuration/system.web/sessionState");
            }
            if (sessionStateNode.Attributes["cookieName"] != null)
            {
                sessionStateNode.Attributes["cookieName"].Value = "session_id";
            }
            else
            {
                XmlAttribute sessionStateNodeCookieNameAttr = doc.CreateAttribute("cookieName");
                sessionStateNodeCookieNameAttr.Value = "session_id";
                sessionStateNode.Attributes.Append(sessionStateNodeCookieNameAttr);
            }
            //<!--删除无用的http头-->
            XmlNode customHeadersNode = doc.DocumentElement.SelectSingleNode("configuration/system.webServer/httpProtocol/customHeaders");
            if (customHeadersNode == null)
            {
                customHeadersNode = CreateXMLPath(doc, "configuration/system.webServer/httpProtocol/customHeaders");
            }
            customHeadersNode.RemoveAll();
            customHeadersNode.InnerXml = "<clear/>";
            //如果是IIS7,IIS8新版本,要添加system.webserver配置节
            if (version == IISVersion.IIS7 || version == IISVersion.IIS8)
            {
                //取消验证
                XmlNode newValidationNode = doc.DocumentElement.SelectSingleNode("system.webServer/validation");
                if (newValidationNode == null)
                {
                    newValidationNode = CreateXMLPath(doc, "configuration/system.webServer/validation");
                }
                newValidationNode = doc.DocumentElement.SelectSingleNode("system.webServer/validation");
                if (newValidationNode.Attributes["validateIntegratedModeConfiguration"] != null)
                {
                    newValidationNode.Attributes["validateIntegratedModeConfiguration"].Value = "false";
                }
                else
                {
                    XmlAttribute validateIntegratedModeConfigurationAttr= doc.CreateAttribute("validateIntegratedModeConfiguration");
                    validateIntegratedModeConfigurationAttr.Value = "false";
                    newValidationNode.Attributes.Append(validateIntegratedModeConfigurationAttr);
                }                
                //添加NFinal.Handler
                XmlNode newFactoryNode = doc.DocumentElement.SelectSingleNode("system.webServer/handlers/add[@type='NFinal.Handler']");
                //不存在则添加
                if (newFactoryNode == null)
                {
                    changed = true;
                    XmlNode handlersNode = CreateXMLPath(doc, "configuration/system.webServer/handlers");
                    XmlNode NFinalHandlerNode = doc.CreateElement("add");
                    XmlAttribute attrName = doc.CreateAttribute("name");
                    attrName.Value = "NFinalHandler";
                    XmlAttribute attrVerb = doc.CreateAttribute("verb");
                    attrVerb.Value = "*";
                    XmlAttribute attrPath = doc.CreateAttribute("path");
                    attrPath.Value = "*.nf";
                    XmlAttribute attrType = doc.CreateAttribute("type");
                    attrType.Value = "NFinal.Handler";
                    XmlAttribute attrPreCondition = doc.CreateAttribute("preCondition");
                    attrPreCondition.Value = "integratedMode";
                    NFinalHandlerNode.Attributes.RemoveAll();
                    NFinalHandlerNode.Attributes.Append(attrName);
                    NFinalHandlerNode.Attributes.Append(attrVerb);
                    NFinalHandlerNode.Attributes.Append(attrPath);
                    NFinalHandlerNode.Attributes.Append(attrType);
                    NFinalHandlerNode.Attributes.Append(attrPreCondition);
                    handlersNode.AppendChild(NFinalHandlerNode);
                }
                //添加NFinal.Module
                XmlNode newRewriterNode = doc.DocumentElement.SelectSingleNode("system.webServer/modules/add[@type='NFinal.Module']");
                if (newRewriterNode == null)
                {
                    changed = true;
                    XmlNode modulesNode = CreateXMLPath(doc, "configuration/system.webServer/modules");
                    XmlNode urlRewriterNode = doc.CreateElement("add");
                    XmlAttribute attrName = doc.CreateAttribute("name");
                    attrName.Value = "NFinalModule";
                    XmlAttribute attrType = doc.CreateAttribute("type");
                    attrType.Value = "NFinal.Module";
                    XmlAttribute attrPreCondition = doc.CreateAttribute("preCondition");
                    attrPreCondition.Value = "integratedMode";
                    urlRewriterNode.Attributes.Append(attrName);
                    urlRewriterNode.Attributes.Append(attrType);
                    urlRewriterNode.Attributes.Append(attrPreCondition);
                    modulesNode.AppendChild(urlRewriterNode);
                }
                //添加属性<modules runAllManagedModulesForAllRequests="true">
                XmlNode modules = doc.DocumentElement.SelectSingleNode("system.webServer/modules");
                if (modules != null)
                {
                    if (modules.Attributes["runAllManagedModulesForAllRequests"] == null)
                    {
                        XmlAttribute attrRunAllModules = doc.CreateAttribute("runAllManagedModulesForAllRequests");
                        attrRunAllModules.Value = "true";
                        modules.Attributes.Append(attrRunAllModules);
                    }
                    else
                    {
                        modules.Attributes["runAllManagedModulesForAllRequests"].Value = "true";
                    }
                }
            }
            //IIS8下要删除老的配置节点
            if(version==IISVersion.IIS8)
            {
                XmlNode FactoryNode = doc.DocumentElement.SelectSingleNode("system.web/httpHandlers");
                if (FactoryNode != null)
                {
                    FactoryNode.ParentNode.RemoveChild(FactoryNode);
                }
                XmlNode RewriterNode = doc.DocumentElement.SelectSingleNode("system.web/httpModules");
                if (RewriterNode != null)
                {
                    RewriterNode.ParentNode.RemoveChild(RewriterNode);
                }
            }
            //IS6,IIS7下的system.web配置
            if (version == IISVersion.IIS6 || version == IISVersion.IIS7)
            {
                //添加NFinal节点属性
                XmlNode FactoryNode = doc.DocumentElement.SelectSingleNode("system.web/httpHandlers/add[@type='NFinal.Handler']");
                //不存在则添加
                if (FactoryNode == null)
                {
                    changed = true;
                    XmlNode handlersNode = CreateXMLPath(doc, "configuration/system.web/httpHandlers");
                    XmlNode NFinalHandlerNode = doc.CreateElement("add");
                    XmlAttribute attrVerb = doc.CreateAttribute("verb");
                    attrVerb.Value = "*";
                    XmlAttribute attrPath = doc.CreateAttribute("path");
                    attrPath.Value = "*.nf";
                    XmlAttribute attrType = doc.CreateAttribute("type");
                    attrType.Value = "NFinal.Handler";
                    NFinalHandlerNode.Attributes.RemoveAll();
                    NFinalHandlerNode.Attributes.Append(attrVerb);
                    NFinalHandlerNode.Attributes.Append(attrPath);
                    NFinalHandlerNode.Attributes.Append(attrType);
                    handlersNode.AppendChild(NFinalHandlerNode);
                }

                //添加Rewriter节点属性
                XmlNode RewriterNode = doc.DocumentElement.SelectSingleNode("system.web/httpModules/add[@type='NFinal.Module']");
                if (RewriterNode == null)
                {
                    changed = true;
                    XmlNode modulesNode = CreateXMLPath(doc, "configuration/system.web/httpModules");
                    XmlNode urlRewriterNode = doc.CreateElement("add");
                    XmlAttribute attrName = doc.CreateAttribute("name");
                    attrName.Value = "NFinalModule";
                    XmlAttribute attrType = doc.CreateAttribute("type");
                    attrType.Value = "NFinal.Module";
                    urlRewriterNode.Attributes.Append(attrName);
                    urlRewriterNode.Attributes.Append(attrType);
                    modulesNode.AppendChild(urlRewriterNode);
                }
            }
            //IIS6下要删除新的system.webServer的配置
            if (version == IISVersion.IIS6)
            {
                XmlNode newWebServerNode = doc.DocumentElement.SelectSingleNode("system.webServer");
                if (newWebServerNode != null)
                {
                    newWebServerNode.ParentNode.RemoveChild(newWebServerNode);
                }
            }
            //如果文档变动就保存
            if (changed)
            {
                doc.Save(webConfig);
            }
        }
    }
}
