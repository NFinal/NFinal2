//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :CodeSegment.cs
//        Description :代码编译类
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;

namespace NFinal.Compile
{
    public class MagicCodeSegment
    {
        public string appRoot;
        public string app;
        public string insertParttern=@"(?:this\s*.\s*)?InsertCodeSegment\(\s*""([^""]+)""(?:\s*,\s*""([^""]+)"")?\s*\)\s*;";
        public string methodParttern = @"(?:public|private|protected)\s+(?:(?:override|new)\s+)*\S+\s+{0}\s*\(([^\(\)]*)\)\s*\{{((?<Open>\{{)|(?<-Open>\}})|[^\{{\}}]+)(?(Open)(?!))\}}";
        public MagicCodeSegment(string appRoot,string app)
        {
            this.appRoot =appRoot;
            this.app = app.Trim('/');
        }
        /// <summary>
        /// 把基于网站根目录的绝对路径改为相对路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string MapPath(string url)
        {
            return appRoot +app+"\\"+url.Trim('/').Replace('/', '\\');
        }
        //解析InsertCodeSegment()函数,并返回相应的csharp代码;
        public string Parse(string csharpCode)
        {
            Regex reg = new Regex(insertParttern);
            MatchCollection mac = reg.Matches(csharpCode);
            string filePath = null;
            string methodName = null;
            Regex methodReg=null;
            Match mat=null;
            StreamReader sr=null;
            string segmentCode = string.Empty;
            int relative_position=0;
            for (int i = 0; i < mac.Count; i++)
            {
                filePath = MapPath(mac[i].Groups[1].Value);
                //如果是两个参数
                if (mac[i].Groups[2].Success)
                {
                    methodName = mac[i].Groups[2].Value;
                    csharpCode = csharpCode.Remove(mac[i].Index+relative_position, mac[i].Length);
                    if (File.Exists(filePath))
                    {
                        sr = new StreamReader(filePath,System.Text.Encoding.UTF8);
                        methodReg =new Regex(string.Format(methodParttern,methodName));
                        mat = methodReg.Match(sr.ReadToEnd());
                        sr.Close();
                        if (mat.Success)
                        {
                            segmentCode=mat.Groups[2].Value;
                            segmentCode = string.Format("#region 调用{0}函数,文件位置:{1}\r\n{2}#endregion",methodName,filePath,segmentCode);
                            csharpCode= csharpCode.Insert(mac[i].Index+relative_position,segmentCode);
                            relative_position -= mac[i].Length;
                            relative_position+=segmentCode.Length;
                        }
                    }
                }
                //如果是从文本文件中读取
                else
                {
                    csharpCode = csharpCode.Remove(mac[i].Index+relative_position, mac[i].Length);
                    
                    if (File.Exists(filePath))
                    {
                        sr = new StreamReader(filePath, System.Text.Encoding.UTF8);
                        segmentCode=sr.ReadToEnd();
                        sr.Close();
                        segmentCode = string.Format("#region 调用代码,文件位置:{1}\r\n{2}#endregion",filePath, segmentCode);
                        csharpCode = csharpCode.Insert(mac[i].Index+relative_position, segmentCode);
                        relative_position -= mac[i].Length;
                        relative_position+=segmentCode.Length;
                    }
                }
            }
            return csharpCode;
        }
    }
}