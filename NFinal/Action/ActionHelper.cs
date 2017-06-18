//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename :ActionHelper.cs
//        Description :控制器行为帮助类，用于分析及执行控制器行为
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using NFinal.Http;

/// <summary>
/// 控制器Action执行代理
/// </summary>
/// <typeparam name="TContext">Http上下文类型</typeparam>
/// <typeparam name="TResquest">Http请求信息类型</typeparam>
/// <param name="context">Http上下文</param>
/// <param name="actionData">控制器执行所需信息</param>
/// <param name="request">Http请求信息</param>
/// <param name="parameters">Http请求信息，KeyValue集合类型</param>
public delegate void ActionExecute<TContext, TResquest>(TContext context,NFinal.Action.ActionData<TContext,TResquest> actionData,TResquest request,NFinal.NameValueCollection parameters);
namespace NFinal.Action
{
    /// <summary>
    /// 控制器中所有行为的URL格式化信息
    /// </summary>
    public class ControllerData
    {
        /// <summary>
        /// URl格式化信息
        /// </summary>
        public Dictionary<RuntimeTypeHandle, Dictionary<string,string>> formatList;
    }
    /// <summary>
    /// 控制器行为的相关信息
    /// </summary>
    /// <typeparam name="TContext">Http上下文类型</typeparam>
    /// <typeparam name="TRequest">Http请求类型</typeparam>
    public class ActionData<TContext,TRequest>
    {
        /// <summary>
        /// 请求URL
        /// </summary>
        public string actionUrl;
        /// <summary>
        /// 视图数据类型
        /// </summary>
        public RuntimeTypeHandle viewBagType;
        /// <summary>
        /// 控制器行为执行代理
        /// </summary>
        public ActionExecute<TContext,TRequest> actionExecute;
        /// <summary>
        /// 插件配置信息
        /// </summary>
        public NFinal.Config.Plug.PlugConfig plugConfig;
        /// <summary>
        /// 当前行为对应方法的特性提供者
        /// </summary>
        public System.Reflection.ICustomAttributeProvider methodProvider;
        /// <summary>
        /// 用户权限验证接口数组
        /// </summary>
        public NFinal.Filter.IAuthorizationFilter[] IAuthorizationFilters;
        /// <summary>
        /// 参数验证接口数组
        /// </summary>
        public NFinal.Filter.IParameterFilter[] IParametersFilters;
        /// <summary>
        /// 在控制器行为之前过滤接口数组
        /// </summary>
        public NFinal.Filter.IBeforeActionFilter[] IBeforeActionFilters;
        /// <summary>
        /// 在控制器行为之后过滤接口数组
        /// </summary>
        public NFinal.Filter.IAfterActionFilter[] IAfterActionFilters;
        /// <summary>
        /// 控制器行为响 应信息过滤器对象数组
        /// </summary>
        public NFinal.Filter.IResponseFilter[] IResponseFilters;
        /// <summary>
        /// 控制器行为对应方法的URL特性中的Url字符串
        /// </summary>
        public string urlString;
        /// <summary>
        /// 控制器对应的类名
        /// </summary>
        public string className;
        /// <summary>
        /// 控制器行为对应的方法名
        /// </summary>
        public string methodName;
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string controllerName;
        /// <summary>
        /// 控制器行为名称
        /// </summary>
        public string actionName;
        /// <summary>
        /// 控制器区域名称
        /// </summary>
        public string areaName;
        /// <summary>
        /// 可接受的请求方法数组
        /// </summary>
        public string[] method;
        /// <summary>
        /// Http响应类型
        /// </summary>
        public string contentType;
        /// <summary>
        /// Http压缩模式
        /// </summary>
        public CompressMode compressMode;
        /// <summary>
        /// 控制器行为对应的请求URl相关信息
        /// </summary>
        public NFinal.Url.ActionUrlData actionUrlData;
    }
    /// <summary>
    /// 控制器行为帮助类，用于分析及执行控制器行为
    /// </summary>
    public class ActionHelper
    {
        //public static Dictionary<string, ActionExecute> actionDic = new Dictionary<string, ActionExecute>();
        //public static NFinal.Collections.FastDictionary<ActionData> actionFastDic;
        /// <summary>
        /// 控制器行为是否初始化
        /// </summary>
        public static bool isInit = false;
        /// <summary>
        /// 控制器行为初始化函数
        /// </summary>
        /// <typeparam name="TContext">Htpp上下文类型</typeparam>
        /// <typeparam name="TRequest">Http请求信息类型</typeparam>
        /// <param name="globalConfig">全局配置数据</param>
        public static void Init<TContext, TRequest>(NFinal.Config.Global.GlobalConfig globalConfig)
        {
            Module[] modules = null;
            Type[] types = null;

            NFinal.Collections.FastDictionary<string, ActionData<TContext, TRequest>> actionDataDictionary = new NFinal.Collections.FastDictionary<string, ActionData<TContext, TRequest>>();
            //List<KeyValuePair<string, ActionData<TContext, TRequest>>> actionDataList = new List<KeyValuePair<string, ActionData<TContext, TRequest>>>();
            NFinal.Collections.FastDictionary<RuntimeTypeHandle, Dictionary<string, NFinal.Url.FormatData>> formatControllerDictionary = new NFinal.Collections.FastDictionary<RuntimeTypeHandle, Dictionary<string, NFinal.Url.FormatData>>();
            Type controller = null;
            for (int i = 0; i < NFinal.Plugs.PlugManager.plugInfoList.Count; i++)
            {
                /////////////////////////////////////////////////////////////////////
                //
                //Assembly.Load
                //精确加载
                //
                //
                //Assembly.LoadFrom
                //加载dll的引入
                //
                //
                //Assembly.LoadFile
                //仅加载自己
                //
                /////////////////////////////////////////////////////////////////////
                NFinal.Plugs.PlugInfo plugInfo = NFinal.Plugs.PlugManager.plugInfoList[i];
                if (!plugInfo.loadSuccess)
                {
                    continue;
                }
                Assembly assembly = plugInfo.assembly;

                modules = assembly.GetModules();
                for (int j = 0; j < modules.Length; j++)
                {
                    types = modules[j].GetTypes();

                    for (int k = 0; k < types.Length; k++)
                    {
                        controller = types[k];
                        //控制器必须以Controller结尾
                        if (!controller.FullName.EndsWith("Controller", StringComparison.Ordinal))
                        {
                            continue;
                        }
                        //该类型继承自IAction并且其泛不是dynamic类型
#if (NET40 || NET451 || NET461)
                        if (typeof(NFinal.Action.IAction<TContext, TRequest>).IsAssignableFrom(controller))
                        {
                            if (!controller.IsGenericType)
#endif
#if NETCORE
                        if (typeof(NFinal.Action.IAction<TContext, TRequest>).IsAssignableFrom(controller))
                        {
                            if (!controller.GetTypeInfo().IsGenericType)
#endif
                            {
                                Dictionary<string, NFinal.Url.FormatData> formatMethodDic = new Dictionary<string, NFinal.Url.FormatData>();
                                AddActionData(actionDataDictionary, formatMethodDic, assembly, controller, globalConfig, plugInfo);
                                formatControllerDictionary.Add(controller.TypeHandle, formatMethodDic);
                            }
                        }

                    }
                }
            }
            //如果未找到任何一个控制器类型，则抛出异常
            if (formatControllerDictionary.Count < 1)
            {
                throw new NFinal.Exceptions.HasNoControllerInProjectException();
            }
            NFinal.Url.ActionUrlHelper.formatControllerDictionary = formatControllerDictionary;
            if (globalConfig.debug.enable)
            {
                NFinal.Url.ActionUrlHelper.GetUrlRouteJsContent(globalConfig);
                NFinal.Url.ActionUrlHelper.GenerateActionDebugHtml(globalConfig);
            }

            //}
            //添加图标响应
            //Icon.Favicon.Init(actionDataList);
            Middleware.Middleware<TContext, TRequest>.actionFastDic = new Collections.FastSearch.FastSearch<ActionData<TContext, TRequest>>(actionDataDictionary);
            actionDataDictionary.Clear();
        }
        /// <summary>
        /// 向全局添加控制器行为信息
        /// </summary>
        /// <typeparam name="TContext">Http请求上下文类型</typeparam>
        /// <typeparam name="TRequest">Http请求类型</typeparam>
        /// <param name="actionDataDictionary">控制器行为数据信息字典</param>
        /// <param name="formatMethodDictionary">控制器行为URl格式化字典</param>
        /// <param name="assembly">当前程序集</param>
        /// <param name="controller">控制器类型信息</param>
        /// <param name="globalConfig">全局配置信息</param>
        /// <param name="plugInfo">插件信息</param>
        public static void AddActionData<TContext, TRequest>(NFinal.Collections.FastDictionary<string, ActionData<TContext, TRequest>> actionDataDictionary,
            Dictionary<string, NFinal.Url.FormatData> formatMethodDictionary,
            Assembly assembly, Type controller,
            NFinal.Config.Global.GlobalConfig globalConfig,
            NFinal.Plugs.PlugInfo plugInfo)
        {
            Type viewBagType = null;
            viewBagType = controller.GetField("ViewBag").FieldType;
            if (viewBagType == typeof(object))
            {
                MethodInfo[] actions = null;
                string controllerName;
                string areaName;

                ActionData<TContext, TRequest> actionData;
                GetControllerUrl(out controllerName, out areaName, controller, globalConfig);
                actions = controller.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
#if NETCORE
                //查找函数
                var controllerAttributeList = controller.GetTypeInfo().GetCustomAttributes(typeof(NFinal.ActionExportAttribute), true);
#else
                var controllerAttributeList = controller.GetCustomAttributes(typeof(NFinal.ActionExportAttribute), true);
#endif
                MethodInfo action = null;
                bool hasAction = false;
                ActionExportAttribute actionExportAttribute = null;
                foreach (var controllerAttribute in controllerAttributeList)
                {
                    actionExportAttribute = (NFinal.ActionExportAttribute)controllerAttribute;
                    if (actionExportAttribute.types == null)
                    {
                        action = controller.GetMethod(actionExportAttribute.methodName);
                    }
                    else
                    {
                        action = controller.GetMethod(actionExportAttribute.methodName, actionExportAttribute.types);
                    }
                    if (action == null)
                    {
                        throw new NFinal.Exceptions.NotFoundExportActionException(controller.FullName, actionExportAttribute.methodName);
                    }
                    hasAction = false;
                    foreach (var actionTmp in actions)
                    {
                        if (actionTmp == action)
                        {
                            hasAction = true;
                        }
                    }
                    if (!hasAction)
                    {
                        actionData = new ActionData<TContext, TRequest>();
                        actionData.viewBagType = actionExportAttribute.viewBagType.TypeHandle;
                        AddActionDataForMethod(actionDataDictionary, formatMethodDictionary, 
                            assembly, controller, 
                            actionData, action, 
                            controllerName, areaName, 
                            globalConfig, plugInfo);
                    }
                }

                //MethodInfo actionExportList =
                for (int m = 0; m < actions.Length; m++)
                {
                    actionData = new ActionData<TContext, TRequest>();
                    string modelTypeName = controller.Namespace + "." + controller.Name + "_Model";
                    modelTypeName += "." + actions[m].Name;
                    viewBagType = assembly.GetType(modelTypeName);
                    if (viewBagType == null)
                    {
                        throw new NFinal.Exceptions.ModelNotFoundException(modelTypeName);
                    }
                    else
                    {
                        actionData.viewBagType = viewBagType.TypeHandle;
                    }
                    AddActionDataForMethod(actionDataDictionary, formatMethodDictionary,
                        assembly, controller,
                        actionData, actions[m],
                        controllerName, areaName,
                        globalConfig, plugInfo);
                }
            }
        }
        public static void AddActionDataForMethod<TContext, TRequest>(NFinal.Collections.FastDictionary<string, ActionData<TContext, TRequest>> actionDataDictionary,
            Dictionary<string, NFinal.Url.FormatData> formatMethodDictionary,
            Assembly assembly, Type controller,
            ActionData<TContext, TRequest> actionData,
            MethodInfo methodInfo,string controllerName,string areaName,
            NFinal.Config.Global.GlobalConfig globalConfig,
            NFinal.Plugs.PlugInfo plugInfo)
        {
            if (methodInfo.IsAbstract || methodInfo.IsVirtual || methodInfo.IsGenericMethod
                        || methodInfo.IsStatic || methodInfo.IsConstructor)
            {
                return;
            }
            string actionName;
            string actionUrl;
            string[] actionKeys;
            string[] method;
            UrlAttribute urlAttribute;
            NFinal.Url.ActionUrlData actionUrlData;
            actionKeys = GetActionKeys(controllerName, areaName, out actionUrl, out actionName, out method,
                out urlAttribute, out actionUrlData,
                methodInfo, globalConfig, plugInfo);
            
            if (urlAttribute == null)
            {
                actionData.urlString = null;
                actionData.contentType = "text/html; charset=utf-8";
                actionData.compressMode = CompressMode.None;
            }
            else
            {
                actionData.urlString = urlAttribute.urlString;
                actionData.contentType = urlAttribute.contentType;
                actionData.compressMode = urlAttribute.compressMode;
            }
            actionData.className = controller.FullName;
            actionData.methodName = methodInfo.Name;
            actionData.actionUrlData = actionUrlData;
            actionData.controllerName = controllerName;
            actionData.areaName = areaName;
            actionData.actionUrl = actionUrl;
            actionData.actionName = actionName;
            actionData.method = method;
            actionData.plugConfig = plugInfo.config;
            if (actionData.actionUrlData != null)
            {
                formatMethodDictionary.Add(methodInfo.Name, new NFinal.Url.FormatData(actionData.actionUrlData.formatUrl, actionData.actionUrlData.actionUrlNames));
            }
            else
            {
                formatMethodDictionary.Add(methodInfo.Name, new NFinal.Url.FormatData(actionData.actionUrl, null));
            }
            actionData.IAuthorizationFilters = GetFilters<NFinal.Filter.IAuthorizationFilter>(
                typeof(NFinal.Filter.IAuthorizationFilter), controller, methodInfo);
            actionData.IParametersFilters = GetFilters<NFinal.Filter.IParameterFilter>(
                typeof(NFinal.Filter.IParameterFilter), controller, methodInfo);
            actionData.IBeforeActionFilters = GetFilters<NFinal.Filter.IBeforeActionFilter>(
                typeof(NFinal.Filter.IBeforeActionFilter), controller, methodInfo
                );
            actionData.IAfterActionFilters = GetFilters<NFinal.Filter.IAfterActionFilter>(
                typeof(NFinal.Filter.IAfterActionFilter), controller, methodInfo
                );
            actionData.IResponseFilters = GetFilters<NFinal.Filter.IResponseFilter>(
                typeof(NFinal.Filter.IResponseFilter), controller, methodInfo);
            actionData.actionExecute = NFinal.Action.Actuator.GetRunActionDelegate<TContext, TRequest>(assembly, controller, methodInfo,actionData);
            foreach (var actionKey in actionKeys)
            {
                if (actionDataDictionary.ContainsKey(actionKey))
                {
                    var oldActionData = actionDataDictionary[actionKey];
                    throw new NFinal.Exceptions.DuplicateActionUrlException(oldActionData.className, oldActionData.methodName, actionData.className, actionData.methodName);
                }
                else
                {
                    actionDataDictionary.Add(actionKey, actionData);
                }
            }
        }
        /// <summary>
        /// 获取控制器行为的URL信息
        /// </summary>
        /// <param name="controllerName">控制器名称</param>
        /// <param name="areaName">控制器区域名称</param>
        /// <param name="controller">控制器类型信息</param>
        /// <param name="globalConfig">全局配置信息</param>
        public static void GetControllerUrl(out string controllerName, out string areaName, Type controller, NFinal.Config.Global.GlobalConfig globalConfig)
        {
            ControllerAttribute[] controllerAttributes = null;
            AreaAttribute[] areaAttributes = null;
            controllerAttributes = (ControllerAttribute[])
#if (NET40 || NET451 || NET461)
                controller
#endif
#if NETCORE
                controller.GetTypeInfo()
#endif                
                .GetCustomAttributes(typeof(ControllerAttribute), true);
            if (controllerAttributes.Length > 0)
            {
                if (!string.IsNullOrEmpty(controllerAttributes[0].Name))
                {
                    controllerName = controllerAttributes[0].Name;
                }
                else
                {
                    controllerName =NFinal.Url.ActionUrlHelper.GetControllerName(controller);
                }
            }
            else
            {
                controllerName = NFinal.Url.ActionUrlHelper.GetControllerName(controller);
            }
            areaAttributes = (AreaAttribute[])
#if (NET40 || NET451 || NET461)
                controller
#endif
#if NETCORE
                controller.GetTypeInfo()
#endif   
                .GetCustomAttributes(typeof(AreaAttribute), true);
            if (areaAttributes.Length > 0)
            {
                areaName = areaAttributes[0].Name;
            }
            else
            {
                areaName = null;
            }
        }
        /// <summary>
        /// 组装并返回控制器对应行为的查找关键字
        /// </summary>
        /// <param name="controllerName">控制器名称</param>
        /// <param name="areaName">区域名称</param>
        /// <param name="actionUrl">请求URL</param>
        /// <param name="actionName">控制器行为名称</param>
        /// <param name="method">可接受的请求方法数组</param>
        /// <param name="urlAttribute"></param>
        /// <param name="actionUrlData"></param>
        /// <param name="methodInfo"></param>
        /// <param name="globalConfig"></param>
        /// <param name="plugInfo"></param>
        /// <returns></returns>
        public static string[] GetActionKeys(string controllerName, string areaName,
            out string actionUrl, out string actionName, out string[] method,
            out UrlAttribute urlAttribute,out NFinal.Url.ActionUrlData actionUrlData,
            MethodInfo methodInfo, NFinal.Config.Global.GlobalConfig globalConfig,
            NFinal.Plugs.PlugInfo plugInfo)
        {
            string urlPrefix = null;
            if (plugInfo.urlPrefix?.Length > 0)
            {
                urlPrefix = plugInfo.urlPrefix;
            }
            List<string> actionKeys = new List<string>();
            actionUrlData = null;
            method = null;
            urlAttribute = null;
            actionUrl = null;
            actionName = null;

            if (!string.IsNullOrEmpty(areaName))
            {
                actionUrl += "/" + areaName;
            }
            if (!string.IsNullOrEmpty(controllerName))
            {
                actionUrl += "/" + controllerName;
            }

            bool hasUrlAttribute = false;
            var attributes = methodInfo.GetCustomAttributes(true);
            foreach (var attr in attributes)
            {
                if (attr
#if (NET40 || NET451 || NET461)
                    .GetType()
#endif
#if NETCORE
                    .GetType().GetTypeInfo()
#endif
                    .IsSubclassOf(typeof(UrlAttribute)))
                {
                    hasUrlAttribute = true;
                    urlAttribute = (UrlAttribute)attr;
                    actionUrlData = NFinal.Url.ActionUrlHelper.GetActionUrlData(urlAttribute.urlString);
                    actionUrl = actionUrlData.actionKey;
                    break;
                } 
            }
            if (!hasUrlAttribute)
            {
                actionUrl += "/" + methodInfo.Name + plugInfo.config.url.extension;
            }
            if (method == null)
            {
                method = plugInfo.config.verbs;
                foreach (var m in method)
                {
                    actionKeys.Add( m + ":" + actionUrl);
                }
            }
            else
            {
                actionKeys.Add(method + ":" + actionUrl);
            }
            
            return actionKeys.ToArray();
            //UrlAttribute[] urlAttributes = (UrlAttribute[])methodInfo.GetCustomAttributes(typeof(UrlAttribute), true);
            //if (actionAttributes.Length > 0)
            //{
            //    actionData.urlString = urlAttributes[0].urlString;
            //    if (urlAttributes[0].urlString.StartsWith("~"))
            //    {
            //        actionData.urlString = urlAttributes[0].urlString;
            //    }
            //    else
            //    {
            //        if (!string.IsNullOrEmpty(actionData.areaName))
            //        {
            //            actionData.actionUrl += "/" + actionData.areaName;
            //        }
            //        if (!string.IsNullOrEmpty(actionData.controllerName))
            //        {
            //            actionData.actionUrl += "/" + actionData.controllerName;
            //        }
            //        if (!string.IsNullOrEmpty(actionData.urlString))
            //        {
            //            actionData.actionUrl += "/" + actionData.actionName;

            //        }
            //    }
            //    actionData.method = urlAttributes[0].methodType.ToString();
            //}
        }
        /// <summary>
        /// 获取控制器行为中的过滤器
        /// </summary>
        /// <typeparam name="TArrayType">过滤器类型</typeparam>
        /// <param name="filterType">过滤器类型信息</param>
        /// <param name="controller">控制器类型</param>
        /// <param name="action">控制器行为类型</param>
        /// <returns></returns>
        public static TArrayType[] GetFilters<TArrayType>(Type filterType, Type controller, MethodInfo action)
        {
            List<TArrayType> filterList = new List<TArrayType>();
            var controllerFilters =
#if (NET40 || NET451 || NET461)
                controller
#endif
#if NETCORE
                controller.GetTypeInfo()   
#endif                
                .GetCustomAttributes(true);
            foreach(var controllerFilter in controllerFilters)
            {
                if (filterType.IsAssignableFrom(controllerFilter.GetType()))
                {
                    filterList.Add((TArrayType)(object)controllerFilter);
                }
            }
            var actionFilters =action.GetCustomAttributes(true);
            foreach (var actionFilter in actionFilters)
            {
                if (filterType.IsAssignableFrom(actionFilter.GetType()))
                {
                    filterList.Add((TArrayType)(object)actionFilter);
                }
            }
            if (filterList.Count > 0)
            {
                return filterList.ToArray();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 组装并返回控制器行为执行代理
        /// </summary>
        /// <typeparam name="TContext">Http上下文类型</typeparam>
        /// <typeparam name="TRequest">Http请求类型</typeparam>
        /// <param name="assmeblyName">程序集名称</param>
        /// <param name="methodInfo">控制器行为对应的方法反射信息</param>
        /// <returns></returns>
        public static ActionExecute<TContext,TRequest> GetActionExecute<TContext, TRequest>(string assmeblyName,MethodInfo methodInfo)
        {
            DynamicMethod method = new DynamicMethod(assmeblyName+methodInfo.DeclaringType.FullName, null, new Type[] { typeof(TContext) });
            ILGenerator methodIL = method.GetILGenerator();
            methodIL.Emit(OpCodes.Nop);
            methodIL.Emit(OpCodes.Ldarg_0);
            methodIL.Emit(OpCodes.Call, methodInfo);
            methodIL.Emit(OpCodes.Ret);
            ActionExecute<TContext,TRequest> methodDelegate = (ActionExecute<TContext,TRequest>)method.CreateDelegate(typeof(ActionExecute<TContext,TRequest>));
            return methodDelegate;
        }
    }
}
