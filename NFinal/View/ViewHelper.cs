//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ViewHelper.cs
//        Description :视图初始化以及执行帮助类
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace NFinal
{
    /// <summary>
    /// 视图初始化以及执行帮助类
    /// </summary>
    public static class ViewHelper
    {
        /// <summary>
        /// 视图是否初始化
        /// </summary>
        public static bool isInit = false;
        //public static Dictionary<string, NFinal.ViewDelegateData>  dicViews = new Dictionary<string, NFinal.ViewDelegateData>();
        /// <summary>
        /// 视图执行代理缓存
        /// </summary>
        
        /// <summary>
        /// 视图初始化
        /// </summary>
        /// <param name="globalConfig"></param>
        public static void Init(NFinal.Config.Global.GlobalConfig globalConfig)
        {
            NFinal.Plugs.PlugInfo plug = null;
            Assembly assembly = null;
            Module[] modules= null;
            NFinal.ViewDelegateData dele;
            ViewAttribute viewAttr;
            NFinal.Collections.FastDictionary<string, ViewDelegateData> viewDataDictionary = new NFinal.Collections.FastDictionary<string, NFinal.ViewDelegateData>();
            for (int i = 0; i < NFinal.Plugs.PlugManager.plugInfoList.Count; i++)
            {
                plug = NFinal.Plugs.PlugManager.plugInfoList[i];
                if (!plug.loadSuccess)
                {
                    continue;
                }
                assembly = plug.assembly;
                modules = assembly.GetModules();
                for (int j = 0; j < modules.Length; j++)
                {
                    Type[] types = modules[j].GetTypes();
                    for (int k = 0; k < types.Length; k++)
                    {
#if (NET40 || NET451 || NET461)
                        var attrs =   types[k].GetCustomAttributes(typeof(ViewAttribute), true);
#endif 
#if NETCORE
                        var attrs = types[k].GetTypeInfo().GetCustomAttributes(typeof(ViewAttribute), false);
#endif
                        if (attrs.Count() > 0)
                        {
                            viewAttr = (ViewAttribute)attrs.First();
                            if (string.IsNullOrEmpty(viewAttr.viewUrl))
                            {
                                viewAttr.viewUrl = types[k].FullName.Replace('.', '/');
                            }
                            dele = new ViewDelegateData();
                            dele.viewTypeHandle = types[k].TypeHandle;
                            dele.renderMethod = null;//GetRenderDelegate(dele.renderMethodInfo);
                            dele.viewClassName = types[k].FullName;
                            //dicViews.Add(viewAttr.viewUrl, dele);
                            if (viewDataDictionary.ContainsKey(viewAttr.viewUrl))
                            {
                                var oldViewDelegateData = viewDataDictionary[viewAttr.viewUrl];
                                throw new NFinal.Exceptions.DuplicateViewUrlException(oldViewDelegateData.viewClassName, dele.viewClassName);
                            }
                            else
                            {
                                viewDataDictionary.Add(viewAttr.viewUrl, dele);
                            }
                        }
                    }
                }
            }
            ViewDelegate.viewFastDic = viewDataDictionary;
        }
        
    }
}
