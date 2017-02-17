using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace NFinal
{
    /// <summary>
    /// 模板引擎扩展类
    /// </summary>
    public static class Extend
    {
        /// <summary>
        /// 把模板渲染后的文本写入到文件
        /// </summary>
        /// <param name="template">模板对象</param>
        /// <param name="fileName">文件中径</param>
        /// <param name="encoding">文本编码</param>
        //public static void Render(this JinianNet.JNTemplate.ITemplate template, string fileName, System.Text.Encoding encoding)
        //{
        //    StringWriter sw = new StringWriter();
        //    template.Render(sw);
        //    sw.Close();
        //    string text = sw.ToString();
        //    text = Extend.DeleteWihteSpaceLine(text);
        //    StreamWriter fw = new StreamWriter(fileName, false, encoding);
        //    fw.Write(text);
        //    fw.Close();
        //}
        /// <summary>
        /// 返回模板渲染后的文本
        /// </summary>
        /// <param name="template">模板对象</param>
        /// <returns>模板渲染后的文本</returns>
        //public static String Render(this JinianNet.JNTemplate.ITemplate template)
        //{
        //    String document;
        //    using (StringWriter writer = new StringWriter())
        //    {
        //        template.Render(writer);
        //        document = writer.ToString();
        //    }
        //    document = DeleteWihteSpaceLine(document);
        //    return document;
        //}
        /// <summary>
        /// 删除多余的空格回车等
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>格式化的字符串</returns>
        public static string DeleteWihteSpaceLine(string text)
        {
            Regex ifBeginReg = new Regex(@"(\r?\n\s*)\r?\n");
            MatchCollection ifBeginMac = ifBeginReg.Matches(text);
            if (ifBeginMac.Count > 0)
            {
                for (int i = ifBeginMac.Count - 1; i >= 0; i--)
                {
                    text = text.Remove(ifBeginMac[i].Groups[1].Index, ifBeginMac[i].Groups[1].Length);
                }
            }
            return text;
        }
    }
}
