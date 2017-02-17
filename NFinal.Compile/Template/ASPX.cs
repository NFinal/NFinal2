using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace NFinal.Compile.Template
{
    /// <summary>
    /// ASPX模板引擎
    /// </summary>
    public class ASPX
    {
        System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding(false);
        /// <summary>
        /// 当某变量为空时设置默认值
        /// </summary>
        /// <param name="val">变量</param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        public string getString(string val, string def)
        {
            return string.IsNullOrEmpty(val) ? def : val;
        }
        /// <summary>
        /// 解析NFinal自定义标签语句
        /// </summary>
        /// <param name="template">模板文本</param>
        /// <returns></returns>
        public string TranslateHTMLTag(string template)
        {
            string ifBeginPattern = @"<if\s+condition\s*=\s*""\s*<%=((?><%(?<open>)|%>(?<-open>)|(?:(?!%>).)*)*(?(Open)(?!)))\s*%>\s*""\s*>";
            string elsePattern = @"<else\s*/>";
            string elseifPattern = @"<elseif\s+condition\s*=\s*""\s*<%=((?><%(?<open>)|%>(?<-open>)|(?:(?!%>).)*)*(?(Open)(?!)))\s*%>\s*""\s*>";
            string ifEndPattern = @"</if>";
            string foreachBeginPattern = @"<foreach\s+enumerator\s*=\s*""\s*<%\s*var\s+([_0-9a-zA-Z]+)\s*=([\s\S]+?)\s*;?\s*%>\s*""\s*>";
            string foreachEndPattern = @"</foreach>";

            string switchBeginPattern = @"<switch\s*variable\s*=\s*""\s*<%=((?><%(?<open>)|%>(?<-open>)|(?:(?!%>).)*)*(?(Open)(?!)))\s*%>\s*""\s*>";
            string caseBeginPattern = @"<case\s*value\s*=\s*""\s*(?:<%=((?><%(?<open>)|%>(?<-open>)|(?:(?!%>).)*)*(?(Open)(?!)))\s*%>|(\S+))\s*""\s*>";
            string caseEndPattern = @"</case>";
            string defaultBeginPattern = @"<default>";
            string defaultEndPattern = @"</default>";
            string switchEndPattern = @"</switch>";

            string forBeginPattern = @"<for\s+start\s*=\s*""\s*<%([^;]+);?\s*%>\s*""\s+condition\s*=\s*""\s*<%\s*=([^;]+);?\s*%>\s*""\s+step\s*=\s*""\s*<%([^;]+);?\s*%>\s*""\s*>";
            string forEndPattern = @"</for>";

            string whileBeginPattern = @"<while\s+condition\s*=\s*""\s*<%=((?><%(?<open>)|%>(?<-open>)|(?:(?!%>).)*)*(?(Open)(?!)))\s*%>\s*""\s*>";
            string whileEndPattern = @"</while>";

            //if
            Regex ifBeginReg = new Regex(ifBeginPattern);
            MatchCollection ifBeginMac = ifBeginReg.Matches(template);
            int relative_position = 0;
            string tempCode = string.Empty;
            for (int i = 0; i < ifBeginMac.Count; i++)
            {
                template = template.Remove(ifBeginMac[i].Index + relative_position, ifBeginMac[i].Length);
                tempCode = string.Format("<%if({0}){{%>", ifBeginMac[i].Groups[1].Value.Trim(';',' '));
                template = template.Insert(ifBeginMac[i].Index + relative_position, tempCode);
                relative_position += tempCode.Length - ifBeginMac[i].Length;
            }
            //else
            Regex elseReg = new Regex(elsePattern);
            MatchCollection elseMac = elseReg.Matches(template);
            relative_position = 0;
            for (int i = 0; i < elseMac.Count; i++)
            {
                template = template.Remove(elseMac[i].Index + relative_position, elseMac[i].Length);
                tempCode = "<%}else{%>";
                template = template.Insert(elseMac[i].Index + relative_position, tempCode);
                relative_position += tempCode.Length - elseMac[i].Length;
            }
            //elseif
            Regex elseifReg = new Regex(elseifPattern);
            MatchCollection elseifMac = elseifReg.Matches(template);
            relative_position = 0;
            for (int i = 0; i < elseifMac.Count; i++)
            {
                template = template.Remove(elseifMac[i].Index + relative_position, elseifMac[i].Length);
                tempCode = string.Format("<%}}else if({0}){{%>", elseifMac[i].Groups[1].Value.Trim(';', ' '));
                template = template.Insert(elseifMac[i].Index + relative_position, tempCode);
                relative_position += tempCode.Length - elseifMac[i].Length;
            }
            template = template.Replace(ifEndPattern, "<%}%>");

            //foreach
            Regex foreachBeginReg = new Regex(foreachBeginPattern);
            MatchCollection foreachBeginMac = foreachBeginReg.Matches(template);
            relative_position = 0;
            for (int i = 0; i < foreachBeginMac.Count; i++)
            {
                template = template.Remove(foreachBeginMac[i].Index + relative_position, foreachBeginMac[i].Length);
                tempCode = string.Format("<%var {0}={1}; while({0}.MoveNext()){{%>",
                    foreachBeginMac[i].Groups[1].Value, foreachBeginMac[i].Groups[2].Value);
                template = template.Insert(foreachBeginMac[i].Index + relative_position, tempCode);
                relative_position += tempCode.Length - foreachBeginMac[i].Length;
            }
            template = template.Replace(foreachEndPattern, "<%}%>");

            //switch
            Regex switchBeginReg = new Regex(switchBeginPattern);
            MatchCollection switchMac = switchBeginReg.Matches(template);
            relative_position = 0;
            for (int i = 0; i < switchMac.Count; i++)
            {
                template = template.Remove(switchMac[i].Index + relative_position, switchMac[i].Length);
                tempCode = string.Format("<%switch({0}){{%>", switchMac[i].Groups[1].Value.Trim(';', ' '));
                template = template.Insert(switchMac[i].Index + relative_position, tempCode);
                relative_position += tempCode.Length - switchMac[i].Length;
            }

            Regex casehBeginReg = new Regex(caseBeginPattern);
            MatchCollection caseMac = casehBeginReg.Matches(template);
            relative_position = 0;
            for (int i = 0; i < caseMac.Count; i++)
            {
                template = template.Remove(caseMac[i].Index + relative_position, caseMac[i].Length);
                if (caseMac[i].Groups[1].Success)
                {
                    tempCode = string.Format("<%case {0}:{{%>", caseMac[i].Groups[1].Value.Trim(';', ' '));
                }
                else
                {
                    tempCode = string.Format("<%case {0}:{{%>", caseMac[i].Groups[2].Value.Trim(';', ' '));
                }
                template = template.Insert(caseMac[i].Index + relative_position, tempCode);
                relative_position += tempCode.Length - caseMac[i].Length;
            }

            template = template.Replace(caseEndPattern, "<%}break;%>");
            template = template.Replace(defaultBeginPattern, "<%default:{%>");
            template = template.Replace(defaultEndPattern, "<%}break;%>");
            template = template.Replace(switchEndPattern, "<%}%>");

            //for
            Regex forBeginReg = new Regex(forBeginPattern);
            MatchCollection forMac = forBeginReg.Matches(template);
            relative_position = 0;
            for (int i = 0; i < forMac.Count; i++)
            {
                template = template.Remove(forMac[i].Index + relative_position, forMac[i].Length);
                tempCode = string.Format("<%for({0};{1};{2}){{%>", forMac[i].Groups[1].Value, forMac[i].Groups[2].Value, forMac[i].Groups[3].Value);
                template = template.Insert(forMac[i].Index + relative_position, tempCode);
                relative_position += tempCode.Length - forMac[i].Length;
            }

            template = template.Replace(forEndPattern, "<%}%>");

            //while
            Regex whileBeginReg = new Regex(whileBeginPattern);
            MatchCollection whileBeginMac = whileBeginReg.Matches(template);
            relative_position = 0;
            tempCode = string.Empty;
            for (int i = 0; i < whileBeginMac.Count; i++)
            {
                template = template.Remove(whileBeginMac[i].Index + relative_position, whileBeginMac[i].Length);
                tempCode = string.Format("<%while({0}){{%>", whileBeginMac[i].Groups[1].Value.Trim(';', ' '));
                template = template.Insert(whileBeginMac[i].Index + relative_position, tempCode);
                relative_position += tempCode.Length - whileBeginMac[i].Length;
            }

            template = template.Replace(whileEndPattern, "<%}%>");

            return template;
        }
        /// <summary>
        /// 获取模板渲染后的字符串
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="encoding">文件的编码方式</param>
        /// <returns></returns>
        public string GetRenderText(string fileName, System.Text.Encoding encoding, int tab,bool compress)
        {
            StringWriter sw = new StringWriter();
            Render(sw, fileName, tab, compress);
            return sw.ToString();
        }
        /// <summary>
        /// 获取完整的模板字符串
        /// </summary>
        /// <param name="template">模板字符串</param>
        /// <returns>模板字符串,包含引用模板的信息</returns>
        public string GetAllTemplateFromReference(string template)
        {
            return template;
        }
        /// <summary>
        /// 获取aspx引入的所有的用户控件
        /// </summary>
        /// <param name="template">aspx模板文本</param>
        /// <returns></returns>
        public Dictionary<string, string> GetRegistWebControls(string template)
        {
            string pattern = @"<%@\s+Register\s+Src=""([^""\s]+)""\s+TagPrefix=""([^""\s]+)""\s+TagName=""([^""\s]+)""\s*%>\s*";
            Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);
            Dictionary<string, string> dicControls = new Dictionary<string, string>();
            MatchCollection mac = reg.Matches(template);
            if (mac.Count > 0)
            {
                for (int i = 0; i < mac.Count; i++)
                {
                    dicControls.Add(mac[i].Groups[2].Value + ":" + mac[i].Groups[3].Value, mac[i].Groups[1].Value);
                }
            }
            return dicControls;
        }
        /// <summary>
        /// 删除ViewBag字符串，使代码可执行
        /// </summary>
        /// <param name="text">模板文本</param>
        /// <returns></returns>
        public string DeleteViewBag(string text)
        {
            string pattern = @"[^_0-9a-zA-Z](ViewBag\s*\.\s*)";
            Regex reg = new Regex(pattern, RegexOptions.Multiline);
            MatchCollection mac = reg.Matches(text);
            int position_relative = 0;
            for (int i = 0; i < mac.Count; i++)
            {
                text= text.Remove(position_relative +mac[i].Groups[1].Index, mac[i].Groups[1].Length);
                position_relative -= mac[i].Groups[1].Length;
            }
            pattern = @"^\s*(ViewBag\s*\.\s*)";
            reg = new Regex(pattern, RegexOptions.Multiline);
            mac = reg.Matches(text);
            position_relative = 0;
            for (int i = 0; i < mac.Count; i++)
            {
                text = text.Remove(position_relative + mac[i].Groups[1].Index, mac[i].Groups[1].Length);
                position_relative -= mac[i].Groups[1].Length;
            }
            return text;
        }
        /// <summary>
        /// 把aspx中的用户控件的标签替换为ascx中的代码
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="deepth">解析深度，防止标签循环嵌套</param>
        /// <returns></returns>
        public string TransWebControls(string template, int deepth)
        {
            Dictionary<string, string> dicControls = GetRegistWebControls(template);
            string pattern = @"<(([^<\s:>]+):([^<\s:>]+))\s+runat\s*=\s*""server""\s*(\s+id\s*=\s*""([^""\s]*)""\s*)?(\s+db\s*=\s*""<%#\s*(ViewBag\s*.\s*([^\s;]+));?\s*%\>"")?\s*/>\s*";
            Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection mac = reg.Matches(template);
            string fileName;
            string content;
            Match m;
            int relative_position = 0;

            string aspxControllHeaderPattern = @"<%@\s+(Page|Control)[^%]+%>\s*";
            Regex aspxControllHeaderRegex = new Regex(aspxControllHeaderPattern);
            Match aspxControllHeaderMatch = null;
            string aspxControllDbPattern = @"db=""\s*([\S]+)\s*""";
            Regex aspxControllDbRegex = new Regex(aspxControllDbPattern);
            Match aspxControllDbMatch = null;
            string replaceVarName = "ViewBag.";
            if (mac.Count > 0 && deepth < 6)
            {
                for (int i = 0; i < mac.Count; i++)
                {
                    m = mac[i];
                    if (dicControls.ContainsKey(mac[i].Groups[1].Value))
                    {
                        fileName = Frame.MapPath(dicControls[mac[i].Groups[1].Value].TrimStart('~'));
                        StreamReader sr = new StreamReader(fileName,utf8,true);
                        content = sr.ReadToEnd();
                        sr.Close();
                        aspxControllHeaderMatch= aspxControllHeaderRegex.Match(content);
                        if (aspxControllHeaderMatch.Success)
                        {
                            aspxControllDbMatch = aspxControllDbRegex.Match(aspxControllHeaderMatch.Value);
                            if (aspxControllDbMatch.Success)
                            {
                                replaceVarName = aspxControllDbMatch.Groups[1].Value+".";
                            }
                        }
                        //content = content.Replace("ViewBag.", mac[i].Groups[8].Value+".");
                        template = template.Remove(relative_position + m.Index, m.Length);
                        if (mac[i].Groups[5].Success == false || mac[i].Groups[5].Value.Trim() == string.Empty || mac[i].Groups[7].Success == false || mac[i].Groups[7].Value.Trim() == string.Empty)
                        {
                            template = template.Insert(relative_position + m.Index, content);
                        }
                        else
                        {
                            content = string.Format("<%{0}.__render__ = ()=>{{ %>{1}<%}};{0}.__render__();%>",
                                mac[i].Groups[8].Value.Trim(), content.Replace(replaceVarName, mac[i].Groups[8].Value + "."));
                            template = template.Insert(relative_position + m.Index, content);
                        }
                        relative_position += content.Length - m.Length;
                    }
                    //如果找不到注册的组件名,则删除
                    else
                    {
                        template = template.Remove(relative_position + m.Index, m.Length);
                        relative_position -= m.Length;
                    }
                }
                template = TransWebControls(template, deepth);
            }
            string aspxHeaderPattern = @"\s*<%@\s+(Page|Control|Register|Import)[^%]+%>\s*";
            Regex aspxHeaderReg = new Regex(aspxHeaderPattern, RegexOptions.IgnoreCase);  
            MatchCollection aspxHeaderMac = aspxHeaderReg.Matches(template);
            relative_position = 0;
            for (int i = 0; i < aspxHeaderMac.Count; i++)
            {
                template = template.Remove(aspxHeaderMac[i].Index + relative_position, aspxHeaderMac[i].Length);
                relative_position -= aspxHeaderMac[i].Length;
            }
            return template;
        }
        /// <summary>
        /// 渲染模板
        /// </summary>
        /// <param name="writer">写操作类</param>
        /// <param name="template">模板字符串</param>
        /// <returns></returns>
        public string Render(TextWriter writer, string template, int tab,bool compress)
        {
            int deepth = 0;
            //template= Reference(template,deepth);
            deepth = 0;
            template = TransWebControls(template, deepth);
            template = TranslateHTMLTag(template);
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
                            text = template.Substring(text_start, text_end - text_start+1);
                            if (!IsNullOrWhiteSpace(text))
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
                            if (!IsNullOrWhiteSpace(text))
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
                            if (!IsNullOrWhiteSpace(text))
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
                        writer.WriteLine(DeleteViewBag(m.Groups[1].Value));
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
            if (!IsNullOrWhiteSpace(text))
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
            text = string.Format("Write({0});", DeleteViewBag(text.Trim()));
            return text;
        }
        public bool IsNullOrWhiteSpace(string text)
        {
            //char[] space = new char[] { ' ', '\r', '\n', '\t', '\f', '\v' };
            return string.IsNullOrEmpty(text.Trim());
        }
        /// <summary>
        /// 返回输出字符串的csharp代码
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns></returns>
        public string BuildWriteCode(string text)
        {
            
            text = ReserveString(text);
            text = string.Format("Write(\"{0}\");", text.Trim());
            return text;
        }
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
        public void RenderTo(string fileName, string outFileName, int tab,bool compress)
        {
            if (!Directory.Exists(Path.GetDirectoryName(outFileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outFileName));
            }
            StreamWriter sw = new StreamWriter(outFileName, false, utf8);
            Render(sw, fileName, tab, compress);
        }
    }
}