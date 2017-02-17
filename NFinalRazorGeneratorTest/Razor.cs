using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Generator;

namespace NFinalRazorGenerator
{
    public class Razor
    {
        private string projectRoot;
        public Razor(string projectRoot)
        {
            this.projectRoot = projectRoot;
        }
   
        public string MapPath(string path)
        {
            return Path.Combine(projectRoot, path);
        }
        public string TranslateTemplate(string templateString, int deepth)
        {
            string renderPagePattern = @"@RenderPage\s*\(\s*""(~?[_a-zA-Z0-9\./]+)""\s*(?:,\s*Model.([_a-zA-Z0-9]+)\s*)?\)\s*;?";
            Regex renderPageReg = new Regex(renderPagePattern);
            MatchCollection mac = renderPageReg.Matches(templateString);
            string fileName = null;
            string controllName = null;
            string subTemplateString = null;
            int relativePosition = 0;
            if (mac.Count > 0 && deepth < 6)
            {
                deepth++;
                foreach (Match mat in mac)
                {
                    if (mat.Success)
                    {
                        fileName = MapPath(mat.Groups[1].Value);
                        StreamReader sr = new StreamReader(fileName);
                        subTemplateString = sr.ReadToEnd();
                        sr.Close();
                        if (mat.Groups[2].Success)
                        {
                            controllName = mat.Groups[2].Value;
                            subTemplateString = subTemplateString.Replace("Model.", controllName + ".");
                        }
                        templateString = templateString.Remove(mat.Index + relativePosition, mat.Length);
                        templateString = templateString.Insert(mat.Index + relativePosition, subTemplateString);
                        relativePosition = subTemplateString.Length - mat.Length;
                    }
                }
                return TranslateTemplate(templateString, deepth);
            }
            else
            {
                return templateString;
            }
        }
        /// <summary>
        /// 渲染模板
        /// </summary>
        /// <param name="writer">写操作类</param>
        /// <param name="template">模板字符串</param>
        /// <returns></returns>
        public string Render(TextWriter writer, string template, List<string> usings, ref string modelType, int tab, bool compress)
        {
            int deepth = 0;
            deepth = 0;
            template = TranslateTemplate(template, deepth);
            template = TranslateToAspxCode(template, usings, ref modelType);
            if (compress)
            {
                template = HtmlCompressor.compress(template);
            }
            string text = "";
            string section = @"(?isx)<%((?><%(?<open>)|%>(?<-open>)|(?:(?!%>).)*)*(?(Open)(?!)))%>";
            Regex reg = new Regex(section);
            MatchCollection matches = reg.Matches(template);
            //开始
            int text_start = 0;
            //结束
            int text_end = 0;
            if (matches.Count > 0)
            {
                foreach (Match m in matches)
                {
                    text_end = m.Index - 1;
                    if (m.Groups[1].Value[0] == '@')
                    {

                        //添加写入html的源码字符串
                        if (text_end - text_start >= 0)
                        {
                            text = template.Substring(text_start, text_end - text_start + 1);
                            if (!string.IsNullOrWhiteSpace(text))
                            {
                                WriteTab(writer, tab);
                                text = BuildWriteCode(text);
                                writer.WriteLine(text);
                            }
                        }
                        //如果<%@ Reference VirtualPath="" %>

                        //让开头指向结尾
                        text_start = m.Index + m.Length;
                    }
                    else if (m.Groups[1].Value[0] == '=')
                    {
                        //添加写入html的源码字符串
                        if (text_end - text_start >= 0)
                        {
                            text = template.Substring(text_start, text_end - text_start + 1);
                            if (!string.IsNullOrWhiteSpace(text))
                            {
                                WriteTab(writer, tab);
                                text = BuildWriteCode(text);
                                writer.WriteLine(text);
                            }
                        }
                        //替换掉<%%>,转为源码

                        if (m.Value.IndexOf("}") > 0)
                        {
                            tab--;
                        }
                        WriteTab(writer, tab);
                        writer.WriteLine(BuildWriteVar(m.Groups[1].Value.Trim().TrimStart('=').TrimEnd(';')));
                        if (m.Value.IndexOf("{") > 0)
                        {
                            tab++;
                        }
                        //让开头指向结尾
                        text_start = m.Index + m.Length;
                    }
                    else
                    {

                        //添加写入html的源码字符串
                        if (text_end - text_start >= 0)
                        {
                            text = template.Substring(text_start, text_end - text_start + 1);
                            if (!string.IsNullOrWhiteSpace(text))
                            {
                                WriteTab(writer, tab);
                                text = BuildWriteCode(text);
                                writer.WriteLine(text);
                            }
                        }

                        if (m.Value.IndexOf("}") > 0)
                        {
                            tab--;
                        }
                        //替换掉<%%>,转为源码
                        WriteTab(writer, tab);
                        writer.WriteLine(m.Groups[1].Value);
                        if (m.Value.IndexOf("{") > 0)
                        {
                            tab++;
                        }
                        //让开头指向结尾
                        text_start = m.Index + m.Length;
                    }
                }
            }
            text_end = template.Length;
            text = template.Substring(text_start, text_end - text_start);
            if (!string.IsNullOrWhiteSpace(text))
            {
                WriteTab(writer, tab);
                text = BuildWriteCode(text);
                writer.WriteLine(text);
            }
            writer.Close();
            return writer.ToString();
        }

