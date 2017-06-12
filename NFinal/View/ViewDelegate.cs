using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace NFinal
{
    /// <summary>
    /// 视渲染函数代理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="writer"></param>
    /// <param name="t"></param>
    public delegate void RenderMethod<T>(NFinal.IO.Writer writer, T t);
    //public delegate void GetRenderMethod<T>(T t);
    /// <summary>
    /// 视图渲染信息
    /// </summary>
    public struct ViewDelegateData
    {
        /// <summary>
        /// 视图类名
        /// </summary>
        public string viewClassName;
        /// <summary>
        /// 视图类型
        /// </summary>
        public Type viewType;
        /// <summary>
        /// 视图渲染函数
        /// </summary>
        public Delegate renderMethod;
    }
    public static class ViewDelegate
    {
        public static NFinal.Collections.FastDictionary<string, NFinal.ViewDelegateData> viewFastDic = null;
        /// <summary>
        /// 视图泛型代理
        /// </summary>
        public static Delegate renderMethodDelegate = null;
        /// <summary>
        /// 组装并返回视图泛型函数代理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="viewType"></param>
        /// <returns></returns>
        public static Delegate GetRenderDelegate<T>(string url, Type viewType)
        {
            PropertyInfo modelProperty = viewType.GetProperty("Model");
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
            methodIL.Emit(OpCodes.Newobj, viewType.GetConstructor(new Type[] { typeof(NFinal.IO.Writer), modelType }));
            methodIL.Emit(OpCodes.Callvirt, viewType.GetMethod("Execute", Type.EmptyTypes));
            methodIL.Emit(OpCodes.Ret);
            renderMethodDelegate = method.CreateDelegate(typeof(NFinal.RenderMethod<>).MakeGenericType(modelType));
            return renderMethodDelegate;
        }
    }
}