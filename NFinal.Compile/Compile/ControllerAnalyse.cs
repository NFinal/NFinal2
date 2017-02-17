//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :Compiler.cs
//        Description :Csharp代码分析类
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace NFinal.Compile
{
    /// <summary>
    /// Csharp代码分析类
    /// </summary>
    public class ControllerAnalyse
    {


        /// <summary>
        /// 得到csharp代码中{}中的内容,如类内容,方法体等
        /// </summary>
        /// <param name="csharptCode">Csharp代码</param>
        /// <param name="index">{符号开始的地方</param>
        /// <returns></returns>
        private string GetContent(string csharptCode, int index)
        {
            char[] buffer = csharptCode.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int begin = 0;
            int begin_index = index + 1;
            int end = 0;
            int end_index = 0;

            for (int i = index; i < buffer.Length; i++)
            {
                if (buffer[i] == '{')
                {
                    begin++;
                }
                if (buffer[i] == '}')
                {
                    end++;
                }
                if (begin == end && begin != 0)
                {
                    end_index = i - 1;
                    break;
                }
            }
            if (end_index > begin_index)
            {
                return csharptCode.Substring(begin_index, end_index - begin_index + 1);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 取得类文件中的类,方法等信息
        /// </summary>
        /// <param name="fileName">类文件的路径</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public ControllerFileData GetFileData(string appRoot, string app, string fileName, System.Text.Encoding encoding, Config config)
        {
            StreamReader sr = new StreamReader(fileName, encoding);
            string csharpCode = sr.ReadToEnd();
            sr.Close();
            MagicCodeSegment codeSeg = new MagicCodeSegment(appRoot, app);
            csharpCode = codeSeg.Parse(csharpCode);
            return GetFileData(fileName, csharpCode,config);
        }
        /// <summary>
        /// 把代码中的注释全部替换为空格
        /// </summary>
        /// <param name="csharpCode"></param>
        /// <returns></returns>
        public string DeleteComment(string csharpCode)
        {
            string parttern1 = @"/\*[^*]*\*+(?:[^/*][^*]*\*+)*/";
            string parttern2 = @"//[^\r\n]*";

            Regex reg1 = new Regex(parttern1);
            MatchCollection mac1 = reg1.Matches(csharpCode);
            for (int i = 0; i < mac1.Count; i++)
            {
                csharpCode = csharpCode.Remove(mac1[i].Index, mac1[i].Length);
                csharpCode = csharpCode.Insert(mac1[i].Index, string.Empty.PadLeft(mac1[i].Length));
            }
            Regex reg2 = new Regex(parttern2);
            MatchCollection mac2 = reg2.Matches(csharpCode);
            for (int i = 0; i < mac2.Count; i++)
            {
                csharpCode = csharpCode.Remove(mac2[i].Index, mac2[i].Length);
                csharpCode = csharpCode.Insert(mac2[i].Index, string.Empty.PadLeft(mac2[i].Length));
            }
            return csharpCode;
        }
        /// <summary>
        /// 获取类文件中的类,方法等信息
        /// </summary>
        /// <param name="fileName">类文件的路径</param>
        /// <param name="csharpCode">类文件中的代码</param>
        /// <returns></returns>
        public ControllerFileData GetFileData(string fileName, string csharpCode, Config config)
        {
            ControllerFileData fileData = new ControllerFileData();
            fileData.config = config;
            fileData.fileName = fileName;
            fileData.csharpCode = csharpCode;
            ControllerClassData classData = null;
            ControllerMethodData methodData = null;
            ControllerParameterData parameterData = null;
            ControllerAttributeData attributeData = null;
            ControllerParameterParser parameterParser = new ControllerParameterParser();
            UrlParser urlParser = null;
            string patern = @"namespace\s+(\S+)\s*\{";
            Regex reg = new Regex(patern);
            Regex regAttribute, regParameter;
            Match mFile, mReturnVarName;
            MatchCollection mClass, mMethod, mAttribute, mParameter;
            //string[] parameters = null;
            //查找命名空间
            if (reg.IsMatch(csharpCode))
            {
                mFile = reg.Match(csharpCode);
                fileData.start = mFile.Index;
                fileData.length = mFile.Length;
                fileData.position = mFile.Index + mFile.Length;
                fileData.Content = GetContent(csharpCode, mFile.Index + mFile.Length - 1);
                fileData.nameSpace = mFile.Groups[1].Value;
                fileData.projectName = fileData.nameSpace.Split('.')[0];
                fileData.appName = fileData.nameSpace.Split('.')[1];
                patern = @"((?:\[(?:(?<open>\[)|(?<-open>\])|[^\[\]])*(?(open)(?!))\]\s*)*)((?:\r?\n\s*\\[^\n]*\s*)*)public class\s+([^\s:]+)(?:\s*:\s*(\S+))?\s*\{";
                reg = new Regex(patern);
                mClass = reg.Matches(fileData.Content);
                //查找类
                if (mClass.Count > 0)
                {
                    classData = null;
                    for (int i = 0; i < mClass.Count; i++)
                    {
                        classData = new ControllerClassData();
                        classData.start = fileData.position + mClass[i].Index;
                        classData.length = mClass[i].Length;

                        classData.position = fileData.position + mClass[i].Index + mClass[i].Length;
                        classData.Content = GetContent(fileData.Content, mClass[i].Index + mClass[i].Length - 1);
                        //取出类中的特性进行分析
                        classData.AttributeString = mClass[i].Groups[1].Value;
                        classData.Attributes = new System.Collections.Generic.List<ControllerAttributeData>();
                        #region 解析baseUrl
                        bool hasBaseUrl = false;
                        if (!string.IsNullOrEmpty(classData.AttributeString))
                        {
                            patern = @"\s*(?:,|\[)\s*([a-zA-Z0-9_\.]+)\s*\(((?:(?<open>\()|(?<-open>\))|[^()])*(?(open)(?!)))\)";
                            regAttribute = new Regex(patern);
                            mAttribute = regAttribute.Matches(classData.AttributeString);

                            for (int k = 0; k < mAttribute.Count; k++)
                            {
                                attributeData = new ControllerAttributeData();
                                attributeData.name = mAttribute[k].Groups[1].Value;
                                attributeData.parameters = mAttribute[k].Groups[2].Value;
                                classData.Attributes.Add(attributeData);
                                if (attributeData.name == "BaseUrl")
                                {
                                    classData.baseUrl = attributeData.parameters.Trim('\"');
                                    if (classData.baseUrl.StartsWith("~/"))
                                    {
                                        classData.baseUrl = classData.baseUrl.Substring(1);
                                    }
                                    else
                                    {
                                        classData.baseUrl = config.urlPrefix+classData.baseUrl;
                                    }
                                    hasBaseUrl = true;
                                }
                            }
                        }
                        #endregion
                        classData.name = mClass[i].Groups[3].Value;
                        classData.fullName = fileData.nameSpace + "." + classData.name;
                        classData.baseName = mClass[i].Groups[4].Value;
                        if (!hasBaseUrl)
                        {
                            classData.baseUrl = config.urlPrefix + "/" + classData.name ;
                        }
                        string temp = (Frame.AssemblyTitle + config.Controller).TrimEnd('/');
                        classData.relativeName = classData.fullName.Substring(temp.Length, classData.fullName.Length - temp.Length).Replace('.', '/');
                        classData.relativeDotName = classData.fullName.Substring(temp.Length, classData.fullName.Length - temp.Length);
                        //patern = @"(public|private|protected)\s+(?:(?:override|new)\s+)*(\S+)\s+(\S+)\s*\(([^\(\)]*)\)\s*\{";
                        patern = @"(\s*///\s*<summary>((?:\s*///\s*.*?)+)\s*///\s*</summary>((?:\s*///\s*<param name=""[_A-Za-z0-9]+"">.*?</param>)*)(\s*///\s*<returns>(.*?)</returns>)?\s*)?((?:\[(?:(?<open>\[)|(?<-open>\])|[^\[\]])*(?(open)(?!))\]\s*)*)((?:\r?\n\s*\\[^\n]*\s*)*)(public|private|protected|internal)\s+(?:(?:static|abstract|sealed|new|virtual|override)\s+)?([^\{\}""]+)\s+(\S+)\s*\(((?:(?<opp>\()|(?<-opp>\))|[^()])*(?(opp)(?!)))\)\s*\{";
                        //patern += @"((?:\[(?:(?<open>\[)|(?<-open>\])|[^\[\]])*(?(open)(?!))\]\s*)*)((?:\r?\n\s*\\[^\n]*\s*)*)(public|private|protected|internal)\s+(?:(?:static|abstract|sealed|new|virtual|override)\s+)?([^\{\}""]+)\s+(\S+)\s*\(((?:(?<opp>\()|(?<-opp>\))|[^()])*(?(opp)(?!)))\)\s*\{";
                        reg = new Regex(patern);
                        mMethod = reg.Matches(classData.Content);
                        //查找方法
                        if (mMethod.Count > 0)
                        {
                            methodData = null;
                            for (int j = 0; j < mMethod.Count; j++)
                            {
                                methodData = new ControllerMethodData();

                                methodData.AttributeString = mMethod[j].Groups[6].Value;
                                methodData.CommitString = mMethod[j].Groups[1].Value;
                                methodData.publicStr = mMethod[j].Groups[8].Value;
                                methodData.isPublic = methodData.publicStr == "public";
                                methodData.returnType = mMethod[j].Groups[9].Value;
                                methodData.returnTypeIndex = mMethod[j].Groups[9].Index;
                                //解析函数注释
                                string summaryPatern = @"\s*///\s*(.*)";
                                Regex summaryReg = new Regex(summaryPatern);
                                MatchCollection summaryMac = summaryReg.Matches(mMethod[j].Groups[2].Value);
                                foreach (Match m in summaryMac)
                                {
                                    methodData.methodCommit += m.Groups[1].Value;
                                }
                                //解析函数参数注释
                                string paramsPatern = @"\s*///\s*<param name=""([_A-Za-z0-9]+)"">(.*?)</param>";
                                Regex paramsReg = new Regex(paramsPatern,RegexOptions.Singleline);
                                MatchCollection paramsMac = paramsReg.Matches(mMethod[j].Groups[3].Value);
                                methodData.paramsCommit = new System.Collections.Specialized.NameValueCollection();
                                if (paramsMac.Count > 0)
                                {
                                    foreach (Match m in paramsMac)
                                    {
                                        if (m.Groups[1].Success)
                                        {
                                            methodData.paramsCommit.Add(m.Groups[1].Value, m.Groups[2].Value);
                                        }
                                    }
                                }
                                //解析函数返回值注释
                                methodData.returnCommit= mMethod[j].Groups[5].Value;

                                methodData.name = mMethod[j].Groups[10].Value;
                                methodData.parameters = mMethod[j].Groups[11].Value;
                                methodData.parametersIndex = classData.position + mMethod[j].Groups[11].Index;
                                methodData.parametersLength = mMethod[j].Groups[11].Length;
                                //bool compress=false;
                                //bool serverCache=false;
                                //如果有函数特性
                                #region 分析函数特性
                                bool hasActionUrlAttribute = false;
                                if (!string.IsNullOrEmpty(methodData.AttributeString))
                                {
                                    patern = @"\s*(?:,|\[)\s*([a-zA-Z0-9_\.]+)\s*\(((?:(?<open>\()|(?<-open>\))|[^()])*(?(open)(?!)))\)";
                                    regAttribute = new Regex(patern);
                                    mAttribute = regAttribute.Matches(methodData.AttributeString);
                                    methodData.Attributes = new System.Collections.Generic.List<ControllerAttributeData>();

                                    for (int k = 0; k < mAttribute.Count; k++)
                                    {
                                        attributeData = new ControllerAttributeData();
                                        attributeData.name = mAttribute[k].Groups[1].Value;
                                        attributeData.parameters = mAttribute[k].Groups[2].Value;
                                        methodData.Attributes.Add(attributeData);
                                        if (attributeData.name == "CacheFile")
                                        {
                                            string[] ps = attributeData.parameters.Split(',');
                                            methodData.minutes = ps[0];
                                            methodData.optimizing |= 2;
                                            methodData.optimizing |= 32;
                                        }
                                        else if (attributeData.name == "CacheNormal")
                                        {
                                            string[] ps = attributeData.parameters.Split(',');
                                            methodData.minutes = ps[0];
                                            methodData.optimizing |= 8;
                                            methodData.optimizing |= 64;
                                        }
                                        else if (attributeData.name == "Cache")
                                        {
                                            //serverCache =true;
                                            string[] ps = attributeData.parameters.Split(',');
                                            if (ps[0].IndexOf("Server") > -1)
                                            {
                                                methodData.minutes = ps[2];
                                                if (ps[0].IndexOf("Server.NoCache") > -1)
                                                {
                                                    methodData.optimizing |= 1;
                                                }
                                                else if (ps[0].IndexOf("Server.FileDependency") > -1)
                                                {
                                                    methodData.optimizing |= 2;
                                                }
                                                else if (ps[0].IndexOf("Server.AbsoluteExpiration") > -1)
                                                {
                                                    methodData.optimizing |= 4;
                                                }
                                                else if (ps[0].IndexOf("Server.SlidingExpiration") > -1)
                                                {
                                                    methodData.optimizing |= 8;
                                                }
                                                if (ps[1].IndexOf("Browser.NoStore") > -1)
                                                {
                                                    methodData.optimizing |= 16;
                                                }
                                                else if (ps[2].IndexOf("Browser.NotModify") > -1)
                                                {
                                                    methodData.optimizing |= 32;
                                                }
                                                else if (ps[2].IndexOf("Browser.Expires") > -1)
                                                {
                                                    methodData.optimizing |= 64;
                                                }
                                                else if (ps[2].IndexOf("Browser.NoExpires") > -1)
                                                {
                                                    methodData.optimizing |= 128;
                                                }
                                            }
                                            else if (ps[0].IndexOf("Standard") > -1)
                                            {
                                                if (ps[0].IndexOf("Standard.File") > -1)
                                                {
                                                    methodData.optimizing |= 2 + 32;
                                                }
                                                if (ps[0].IndexOf("Standard.Normal") > -1)
                                                {
                                                    methodData.optimizing |= 8 + 64;
                                                    methodData.minutes = ps[1];
                                                }
                                            }
                                        }
                                        else if (attributeData.name == "RewriteDirectory")
                                        {
                                            string[] ps = StringAnalyse.GetStringFromCode(attributeData.parameters, false);
                                            RewriteDirectory rewriteDirectory = new RewriteDirectory();
                                            rewriteDirectory.from = ps[0];
                                            rewriteDirectory.to = ps[1];
                                            Frame.reWriteData.rewriteDirectoryList.Add(rewriteDirectory);
                                        }
                                        else if (attributeData.name == "RewriteFile")
                                        {
                                            string[] ps = StringAnalyse.GetStringFromCode(attributeData.parameters, false);
                                            RewriteFile rewriteFile = new RewriteFile();
                                            rewriteFile.from = ps[0];
                                            rewriteFile.to = ps[1];
                                            Frame.reWriteData.rewriteFileList.Add(rewriteFile);
                                        }
                                        Regex verbRegex = new Regex("^(Get|Post)?(Html|Empty|Redirect|Json|Css|JavaScript|File|Jpeg|Gif|Png|Xml|Text|Svg)(Zip|Deflate)?$");
                                        Match verbMat = verbRegex.Match(attributeData.name);
                                        if (verbMat.Success)
                                        {
                                            hasActionUrlAttribute = true;
                                            methodData.url = attributeData.parameters.Trim('\"');
                                            //提交方法
                                            if (verbMat.Groups[1].Success)
                                            {
                                                if (verbMat.Groups[1].Value == "Get")
                                                {
                                                    methodData.verbMethod = VerbMethod.Get;
                                                }
                                                else if (verbMat.Groups[1].Value == "Post")
                                                {
                                                    methodData.verbMethod = VerbMethod.Post;
                                                }
                                                else
                                                {
                                                    methodData.verbMethod = VerbMethod.Request;
                                                }
                                            }
                                            methodData.contentType = "text/html; charset=utf-8";
                                            //请求内容
                                            if (verbMat.Groups[2].Success)
                                            {
                                                if (verbMat.Groups[2].Value == "Html")
                                                {
                                                    methodData.contentType = "text/html; charset=utf-8";
                                                }
                                                else if (verbMat.Groups[2].Value == "Empty")
                                                {
                                                    methodData.contentType = "text/html; charset=utf-8";
                                                }
                                                else if (verbMat.Groups[2].Value == "Redirect")
                                                {
                                                    methodData.contentType = "text/html; charset=utf-8";
                                                }
                                                else if (verbMat.Groups[2].Value == "Json")
                                                {
                                                    methodData.contentType = "application/json; charset=utf-8";
                                                }
                                                else if (verbMat.Groups[2].Value == "Css")
                                                {
                                                    methodData.contentType = "text/css; charset=utf-8";
                                                }
                                                else if (verbMat.Groups[2].Value == "JavaScript")
                                                {
                                                    methodData.contentType = "text/javascript; charset=utf-8";
                                                }
                                                else if (verbMat.Groups[2].Value == "File")
                                                {
                                                    methodData.contentType = "application/octet-stream";
                                                }
                                                else if (verbMat.Groups[2].Value == "Jpeg")
                                                {
                                                    methodData.contentType = "image/jpeg";
                                                }
                                                else if (verbMat.Groups[2].Value == "Gif")
                                                {
                                                    methodData.contentType = "image/gif";
                                                }
                                                else if (verbMat.Groups[2].Value == "Png")
                                                {
                                                    methodData.contentType = "image/png";
                                                }
                                                else if (verbMat.Groups[2].Value == "Xml")
                                                {
                                                    methodData.contentType = "text/xml; charset=utf-8";
                                                }
                                                else if (verbMat.Groups[2].Value == "Text")
                                                {
                                                    methodData.contentType = "text/plain; charset=utf-8";
                                                }
                                                else if (verbMat.Groups[2].Value == "Svg")
                                                {
                                                    methodData.contentType = "image/svg+xml";
                                                }
                                                else
                                                {
                                                    methodData.contentType = "text/html; charset=utf-8";
                                                }
                                            }
                                            //设置压缩状态
                                            if (verbMat.Groups[3].Success)
                                            {
                                                if (verbMat.Groups[3].Value == "Zip")
                                                {
                                                    methodData.optimizing |= 512;
                                                }
                                                else if (verbMat.Groups[3].Value == "Deflate")
                                                {
                                                    methodData.optimizing |= 1024;
                                                }
                                            } 
                                        }
                                    }
                                }
                                #endregion
                                #region 获取函数参数
                                //如果有函数参数
                                if (!string.IsNullOrEmpty(methodData.parameters))
                                {
                                    methodData.hasParameters = true;
                                    patern = @"((?:\s*\[(?:(?<open>\[)|(?<-open>\])|[^\[\]])*(?(open)(?!))\])*)?\s*([a-zA-Z0-9_\.]+)(\s*\[\])?\s+([a-zA-Z0-9_]+)(\s*=\s*([^=\[,]+))?";
                                    regParameter = new Regex(patern);
                                    mParameter = regParameter.Matches(methodData.parameters);

                                    for (int k = 0; k < mParameter.Count; k++)
                                    {
                                        parameterData = new ControllerParameterData();
                                        parameterData.type = mParameter[k].Groups[2].Value;
                                        parameterData.isArray = mParameter[k].Groups[3].Success;
                                        parameterData.name = mParameter[k].Groups[4].Value;
                                        parameterData.hasDefaultValue = mParameter[k].Groups[5].Success;
                                        parameterData.defaultValue = mParameter[k].Groups[6].Value;
                                        parameterData.AttributeString = mParameter[k].Groups[1].Value;
                                        parameterData.parameterCommit = methodData.paramsCommit[parameterData.name];
                                        parameterData.Attributes = new System.Collections.Generic.List<ControllerAttributeData>();
                                        //如果有参数属性
                                        if (!string.IsNullOrEmpty(parameterData.AttributeString))
                                        {
                                            patern = @"\s*(?:,|\[)\s*([a-zA-Z0-9_\.]+)\s*\(((?:(?<open>\()|(?<-open>\))|[^()])*(?(open)(?!)))\)";
                                            regAttribute = new Regex(patern);
                                            mAttribute = regAttribute.Matches(parameterData.AttributeString);
                                            methodData.verifyCode.Append("if(!(");
                                            for (int n = 0; n < mAttribute.Count; n++)
                                            {
                                                attributeData = new ControllerAttributeData();
                                                attributeData.name = mAttribute[n].Groups[1].Value;
                                                attributeData.parameters = mAttribute[n].Groups[2].Value;
                                                parameterData.Attributes.Add(attributeData);
                                                string andChars = "";
                                                if (n != 0)
                                                {
                                                    andChars = "&&";
                                                }
                                                //添加html5服务端验证代码
                                                if (attributeData.name == "type")
                                                {
                                                    if (attributeData.parameters == "text")
                                                    {
                                                        //none
                                                    }
                                                    else if (attributeData.parameters == "email")
                                                    {
                                                        methodData.verifyCode.Append(string.Format(" {0} NFinal.Advanced.html5Validate.typeValid(get[\"{1}\"], NFinal.Advanced.type.email) \r\n\t\t\t\t\t\t\t", andChars, parameterData.name));
                                                    }
                                                    else if (attributeData.parameters == "url")
                                                    {
                                                        methodData.verifyCode.Append(string.Format(" {0} NFinal.Advanced.html5Validate.typeValid(get[\"{1}\"], NFinal.Advanced.type.url) \r\n\t\t\t\t\t\t\t", andChars, parameterData.name));
                                                    }
                                                    else if (attributeData.parameters == "number")
                                                    {
                                                        methodData.verifyCode.Append(string.Format(" {0} NFinal.Advanced.html5Validate.typeValid(get[\"{1}\"], NFinal.Advanced.type.number) \r\n\t\t\t\t\t\t\t", andChars, parameterData.name));
                                                    }
                                                    else if (attributeData.parameters == "range")
                                                    {
                                                        methodData.verifyCode.Append(string.Format(" {0} NFinal.Advanced.html5Validate.typeValid(get[\"{1}\"], NFinal.Advanced.type.range) \r\n\t\t\t\t\t\t\t", andChars, parameterData.name));
                                                    }
                                                    else if (attributeData.parameters == "date")
                                                    {
                                                        methodData.verifyCode.Append(string.Format(" {0} NFinal.Advanced.html5Validate.typeValid(get[\"{1}\"], NFinal.Advanced.type.date) \r\n\t\t\t\t\t\t\t", andChars, parameterData.name));
                                                    }
                                                    else if (attributeData.parameters == "month")
                                                    {
                                                        methodData.verifyCode.Append(string.Format(" {0} NFinal.Advanced.html5Validate.typeValid(get[\"{1}\"], NFinal.Advanced.type.month) \r\n\t\t\t\t\t\t\t", andChars, parameterData.name));
                                                    }
                                                    else if (attributeData.parameters == "week")
                                                    {
                                                        methodData.verifyCode.Append(string.Format(" {0} NFinal.Advanced.html5Validate.typeValid(get[\"{1}\"], NFinal.Advanced.type.week) \r\n\t\t\t\t\t\t\t", andChars, parameterData.name));
                                                    }
                                                    else if (attributeData.parameters == "time")
                                                    {
                                                        methodData.verifyCode.Append(string.Format(" {0} NFinal.Advanced.html5Validate.typeValid(get[\"{1}\"], NFinal.Advanced.type.time) \r\n\t\t\t\t\t\t\t", andChars, parameterData.name));
                                                    }
                                                    else if (attributeData.parameters == "datetime_local")
                                                    {
                                                        methodData.verifyCode.Append(string.Format(" {0} NFinal.Advanced.html5Validate.typeValid(get[\"{1}\"], NFinal.Advanced.type.datetime_local) \r\n\t\t\t\t\t\t\t", andChars, parameterData.name));
                                                    }
                                                    else if (attributeData.parameters == "search")
                                                    {
                                                        methodData.verifyCode.Append(string.Format(" {0} NFinal.Advanced.html5Validate.typeValid(get[\"{1}\"], NFinal.Advanced.type.search) \r\n\t\t\t\t\t\t\t", andChars, parameterData.name));
                                                    }
                                                    else if (attributeData.parameters == "color")
                                                    {
                                                        methodData.verifyCode.Append(string.Format(" {0} NFinal.Advanced.html5Validate.typeValid(get[\"{1}\"], NFinal.Advanced.type.color) \r\n\t\t\t\t\t\t\t", andChars, parameterData.name));
                                                    }
                                                }
                                                else if (attributeData.name == "maxLength")
                                                {
                                                    methodData.verifyCode.Append(string.Format(" {0} (get[\"{1}\"].Length <= {2}) \r\n\t\t\t\t\t\t\t", andChars, parameterData.name, attributeData.parameters));
                                                }
                                                else if (attributeData.name == "minLength")
                                                {
                                                    methodData.verifyCode.Append(string.Format(" {0} (get[\"{1}\"].Length >= {2}) \r\n\t\t\t\t\t\t\t", andChars, parameterData.name, attributeData.parameters));
                                                }
                                                else if (attributeData.name == "max")
                                                {
                                                    methodData.verifyCode.Append(string.Format(" {0} (Convert.ToDecimal({1}) <= {2}) \r\n\t\t\t\t\t\t\t", andChars, parameterData.name, attributeData.parameters));
                                                }
                                                else if (attributeData.name == "min")
                                                {
                                                    methodData.verifyCode.Append(string.Format(" {0} (Convert.ToDecimal({1}) >= {2}) \r\n\t\t\t\t\t\t\t", andChars, parameterData.name, attributeData.parameters));
                                                }
                                                else if (attributeData.name == "step")
                                                {

                                                }
                                                else if (attributeData.name == "placeholder")
                                                {

                                                }
                                                else if (attributeData.name == "required")
                                                {
                                                    methodData.verifyCode.Append(string.Format(" {0} (get[\"{1}\"]!=null) \r\n\t\t\t\t\t\t\t", andChars, parameterData.name));
                                                }
                                                else if (attributeData.name == "pattern")
                                                {
                                                    methodData.verifyCode.Append(string.Format(" {0} new Regex.IsMatch(get[\"{1}\"]) \r\n\t\t\t\t\t\t\t", andChars, parameterData.name));
                                                }
                                            }
                                            methodData.verifyCode.Append(")){break;}");
                                        }
                                        parameterData.getParamterCode = parameterParser.BuildGetParameterCode(config.APP.Trim('/'),parameterData.type,
                                               parameterData.isArray, parameterData.name, parameterData.hasDefaultValue, parameterData.defaultValue);
                                        methodData.parameterNames += parameterData.name + ",";

                                        methodData.parameterTypeAndNames += parameterData.type + (parameterData.isArray ? "[] " : " ") + parameterData.name + ",";

                                        methodData.parameterDataList.Add(parameterData);

                                    }
                                    methodData.parameterNames = methodData.parameterNames.TrimEnd(',');
                                    methodData.parameterTypeAndNames = methodData.parameterTypeAndNames.TrimEnd(',');
                                }
                                #endregion
                                #region URL处理
                                //如果有ActionUrl属性
                                if (hasActionUrlAttribute && !string.IsNullOrEmpty(methodData.url))
                                {
                                    if (methodData.url.StartsWith("~/"))
                                    {
                                        urlParser = new UrlParser(config.urlPrefix + methodData.url.Substring(1),methodData.parameterDataList);
                                    }
                                    else
                                    {
                                        urlParser = new UrlParser(classData.baseUrl + methodData.url, methodData.parameterDataList);
                                    }
                                }
                                //如果没有则默认填充
                                else
                                {
                                    if (methodData.hasParameters)
                                    {
                                        methodData.url = classData.baseUrl + "/" + methodData.name;
                                        foreach (ControllerParameterData pd in methodData.parameterDataList)
                                        {
                                            methodData.url += "/{" + pd.name + "}";
                                        }
                                        methodData.url += config.urlExtension;
                                        //methodData.url = classData.baseUrl + "/" + methodData.name + config.urlExtension;
                                        urlParser = new UrlParser(methodData.url, methodData.parameterDataList);
                                    }
                                    else
                                    {
                                        methodData.url = classData.baseUrl + "/" + methodData.name + config.urlExtension;
                                        urlParser = new UrlParser(methodData.url, methodData.parameterDataList);
                                    }
                                }
                                methodData.urlParser = urlParser;
   
                                #endregion URL处理结束
                                methodData.start = classData.position + mMethod[j].Index;
                                methodData.length = mMethod[j].Length;

                                methodData.position = classData.position + mMethod[j].Index + mMethod[j].Length;
                                methodData.Content = GetContent(classData.Content, mMethod[j].Index + mMethod[j].Length - 1);

                                patern = @"return\s+([^\s;]+)\s*;";
                                reg = new Regex(patern, RegexOptions.RightToLeft);
                                mReturnVarName = reg.Match(methodData.Content);
                                if (mReturnVarName.Success)
                                {
                                    methodData.returnVarName = mReturnVarName.Groups[1].Value;
                                }
                                //排除Before和After
                                if (methodData.name != "Before" && methodData.name!="After")
                                {
                                    classData.MethodDataList.Add(methodData);
                                }
                            }
                        }
                        fileData.ClassDataList.Add(classData);
                    }
                }
            }
            return fileData;
        }
    }

}