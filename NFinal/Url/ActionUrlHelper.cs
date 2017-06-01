//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ActionUrlHelper.cs
//        Description :计算控制器行为所对应URL以及相关信息的帮助类
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;

namespace NFinal.Url
{
    /// <summary>
    /// 生成控制器行为对应URL的函数代理
    /// </summary>
    /// <param name="methodName"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public delegate string UrlDelegate(string methodName, StringContainer[] parameters);

    //public class Parameter
    //{
    //    public int index;
    //    public Type parameterType;
    //    public string name;
    //}
    /// <summary>
    /// 生成URL时用到的格式化字符串信息
    /// </summary>
    public class FormatData
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="formatUrl"></param>
        /// <param name="actionUrlNames"></param>
        public FormatData(string formatUrl, string[] actionUrlNames)
        {
            this.formatUrl = formatUrl;
            this.actionUrlNames = actionUrlNames;
        }
        /// <summary>
        /// 生成URL的格式化字符串
        /// </summary>
        public string formatUrl;
        /// <summary>
        /// Url中所包含的参数名
        /// </summary>
        public string[] actionUrlNames;
    }
    /// <summary>
    /// 控制器行为对应生成Url格式化字符串所需的信息数据，以及解析URL所需要的信息数据
    /// </summary>
    public class ActionUrlData
    {
        /// <summary>
        /// 控制器行为对应方法的反射信息
        /// </summary>
        public MethodInfo methodInfo;
        /// <summary>
        /// 后缀长度
        /// </summary>
        public int extensionLength;
        /// <summary>
        /// actionUrl不带后缀以及Http请求方法名
        /// </summary>
        public string actionUrl;
        /// <summary>
        /// 请求Url中包含的参数名
        /// </summary>
        public string[] actionUrlNames;
        /// <summary>
        /// 请求Url中不包含任何参数
        /// </summary>
        public bool hasNoParamsInUrl;
        /// <summary>
        /// 该Url是否仅以-与/分隔控制器名，方法名以及参数的简单Url
        /// </summary>
        public bool isSimpleUrl;
        /// <summary>
        /// actionKey,为requestMethod+actionUrl,用于搜索对应的控制器行为信息
        /// </summary>
        public string actionKey;
        /// <summary>
        /// 生成Url时，所用到的格式化字符串
        /// </summary>
        public string formatUrl;
        /// <summary>
        /// 解析请求Url中的参数时用到的正则表达式
        /// </summary>
        public string parameterRegex;
    }
    /// <summary>
    /// 计算控制器行为所对应URL以及相关信息的帮助类
    /// </summary>
    public class ActionUrlHelper
    {
        /// <summary>
        /// 缓存所有类型
        /// </summary>
        public static NFinal.Collections.FastDictionary<Type,Dictionary<string,FormatData>> formatControllerDictionary;
        /// <summary>
        /// 是否是URL分隔符
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool isSeparatorChar(char ch)
        {
            bool result = false;
            char[] sc = new char[] { '-', '/', '_' };
            foreach (char c in sc)
            {
                if (c == ch)
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// 解析自定义actionUrl中的参数，后缀等信息
        /// </summary>
        /// <param name="actionUrl"></param>
        /// <returns></returns>
        public static ActionUrlData GetActionUrlData(string actionUrl)
        {
            ActionUrlData actionUrlData = new ActionUrlData();
            string[] actionUrlNames;
            bool hasNoParamsInUrl = true;
            bool isSimpleUrl = true;
            string actionKey = null;
            string formatUrl = null;
            string parameterRegex = null;
            int leftBraceCount = 0;
            int leftBracePosition = 0;
            int rightBraceCount = 0;
            int rightBracePosition = 0;
            int lastSingleRightBraceIndex = 0;
            //用于解析字符串
            List<UrlSegment> segmentList = new List<UrlSegment>();
            UrlSegment segment = null;
            int firstLeftBracePosition = 0;
            #region
            for (int i = 0; i < actionUrl.Length - 1; i++)
            {
                if (actionUrl[i] == '{')
                {
                    if (actionUrl[i + 1] == '{')
                    {
                        //chars[i]='\\';
                        i++;
                    }
                    else
                    {
                        if (firstLeftBracePosition == 0)
                        {
                            firstLeftBracePosition = i;
                        }
                        leftBraceCount++;
                        leftBracePosition = i;
                        if (leftBracePosition > 1)
                        {
                            if (!isSeparatorChar(actionUrl[leftBracePosition - 1]))
                            {
                                isSimpleUrl = false;
                            }
                        }
                        i++;
                    }
                }
                else if (actionUrl[i] == '}')
                {
                    if (actionUrl[i + 1] == '}')
                    {
                        if (leftBraceCount == rightBraceCount)
                        {
                            //chars[i] = '\\';
                            i++;
                        }
                        else
                        {
                            rightBraceCount++;
                            rightBracePosition = i;
                            if (rightBracePosition + 2 < actionUrl.Length)
                            {
                                if (!isSeparatorChar(actionUrl[rightBracePosition]))
                                {
                                    isSimpleUrl = false;
                                }
                            }
                            if (leftBraceCount == rightBraceCount)
                            {
                                segment = new UrlSegment(actionUrl, leftBracePosition + 1, rightBracePosition - leftBracePosition - 1);
                                segmentList.Add(segment);
                            }
                        }
                    }
                    else
                    {
                        rightBraceCount++;
                        rightBracePosition = i;

                        if (leftBraceCount == rightBraceCount)
                        {
                            lastSingleRightBraceIndex = i;
                            segment = new UrlSegment(actionUrl, leftBracePosition + 1, rightBracePosition - leftBracePosition - 1);
                            segmentList.Add(segment);
                        }
                    }
                }
                if (i == actionUrl.Length - 2 && actionUrl[i] != '}' && actionUrl[i + 1] == '}')
                {
                    rightBraceCount++;
                    rightBracePosition = i + 1;

                    if (leftBraceCount == rightBraceCount)
                    {
                        segment = new UrlSegment(actionUrl, leftBracePosition + 1, rightBracePosition - leftBracePosition - 1);
                        segmentList.Add(segment);
                    }
                }
            }
            actionUrlData.isSimpleUrl = isSimpleUrl;
            actionUrlData.extensionLength = actionUrl.Length - lastSingleRightBraceIndex - 1;
            #endregion
            //生成名称数组
            #region
            actionUrlNames = new string[segmentList.Count];
            int ii = 0;
            foreach (var segmenta in segmentList)
            {
                actionUrlNames[ii] =segmenta.name;
                ii++;
            }
            actionUrlData.actionUrlNames = actionUrlNames;
            #endregion
            //生成查找路由的字符串
            #region
            if (segmentList.Count > 0)
            {
                hasNoParamsInUrl = false;
                actionKey = actionUrl.Substring(0, firstLeftBracePosition).Replace("{{", "{").Replace("}}", "}");
            }
            else
            {
                hasNoParamsInUrl = true;
                actionKey = actionUrl;
            }
            actionUrlData.hasNoParamsInUrl = hasNoParamsInUrl;
            actionUrlData.actionKey = actionKey;
            #endregion
            //格式化Url
            #region
            formatUrl = actionUrl;
            int relitivePosition = 0;
            string format = "";
            for (int i = 0; i < segmentList.Count; i++)
            {
                formatUrl = formatUrl.Remove(segmentList[i].start + relitivePosition, segmentList[i].length);
                if (string.IsNullOrEmpty(segmentList[i].format))
                {
                    format = i.ToString();
                }
                else
                {
                    format = i.ToString() + ":" + segmentList[i].format;
                }
                formatUrl = formatUrl.Insert(segmentList[i].start + relitivePosition, format);
                relitivePosition += format.Length - segmentList[i].length;
                if (i == segmentList.Count - 1)
                {
                    string ext = formatUrl.Substring(segmentList[i].start + relitivePosition + segmentList[i].length);
                    int pos = ext.LastIndexOf('.');
                    if (pos > -1)
                    {
                        formatUrl = formatUrl.Insert(segmentList[i].start + relitivePosition + segmentList[i].length + pos, (actionKey.Length).ToString("00"));
                    }
                    else
                    {
                        formatUrl = formatUrl + (actionKey.Length).ToString("00");
                    }
                }
            }
            actionUrlData.formatUrl = formatUrl;
            #endregion
            //生成解析URL的代码
            #region
            relitivePosition = 0;
            parameterRegex = actionUrl;
            for (int i = 0; i < segmentList.Count; i++)
            {
                parameterRegex = parameterRegex.Remove(segmentList[i].start - 1 + relitivePosition, segmentList[i].length + 2);
                parameterRegex = parameterRegex.Insert(segmentList[i].start - 1 + relitivePosition, string.Format("({0})", segmentList[i].regex));
                relitivePosition += segmentList[i].regex.Length - segmentList[i].length;
                if (i == segmentList.Count - 1)
                {
                    string ext = parameterRegex.Substring(segmentList[i].start + relitivePosition + segmentList[i].length);
                    int pos = ext.LastIndexOf('.');
                    if (pos > -1)
                    {
                        parameterRegex = parameterRegex.Insert(segmentList[i].start + relitivePosition + segmentList[i].length + pos, (actionKey.Length).ToString("00"));
                    }
                    else
                    {
                        parameterRegex = parameterRegex + (actionKey.Length).ToString("00");
                    }
                }
            }
            parameterRegex = parameterRegex.Replace("{{", "{").Replace("}}", "}");
            actionUrlData.parameterRegex = parameterRegex;
            #endregion
            return actionUrlData;
        }
        /// <summary>
        /// 简化的字符串格式化函数
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static String Format(String format,params StringContainer[] args)
        {
            if (format == null || args == null)
                throw new ArgumentNullException((format == null) ? "format" : "args");

            StringBuilder sb =StringBuilderCache.Acquire(format.Length + args.Length * 8);
            int pos = 0;
            int len = format.Length;
            char ch = '\x0';
            int width = 0;
            while (pos < len)
            {
                ch = format[pos];
                pos++;
                if (ch == '{')
                {
                    if (format[pos] == '{')
                    {
                        pos++;
                        sb.Append('{');
                    }
                    else
                    {
                        ch = format[pos];
                        pos++;
                        width = 0;
                        while (!(ch < '0' || ch > '9'))
                        {
                            width = width * 10 + ch - '0';
                            ch = format[pos];
                            pos++;
                        }

                        pos--;
                    }
                }
                else if (ch == '}')
                {
                    if (format[pos] == '}')
                    {
                        pos++;
                        sb.Append('}');
                    }
                    else
                    {
                        sb.Append(args[width].value);
                    }
                }
                else
                {
                    sb.Append(ch);
                }
            }
            return StringBuilderCache.GetStringAndRelease(sb);
        }
        /// <summary>
        /// 解析只以-和/符号分隔控制器名，行为名，参数的Url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="nvc"></param>
        /// <param name="urlParameterNames"></param>
        /// <param name="actionKeyWithoutSubDomainAndMethodLength"></param>
        /// <param name="extensionLength"></param>
        public static void SimpleParse(string url, NameValueCollection nvc,string[] urlParameterNames, int actionKeyWithoutSubDomainAndMethodLength,int extensionLength)
        {
            int pos = 0;
            int len = url.Length;
            char ch = '\x0';

            int left = 0;
            int right = 0;
            int index = 0;

            //int colonCount = 0;
            //while (pos < len)
            //{
            //    if (url[pos] == ':')
            //    {
            //        colonCount++;
            //        if (colonCount == 2)
            //        {
            //            break;
            //        }
            //    }
            //    pos++;
            //}
            pos += actionKeyWithoutSubDomainAndMethodLength-1;
            
            left = pos;
            right = pos;
            pos++;
            while (pos < len)
            {
                ch = url[pos];
                if (isSeparatorChar(ch))
                {
                    left = right+1;
                    right = pos;
                    nvc.Add(urlParameterNames[index], url.Substring(left, right-left));
                    index++;
                }
                pos++;
            }

            left = right+1;
            //这里要减去两个数字定位符
            right = url.Length-left-extensionLength-2;
            nvc.Add(urlParameterNames[index], url.Substring(left, right));
        }
        /// <summary>
        /// 利用正则解析Url中的参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="nvc"></param>
        /// <param name="urlParameterNames"></param>
        /// <param name="parameterRegex"></param>
        public static void RegexParse(string url,NameValueCollection nvc,string[] urlParameterNames,string parameterRegex)
        {
            var Regex = new Regex(parameterRegex);
            Match mat = Regex.Match(url);
            if (mat.Success)
            {
                for (int i = 0; i < urlParameterNames.Length; i++)
                {
                    nvc.Add(urlParameterNames[i], mat.Groups[i + 1].Value);
                }
            }
        }
        /// <summary>
        ///  生成包含生成Url函数的javascript文件，用于前端生成Url时使用。
        /// </summary>
        /// <param name="globalConfig"></param>
        public static void GetUrlRouteJsContent(NFinal.Config.Global.GlobalConfig globalConfig)
        {
            UrlRouteJsModel model = new UrlRouteJsModel();
            model.formatControllerDictionary = formatControllerDictionary;
            string directory = NFinal.IO.Path.MapPath(globalConfig.projectType,"/Scripts");
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
            NFinal.IO.FileWriter fileWriter = new IO.FileWriter(System.IO.Path.Combine(directory, "Url.js"));
            NFinal.Url.UrlRouteJs.Render(fileWriter, model);
            fileWriter.Dispose();
        }
        /// <summary>
        ///  生成调试html,用浏览器打开这些html，可以直接调试其目录名对应的控制器行为所对应的函数
        /// </summary>
        /// <param name="globalConfig"></param>
        public static void GenerateActionDebugHtml(NFinal.Config.Global.GlobalConfig globalConfig)
        {
            NFinal.IO.FileWriter fileWriter;
            DebugData debugData;
            string fileName;
            string relativePath;
            string absolutePath;
            if (globalConfig.debug.directory == null)
            {
                globalConfig.debug.directory = "Debug";
            }
            foreach (var controller in formatControllerDictionary)
            {
                relativePath = "/" + globalConfig.debug.directory.TrimStart('/')+"/"+ controller.Key.Namespace.Replace('.','/') + "/" + ActionUrlHelper.GetControllerName(controller.Key);
                absolutePath = NFinal.IO.Path.GetProjectPath(relativePath);

                if (!System.IO.Directory.Exists(absolutePath))
                {
                    System.IO.Directory.CreateDirectory(absolutePath);
                }
                foreach (var method in controller.Value)
                {
                    fileName = System.IO.Path.Combine(absolutePath, method.Key + ".html");
                    if (!System.IO.File.Exists(fileName))
                    {
                        debugData = new NFinal.Url.DebugData();
                        debugData.classFullName = controller.Key.FullName;
                        debugData.methodName = method.Key;
                        debugData.formatData = method.Value;
                        debugData.debugUrl = globalConfig.debug.url;
                        using (fileWriter = new IO.FileWriter(fileName))
                        {
                            NFinal.Url.Debug.Render(fileWriter, debugData);
                        }
                    }

                }
            }
        }
        /// <summary>
        /// 获取控制器名称
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static string GetControllerName(Type controller)
        {
            int SuffixLength = "Controller".Length;
            int NameLength = controller.Name.Length;
            if (NameLength > SuffixLength)
            {
                string suffix = controller.Name.Substring(controller.Name.Length - SuffixLength, SuffixLength);
                if (suffix.Equals("Controller", StringComparison.OrdinalIgnoreCase))
                {
                    return controller.Name.Substring(0, controller.Name.Length - SuffixLength);
                }
            }
            throw new Exceptions.InvalidControllerNameException(controller.Namespace, controller.Name);
        }
    }
#if EMITDEBUG
    /// <summary>
    /// 输出URL专用
    /// </summary>
    public class UrlBuildData
    {
        /// <summary>
        /// 请求方法名
        /// </summary>
        public string methodName;
        /// <summary>
        /// 生成Url所需的格式化字符串
        /// </summary>
        public string formatUrl;
    }

    public class UrlParseData
    {
        public string parameterRegex;
        public string actionKey;
        public bool isSimpleUrl;
        public string actionUrl;
        public bool hasNoParamsInUrl;
        public string[] urlParameterNames;
    }
    public class IndexController
    {
        //public NFinal.Collections.FastDictionary<string> UrlRouteData;
        public void Init()
        {
            
        }
    }
    public class UrlRun
    {
        //public static string Run(string methodName, params StringContainer[] urlParameters)
        //{
        //    NFinal.Collections.FastDictionary<string> formatDictionary=null;
        //    return ActionUrlHelper.Format(formatDictionary[methodName], urlParameters);
        //}
    }
#endif
}
