using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Generator;
using System.Text.RegularExpressions;

namespace NFinalCompiler.Razor
{
    public class RazorWriter
    {
        public List<string> usings;
        public string modelType = "dynamic";
        public string renderContent;
        public RazorWriter(string fileName)
        {
            this.usings = new List<string>();
            StreamReader sr = new StreamReader(fileName,System.Text.Encoding.UTF8);
            System.Web.Razor.Parser.RazorParser parser = new System.Web.Razor.Parser.RazorParser(
                new System.Web.Razor.Parser.CSharpCodeParser()
                , new System.Web.Razor.Parser.HtmlMarkupParser());
            parser.DesignTimeMode = false;
            var results = parser.Parse(sr);
            sr.Dispose();
            StringWriter tw = new StringWriter();
            //取得转换成aspx的代码
            WriteSyntaxTreenode(results.Document, tw);
            this.renderContent = tw.ToString();
            tw.Dispose();
        }
        public void WriteTab(TextWriter tw, int times)
        {
            for (int i = 0; i < times; i++)
            {
                tw.Write('\t');
            }
        }
        public void WriteTemplate(TextWriter sw, string nameSpace, string className)
        {
            usings.Add("using System;");
            usings.Add("using NFinal;");
            string usingString = null;
            foreach (var us in usings)
            {
                usingString = us.TrimEnd('\r', '\n', ';');
                sw.Write(usingString);
                sw.WriteLine(";");
            }
            sw.WriteLine();
            sw.WriteLine("//此代码由NFinalRazorGenerator生成。");
            sw.WriteLine("//http://bbs.nfinal.com");
            sw.Write("namespace ");
            sw.WriteLine(nameSpace);
            sw.WriteLine("{");
            sw.Write("\t[View(\"");
            sw.Write("/");
            sw.Write(nameSpace.Replace('.', '/'));
            sw.Write("/");
            sw.Write(className);
            sw.Write(".cshtml");
            sw.WriteLine("\")]");
            sw.Write("\tpublic class ");
            sw.Write(className);
            sw.Write(" : NFinal.View.RazorView<");
            sw.Write(modelType);
            sw.WriteLine(">");
            sw.WriteLine("\t{");
            sw.Write("\t\tpublic ");
            sw.Write(className);
            sw.Write("(");
            sw.Write("NFinal.IO.Writer writer,");
            if (modelType == "dynamic")
            {
                sw.Write(modelType);
                sw.WriteLine(" ViewBag) : base(writer ,ViewBag)");
                sw.WriteLine("\t\t{");
                //sw.WriteLine("\t\t\tthis.writer=writer;");
                //sw.WriteLine("\t\t\tthis.ViewBag=ViewBag;");
                sw.WriteLine("\t\t}");
            }
            else
            {
                sw.Write(modelType);
                sw.WriteLine(" Model) : base(writer ,Model)");
                sw.WriteLine("\t\t{");
                //sw.WriteLine("\t\t\tthis.writer=writer;");
                //sw.WriteLine("\t\t\tthis.Model=Model;");
                sw.WriteLine("\t\t}");
            }
            sw.WriteLine("\t\t//如果此处报错，请添加NFinal引用");
            sw.WriteLine("\t\t//PMC命令为：Install-Package NFinal");
            sw.WriteLine("\t\tpublic override void Execute()");
            //sw.Write("NFinal.IO.Writer writer,");
            //if (modelType == "dynamic")
            //{
            //    sw.Write(modelType);
            //    sw.WriteLine(" ViewBag)");
            //}
            //else
            //{
            //    sw.Write(modelType);
            //    sw.WriteLine(" Model)");
            //}
            sw.WriteLine("\t\t{");
            sw.Write(renderContent);
            sw.WriteLine("\t\t}");
            sw.WriteLine("\t}");
            sw.WriteLine("}");
        }
        public void WriteSyntaxTreenode(SyntaxTreeNode treeNode, TextWriter tw)
        {
            if (treeNode.IsBlock)
            {
                var block = treeNode as Block;
                WriteBlock(block, tw);
            }
            else
            {
                var span = treeNode as Span;
                WriteSpan(span, tw);
            }
        }
        public void WriteBlock(Block block, TextWriter tw)
        {
            if (block.Type == BlockType.Markup)
            {
                foreach (var child in block.Children)
                {
                    if (child.IsBlock)
                    {
                        var blockChild = child as Block;
                        WriteBlock(blockChild, tw);
                    }
                    else
                    {
                        var spanChild = child as Span;
                        WriteSpan(spanChild, tw);
                    }
                }
            }
            else if (block.Type == BlockType.Comment)
            {
                if (block.CodeGenerator is RazorCommentCodeGenerator)
                {
                    foreach (var child in block.Children)
                    {
                        Span span = child as Span;
                        if (span.Kind == SpanKind.Comment)
                        {
                            tw.Write("\t\t\t/*");
                            tw.Write(span.Content);
                            tw.Write("*/\r\n");
                        }
                    }
                }
            }
            else if (block.Type == BlockType.Directive)
            {
                foreach (var child in block.Children)
                {
                    Span span = child as Span;
                    if (span.Kind == SpanKind.Code)
                    {
                        if (span.CodeGenerator is AddImportCodeGenerator)
                        {
                            usings.Add(span.Content);
                        }
                        else if (span.CodeGenerator is SetBaseTypeCodeGenerator)
                        {
                            SetBaseTypeCodeGenerator setBase = span.CodeGenerator as SetBaseTypeCodeGenerator;
                            string regGenerateStr = @"<([\S\s]+)>$";
                            Regex regGenerate = new Regex(regGenerateStr);
                            Match matGenerate = regGenerate.Match(setBase.BaseType);
                            if (matGenerate.Success)
                            {
                                this.modelType = matGenerate.Groups[1].Value;
                            }
                        }
                    }
                }
            }
            else if (block.Type == BlockType.Expression)
            {
                foreach (var child in block.Children)
                {
                    Span span = child as Span;
                    if (span.Kind == SpanKind.Code)
                    {
                        if (!string.IsNullOrEmpty(span.Content))
                        {
                            tw.Write("\t\t\twriter.Write(");
                            tw.Write(span.Content);
                            tw.Write(");\r\n");
                        }
                    }
                }
            }
            else if (block.Type == BlockType.Functions)
            {

            }
            else if (block.Type == BlockType.Helper)
            { }
            else if (block.Type == BlockType.Section)
            { }
            else if (block.Type == BlockType.Statement)
            {
                //if (isFirstBlock)
                //{
                //    isFirstBlock = false;
                //    string firstBlock = templateString.Substring(block.Start.CharacterIndex, block.Length);
                //    Regex regex = new Regex(@"([_a-zA-Z][_a-zA-Z0-9]+\s*\.\s*)*([_a-zA-Z][_a-zA-Z0-9]+)\s+([_a-zA-Z][_a-zA-Z0-9]+)\s*=\s*new\s+(([_a-zA-Z][_a-zA-Z0-9]+\s*\.\s*)*([_a-zA-Z][_a-zA-Z0-9]+))\s*\(\s*\)\s*;");
                //    MatchCollection mac = regex.Matches(firstBlock);
                //    foreach (Match mat in mac)
                //    {
                //        if (mat.Success)
                //        {
                //            if (mat.Groups[3].Value == "Model")
                //            {
                //                modelType = mat.Groups[4].Value;
                //            }
                //        }
                //    }
                //}
                //else
                {
                    foreach (var child in block.Children)
                    {
                        if (child.IsBlock)
                        {
                            var blockChild = child as Block;
                            WriteBlock(blockChild, tw);
                        }
                        else
                        {
                            var spanChild = child as Span;
                            WriteSpan(spanChild, tw);
                        }
                    }
                }
            }
            else if (block.Type == BlockType.Template)
            { }
        }
        public void WriteSpan(Span span, TextWriter tw)
        {
            if (span.Kind == SpanKind.Code)
            {
                //代码
                if (!string.IsNullOrWhiteSpace(span.Content))
                {
                    //tw.Write("\t\t\t");
                }
                tw.Write(span.Content);
            }
            else if (span.Kind == SpanKind.Comment)
            {
                //注释
                tw.Write("\t\t\t/*");
                tw.Write(span.Content);
                tw.Write("*/");
            }
            else if (span.Kind == SpanKind.Markup)
            {
                bool isFirst = ((span.Previous != null) && span.Previous.Kind != SpanKind.Markup) || span.Previous == null;
                //bool isMid = false;
                bool isLast = (span.Next != null && span.Next.Kind != SpanKind.Markup) || (span.Next == null);
                //<html><html><html>
                if (isLast == true && isFirst == true)
                {
                    //isMid = true;
                    tw.Write("\t\t\twriter.Write(\"");
                    if (!string.IsNullOrEmpty(span.Content))
                    {
                        tw.Write(ReserveString(span.Content));
                    }
                    tw.Write("\");\r\n");
                }
                else if (isFirst == false && isLast == false)
                {
                    //isMid = true;
                    if (!string.IsNullOrEmpty(span.Content))
                    {
                        tw.Write(ReserveString(span.Content));
                    }
                }
                else if (isFirst == true && isLast == false)
                {
                    tw.Write("\t\t\twriter.Write(\"");
                    if (!string.IsNullOrEmpty(span.Content))
                    {
                        tw.Write(ReserveString(span.Content));
                    }
                }
                else if (isFirst == false && isLast == true)
                {
                    if (!string.IsNullOrEmpty(span.Content))
                    {
                        tw.Write(ReserveString(span.Content));
                    }
                    tw.Write("\");\r\n");
                }
            }
            else if (span.Kind == SpanKind.MetaCode)
            {
                //{}
                //tw.Write("\t\t\t");
                //tw.Write(span.Content);
            }
            else if (span.Kind == SpanKind.Transition)
            {
                //@符号
            }
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
    }
}
