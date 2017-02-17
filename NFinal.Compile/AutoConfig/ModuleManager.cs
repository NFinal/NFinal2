using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NFinal.AutoConfig
{
    /// <summary>
    /// AutoConfig.exe自动配置类
    /// </summary>
    public class ModuleManager
    {
        private string root;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="root">项目根目录</param>
        public ModuleManager(string root)
        {
            this.root = root;
        }
        /// <summary>
        /// 初始化模块
        /// </summary>
        public void CreateModule()
        {
            string webconfig = root + "Web.config";
            FileInfo[] projFiles = new DirectoryInfo(root).GetFiles("*.csproj");
            if (File.Exists(webconfig))
            {
                NFinal.AutoConfig.HtmlXSD.RewriteXSD(root);
                Project proj = null;
                //如果是Web应用程序
                if (projFiles.Length > 0)
                {
                    Console.WriteLine("请输入要添加的模块名称:");
                    string AppName = Console.ReadLine();
                    Config.IISVersion version = Config.IISVersion.IIS7;
                    if (version == Config.IISVersion.Unknown)
                    {
                        version = Config.IISVersion.IIS7;
                    }
                    Console.WriteLine("开始引入dll");
                    proj = new Project(projFiles[0].FullName);
                    //添加相关dll文件
                    //proj.AddLibrary();
                    //proj.AddFiles(proj.MapPath("NFinal"));
                    Console.WriteLine("开始配置Web.config");
                    string[] apps = Config.GetApps(root);
                    //设置webconfig
                    Config.SetWebConfig(version, root, apps);
                    Config config = new Config(root);
                    //配置全局,兼容.net mvc
                    Console.WriteLine("配置路由兼容");
                    config.ModifyGlobal(apps);
                    Console.WriteLine("生成全局文件夹");
                    //,App_Start,Scripts
                    config.InitFolder();
                    Console.WriteLine("生成全局Scripts文件夹");
                    config.InitScripts(AppName);
                    Console.WriteLine("生成vNext文件");
                    config.InitAppStart(AppName);
                    //如果已经配置过,则不需要重新生成
                    string AppDir = root + "\\" + AppName;
                    if (Directory.Exists(AppDir))
                    {
                        Console.WriteLine(AppName + "已经存在.");
                        Console.ReadKey();
                        return;
                    }
                    Console.WriteLine("初始化数据库");
                    //初始化数据库
                    config.InitAppData(AppName);
                    Console.WriteLine("创建主页");
                    //创建主页文件
                    config.InitIndexHTML(AppName);
                    //路由
                    Console.WriteLine("初始化路由");
                    config.InitHanderAndModuleAndRouter(AppName);
                    //Url生成
                    Console.WriteLine("初始化URL生成");
                    config.InitUrlFunction(AppName);
                    Console.WriteLine("\r\n开始配置" + AppName);
                    //App
                    Directory.CreateDirectory(root + "\\" + AppName + "\\");
                    //folders
                    config.InitFolder(AppName);
                    //App/config.json
                    Console.WriteLine("生成config.json");
                    config.InitConfigJson(AppName);
                    Console.WriteLine("生成Controller.cs");
                    config.InitControllerBaseClass(AppName);
                    Console.WriteLine("生成Extension.cs");
                    //App/Extend.cs
                    config.InitExtend(AppName);
                    Console.WriteLine("生成WebCompiler.aspx");
                    //App/WebCompiler.cs
                    config.InitWebCompiler(AppName);
                    Console.WriteLine("生成Common层");
                    //Code
                    config.InitCode(AppName);
                    Console.WriteLine("生成Content层");
                    //Content
                    config.InitContent(AppName);
                    Console.WriteLine("生成Controllers层");
                    //Controllers
                    config.InitControllers(AppName);
                    Console.WriteLine("生成Models层");
                    //Models
                    config.InitModels(AppName);
                    Console.WriteLine("生成Views层");
                    //Views
                    config.InitViews(AppName);
                    Console.WriteLine("生成Web层");
                    //Web
                    config.InitWeb(AppName);
                    //添加模块文件
                    proj.AddModule(proj.MapPath(AppName));
                    proj.AddFile(proj.MapPath("IDE.html"));
                    proj.AddFile(proj.MapPath("Web.config"));
                    proj.AddFiles(proj.MapPath("/Scripts"));
                    proj.AddFiles(proj.MapPath("/App_Start"));
                    proj.Save();
                    Console.WriteLine(AppName + "配置完成\r\n");
                }
                else
                {
                    Console.WriteLine("NFinal框架不能安装在Web网站上.");
                    Console.WriteLine("请把Web网站转化为Web应用程序,然后再安装即可.");
                }
                Console.WriteLine("请按任意键退出...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("请把NFinal放到项目根目录下");
                Console.WriteLine("请按任意键退出...");
                Console.ReadKey();
            }
        }
    }
}
