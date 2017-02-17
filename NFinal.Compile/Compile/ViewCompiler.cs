//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :ViewCompiler.cs
//        Description : 模板(视图)编译类
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
    /// <summary>
    /// Controller中View函数信息类
    /// </summary>
    public class ViewData
    {
        public static string[] Styles = null;
        public static string[] GetStyles(Config config)
        {
            string[] styles= Directory.GetDirectories(Frame.MapPath(config.Views));
            return styles;
        }
        public string templateStyle = string.Empty;
        public string template = string.Empty;
        public string csharpCode = string.Empty;
        public int index = 0;
        public int length = 0;
    }
    /// <summary>
    /// Controller中View函数编译类
    /// </summary>
    public class ViewCompiler
    {
        public static Regex viewReg = new Regex(@"(?:this\.)?View\s*\((?:\s*(\S+),)?\s*(?:""([^""]*)"")?\s*\)\s*;(\s*\r?\n)?");
        /// <summary>
        /// 在某段位置替换成另一段代码
        /// </summary>
        /// <param name="str">要修改的内容</param>
        /// <param name="index">开始位置</param>
        /// <param name="length">长度</param>
        /// <param name="rep">替换的字符串</param>
        /// <returns>返回修改后的字符串</returns>
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
        public ViewCompiler()
        { }

        /// <summary>
        /// 从csharp语句中分析出Views函数信息
        /// </summary>
        /// <param name="csharpCode"></param>
        /// <returns></returns>
        public System.Collections.Generic.List<ViewData> Compile(string csharpCode)
        {
            MatchCollection matViews = viewReg.Matches(csharpCode);
            Match mat = null;
            System.Collections.Generic.List<ViewData> viewDatas = new System.Collections.Generic.List<ViewData>();
            ViewData viewData = null;
            if (matViews.Count > 0)
            {
                for (int i = 0; i < matViews.Count; i++)
                {
                    mat = matViews[i];
                    if (mat.Success)
                    {
                        viewData = new ViewData();
                        viewData.templateStyle = mat.Groups[1].Value;
                        viewData.template = mat.Groups[2].Value;
                        viewData.csharpCode = mat.Value;
                        viewData.index = mat.Index;
                        viewData.length = mat.Length;
                        viewDatas.Add(viewData);
                    }
                }
            }
            return viewDatas;
        }

        /// <summary>
        /// 替换View函数,生成Web层
        /// </summary>
        /// <param name="csharpCode">csharp代码</param>
        /// <param name="relative_position">前面代码的相对位移</param>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="ClassName">类名称</param>
        /// <param name="methodName">方法名</param>
        /// <param name="viewDatas">View函数信息</param>
        /// <param name="config">配置信息</param>
        /// <returns>替换后,后面代码的相对位移</returns>
        public int SetMagicFunction(ref string csharpCode, int relative_position, string nameSpace, string ClassName, string methodName, System.Collections.Generic.List<ViewData> viewDatas, Config config)
        {
            if (viewDatas.Count > 0)
            {
                //模板目录,
                string tplDir = "";
                //模板文件
                string tplFileName = "";

                ViewData viewData = null;
                StreamReader sr = null;
                StringWriter compressWriter = null;
                string aspxCode = "";
                for (int i = 0; i < viewDatas.Count; i++)
                {
                    viewData = viewDatas[i];
                    bool IsVarName = false;
                    //获取模板样式变量或字符串
                    if (string.IsNullOrEmpty(viewData.templateStyle))
                    {
                        //此处当字符串来算.
                        IsVarName = false;
                        viewData.templateStyle ="\""+ config.defaultStyle.Trim('/')+"\"";
                    }
                    else
                    {
                        if (viewData.templateStyle.IndexOf('\"') > -1)
                        {
                            IsVarName = false;
  
                        }
                        else
                        {
                            IsVarName = true;
                        }
                    }
                    //如果是字符串则直接取值就行了.
                    string[] templateStyle = null;
                    string[] styles = Directory.GetDirectories(Frame.MapPath(config.Views));
                    int defaultTemplateStyleIndex = 0;
                    string[] sharpCodes=null;
                    if (styles.Length > 0)
                    {
                        sharpCodes=new string[styles.Length];
                        templateStyle=new string[styles.Length];
                        for(int j=0;j<styles.Length;j++)
                        {
                            templateStyle[j] = new DirectoryInfo(styles[j]).Name;
                            //如果不是变量而是字符串
                            if (!IsVarName)
                            {
                                if (viewData.templateStyle.Trim('\"') == templateStyle[j])
                                {
                                    defaultTemplateStyleIndex = j;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            //如果参数是字符串
                            else
                            {
                                if (templateStyle[j] == config.defaultStyle.Trim('/'))
                                {
                                    defaultTemplateStyleIndex = j;
                                }
                            }
                            tplDir = config.ChangeControllerName(config.Views + templateStyle[j]+'/', (nameSpace + '.' + ClassName).Replace('.', '/')) + "/";

                            if (!string.IsNullOrEmpty(viewData.template))
                            {
                                //如果参数为/IndexController/Index.aspx
                                if (viewData.template.IndexOf('/') == 0)
                                {
                                    tplFileName = Frame.MapPath(config.Views + templateStyle[j]+"/" + viewData.template);
                                }
                                //如果参数为IndexController/Index.aspx
                                else if (viewData.template.IndexOf('/') > 0)
                                {
                                    tplFileName = Frame.MapPath(tplDir.Substring(0, tplDir.Length - ClassName.Length - 1) + viewData.template);
                                }
                                //如果参数为Index.aspx
                                else
                                {
                                    tplFileName = Frame.MapPath(tplDir + viewData.template);
                                }
                            }
                            //如果参数为空
                            else
                            {
                                tplFileName = Frame.MapPath(tplDir + methodName + ".aspx");
                            }
                            tplDir = Path.GetDirectoryName(tplFileName);
                            if (!Directory.Exists(tplDir))
                            {
                                Directory.CreateDirectory(tplDir);
                            }
                            //如果模板存在,则重写自动提示类
                            if (File.Exists(tplFileName))
                            {
                                string tplFileClassName = tplFileName + ".cs";
                                //重写自动提示类
                                if (tplFileName.EndsWith(".aspx") || tplFileName.EndsWith(".ascx"))
                                {
                                    Template.ASPX aspx = new Template.ASPX();
                                    sr = new StreamReader(tplFileName, System.Text.Encoding.UTF8);
                                    compressWriter = new StringWriter();
                                    aspxCode = sr.ReadToEnd();
                                    sr.Close();
                                    if (IsVarName)
                                    {
                                        sharpCodes[j] = aspx.Render(compressWriter, aspxCode, 5, config.CompressHTML);
                                    }
                                    else
                                    {
                                        sharpCodes[j] = aspx.Render(compressWriter, aspxCode, 3, config.CompressHTML);
                                    }
                                }
                                else if (tplFileName.EndsWith(".cshtml"))
                                {
                                    Template.Razor razor = new Template.Razor();
                                    sr = new StreamReader(tplFileName, System.Text.Encoding.UTF8);
                                    compressWriter = new StringWriter();
                                    aspxCode = sr.ReadToEnd();
                                    sr.Close();
                                    if (IsVarName)
                                    {
                                        sharpCodes[j] = razor.Render(compressWriter, aspxCode, 5, config.CompressHTML);
                                    }
                                    else
                                    {
                                        sharpCodes[j] = razor.Render(compressWriter, aspxCode, 3, config.CompressHTML);
                                    }
                                }
                                else
                                {
                                    sharpCodes[j] = "";
                                }

                            }
                            else
                            {
                                sharpCodes[j] = "";
                            }
                        }

                        StringWriter swTemplate = new StringWriter();
                        //如果参数是变量,而非字符串
                        if (IsVarName)
                        {
                            swTemplate.Write("switch(");
                            swTemplate.Write(viewData.templateStyle);
                            swTemplate.Write(")\r\n\t\t\t{\r\n");

                            if (styles.Length > 0)
                            {
                                for (int j = 0; j < styles.Length; j++)
                                {
                                    if (j != defaultTemplateStyleIndex)
                                    {
                                        swTemplate.Write("\t\t\t\tcase \"");
                                        swTemplate.Write(templateStyle[j]);
                                        swTemplate.Write("\":\r\n\t\t\t\t{\r\n");
                                        swTemplate.Write(sharpCodes[j]);
                                        swTemplate.Write("\t\t\t\t\tbreak;\r\n\t\t\t\t}\r\n");
                                    }
                                }
                            }

                            if (styles.Length > 0)
                            {
                                for (int j = 0; j < styles.Length; j++)
                                {
                                    if (j == defaultTemplateStyleIndex)
                                    {
                                        swTemplate.Write("\t\t\t\tdefault :\r\n\t\t\t\t{\r\n");
                                        swTemplate.Write(sharpCodes[j]);
                                        swTemplate.Write("\t\t\t\t\tbreak;\r\n\t\t\t\t}\r\n");
                                    }
                                }
                            }
                            swTemplate.Write("\t\t\t}\r\n");
                        }
                        else
                        {
                            if (styles.Length > 0)
                            {
                                for (int j = 0; j < styles.Length; j++)
                                {
                                    if (j == defaultTemplateStyleIndex)
                                    {
                                        swTemplate.Write("\r\n");
                                        swTemplate.Write(sharpCodes[j]);
                                    }
                                }
                            }
                        }
                        relative_position += Replace(ref csharpCode, relative_position + viewData.index, viewData.length, swTemplate.ToString());
                        swTemplate.Close();
                    }   
                }
            }
            return relative_position;
        }
    }
}