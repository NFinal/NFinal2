//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : Middleware.cs
//        Description :Http中间件泛型类
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
using System.IO;
using System.Threading.Tasks;
using NFinal.Action;
using NFinal.Http;

namespace NFinal.Middleware
{
    /// <summary>
    /// Http执行代理
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <param name="context"></param>
    /// <returns></returns>
    public delegate Task InvokeDelegate<TContext>(TContext context);
    /// <summary>
    /// Http中间件泛型接口
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TRequest"></typeparam>
    public interface IMiddleware<TContext,TRequest>
    {
        /// <summary>
        /// 处理Http上下文
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task Invoke(TContext context);
    }
    /// <summary>
    /// Http中间件泛型类
    /// </summary>
    /// <typeparam name="TContext">Context或Environment等</typeparam>
    /// <typeparam name="TRequest">Request或IOwinContext等</typeparam>
    public abstract class Middleware<TContext,TRequest>:IMiddleware<TContext,TRequest>
    {
        /// <summary>
        /// 管道中下一个Http上下文处理函数
        /// </summary>
        public readonly InvokeDelegate<TContext> _next;
        /// <summary>
        /// 控制器行为字典
        /// </summary>
        public static NFinal.Collections.FastSearch.FastSearch<ActionData<TContext,TRequest>> actionFastDic=null;
        /// <summary>
        /// Http响应完成
        /// </summary>
        public static Task<int> FinishedTask = FromResult(0);
        /// <summary>
        /// 是否调试
        /// </summary>
        private static bool debug;
        /// <summary>
        /// 默认URL
        /// </summary>
        public static string defaultUrl = null;
        /// <summary>
        /// 中间件初始化
        /// </summary>
        /// <param name="next">Http上下文处理函数</param>
        public Middleware(InvokeDelegate<TContext> next)
        {
            ////初始化插件
            if (!NFinal.Plugs.PlugManager.isInit)
            {
                NFinal.Plugs.PlugManager.Init();
            }
            defaultUrl = NFinal.Config.Configration.globalConfig.server.indexDocument;
            debug = NFinal.Config.Configration.globalConfig.debug.enable;
            //初始化Action
            if (!ActionHelper.isInit)
            {
                ActionHelper.Init<TContext, TRequest>(NFinal.Config.Configration.globalConfig);
            }
            //初始化View
            if (!ViewHelper.isInit)
            {
                ViewHelper.Init(NFinal.Config.Configration.globalConfig);
            }
            this._next = next;
        }
        /// <summary>
        /// 获取请求Url
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract string GetRequestPath(TContext context);
        /// <summary>
        /// 获取默认Controller
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract IAction<TContext,TRequest> GetAction(TContext context);
        /// <summary>
        /// 获取请求信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract TRequest GetRequest(TContext context);
        /// <summary>
        /// 获取请求参数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract NameValueCollection GetParameters(TRequest request);
        /// <summary>
        /// 获取请求方法
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract string GetRequestMethod(TContext context);
        /// <summary>
        /// 获取二级域名
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract string GetSubDomain(TContext context);
        /// <summary>
        /// 主入口
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual Task Invoke(TContext context)
        {
            string requestPath = GetRequestPath(context);
            //路径为"/"时变为类"/Index.html"
            if (requestPath.Length==1)
            {
                requestPath = defaultUrl;
            }
            string actionKey;
            int shortActionKeyLength;
            //获取actionKey
            if (debug)
            {
                actionKey = NFinal.Url.ActionKey.GetActionKey(GetRequestMethod(context), requestPath,out shortActionKeyLength);
            }
            else
            {
                actionKey = NFinal.Url.ActionKey.GetActionKey(GetRequestMethod(context), requestPath,out shortActionKeyLength);
            }
            bool hasError = false;
            NFinal.Action.ActionData<TContext,TRequest> actionData;
            if (actionFastDic.TryGetValue(actionKey,actionKey.Length, out actionData))
            {

                TRequest request = default(TRequest);
                try
                {
                    request = GetRequest(context);
                }
                catch
                {
                    hasError = true;
                    if (actionData.plugConfig.customErrors.mode ==Config.Plug.CustomErrorsMode.Off)
                    {
                        using (IAction<TContext, TRequest> controller = GetAction(context))
                        {
                            controller.Initialization(context,actionData.methodName, null, request, CompressMode.GZip, actionData.plugConfig);
                            controller.SetResponseHeader("Content-Type", "text/html; charset=utf-8");
                            //解析出错
                            controller.SetResponseStatusCode(200);
                            controller.Close();
                        }
                    }
                }
                NameValueCollection parameters = GetParameters(request);
                //获取Url中的参数
                if (actionData.actionUrlData != null && !actionData.actionUrlData.hasNoParamsInUrl)
                {
                    if (actionData.actionUrlData.isSimpleUrl)
                    {
                        NFinal.Url.ActionUrlHelper.SimpleParse(requestPath, parameters, actionData.actionUrlData.actionUrlNames, shortActionKeyLength, actionData.actionUrlData.extensionLength);
                    }
                    else
                    {
                        NFinal.Url.ActionUrlHelper.RegexParse(requestPath, parameters, actionData.actionUrlData.actionUrlNames,actionData.actionUrlData.parameterRegex);
                    }
                }
                //生产环境下
                if (!debug)
                {
                    if (!NFinal.Filter.FilterHelper.ParamaterFilter(actionData.IParametersFilters, parameters))
                    {
                        return FinishedTask;
                    }
                    try
                    {
                        actionData.actionExecute(context, actionData, request, parameters);
                    }
                    catch(Exception)
                    {
                        hasError = true;
                        if (actionData.plugConfig.customErrors.mode == Config.Plug.CustomErrorsMode.Off)
                        {
                            using (IAction<TContext, TRequest> controller = GetAction(context))
                            {
                                controller.Initialization(context,null, null, request, CompressMode.GZip, actionData.plugConfig);
                                controller.SetResponseHeader("Content-Type", "text/html; charset=utf-8");
                                //服务器错误
                                controller.SetResponseStatusCode(500);
                                controller.Close();
                            }
                        }
                    }
                    if (hasError)
                    {
                        using (IAction<TContext, TRequest> controller = GetAction(context))
                        {
                            controller.Initialization(context,null, null, request, CompressMode.GZip,actionData.plugConfig);
                            controller.Redirect(actionData.plugConfig.customErrors.defaultRedirect);
                        }
                    }
                }
                //测试环境下
                else
                {
                    actionData.actionExecute(context, actionData, request, parameters);
                    try
                    {
                        //actionData.actionExecute(context, actionData,request,parameters);
                    }
                    catch (System.Exception e)
                    {
                        using (IAction<TContext, TRequest> controller = GetAction(context))
                        {
                            controller.SetResponseHeader("Content-Type", "text/html; charset=utf-8");
                            controller.SetResponseStatusCode(200);
                            controller.Write("错误消息：<br/>");
                            controller.Write(e.Message);
                            controller.Write("<br/>");
                            controller.Write("请求时发生错误：<br>");
                            controller.Write(GetRequestPath(context));
                            controller.Write("<br/>");
                            controller.Write("错误跟踪：</br>");
                            string[] stackTraces = e.StackTrace.Split('\n');
                            for (int i = 0; i < stackTraces.Length; i++)
                            {
                                controller.Write(stackTraces[i]);
                                controller.Write("</br>");
                            }
                            controller.Close();
                        }
                    }
                }
                return FinishedTask;
            }
            else
            {
                return _next.Invoke(context);
            }
        }
        #region 参数解析
        /// <summary>
        /// Http参数解析函数
        /// </summary>
        /// <param name="requestMethod"></param>
        /// <param name="contentTypeString"></param>
        /// <param name="requestQueryString"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public NameValueCollection GetParameters(string requestMethod, string contentTypeString, string requestQueryString, Stream requestBody)
        {
            //请求参数
            var parameters = new NFinal.NameValueCollection();
            MethodType methodType = MethodType.NONE;
            //获取POST方法
            switch (requestMethod)
            {
                case NFinal.Constant.MethodTypePOST: methodType = MethodType.POST; break;
                case NFinal.Constant.MethodTypeGET: methodType = MethodType.GET; break;
                case NFinal.Constant.MethodTypeDELETE: methodType = MethodType.DELETE; break;
                case NFinal.Constant.MethodTypePUT: methodType = MethodType.PUT; break;
                case NFinal.Constant.MethodTypeAJAX: methodType = MethodType.AJAX; break;
                default: methodType = MethodType.NONE; break;
            }
            //提取内容类型
            if (methodType == MethodType.POST)
            {
                ContentType contentType = ContentType.NONE;
                switch (contentTypeString.Split(NFinal.Constant.CharSemicolon)[0])
                {
                    case NFinal.Constant.ContentType_Multipart_form_data: contentType = ContentType.Multipart_form_data; break;
                    case NFinal.Constant.ContentType_Text_json:
                    case NFinal.Constant.ContentType_Application_json: contentType = ContentType.Application_json; break;
                    case NFinal.Constant.ContentType_Application_x_www_form_urlencoded: contentType = ContentType.Application_x_www_form_urlencoded; break;
                    case NFinal.Constant.ContentType_Application_xml:
                    case NFinal.Constant.ContentType_Text_xml: contentType = ContentType.Text_xml; break;
                    default: contentType = ContentType.NONE; break;
                }
                //提取form参数
                if (requestBody != System.IO.Stream.Null)
                {
                    if (contentType == ContentType.Application_x_www_form_urlencoded)
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(requestBody);
                        string body = sr.ReadToEnd();
                        sr.Dispose();
                        if (!string.IsNullOrEmpty(body))
                        {
                            string[] formArray = body.Split(NFinal.Constant.CharAnd, NFinal.Constant.CharEqual);
                            if (formArray.Length > 1 && (formArray.Length & 1) == 0)
                            {
                                for (int i = 0; i < formArray.Length; i += 2)
                                {
                                    parameters.Add(formArray[i], formArray[i + 1].UrlDecode());
                                }
                            }
                        }
                    }
                    //else if (contentType == NFinal.ContentType.Text_xml)
                    //{
                    //    System.IO.StreamReader sr = new System.IO.StreamReader(stream);
                    //    string body = sr.ReadToEnd();
                    //    sr.Dispose();
                    //    if (!string.IsNullOrEmpty(body))
                    //    {
                    //        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    //        doc.LoadXml(body);
                    //        if (doc.DocumentElement != null)
                    //        {
                    //            foreach (System.Xml.XmlElement xmlNode in doc.DocumentElement.ChildNodes)
                    //            {
                    //                get.Add(xmlNode.Name, xmlNode.Value);
                    //            }
                    //        }
                    //    }
                    //}
                    else if (contentType == ContentType.Application_json)
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(requestBody);
                        string body = sr.ReadToEnd();
                        sr.Dispose();
                        NFinal.Json.IJsonSerialize serializable = new NFinal.Json.NewtonsoftJsonSerialize();
                        IDictionary<string, object> data = serializable.DeserializeObject<IDictionary<string, object>>(body);
                        foreach (var ele in data)
                        {
                            parameters.Add(ele.Key, ele.Value.ToString());
                        }
                    }
                    //else if (contentType == ContentType.Multipart_form_data)
                    //{
                    //    //multipart/form-data
                    //    string boundary = NFinal.Http.HttpMultipart.HttpMultipart.boundaryReg.Match(contentTypeString).Value;
                    //    var multipart = new NFinal.Http.HttpMultipart.HttpMultipart(requestBody, boundary);
                    //    files = new NFinal.Collections.FastDictionary<string, NFinal.Http.HttpMultipart.HttpFile>(StringComparer.Ordinal);
                    //    foreach (var httpMultipartBoundary in multipart.GetBoundaries())
                    //    {
                    //        if (string.IsNullOrEmpty(httpMultipartBoundary.Filename))
                    //        {
                    //            string name = httpMultipartBoundary.Name;
                    //            if (!string.IsNullOrEmpty(name))
                    //            {
                    //                string value = new System.IO.StreamReader(httpMultipartBoundary.Value).ReadToEnd();
                    //                parameters.Add(name, value);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            files.Add(httpMultipartBoundary.Name, new NFinal.Http.HttpMultipart.HttpFile(httpMultipartBoundary));
                    //        }
                    //    }
                    //}
                    //提取URL?后的参数
                    if (requestQueryString.Length > 0)
                    {
                        string[] queryArray = requestQueryString.Split(NFinal.Constant.CharAnd, NFinal.Constant.CharEqual);
                        if (queryArray.Length > 1 && (queryArray.Length & 1) == 0)
                        {
                            for (int i = 0; i < queryArray.Length; i += 2)
                            {
                                parameters.Add(queryArray[i], queryArray[i + 1].UrlDecode());
                            }
                        }
                    }
                    
                }
            }
            return parameters;
        }
#endregion
        /// <summary>
        /// 返回一个已经完成的任务
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="resultValue"></param>
        /// <returns></returns>
        public static Task<TResult> FromResult<TResult>(TResult resultValue)
        {
            var completionSource = new TaskCompletionSource<TResult>();
            completionSource.SetResult(resultValue);
            return completionSource.Task;
        }
    }
}
