using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace NFinal
{
    public delegate void RenderMethod<T>(NFinal.IO.IWriter writer, T t);
    //public delegate void GetRenderMethod<T>(T t);
    public struct ViewDelegateData
    {
        public System.Reflection.MethodInfo renderMethodInfo;
        public Delegate renderMethod;
    }
    public static class ViewHelper
    {
        public static bool isInit = false;
        //public static Dictionary<string, NFinal.ViewDelegateData>  dicViews = new Dictionary<string, NFinal.ViewDelegateData>();
        public static NFinal.Collections.FastDictionary<NFinal.ViewDelegateData> viewFastDic = null;
        public static void Init(Middleware.MiddlewareConfigOptions options)
        {
            Middleware.Plug plug = null;
            Assembly assembly = null;
            Module[] modules= null;
            NFinal.ViewDelegateData dele;
            ViewAttribute viewAttr;
            List<KeyValuePair<string, ViewDelegateData>> viewList = new List<KeyValuePair<string, NFinal.ViewDelegateData>>();
            for (int i = 0; i < options.plugs.Length; i++)
            {
                plug = options.plugs[i];
                assembly = Assembly.LoadFrom(plug.filePath);
                modules = assembly.GetModules();
                for (int j = 0; j < modules.Length; j++)
                {
                    Type[] types = modules[j].GetTypes();
                    for (int k = 0; k < types.Length; k++)
                    {
                        object[] attrs = types[k].GetCustomAttributes(typeof(ViewAttribute), true);
                        if (attrs.Length > 0)
                        {
                            viewAttr = (ViewAttribute)attrs[0];
                            if (string.IsNullOrEmpty(viewAttr.viewUrl))
                            {
                                viewAttr.viewUrl = types[k].FullName.Replace('.', '/');
                            }
                            dele = new ViewDelegateData();
                            dele.renderMethodInfo = types[k].GetMethod("Render");
                            dele.renderMethod = null;// GetRenderDelegate(dele.renderMethodInfo);
                            //dicViews.Add(viewAttr.viewUrl, dele);
                            viewList.Add(new KeyValuePair<string, ViewDelegateData>(viewAttr.viewUrl, dele));
                        }
                    }
                }
            }
            viewFastDic = new Collections.FastDictionary<NFinal.ViewDelegateData>(viewList, viewList.Count);
        }
        public static Delegate renderMethodDelegate = null;
        public static Delegate GetRenderDelegate<T>(string url,MethodInfo renderMethodInfo)
        {
            ParameterInfo[] parameters= renderMethodInfo.GetParameters();
            Type modelType = parameters[1].ParameterType;
            if (typeof(T) != modelType)
            {
                throw new Exceptions.ViewModelTypeUnMatched(url, typeof(T), modelType);
            }
            DynamicMethod method = new DynamicMethod("RenderX", typeof(void), new Type[] { typeof(NFinal.IO.IWriter), modelType });
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
            methodIL.Emit(OpCodes.Call, renderMethodInfo);
            methodIL.Emit(OpCodes.Ret);
            renderMethodDelegate = method.CreateDelegate(typeof(NFinal.RenderMethod<>).MakeGenericType(modelType));
            return renderMethodDelegate;
        }
    }
}
