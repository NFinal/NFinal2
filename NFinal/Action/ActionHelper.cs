using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using NFinal.Http;

public delegate void ActionExecute<TContext, TResquest>(TContext context,NFinal.Action.ActionData<TContext,TResquest> actionData,TResquest request,NFinal.NameValueCollection parameters);
namespace NFinal.Action
{
    public class ControllerData
    {
        public Dictionary<Type, Dictionary<string,string>> formatList;
    }
    public class ActionData<TContext,TRequest>
    {
        public string actionUrl;
        public Type viewBagType;
        public ActionExecute<TContext,TRequest> actionExecute;
        public NFinal.Config.Plug.PlugConfig plugConfig;
        public System.Reflection.ICustomAttributeProvider methodProvider;
        public NFinal.Filter.IBaseFilter<TContext>[] IBaseFilters;
        public NFinal.Filter.IRequestFilter<TRequest>[] IRequestFilters;
        public NFinal.Filter.IResponseFilter[] IResponseFilters;
        public string urlString;
        public string className;
        public string methodName;
        public string controllerName;
        public string actionName;
        public string areaName;
        public string[] method;
        public string contentType;
        public CompressMode compressMode;
        public NFinal.Url.ActionUrlData actionUrlData;
    }
    public class ActionHelper
    {
        //public static Dictionary<string, ActionExecute> actionDic = new Dictionary<string, ActionExecute>();
        //public static NFinal.Collections.FastDictionary<ActionData> actionFastDic;
        public static bool isInit = false;
        public static void Init<TContext, TRequest>(NFinal.Config.Global.GlobalConfig globalConfig)
        {
            Module[] modules = null;
            Type[] types = null;

            Dictionary<string, ActionData<TContext, TRequest>> actionDataDictionary = new Dictionary<string, ActionData<TContext, TRequest>>();
            //List<KeyValuePair<string, ActionData<TContext, TRequest>>> actionDataList = new List<KeyValuePair<string, ActionData<TContext, TRequest>>>();
            Dictionary<Type, Dictionary<string, NFinal.Url.FormatData>> formatControllerDictionary = new Dictionary<Type, Dictionary<string, NFinal.Url.FormatData>>();
            Type controller = null;
            for (int i = 0; i < NFinal.Plugs.PlugManager.plugInfoList.Count; i++)
            {
                /////////////////////////////////////////////////////////////////////
                ///
                ///Assembly.Load
                ///精确加载
                ///
                ///
                ///Assembly.LoadFrom
                ///加载dll的引入
                ///
                ///
                ///Assembly.LoadFile
                ///仅加载自己
                ///
                /////////////////////////////////////////////////////////////////////
                NFinal.Plugs.PlugInfo plugInfo = NFinal.Plugs.PlugManager.plugInfoList[i];
                Assembly assembly = plugInfo.assembly;
               
                modules = assembly.GetModules();
                for (int j = 0; j < modules.Length; j++)
                {
                    types = modules[j].GetTypes();
                   
                    for (int k = 0; k < types.Length; k++)
                    {
                        controller = types[k];
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
                                formatControllerDictionary.Add(controller, formatMethodDic);
                            }
                        }
                        
                    }
                }
            }
            NFinal.Url.ActionUrlHelper.formatControllerDictionary= formatControllerDictionary;
            if (globalConfig.debug.enable)
            {
                NFinal.Url.ActionUrlHelper.GetUrlRouteJsContent(globalConfig);
                NFinal.Url.ActionUrlHelper.GenerateActionDebugHtml(globalConfig);
            }
            //}
            //添加图标响应
            //Icon.Favicon.Init(actionDataList);
            Middleware.Middleware<TContext, TRequest>.actionFastDic = new Collections.FastDictionary<ActionData<TContext, TRequest>>(actionDataDictionary, actionDataDictionary.Count);
            actionDataDictionary.Clear();
        }
        public static void AddActionData<TContext, TRequest>(Dictionary<string, ActionData<TContext, TRequest>> actionDataDictionary,
            Dictionary<string, NFinal.Url.FormatData> formatMethodDictionary,
            Assembly assembly,Type controller, 
            NFinal.Config.Global.GlobalConfig globalConfig,
            NFinal.Plugs.PlugInfo plugInfo)
        {
            Type viewBagType = null;
            MethodInfo methodInfo = null;
            viewBagType = controller.GetField("ViewBag").FieldType;
            if (viewBagType == typeof(object))
            {
                MethodInfo[] actions = null;
                string[] actionKeys;
                string controllerName;
                string areaName;
                string actionUrl;
                string actionName;
                string[] method;
                UrlAttribute urlAttribute;
                NFinal.Url.ActionUrlData actionUrlData;
                ActionData<TContext, TRequest> actionData;
                GetControllerUrl(out controllerName, out areaName, controller, globalConfig);
                actions = controller.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
                for (int m = 0; m < actions.Length; m++)
                {
                    if (actions[m].IsAbstract || actions[m].IsVirtual
                        || actions[m].IsStatic || actions[m].IsConstructor)
                    {
                        continue;
                    }
                    
                    actionKeys = GetActionKeys(controllerName, areaName, out actionUrl, out actionName, out method,
                        out urlAttribute,out actionUrlData,
                        actions[m], globalConfig,plugInfo);
                    actionData = new ActionData<TContext, TRequest>();
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
                    actionData.methodName = actions[m].Name;
                    actionData.actionUrlData = actionUrlData;
                    actionData.controllerName = controllerName;
                    actionData.areaName = areaName;
                    actionData.actionUrl = actionUrl;
                    actionData.actionName = actionName;
                    actionData.method = method;
                    actionData.plugConfig = plugInfo.config;
                    if (actionData.actionUrlData != null)
                    {
                        formatMethodDictionary.Add(actions[m].Name, new NFinal.Url.FormatData(actionData.actionUrlData.formatUrl, actionData.actionUrlData.actionUrlNames));
                    }
                    else
                    {
                        formatMethodDictionary.Add(actions[m].Name, new NFinal.Url.FormatData(actionData.actionUrl,null));
                    }
                    methodInfo = actions[m];
                    actionData.IBaseFilters = GetFilters<NFinal.Filter.IBaseFilter<TContext>>(
                        typeof(NFinal.Filter.IBaseFilter<TContext>), controller, actions[m]);
                    actionData.IRequestFilters = GetFilters<NFinal.Filter.IRequestFilter<TRequest>>(
                        typeof(NFinal.Filter.IRequestFilter<TRequest>), controller, actions[m]);
                    actionData.IResponseFilters = GetFilters<NFinal.Filter.IResponseFilter>(
                        typeof(NFinal.Filter.IResponseFilter), controller, actions[m]);
                    actionData.actionExecute = NFinal.Action.Actuator.GetRunActionDelegate<TContext, TRequest>(assembly,controller, methodInfo);
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
            }
        }
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
