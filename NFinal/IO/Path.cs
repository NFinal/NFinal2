//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : Path.cs
//        Description :Path工具类,用于计算路径
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

namespace NFinal.IO
{
    /// <summary>
    /// Path工具类
    /// </summary>
    public class Path
    {   
        /// <summary>
        /// 获取绝对路径
        /// </summary>
        /// <param name="absoluteDirectory">绝对根目录</param>
        /// <param name="relativePath">相对路径</param>
        /// <returns></returns>
        public static string GetAbsolutePath(string absoluteDirectory, string relativePath)
        {
            string[] relativeDirectories = relativePath.Split('/');
            string absolutePath = absoluteDirectory;
            for(int i= 0;i < relativeDirectories.Length;i++)
            {
                if (relativeDirectories[i] == "..")
                {
                    absolutePath = System.IO.Path.GetDirectoryName(absolutePath);
                }
                else if (relativeDirectories[i] == ".")
                {
                    absolutePath = absoluteDirectory;
                }
                else if (relativeDirectories[i] == "")
                {
                    absolutePath = absoluteDirectory;
                }
                else
                {
                    absolutePath = System.IO.Path.Combine(absolutePath, relativeDirectories[i]);
                }
            }
            return absolutePath;
        }
        /// <summary>
        /// 默认网站根目录
        /// </summary>
        public static string rootPath =
#if (NFinalIO)
                AppContext.BaseDirectory;
#endif
#if (NET40 || NET451 || NET461)
                (AppDomain.CurrentDomain.GetData(".appPath") as string) ?? Environment.CurrentDirectory;
#endif
#if NETCORE
                AppContext.BaseDirectory;
#endif
        /// <summary>
        /// 获取网站根目录，返回两个结果
        /// </summary>
        /// <param name="projectType">项目类型</param>
        /// <param name="relativePath">相对路径</param>
        /// <returns>网站根目录，以及应用程序</returns>
        public static string MapPath(NFinal.Config.Global.ProjectType projectType,string relativePath)
        {
            if (projectType == Config.Global.ProjectType.Web)
            {
                return GetProjectPath(relativePath);
            }
            else
            {
                return GetApplicationPath(relativePath);
            }
        }
        /// <summary>
        /// 项目文件路径
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public static string GetProjectPath(string relativePath=null)
        {
            var rootPathArray = rootPath.Split(System.IO.Path.DirectorySeparatorChar);
            string projectPath = Directory.GetCurrentDirectory();
            bool find = false;
            if (!find)
            {
                projectPath = Directory.GetCurrentDirectory();
                //再通过project文件推断
                DirectoryInfo directory = new DirectoryInfo(projectPath);
                while (directory != null)
                {
                    var files = directory.GetFiles("*.csproj", SearchOption.TopDirectoryOnly);
                    if (files.Length > 0)
                    {
                        projectPath = files[0].DirectoryName;
                        find = false;
                        break;
                    }
                    directory = directory.Parent;
                }
            }
            
            if (!find)
            {
                projectPath = rootPath;
                //再通过project文件推断
                DirectoryInfo directory = new DirectoryInfo(projectPath);
                while (directory != null)
                {
                    var files = directory.GetFiles("*.csproj", SearchOption.TopDirectoryOnly);
                    if (files.Length > 0)
                    {
                        projectPath = files[0].DirectoryName;
                        find = true;
                        break;
                    }
                    directory = directory.Parent;
                }
            }
            //先通过bin文件夹推断
            if (!find)
            {
                projectPath = rootPath;
                for (int i = 0; i < rootPathArray.Length - 1; i++)
                {
                    if (rootPathArray[i] == "bin" && rootPathArray[i + 1] == "Debug" || rootPathArray[i + 1] == "Release")
                    {
                        projectPath = string.Join(System.IO.Path.DirectorySeparatorChar.ToString(), rootPathArray, 0, i);
                        find = true;
                        break;
                    }
                }
            }
            
            if (find)
            {
                return GetAbsolutePath(projectPath, relativePath);
            }
            else
            {
                return GetAbsolutePath(rootPath,relativePath);
            }
        }
        /// <summary>
        /// 程序所在目录
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public static string GetApplicationPath(string relativePath=null)
        {
            return GetAbsolutePath(rootPath, relativePath);
        }
    }
}
