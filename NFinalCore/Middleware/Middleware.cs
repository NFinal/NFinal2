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
    public delegate Task InvokeDelegate<TContext>(TContext context);
    public interface IMiddleware<TContext,TRequest>
    {
        Task Invoke(TContext context);
    }
    /// <summary>
    /// 泛型中间件
    /// </summary>
    /// <typeparam name="TContext">Context或Environment等</typeparam>
    /// <typeparam name="TRequest">Request或IOwinContext等</typeparam>
    public abstract class Middleware<TContext,TRequest>:IMiddleware<TContext,TRequest>
    {
        public readonly InvokeDelegate<TContext> _next;
        public static NFinal.Collections.FastDictionary<ActionData<TContext,TRequest>> actionFastDic=null;
        public static Task<int> FinishedTask = FromResult(0);
        private static string rootDir = null;
        private static bool debug;
        public static string defaultUrl = null;
        public static string defaultSubDomain=null;
        public static CustomErrors customErrors = null;
        public static UrlRouteRule urlRouteRule;
        public Middleware(InvokeDelegate<TContext> next, MiddlewareConfigOptions options)
        {
            customErrors = options.customErrors;
            defaultSubDomain = options.defaultSubDomain;
            defaultUrl ="/"+options.defaultDocument;
            urlRouteRule = options.urlRouteRule;
            //初始化Action
            if (!ActionHelper.isInit)
            {
                ActionHelper.Init<TContext, TRequest>(options);
            }
            //初始化View
            if (!ViewHelper.isInit)
            {
                ViewHelper.Init(options);
            }
            ////初始化Config
            //if (!Config.Configration.isInit)
            //{
            //    Config.Configration.Init(options);
            //}
            debug = options.debug;
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
        public abstract TRequest GetRequest(TContext context);
        public abstract NameValueCollection GetParameters(TRequest request);
        public abstract string GetRequestMethod(TContext context);
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
            if (defaultSubDomain != null)
            {
                if (debug)
                {
                    actionKey = NFinal.Url.ActionKey.GetActionKey(defaultSubDomain, GetRequestMethod(context), requestPath,out shortActionKeyLength, urlRouteRule);
                }
                else
                {
                    actionKey = NFinal.Url.ActionKey.GetActionKey(GetSubDomain(context), GetRequestMethod(context), requestPath,out shortActionKeyLength, urlRouteRule);
                }
            }
            else
            {
                if (debug)
                {
                    actionKey = NFinal.Url.ActionKey.GetActionKey(null, GetRequestMethod(context), requestPath,out shortActionKeyLength, urlRouteRule);
                }
                else
                {
                    actionKey = NFinal.Url.ActionKey.GetActionKey(null, GetRequestMethod(context), requestPath,out shortActionKeyLength, urlRouteRule);
                }
            }
            if (rootDir == null)
            {
                rootDir = NFinal.Utility.rootPath;
            }
            bool hasError = false;
            NFinal.Middleware.ActionData<TContext,TRequest> actionData;
            if (actionFastDic.TryGetValue(actionKey, out actionData))
            {
                //if (actionData.method != null)
                //{
                //    if (actionData.method != GetRequestMethod(context))
                //    {
                //        return this._next(context);
                //    }
                //}
                if (actionData.IBaseFilters != null)
                {
                    foreach (var iBaseFilter in actionData.IBaseFilters)
                    {
                        if (!iBaseFilter.BaseFilter(context))
                        {
                            return FinishedTask;
                        }
                    }
                }

                TRequest request = default(TRequest);
                try
                {
                    request = GetRequest(context);
                }
                catch
                {
                    hasError = true;
                    if (customErrors.mode == Mode.Off)
                    {
                        using (IAction<TContext, TRequest> controller = GetAction(context))
                        {
                            controller.Initialization(context,actionData.methodName, null, request, CompressMode.GZip);
                            controller.SetResponseHeader("Content-Type", "text/html; charset=utf-8");
                            //解析出错
                            controller.SetResponseStatusCode(200);
                            controller.Close();
                        }
                    }
                }
                if (actionData.IRequestFilters != null)
                {
                    foreach (var iRequestFilter in actionData.IRequestFilters)
                    {
                        if (!iRequestFilter.RequestFilter(request))
                        {
                            return FinishedTask;
                        }
                    }
                }
                NameValueCollection parameters = GetParameters(request);
                //获取Url中的参数
                if (urlRouteRule == UrlRouteRule.AreaControllerCustomActionUrl)
                {
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
                }
                //生产环境下
                if (!debug)
                {
                    try
                    {
                        actionData.actionExecute(context, actionData, request, parameters);
                    }
                    catch(Exception)
                    {
                        hasError = true;
                        if (customErrors.mode == Mode.Off)
                        {
                            using (IAction<TContext, TRequest> controller = GetAction(context))
                            {
                                controller.Initialization(context,null, null, request, CompressMode.GZip);
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
                            controller.Initialization(context,null, null, request, CompressMode.GZip);
                            controller.Redirect(customErrors.defaultRedirect);
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
        public static Task<TResult> FromResult<TResult>(TResult resultValue)
        {
            var completionSource = new TaskCompletionSource<TResult>();
            completionSource.SetResult(resultValue);
            return completionSource.Task;
        }
    }
}
