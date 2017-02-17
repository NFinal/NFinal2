using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace NFinal.Emit
{
    public class CopyHelper
    {
        public static Dictionary<long, Delegate> CopyDic = new Dictionary<long, Delegate>();
        public delegate To CopyDelegate<From, To>(From f, To t);
        public static To CopyEmit<From, To>(From f, To t)
        {
            Type fromType = typeof(From);
            Type toType = typeof(To);
            long key = (long)fromType.GetHashCode() << 32 | (long)toType.GetHashCode();
            CopyDelegate<From, To> copyDelegate;
            if (CopyDic.ContainsKey(key))
            {
                Delegate delegateTemp = CopyDic[key];
                copyDelegate = (CopyDelegate<From, To>)delegateTemp;
            }
            else
            {
                DynamicMethod CopyMethod = new DynamicMethod(key.ToString(), typeof(To), new Type[] { typeof(From), typeof(To) }, true);
                ILGenerator methodIL = CopyMethod.GetILGenerator();
                PropertyInfo[] fromTypePropertyInfo = fromType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo[] toTypePropertyInfo = toType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo fp in fromTypePropertyInfo)
                {
                    foreach (PropertyInfo tp in toTypePropertyInfo)
                    {
                        if (fp.Name == tp.Name && fp.PropertyType == tp.PropertyType)
                        {
                            methodIL.Emit(OpCodes.Ldarg_1);
                            methodIL.Emit(OpCodes.Ldarg_0);
                            methodIL.Emit(OpCodes.Callvirt, fp.GetGetMethod());
                            methodIL.Emit(OpCodes.Callvirt, tp.GetSetMethod());
                        }
                    }
                }
                FieldInfo[] fromTypeFieldInfo = fromType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                FieldInfo[] toTypeFieldInfo = toType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                foreach (FieldInfo fi in fromTypeFieldInfo)
                {
                    foreach (FieldInfo ti in toTypeFieldInfo)
                    {
                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Ldfld, fi);
                        methodIL.Emit(OpCodes.Stfld, ti);
                    }
                }
                methodIL.Emit(OpCodes.Ldarg_1);
                methodIL.Emit(OpCodes.Ret);
                Delegate delegateTemp = CopyMethod.CreateDelegate(typeof(CopyDelegate<From, To>));
                CopyDic.Add(key, delegateTemp);
                copyDelegate = (CopyDelegate<From, To>)delegateTemp;
            }
            return copyDelegate(f, t);
        }
    }
}
