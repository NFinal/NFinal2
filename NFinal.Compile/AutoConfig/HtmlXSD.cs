using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.AutoConfig
{
    /// <summary>
    /// aspx模板中nfinal自定义标签自动提示类
    /// </summary>
    public class HtmlXSD
    {
        /// <summary>
        /// 获取本机所有安装的VS目录
        /// </summary>
        /// <returns></returns>
        public static string[] GetVSTools()
        {
            string vsTool = null;
            System.Collections.Generic.List<string> vsTools = new System.Collections.Generic.List<string>();
            for (int i = 60; i < 150; i = i + 10)
            {
                vsTool = System.Environment.GetEnvironmentVariable(string.Format("VS{0}COMNTOOLS", i), EnvironmentVariableTarget.Machine);
                if (!string.IsNullOrEmpty(vsTool))
                {
                    vsTools.Add(vsTool);
                }
            }
            return vsTools.ToArray();
        }
        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sources">源路径</param>
        /// <param name="dest">新路径</param>
        private static void CopyFile(string sources, string dest)
        {
            System.IO.DirectoryInfo dinfo = new System.IO.DirectoryInfo(sources);
            //注，这里面传的是路径，并不是文件，所以不能保含带后缀的文件                
            foreach (System.IO.FileSystemInfo f in dinfo.GetFileSystemInfos())
            {
                //目标路径destName = 目标文件夹路径 + 原文件夹下的子文件(或文件夹)名字                
                //Path.Combine(string a ,string b) 为合并两个字符串                     
                String destName = System.IO.Path.Combine(dest, f.Name);
                if (f is System.IO.FileInfo)
                {
                    //如果是文件就复制       
                    System.IO.File.Copy(f.FullName, destName, true);//true代表可以覆盖同名文件                     
                }
                else
                {
                    //如果是文件夹就创建文件夹然后复制然后递归复制              
                    System.IO.Directory.CreateDirectory(destName);
                    CopyFile(f.FullName, destName);
                }
            }
        }
        /// <summary>
        /// 重写XSD文档
        /// </summary>
        /// <param name="root">VS目录</param>
        public static void RewriteXSD(string root)
        {
            string[] vsTools = GetVSTools();
            string commonPath = null;
            string xsdFolder = null;
            string[] htmlFiles = null;
            for (int i = 0; i < vsTools.Length; i++)
            {
                commonPath = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(vsTools[i]));
                xsdFolder = System.IO.Path.Combine(commonPath, "Packages\\schemas\\html");
                if (System.IO.Directory.Exists(xsdFolder))
                {
                    htmlFiles = System.IO.Directory.GetFiles(xsdFolder);
                    CopyFile(System.IO.Path.Combine(root, "NFinal\\Resource\\schemas\\html\\"), xsdFolder);
                }
            }
        }
    }
}
