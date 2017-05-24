﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace NFinal
{
    public delegate void RenderMethod<T>(NFinal.IO.Writer writer, T t);
    //public delegate void GetRenderMethod<T>(T t);
    public struct ViewDelegateData
    {
        public string viewClassName;
        public Type viewType;
        public Delegate renderMethod;
    }
    public static class ViewHelper
    {
        public static bool isInit = false;
        //public static Dictionary<string, NFinal.ViewDelegateData>  dicViews = new Dictionary<string, NFinal.ViewDelegateData>();
        public static NFinal.Collections.FastDictionary<string,NFinal.ViewDelegateData> viewFastDic = null;
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
                            dele.viewType = types[k];
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
            viewFastDic =viewDataDictionary;
        }
        public static Delegate renderMethodDelegate = null;
        public static Delegate GetRenderDelegate<T>(string url,Type viewType)
        {
            PropertyInfo modelProperty= viewType.GetProperty("Model");
            Type modelType = modelProperty.PropertyType;
            if (typeof(T) != modelType)
            {
                throw new Exceptions.ViewModelTypeUnMatchedException(url, typeof(T), modelType);
            }
            DynamicMethod method = new DynamicMethod("RenderX", typeof(void), new Type[] { typeof(NFinal.IO.Writer), modelType });
            ILGenerator methodIL = method.GetILGenerator();
            var model = methodIL.DeclareLocal(modelType);
            methodIL.Emit(OpCodes.Nop);
            //methodIL.Emit(OpCodes.Ldarg_1);
            //methodIL.Emit(OpCodes.Castclass, modelType);
            //methodIL.Emit(OpCodes.Stloc,model);
            //methodIL.Emit(OpCodes.Ldarg_0);
            //methodIL.Emit(OpCodes.Ldloc,model);
            methodIL.Emit(OpCodes.Ldarg_0);
            methodIL.Emit(OpCodes.Ldarg_1);
            methodIL.Emit(OpCodes.Newobj,viewType.GetConstructor(new Type[] { typeof(NFinal.IO.Writer),modelType }));
            methodIL.Emit(OpCodes.Callvirt,viewType.GetMethod("Execute",Type.EmptyTypes));
            methodIL.Emit(OpCodes.Ret);
            renderMethodDelegate = method.CreateDelegate(typeof(NFinal.RenderMethod<>).MakeGenericType(modelType));
            return renderMethodDelegate;
        }
    }
}
