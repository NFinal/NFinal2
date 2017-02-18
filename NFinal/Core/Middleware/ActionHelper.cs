using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

public delegate void ActionExecute<TContext, TResquest>(TContext context,NFinal.Middleware.ActionData<TContext,TResquest> actionData,TResquest request,NFinal.NameValueCollection parameters);
namespace NFinal.Middleware
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
        public System.Reflection.ICustomAttributeProvider methodProvider;
        public NFinal.Filter.IBaseFilter<TContext>[] IBaseFilters;
        public NFinal.Filter.IRequestFilter<TRequest>[] IRequestFilters;
        public NFinal.Filter.IResponseFilter[] IResponseFilters;
        public string subDomain;
        public string urlString;
        public string className;
        public string methodName;
        public string controllerName;
        public string actionName;
        public string areaName;
        public string[] method;
        public string contentType;
        public NFinal.CompressMode compressMode;
        public ActionUrlData actionUrlData;
    }
    public class ActionHelper
    {
        //public static Dictionary<string, ActionExecute> actionDic = new Dictionary<string, ActionExecute>();
        //public static NFinal.Collections.FastDictionary<ActionData> actionFastDic;
        public static bool isInit = false;
        public static void Init<TContext, TRequest>(MiddlewareConfigOptions options)
        {
            Module[] modules = null;
            Type[] types = null;

            List<KeyValuePair<string, ActionData<TContext, TRequest>>> actionDataList = new List<KeyValuePair<string, ActionData<TContext, TRequest>>>();
            Dictionary<Type, Dictionary<string, string>> formatControllerDictionary = new Dictionary<Type, Dictionary<string, string>>();
            Type controller = null;



            //if (options.plugs == null)
            //{
            //    subDomain = options.defaultSubDomain;
            //    Assembly assembly = Assembly.GetExecutingAssembly();
            //    modules = assembly.GetModules();
            //    for (int j = 0; j < modules.Length; j++)
            //    {
            //        types = modules[j].GetTypes();
            //        for (int k = 0; k < types.Length; k++)
            //        {
            //            controller = types[k];
            //            //该类型继承自IAction并且其泛不是dynamic类型
            //            if (typeof(NFinal.IAction<TContext, TRequest>).IsAssignableFrom(controller))
            //            {
            //                if (!controller.IsGenericType)
            //                {
            //                    actionData = new ActionData<TContext, TRequest>();
            //                    actionData.subDomain = subDomain;
            //                    AddActionData(actionDataList, actionData, controller, options);
            //                }
            //            }
            //        }
            //    }
            //}
            //else
            //{
            for (int i = 0; i < options.plugs.Length; i++)
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
                Assembly assembly = Assembly.LoadFrom(options.plugs[i].filePath);
                modules = assembly.GetModules();
                for (int j = 0; j < modules.Length; j++)
                {
                    types = modules[j].GetTypes();
                   
                    for (int k = 0; k < types.Length; k++)
                    {
                        controller = types[k];
                        //该类型继承自IAction并且其泛不是dynamic类型
                        if (typeof(NFinal.IAction<TContext, TRequest>).IsAssignableFrom(controller))
                        {
                            if (!controller.IsGenericType)
                            {
                                Dictionary<string, string> formatMethodDic = new Dictionary<string, string>();
                                AddActionData(actionDataList, formatMethodDic, assembly, controller, options);
                                formatControllerDictionary.Add(controller, formatMethodDic);
                            }
                        }
                        
                    }
                }
            }
            Middleware.ActionUrlHelper.formatControllerDictionary=formatControllerDictionary;
            //}
            //添加图标响应
            //Icon.Favicon.Init(actionDataList);
            Middleware.Middleware<TContext, TRequest>.actionFastDic = new Collections.FastDictionary<ActionData<TContext, TRequest>>(actionDataList, actionDataList.Count);
        }
        public static void AddActionData<TContext, TRequest>(List<KeyValuePair<string, ActionData<TContext, TRequest>>> actionDataList,
            Dictionary<string,string> formatMethodDictionary,
            Assembly assembly,Type controller, NFinal.Middleware.MiddlewareConfigOptions options)
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
                string subDomain;
                string actionUrl;
                string actionName;
                string[] method;
                UrlAttribute urlAttribute;
                NFinal.Middleware.ActionUrlData actionUrlData;
                ActionData<TContext, TRequest> actionData;
                GetControllerUrl(out controllerName, out areaName, out subDomain, controller, options);
                actions = controller.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
                for (int m = 0; m < actions.Length; m++)
                {
                    if (actions[m].IsAbstract || actions[m].IsVirtual
                        || actions[m].IsStatic || actions[m].IsConstructor)
                    {
                        continue;
                    }
                    
                    actionKeys = GetActionKeys(controllerName, areaName, subDomain, out actionUrl, out actionName, out method,
                        out urlAttribute,out actionUrlData,
                        actions[m], options);
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
                    actionData.actionUrlData = actionUrlData;
                    actionData.controllerName = controllerName;
                    actionData.areaName = areaName;
                    actionData.subDomain = subDomain;
                    actionData.actionUrl = actionUrl;
                    actionData.actionName = actionName;
                    actionData.method = method;
                    formatMethodDictionary.Add(actions[m].Name, actionData.actionUrl);
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
                        actionDataList.Add(new KeyValuePair<string, ActionData<TContext, TRequest>>(actionKey, actionData));
                    }
                }
            }
        }
        public static void GetControllerUrl(out string controllerName, out string areaName, out string subDomain, Type controller, NFinal.Middleware.MiddlewareConfigOptions options)
        {
            ControllerAttribute[] controllerAttributes = null;
            AreaAttribute[] areaAttributes = null;
            SubDomainAttribute[] subDomainAttributes = null;
            controllerAttributes = (ControllerAttribute[])controller.GetCustomAttributes(typeof(ControllerAttribute), true);
            if (controllerAttributes.Length > 0)
            {
                controllerName = controllerAttributes[0].Name;
            }
            else
            {
                controllerName = controller.Name;
            }
            areaAttributes = (AreaAttribute[])controller.GetCustomAttributes(typeof(AreaAttribute), true);
            if (areaAttributes.Length > 0)
            {
                areaName = areaAttributes[0].Name;
            }
            else
            {
                areaName = null;
            }
            subDomainAttributes = (SubDomainAttribute[])controller.GetCustomAttributes(typeof(SubDomainAttribute), true);
            if (subDomainAttributes.Length > 0)
            {
                subDomain = subDomainAttributes[0].Name;
            }
            else
            {
                subDomain = options.defaultSubDomain;
            }
        }
        public static string[] GetActionKeys(string controllerName, string areaName, string subDomain,
            out string actionUrl, out string actionName, out string[] method,
            out UrlAttribute urlAttribute,out NFinal.Middleware.ActionUrlData actionUrlData,
            MethodInfo methodInfo, NFinal.Middleware.MiddlewareConfigOptions options)
        {
            
            List<string> actionKeys = new List<string>();
            actionUrlData = null;
            method = null;
            urlAttribute = null;
            actionUrl = null;
            actionName = null;
            if (options.urlRouteRule == UrlRouteRule.AreaControllerActionParameters)
            {
                if (!string.IsNullOrEmpty(areaName))
                {
                    actionUrl = "/" + areaName;
                }
                if (!string.IsNullOrEmpty(controllerName))
                {
                    actionUrl += "/" + controllerName;
                }
                ActionAttribute[] actionAttributes = null;
                actionAttributes = (ActionAttribute[])methodInfo.GetCustomAttributes(typeof(ActionAttribute), true);
                if (actionAttributes.Length > 0)
                {
                    actionName = actionAttributes[0].Name;
                }
                else
                {
                    actionName = methodInfo.Name;
                }
                if (!string.IsNullOrEmpty(actionName))
                {
                    actionUrl += "/" + actionName + options.defaultSuffix;
                }
            }
            else if (options.urlRouteRule == UrlRouteRule.AreaControllerCustomActionUrl)
            {
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
                    if (attr.GetType().IsSubclassOf(typeof(UrlAttribute)))
                    {
                        hasUrlAttribute = true;
                        urlAttribute = (UrlAttribute)attr;
                        actionUrlData = NFinal.Middleware.ActionUrlHelper.GetActionUrlData(urlAttribute.urlString);
                        break;
                    } 
                }
                if (hasUrlAttribute)
                {
                    actionUrl += "/" + method + options.defaultSuffix;
                }
            }
            if (method == null)
            {
                method = options.verbs;
                foreach (var m in method)
                {
                    actionKeys.Add(subDomain + ":" + m + ":" + actionUrl);
                }
            }
            else
            {
                actionKeys.Add(subDomain + ":" + method + ":" + actionUrl);
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
            object[] controllerFilters = controller.GetCustomAttributes(true);
            for(int i=0;i<controllerFilters.Length;i++)
            {
                if (filterType.IsAssignableFrom(controllerFilters[i].GetType()))
                {
                    filterList.Add((TArrayType)controllerFilters[i]);
                }
            }
            object[] actionFilters = action.GetCustomAttributes(true);
            for (int i = 0; i < actionFilters.Length; i++)
            {
                if (filterType.IsAssignableFrom(actionFilters[i].GetType()))
                {
                    filterList.Add((TArrayType)actionFilters[i]);
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