        /// <summary>
        /// 输入代码中的缩进
        /// </summary>
        /// <param name="tw">写类</param>
        /// <param name="tab">table符的数量</param>
        /// <param name="isFirstLine">是否是第一行</param>
        public void WriteTab(TextWriter tw, int tab)
        {
            if (tab > 0)
            {
                for (int i = 0; i < tab; i++)
                {
                    tw.Write("\t");
                }
            }
        }
        /// <summary>
        /// 返回写变量的csharp代码
        /// </summary>
        /// <param name="text">内容</param>
        /// <returns></returns>
        public string BuildWriteVar(string text)
        {
            //text = ReserveString(text);//输出csharp无需转义
            text = string.Format("action.Write({0});", text.Trim());
            return text;
        }
        /// <summary>
        /// 返回输出字符串的csharp代码
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns></returns>
        public string BuildWriteCode(string text)
        {

            text = ReserveString(text);
            text = string.Format("action.Write(\"{0}\");", text.Trim());
            return text;
        }
        /// <summary>
        /// 删除ViewBag字符串，使代码可执行
        /// </summary>
        /// <param name="text">模板文本</param>
        /// <returns></returns>
        //public string DeleteViewBag(string text)
        //{
        //    string pattern = @"[^_0-9a-zA-Z](ViewBag\s*\.\s*)";
        //    Regex reg = new Regex(pattern, RegexOptions.Multiline);
        //    MatchCollection mac = reg.Matches(text);
        //    int position_relative = 0;
        //    for (int i = 0; i < mac.Count; i++)
        //    {
        //        text = text.Remove(position_relative + mac[i].Groups[1].Index, mac[i].Groups[1].Length);
        //        position_relative -= mac[i].Groups[1].Length;
        //    }
        //    pattern = @"^\s*(ViewBag\s*\.\s*)";
        //    reg = new Regex(pattern, RegexOptions.Multiline);
        //    mac = reg.Matches(text);
        //    position_relative = 0;
        //    for (int i = 0; i < mac.Count; i++)
        //    {
        //        text = text.Remove(position_relative + mac[i].Groups[1].Index, mac[i].Groups[1].Length);
        //        position_relative -= mac[i].Groups[1].Length;
        //    }
        //    return text;
        //}
        /// <summary>
        /// 删除viewBag声明
        /// </summary>
        /// <param name="templateString"></param>
        /// <returns></returns>
        //public string DeleteViewBagDeclaration(string templateString)
        //{
        //    string viewBagPattern = @"@\{(\s*(?:var|[_a-zA-Z0-9\.]+)\s+ViewBag\s*=\s*new\s+[_a-zA-Z0-9\.]+\s*\(\s*\)\s*;\s*)}";
        //    Regex viewBagReg = new Regex(viewBagPattern);
        //    Match viewBagMat = viewBagReg.Match(templateString);
        //    if (viewBagMat.Success)
        //    {
        //        templateString = templateString.Remove(viewBagMat.Index, viewBagMat.Length);
        //    }
        //    return templateString;
        //}
        /// <summary>
        /// 字符串反转义
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>返回csharp中的字符串表示</returns>
        public string ReserveString(string text)
        {
            char[] temp_old = text.ToCharArray();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < temp_old.Length; i++)
            {
                switch (temp_old[i])
                {
                    case '\'': sb.Append("\\\'"); break;
                    case '\"': sb.Append("\\\""); break;
                    case '\\': sb.Append("\\\\"); break;
                    case '\0': sb.Append("\\0"); break;
                    case '\a': sb.Append("\\a"); break;
                    case '\b': sb.Append("\\b"); break;
                    case '\f': sb.Append("\\f"); break;
                    case '\n': sb.Append("\\n"); break;
                    case '\r': sb.Append("\\r"); break;
                    case '\t': sb.Append("\\t"); break;
                    case '\v': sb.Append("\\v"); break;
                    default: sb.Append(temp_old[i]); break;
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 渲染模板到指定文件
        /// </summary>
        /// <param name="fileName">模板文件路径</param>
        /// <param name="outFileName">输出文件路径</param>
        //public void RenderTo(string fileName, string outFileName, int tab, bool compress)
        //{
        //    if (!Directory.Exists(Path.GetDirectoryName(outFileName)))
        //    {
        //        Directory.CreateDirectory(Path.GetDirectoryName(outFileName));
        //    }
        //    StreamWriter sw = new StreamWriter(outFileName, false, System.Text.Encoding.UTF8);
        //    Render(sw, fileName, tab, compress);
        //}
        public string TranslateToAspxCode(string templateString, List<string> usings, ref string modelType)
        {
            StringReader streader = new StringReader(templateString);
            System.Web.Razor.Parser.RazorParser parser = new System.Web.Razor.Parser.RazorParser(
                new System.Web.Razor.Parser.CSharpCodeParser()
                , new System.Web.Razor.Parser.HtmlMarkupParser());
            parser.DesignTimeMode = false;
            var results = parser.Parse(streader);
            streader.Close();
            StringBuilder sb = new StringBuilder();
            //取得转换成aspx的代码
            //GetAspxCode(sb, ref modelType, usings, results.Document.Children);
            string aspxCode = sb.ToString();
            return aspxCode;
        }
    }
}
