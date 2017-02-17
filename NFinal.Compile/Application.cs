//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename :Application.cs
//        Description :生成项目
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Web;
using NFinal.Compile;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using RazorEngine;
using RazorEngine.Templating;

namespace NFinal.Compile
{
    /// <summary>
    /// 模块生成类
    /// </summary>
    public class Application
    {
        /// <summary>
        /// 配置
        /// </summary>
        public Config config;
        /// <summary>
        /// 生成配置
        /// </summary>
        public GenConfig genConfig;
        /// <summary>
        /// 实始化
        /// </summary>
        /// <param name="config">模块配置</param>
        public Application(Config config)
        {
            this.config = config;
            genConfig = GenConfig.Load(Frame.MapPath(config.APP + "compile.xml"));
        }
        /// <summary>
        /// 把路径转为命名空间
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public string GetNameSpace(string folder)
        {
            return Frame.AssemblyTitle + "." + folder.Trim('/').Replace('/', '.');
        }
        /// <summary>
        /// 生成实体类
        /// </summary>
        public void CreateEntities()
        {

        }
        /// <summary>
        /// 创建Cookie操作类
        /// </summary>
        public void CreateCookieManager()
        {
            StreamReader sr = new StreamReader(Frame.MapPath(config.Code + "Data/CookieInfo.cs"), System.Text.Encoding.UTF8);
            string CookieInfoCodeString = sr.ReadToEnd();
            NFinal.Compile.CSharpDeclarationParser parser = new CSharpDeclarationParser();
            System.Collections.Generic.List<NFinal.Compile.CSharpDeclaration> declarations = parser.Parse(CookieInfoCodeString);
            for (int i = 0; i < declarations.Count; i++)
            {
                switch (declarations[i].typeName.Trim())
                {
                    case "System.String": declarations[i].isString = true; break;
                    case "String": declarations[i].isString = true; break;
                    case "string": declarations[i].isString = true; break;
                    default: declarations[i].isString = false; break;
                }
            }
            StreamWriter sw = File.CreateText(Frame.MapPath(config.Code + "Data/CookieManager.cs"));
            var CookieManagerModel = new NFinal.Compile.Template.App.Code.Data.CookieManagerModel
            { project = Frame.AssemblyTitle,
                app = config.APP.Trim('/'),
                declarations = declarations,
                cookiePrefix = config.cookiePrefix };
            var CookieManagerTemplate = new NFinal.Compile.Template.App.Code.Data.CookieManager();
            CookieManagerTemplate.Model = CookieManagerModel;
            sw.Write(CookieManagerTemplate.TransformText());
            //RazorEngine.Engine.Razor.RunCompile(
            //    System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Code.Data.Data.CookieManager),
            //    "Data/CookieManager.cs",
            //    sw,
            //    CookieManagerModel.GetType(),
            //    CookieManagerModel);
            sw.Dispose();

        }
        /// <summary>
        /// 创建Session操作类
        /// </summary>
        public void CreateSessionManager()
        {
            StreamReader sr = new StreamReader(Frame.MapPath(config.Code + "Data/SessionInfo.cs"), System.Text.Encoding.UTF8);
            string CookieInfoCodeString = sr.ReadToEnd();
            NFinal.Compile.CSharpDeclarationParser parser = new CSharpDeclarationParser();
            System.Collections.Generic.List<NFinal.Compile.CSharpDeclaration> declarations = parser.Parse(CookieInfoCodeString);
            for (int i = 0; i < declarations.Count; i++)
            {
                switch (declarations[i].typeName.Trim())
                {
                    case "System.String": declarations[i].isString = true; break;
                    case "String": declarations[i].isString = true; break;
                    case "string": declarations[i].isString = true; break;
                    default: declarations[i].isString = false; break;
                }
            }
            StreamWriter sw = File.CreateText(Frame.MapPath(config.Code + "Data/SessionManager.cs"));
            var SessionManagerModel = new NFinal.Compile.Template.App.Code.Data.SessionManagerModel
            { project = Frame.AssemblyTitle,
                app = config.APP.Trim('/'),
                declarations = declarations,
                cookiePrefix = config.cookiePrefix,
                sessionPrefix = config.sessionPrefix };
            var SessionManagerTemplate = new NFinal.Compile.Template.App.Code.Data.SessionManager();
            SessionManagerTemplate.Model = SessionManagerModel;
            sw.Write(SessionManagerTemplate.TransformText());
            //RazorEngine.Engine.Razor.RunCompile(
            //    System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Code.Data.Data.SessionManager),
            //    "Data/SessionManager.cs",
            //    sw,
            //    SessionManagerModel.GetType(),
            //    SessionManagerModel);
            sw.Dispose();
        }
        /// <summary>
        /// 创建Cookie实体类
        /// </summary>
        //public void CreateCommonFile()
        //{
        //    if (!Directory.Exists(Frame.MapPath(config.Code)))
        //    {
        //        Directory.CreateDirectory(Frame.MapPath(config.Code));
        //    }
        //    if (!Directory.Exists(Frame.MapPath(config.Code + "Data")))
        //    {
        //        Directory.CreateDirectory(Frame.MapPath(config.Code + "Data"));
        //    }
        //    StreamWriter sw = File.CreateText(Frame.MapPath(config.Code + "Data/CookieInfo.cs"));
        //    var CookieInfoModel = new
        //    { project = Frame.AssemblyTitle,
        //        app = config.APP.Trim('/') };
        //    RazorEngine.Engine.Razor.RunCompile(
        //        System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Code.Data.Data.CookieInfo),
        //        "Data/CookieInfo.cs",
        //        sw,
        //        null,
        //        CookieInfoModel);
        //    sw.Dispose();
        //    CreateCookieManager();
        //}
        /// <summary>
        /// 创建ORM实体类及自动提示类
        /// </summary>
        public void CreateModelsFile()
        {
            if (Frame.ConnectionStrings.Count > 0)
            {
                StreamWriter sw = null;
                for (int i = 0; i < Frame.ConnectionStrings.Count; i++)
                {
                    sw = File.CreateText(Frame.MapPath(config.Models + Frame.ConnectionStrings[i].name + ".cs"));
                    var DBModel = new NFinal.Compile.Template.App.Models.DBModel
                    { project = Frame.AssemblyTitle, app = config.APP.Trim('/'), database = Frame.ConnectionStrings[i].name };
                    var DBTemplate = new NFinal.Compile.Template.App.Models.DB();
                    DBTemplate.Model = DBModel;
                    sw.Write(DBTemplate.TransformText());
                    //RazorEngine.Engine.Razor.RunCompile(
                    //    System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Models.Models.DB),
                    //    "Models/DB.cs",
                    //    sw,
                    //    DBModel.GetType(),
                    //    DBModel);
                    sw.Dispose();
                }
                //生成oracle创建自增字段的代码
                for (int i = 0; i < Frame.ConnectionStrings.Count; i++)
                {
                    if (Frame.ConnectionStrings[i].type == DB.DBType.Oracle)
                    {
                        sw = File.CreateText(Frame.MapPath(config.Models + Frame.ConnectionStrings[i].name + ".sql"));
                        var IdIncrementModel = new NFinal.Compile.SqlTemplate.Model.IdIncrement
                        { tables = DB.Coding.DbCache.DbStore[Frame.ConnectionStrings[i].name].tables };
                        NFinal.Compile.SqlTemplate.oracle.IdIncrement idIncrement = new SqlTemplate.oracle.IdIncrement();
                        idIncrement.Model = IdIncrementModel;
                        sw.Write(idIncrement.TransformText());
                        
                        //RazorEngine.Engine.Razor.RunCompile(
                        //    System.Text.Encoding.UTF8.GetString(NFinal.Compile.SqlTemplate.oracle.oracle.IdIncrement),
                        //    "Oracle/IdIncrement.sql",
                        //    sw,
                        //    IdIncrementModel.GetType(),
                        //    IdIncrementModel);
                        sw.Dispose();
                    }
                }
                //生成自动提示类
                string tipsPath;
                if (DB.Coding.DbCache.DbStore.Count > 0)
                {
                    for (int i = 0; i < Frame.ConnectionStrings.Count; i++)
                    {
                        tipsPath = Frame.MapPath(config.Models + "Tips/" + Frame.ConnectionStrings[i].name);
                        if (!Directory.Exists(tipsPath))
                        {
                            Directory.CreateDirectory(tipsPath);
                        }
                        foreach (DB.Coding.Table table in DB.Coding.DbCache.DbStore[Frame.ConnectionStrings[i].name].tables)
                        {
                            sw = File.CreateText(tipsPath + "\\" + table.name + ".cs");
                            var TipsModel = new NFinal.Compile.Template.App.Models.TipsModel
                            { database = Frame.ConnectionStrings[i].name,
                                project = Frame.AssemblyTitle,
                                app = config.APP.Trim('/'),
                                table = table };
                            var TipsTemplate = new NFinal.Compile.Template.App.Models.Tips();
                            TipsTemplate.Model = TipsModel;
                            sw.Write(TipsTemplate.TransformText());
                            //RazorEngine.Engine.Razor.RunCompile(
                            //    System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Models.Models.Tips),
                            //    "Models/Tips.cs",
                            //    sw,
                            //    TipsModel.GetType(),
                            //    TipsModel);
                            sw.Dispose();
                        }
                    }
                }
                //生成实体类
                string entityPath;
                if (DB.Coding.DbCache.DbStore.Count > 0)
                {
                    for (int i = 0; i < Frame.ConnectionStrings.Count; i++)
                    {
                        entityPath = Frame.MapPath(config.Models + "Entity/" + Frame.ConnectionStrings[i].name);
                        if (!Directory.Exists(entityPath))
                        {
                            Directory.CreateDirectory(entityPath);
                        }
                        foreach (DB.Coding.Table table in DB.Coding.DbCache.DbStore[Frame.ConnectionStrings[i].name].tables)
                        {
                            sw = File.CreateText(entityPath + "\\" + table.name + ".cs");
                            string nameSpace = $"{Frame.AssemblyTitle}.{config.APP.Trim()}.Models.Entity.{Frame.ConnectionStrings[i].name}";
                            var EntityModel = new NFinal.Compile.Template.App.Models.EntityModel
                            {  nameSpace=nameSpace,
                                table = table };
                            var EntityTemplate = new NFinal.Compile.Template.App.Models.Entity();
                            EntityTemplate.Model = EntityModel;
                            sw.Write(EntityTemplate.TransformText());
                            //RazorEngine.Engine.Razor.RunCompile(
                            //    System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.App.Models.Models.Entity),
                            //    "Models/Entity.cs",
                            //    sw,
                            //    EntityModel.GetType(),
                            //    EntityModel);
                            sw.Dispose();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 创建模块默认目录
        /// </summary>
        /// <param name="folders">目录数组</param>
        private void CreateFolders(string[] folders)
        {
            string folder = "/";
            for (int i = 0; i < folders.Length; i++)
            {
                folder = Frame.MapPath(folders[i]);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }
        }
        /// <summary>
        /// 创建模块默认目录
        /// </summary>
        public void CreateApp()
        {
            //创建目录
            string[] folders = { config.Controller,
                                   config.Code,
                                   config.Views,
                                   config.Views+config.defaultStyle,
                                   config.Views+config.defaultStyle+"/IndexController/",
                                   config.BLL,
                                   config.DAL,
                                   config.Web ,
                                   config.Content,
                                   config.ContentJS,
                                   config.ContentImages,
                                   config.ContentCss,
                                   config.Models
                               };
            CreateFolders(folders);
            ////生成首页
            //string fileName = Frame.MapPath(config.Controller + string.Format("Index{0}.cs",config.ControllerSuffix));
            //if (!File.Exists(fileName))
            //{
            //    JinianNet.JNTemplate.ITemplate template =new JinianNet.JNTemplate.Template(
            //        System.Text.Encoding.UTF8.GetString(NFinal.Template.Template.IndexController_cs));
            //    template.Context.TempData["nameSpace"] = Frame.AssemblyTitle + "." + config.Controller.Trim('/').Replace('/', '.');
            //    template.Render(fileName,System.Text.Encoding.UTF8);
            //}
            ////生成首页
            //fileName = Frame.MapPath(config.Views +config.defaultStyle+ "/Index"+config.ControllerSuffix+"/" + "Index.aspx");
            //if (!File.Exists(fileName))
            //{
            //    File.Copy(Frame.MapPath("/NFinal/Template/Index.aspx.tpl"), fileName);
            //}
        }

        /// <summary>
        /// 字符串替换函数
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="index">替换开始位置</param>
        /// <param name="length">替换长度</param>
        /// <param name="rep">替换的字符串</param>
        /// <returns></returns>
        public int Replace(ref string str, int index, int length, string rep)
        {
            if (length > 0)
            {
                str = str.Remove(index, length);
            }
            if (index > 0)
            {
                str = str.Insert(index, rep);
            }
            return rep.Length - length;
        }
        /// <summary>
        /// 生成ORM的业务逻辑层
        /// </summary>
        /// <param name="build">是否生成</param>
        public void CreateDAL(bool build)
        {
            string[] fileNames;
            //获取要生成的文件
            if (genConfig != null)
            {
                if (genConfig.bllFiles.Count > 0)
                {
                    fileNames = new string[genConfig.bllFiles.Count];
                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        fileNames[i] = Frame.MapPath(genConfig.bllFiles[i]);
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (!Directory.Exists(Frame.MapPath(config.BLL)))
                {
                    return;
                }
                //获取所有的bll文件
                fileNames = Directory.GetFiles(Frame.MapPath(config.BLL), "*.cs", SearchOption.AllDirectories);
                if (fileNames == null || fileNames.Length < 1)
                {
                    return;
                }
            }

            //生成的代码
            string compileCode = "";
            //类名称
            string ClassName = "";

            int relative_position = 0;

            if (fileNames.Length > 0)
            {
                ControllerAnalyse com = new ControllerAnalyse();
                ControllerFileData fileData = null;
                ControllerClassData classData = null;
                ControllerMethodData methodData = null;
                StreamWriter sw = null;

                string compileFileName = "";
                string compileDir = "";
                string methodContent = "";

                //List<ControllerFileData> fileDataList = new List<ControllerFileData>();
                //for (int i = 0; i < fileNames.Length; i++)
                //{
                //    fileData = com.GetFileData(Frame.appRoot, this.config.APP, fileNames[i], System.Text.Encoding.UTF8);
                //    fileDataList.Add(fileData);
                //}
                for (int i = 0; i < fileNames.Length; i++)
                {
                    relative_position = 0;
                    fileData = com.GetFileData(Frame.appRoot, this.config.APP, fileNames[i], System.Text.Encoding.UTF8, this.config);

                    ClassName = Path.GetFileNameWithoutExtension(fileNames[i]);
                    classData = fileData.GetClassData(ClassName);
                    compileCode = fileData.csharpCode.ToString();
                    //相对
                    compileDir = config.ChangeBLLName(config.DAL.Trim('/'), fileData.nameSpace.Replace('.', '/'));
                    //绝对
                    compileFileName = Frame.MapPath(compileDir + "\\" + ClassName + ".cs");
                    //更改命名空间
                    relative_position += Replace(ref compileCode, relative_position + fileData.start, fileData.length, "namespace " + (Frame.AssemblyTitle + "/" + compileDir.TrimEnd('/')).Replace('/', '.') + "\r\n{");
                    if (classData.MethodDataList.Count > 0)
                    {

                        for (int j = 0; j < classData.MethodDataList.Count; j++)
                        {

                            methodData = classData.MethodDataList[j];
                            methodContent = methodData.Content;
                            SqlFunctionCompiler sqlCompiler = new NFinal.Compile.SqlFunctionCompiler();

                            //从代码中分析出数据库相关函数
                            System.Collections.Generic.List<DbFunctionData> dbFunctions = sqlCompiler.Compile(methodContent);

                            if (dbFunctions.Count > 0)
                            {
                                SqlAnalyse analyse = new SqlAnalyse();
                                //与数据库相结合,从sql语句中分析出所有的表信息,列信息
                                methodData.dbFunctions = analyse.FillFunctionDataList(DB.Coding.DbCache.DbStore, dbFunctions);
                            }
                            //数据库函数替换
                            int content_relative_position = 0;
                            if (dbFunctions.Count > 0)
                            {
                                bool hasSameVarName = false;
                                System.Collections.Generic.List<string> varNames = new System.Collections.Generic.List<string>();
                                //添加struct类型
                                for (int s = 0; s < dbFunctions.Count; s++)
                                {
                                    //去除重复项
                                    if (varNames.Count > 0)
                                    {
                                        hasSameVarName = false;
                                        for (int c = 0; c < varNames.Count; c++)
                                        {
                                            //如果发现重复项,则跳过循环
                                            if (varNames[c] == dbFunctions[s].varName)
                                            {
                                                hasSameVarName = true;
                                                break;
                                            }
                                        }
                                        if (hasSameVarName)
                                        {
                                            continue;
                                        }
                                    }
                                    varNames.Add(dbFunctions[s].varName);
                                    //分析出sql返回的List<dynamic>和dynamic类型是否有用AddNewField(string fileName,Type t);添加相关的类型
                                    MagicNewField newFiled = new MagicNewField(dbFunctions[s].varName);
                                    System.Collections.Generic.List<NFinal.Compile.StructField> structFieldList = newFiled.GetFields(ref methodContent, methodData.name);
                                    //添加struct字段
                                    string StructData = sqlCompiler.SetMagicStruct(methodData.name, dbFunctions[s], structFieldList, Frame.appRoot);
                                    if (!string.IsNullOrEmpty(StructData))
                                    {
                                        compileCode = compileCode.Insert(methodData.start + relative_position, StructData);
                                        relative_position += StructData.Length;
                                    }
                                }

                                //修正函数返回类型,提高执行效率
                                if (methodData.returnType.IndexOf("dynamic") > -1)
                                {
                                    string returnTypeString = "";
                                    if (new System.Text.RegularExpressions.Regex(@"List\s*<\s*dynamic\s*>").IsMatch(methodData.returnType))
                                    {
                                        returnTypeString = string.Format("List<__{0}_{1}__>", methodData.name, methodData.returnVarName);
                                    }
                                    else
                                    {
                                        returnTypeString = string.Format("__{0}_{1}__", methodData.name, methodData.returnVarName);
                                    }
                                    relative_position += Replace(ref compileCode,
                                            methodData.returnTypeIndex + relative_position + classData.position,
                                            methodData.returnType.Length,
                                            returnTypeString);
                                }
                                //更换函数内的数据库操作函数
                                content_relative_position += sqlCompiler.SetMagicFunction(methodData.name,
                                    ref methodContent,
                                    content_relative_position,
                                    methodData.dbFunctions,
                                    Frame.appRoot);
                            }
                            //分析并更换其中的连接字符串
                            content_relative_position += sqlCompiler.SetMagicConnection(methodData.name,
                                ref methodContent,
                                Frame.appRoot
                                );
                            if (build)
                            {
                                relative_position += Replace(ref compileCode, relative_position + methodData.position, methodData.Content.Length, methodContent);
                            }
                            else
                            {
                                if (methodData.returnType == "void")
                                {
                                    relative_position += Replace(ref compileCode, relative_position + methodData.position, methodData.Content.Length, string.Empty);
                                }
                                else
                                {
                                    relative_position += Replace(ref compileCode, relative_position + methodData.position, methodData.Content.Length, "return null;");
                                }
                            }
                        }
                    }
                    //如果文件夹不存在则新建
                    if (!Directory.Exists(Frame.MapPath(compileDir)))
                    {
                        Directory.CreateDirectory(Frame.MapPath(compileDir));
                    }
                    //写DAL层.class文件
                    sw = new StreamWriter(compileFileName, false, System.Text.Encoding.UTF8);
                    sw.Write(compileCode);
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// 生成Web层
        /// </summary>
        /// <param name="build">是否生成</param>
        public void CreateCompile(bool build)
        {
            string[] fileNames;
            //获取要生成的文件
            if (genConfig != null)
            {
                if (genConfig.controllerFiles.Count > 0)
                {
                    fileNames = new string[genConfig.controllerFiles.Count];
                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        fileNames[i] = Frame.MapPath(genConfig.controllerFiles[i]);
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (!Directory.Exists(Frame.MapPath(config.Controller)))
                {
                    return;
                }
                //获取所有的controls文件
                fileNames = Directory.GetFiles(Frame.MapPath(config.Controller), "*.cs", SearchOption.AllDirectories);
                if (fileNames == null || fileNames.Length < 1)
                {
                    return;
                }
            }

            //生成的代码
            string compileCode = "";
            //类名称
            string ClassName = "";

            //前面的字符串部分添加或删除后，后面的代码的相对位置
            int relative_postion = 0;

            if (fileNames.Length > 0)
            {
                ControllerAnalyse com = new ControllerAnalyse();
                ControllerFileData fileData = null;
                ControllerClassData classData = null;
                ControllerMethodData methodData = null;
                StreamWriter sw = null;
                //VTemplate.Engine.TemplateDocument swDebug = null;
                //JinianNet.JNTemplate.ITemplate swDebug = null;

                string compileFileName = "";
                string compileDir = "";
                string debugFileName = "";
                string methodContent = "";
                for (int i = 0; i < fileNames.Length; i++)
                {
                    fileData = com.GetFileData(Frame.appRoot, this.config.APP, fileNames[i], System.Text.Encoding.UTF8, this.config);
                    ClassName = Path.GetFileNameWithoutExtension(fileNames[i]);
                    classData = fileData.GetClassData(ClassName);
                    if (classData.MethodDataList.Count > 0)
                    {
                        for (int j = 0; j < classData.MethodDataList.Count; j++)
                        {
                            //只有公开方法才能访问
                            if (classData.MethodDataList[j].isPublic)
                            {
                                relative_postion = 0;
                                compileCode = fileData.csharpCode.ToString();
                                methodData = classData.MethodDataList[j];
                                //相对
                                compileDir = config.ChangeControllerName(config.Web + config.defaultStyle + "/", (fileData.nameSpace + '.' + ClassName).Replace('.', '/')) + "/";
                                //绝对
                                compileFileName = Frame.MapPath(compileDir + methodData.name + ".cs");
                                if (!Directory.Exists(Path.GetDirectoryName(compileFileName)))
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(compileFileName));
                                }
                                //调试文件
                                debugFileName = Frame.MapPath(compileDir + methodData.name + ".html");
                                //输出调试文件
                                if (!File.Exists(debugFileName))
                                {
                                    sw = File.CreateText(debugFileName);
                                    var DebugModel = new NFinal.Compile.Template.DebugModel
                                    {
                                        parameterDataList = methodData.parameterDataList,
                                        methodParameters = methodData.urlParser.formatParameterNames,
                                        methodName = config.APP.Trim('/') + "_" + ClassName + "_" + methodData.name
                                    };
                                    var DebugTemplate = new NFinal.Compile.Template.Debug();
                                    DebugTemplate.Model = DebugModel;
                                    sw.Write(DebugTemplate.TransformText());
                                    //RazorEngine.Engine.Razor.RunCompile(
                                    //    System.Text.Encoding.UTF8.GetString(NFinal.Compile.Template.Template.Debug),
                                    //    "/Compile/Template/Debug.cs",
                                    //    sw,
                                    //    DebugModel.GetType(),
                                    //    DebugModel);
                                    sw.Dispose();
                                }
                                relative_postion += Replace(ref compileCode, relative_postion + fileData.start, fileData.length, "namespace " + (Frame.AssemblyTitle + compileDir.TrimEnd('/')).Replace('/', '.') + "\r\n{");
                                relative_postion += Replace(ref compileCode, relative_postion + classData.start, classData.length, "public class " +
                                                methodData.name + "Action " + (string.IsNullOrEmpty(classData.baseName) ? "" : " : " + classData.baseName) + "\r\n\t{"
                                                //添加初始化函数
                                                + "\r\n\t\t#if (!AspNET && !MicrosoftOwin && !NFinalOwin)"
                                                + "\r\n\t\tpublic " + methodData.name + "Action(HttpContext context):base(context){}"
                                                + "\r\n\t\tpublic " + methodData.name + "Action(Microsoft.Owin.IOwinContext context):base(context){}"
                                                + "\r\n\t\tpublic " + methodData.name + "Action(NFinal.Owin.HtmlWriter writer,NFinal.Owin.Request request):base(writer,request){}"
                                                + "\r\n\t\t#endif"
                                                + "\r\n\t\t#if AspNET"
                                                + "\r\n\t\tpublic " + methodData.name + "Action(HttpContext context):base(context){}"
                                                + "\r\n\t\t#endif"
                                                + "\r\n\t\t#if MicrosoftOwin"
                                                + "\r\n\t\tpublic " + methodData.name + "Action(Microsoft.Owin.IOwinContext context):base(context){}"
                                                + "\r\n\t\t#endif"
                                                + "\r\n\t\t#if NFinalOwin"
                                                + "\r\n\t\tpublic " + methodData.name + "Action(string fileName, NFinal.Owin.Request request, NFinal.CompressMode compressMode) :base(fileName,request,compressMode){}"
                                                + "\r\n\t\tpublic " + methodData.name + "Action(System.IO.Stream stream,NFinal.Owin.Request request, NFinal.CompressMode compressMode) : base(stream,request,compressMode) {}"
                                                + "\r\n\t\tpublic " + methodData.name + "Action(System.Collections.Generic.IDictionary<string, object> enviroment, NFinal.Owin.Request request, NFinal.CompressMode compressMode):base(enviroment,request,compressMode){}"
                                                + "\r\n\t\t#endif"
                                                );
                                //循环内部所有方法
                                for (int k = 0; k < classData.MethodDataList.Count; k++)
                                {
                                    methodData = classData.MethodDataList[k];
                                    //如果两个相等,则进行替换
                                    if (j == k || (!classData.MethodDataList[k].isPublic))
                                    {
#if NFinalOwin
                                        int a;
#endif

                                        #region "替换原有方法"
                                        //排除非公开和非基类的方法,替换原有方法体
                                        //if (methodData.isPublic)
                                        {
                                            methodContent = methodData.Content;
                                            //
                                            SqlFunctionCompiler sqlCompiler = new NFinal.Compile.SqlFunctionCompiler();
                                            //从代码中分析出数据库相关函数
                                            System.Collections.Generic.List<DbFunctionData> dbFunctions = sqlCompiler.Compile(methodContent);

                                            if (dbFunctions.Count > 0)
                                            {
                                                SqlAnalyse analyse = new SqlAnalyse();
                                                //与数据库相结合,从sql语句中分析出所有的表信息,列信息
                                                methodData.dbFunctions = analyse.FillFunctionDataList(DB.Coding.DbCache.DbStore, dbFunctions);
                                            }
                                            //数据库函数替换
                                            int content_relative_position = 0;
                                            string StructDatas = string.Empty;
                                            if (dbFunctions.Count > 0)
                                            {
                                                bool hasSameVarName = false;
                                                System.Collections.Generic.List<string> varNames = new System.Collections.Generic.List<string>();
                                                //添加struct类型
                                                for (int s = 0; s < dbFunctions.Count; s++)
                                                {
                                                    //去除重复项
                                                    if (varNames.Count > 0)
                                                    {
                                                        hasSameVarName = false;
                                                        for (int c = 0; c < varNames.Count; c++)
                                                        {
                                                            //如果发现重复项,则跳过循环
                                                            if (varNames[c] == dbFunctions[s].varName)
                                                            {
                                                                hasSameVarName = true;
                                                                break;
                                                            }
                                                        }
                                                        if (hasSameVarName)
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                    varNames.Add(dbFunctions[s].varName);
                                                    //分析出sql返回的List<dynamic>和dynamic类型是否有用AddNewField(string fileName,Type t);添加相关的类型
                                                    MagicNewField newFiled = new MagicNewField(dbFunctions[s].varName);
                                                    System.Collections.Generic.List<NFinal.Compile.StructField> structFieldList = newFiled.GetFields(ref methodContent, methodData.name);
                                                    //如果没有写类型，则自动生成类型
                                                    if (!dbFunctions[s].hasGenericType)
                                                    {
                                                        //添加struct字段
                                                        string StructData = sqlCompiler.SetMagicStruct(methodData.name, dbFunctions[s], structFieldList, Frame.appRoot);
                                                        StructDatas += StructData;
                                                        if (!string.IsNullOrEmpty(StructData))
                                                        {
                                                            compileCode = compileCode.Insert(methodData.start + relative_postion, StructData);
                                                            relative_postion += StructData.Length;
                                                        }
                                                    }
                                                }
                                                //更换函数中的数据库操作
                                                content_relative_position += sqlCompiler.SetMagicFunction(methodData.name,
                                                    ref methodContent,
                                                    content_relative_position,
                                                    methodData.dbFunctions,
                                                    Frame.appRoot);

                                            }
                                            //分析并更换其中的连接字符串
                                            content_relative_position += sqlCompiler.SetMagicConnection(methodData.name,
                                                ref methodContent,
                                                Frame.appRoot
                                                );
                                            if (methodData.parameterTypeAndNames != string.Empty)
                                            {
                                                relative_postion += Replace(ref compileCode, relative_postion + methodData.parametersIndex, methodData.parametersLength, methodData.parameterTypeAndNames);
                                            }
                                            //从代码中分析出views函数
                                            NFinal.Compile.ViewCompiler viewCompiler = new NFinal.Compile.ViewCompiler();
                                            System.Collections.Generic.List<ViewData> views = viewCompiler.Compile(methodContent);
                                            //模版替换
                                            if (views.Count > 0)
                                            {
                                                content_relative_position = 0;
                                                content_relative_position = viewCompiler.SetMagicFunction(ref methodContent,
                                                    content_relative_position,
                                                    fileData.nameSpace,
                                                    ClassName,
                                                    methodData.name, views, config);
                                            }
                                            if (build)
                                            {
                                                relative_postion += Replace(ref compileCode, relative_postion + methodData.position, methodData.Content.Length, methodContent);
                                            }
                                            else
                                            {
                                                relative_postion += Replace(ref compileCode, relative_postion + methodData.position, methodData.Content.Length, string.Empty);
                                            }
                                            //生成自动提示类
                                            //views,Structs,DBFunctions
                                            AutoCompleteCompiler autoComplete = new AutoCompleteCompiler();
                                            autoComplete.Compile(classData.baseName, methodData, StructDatas, views, fileData.nameSpace, ClassName, config);

                                        }
                                        #endregion
                                    }
                                    //如果两个不相等
                                    else
                                    {
                                        compileCode = compileCode.Remove(relative_postion + classData.MethodDataList[k].start,
                                            classData.MethodDataList[k].length +
                                            classData.MethodDataList[k].Content.Length + 1);//去掉最后一个}
                                        relative_postion -= classData.MethodDataList[k].length +
                                            classData.MethodDataList[k].Content.Length + 1;
                                    }
                                }
                                //写aspx页面的自动提示层

                                //写Web层.class文件
                                sw = new StreamWriter(compileFileName, false, System.Text.Encoding.UTF8);
                                sw.Write(compileCode);
                                sw.Close();
                            }
                        }
                    }
                }
            }
        }
    }
}