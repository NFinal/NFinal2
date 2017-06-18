using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace NFinal.Emit
{
    public class ExpandoObject
    {
        public static CallSite<Func<CallSite, object, int, object>> fieldSample;
        public void AddFields(List<Field> fields)
        {
            string typeName = Guid.NewGuid().ToString();
            Emit.ClassHelper structHelper = new ClassHelper( this.GetType().FullName,this.GetType().Name, "<>nf__2");
            List<FieldInfo> fieldInfos = new List<FieldInfo>();
            for(int i=0;i<fields.Count;i++)
            {
                Field field = fields[i];
                Type staticType= typeof(CallSite<>).
                    MakeGenericType(typeof(Func<,,,>).
                    MakeGenericType(typeof(CallSite), typeof(object), field.type, typeof(object)));
                structHelper.AddStaticField("<>p__"+i.ToString(), staticType);
            }
            var ViewBagOperateType= structHelper.ToType();
            for (int i = 0; i < fields.Count; i++)
            {
                FieldInfo fieldInfo=  ViewBagOperateType.GetField("<>p__" + i.ToString());
                fieldInfos.Add(fieldInfo);
               
            }
            

            DynamicMethod method = new DynamicMethod("AddFields", typeof(void),new Type[] { typeof(object) });
            ILGenerator methodIL= method.GetILGenerator();
            var localViewBag = methodIL.DeclareLocal(typeof(object));
            //methodIL.Emit(OpCodes.Newobj,typeof(System.Dynamic.ExpandoObject).GetConstructor(Type.EmptyTypes));
            methodIL.Emit(OpCodes.Ldarg_0);
            methodIL.Emit(OpCodes.Stloc, localViewBag);
            for (int i = 0; i < fields.Count; i++)
            {
                var hasNoOperateField = methodIL.DefineLabel();
                var hasNoOperateFiledEnd = methodIL.DefineLabel();
                //if(<>p_0==null)
                FieldInfo fieldInfo = fieldInfos[i];
                methodIL.Emit(OpCodes.Ldsfld, fieldInfo);
                methodIL.Emit(OpCodes.Brfalse_S,hasNoOperateField);
                methodIL.Emit(OpCodes.Br_S,hasNoOperateFiledEnd);
                methodIL.MarkLabel(hasNoOperateField);
                {
                    //CSharpBinderFlags.None
                    methodIL.Emit(OpCodes.Ldc_I4_0);
                    methodIL.Emit(OpCodes.Ldstr, fields[i].name);
                    methodIL.Emit(OpCodes.Ldtoken, ViewBagOperateType);
                    methodIL.Emit(OpCodes.Call, typeof(System.Type).GetMethod("GetTypeFromHandle", new Type[] { typeof(RuntimeTypeHandle) }));
                    methodIL.Emit(OpCodes.Ldc_I4_2);
                    methodIL.Emit(OpCodes.Newarr, typeof(Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo));
                    //CSharpArgumentInfo[0]
                    methodIL.Emit(OpCodes.Dup);
                    methodIL.Emit(OpCodes.Ldc_I4_0);
                    //CSharpArgumentInfoFlags.None
                    methodIL.Emit(OpCodes.Ldc_I4_0);
                    //null
                    methodIL.Emit(OpCodes.Ldnull);
                    //Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create(CSharpArgumentInfoFlags,string);
                    methodIL.Emit(OpCodes.Call, typeof(CSharpArgumentInfo).GetMethod("Create", new Type[] { typeof(CSharpArgumentInfoFlags), typeof(string) }));

                    methodIL.Emit(OpCodes.Stelem_Ref);

                    //CSharpArgumentInfo[1]
                    methodIL.Emit(OpCodes.Dup);
                    methodIL.Emit(OpCodes.Ldc_I4_1);
                    //CSharpArgumentInfoFlags.UseCompileTimeType|CSharpArgumentInfoFlags.Constant
                    methodIL.Emit(OpCodes.Ldc_I4_3);
                    //null
                    methodIL.Emit(OpCodes.Ldnull);
                    //Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create(CSharpArgumentInfoFlags,string);
                    methodIL.Emit(OpCodes.Call, typeof(CSharpArgumentInfo).GetMethod("Create", new Type[] { typeof(CSharpArgumentInfoFlags), typeof(string) }));
                    methodIL.Emit(OpCodes.Stelem_Ref);
                    //Binder.SetMember(CSharpBinderFlags,string,Type,IEnumerable<CSharpArgumentInfo>)
                    methodIL.Emit(OpCodes.Call, typeof(Microsoft.CSharp.RuntimeBinder.Binder).GetMethod("SetMember", new Type[] { typeof(CSharpBinderFlags), typeof(string), typeof(Type), typeof(IEnumerable<CSharpArgumentInfo>) }));
                    //CallSite<Func<CallSite, object, int, object>>.Create(CallSiteBinder)
                    Type staticType = typeof(CallSite<>).
                    MakeGenericType(typeof(Func<,,,>).
                    MakeGenericType(typeof(CallSite), typeof(object), fields[i].type, typeof(object)));
                    methodIL.Emit(OpCodes.Call, staticType.GetMethod("Create", new Type[] { typeof(CallSiteBinder) }));

                    methodIL.Emit(OpCodes.Stsfld, fieldInfo);
                    methodIL.Emit(OpCodes.Ldsfld, fieldInfo);
                    methodIL.Emit(OpCodes.Ldfld, fieldInfo.FieldType.GetField("Target"));
                    methodIL.Emit(OpCodes.Ldsfld, fieldInfo);
                    methodIL.Emit(OpCodes.Ldloc, localViewBag);
                    methodIL.Emit(OpCodes.Ldc_I4_1);
                    methodIL.Emit(OpCodes.Callvirt, typeof(Func<,,,>).
                    MakeGenericType(typeof(CallSite), typeof(object), fields[i].type, typeof(object)).GetMethod("Invoke"));
                    methodIL.Emit(OpCodes.Pop);
                }
                methodIL.MarkLabel(hasNoOperateFiledEnd); 
            }
            methodIL.Emit(OpCodes.Ret);
            Action<object> methodDelegate= (Action<object>)method.CreateDelegate(typeof(Action<object>));
            dynamic ViewBag = new System.Dynamic.ExpandoObject();
            methodDelegate(ViewBag);
            Console.WriteLine(ViewBag.a);
        }
    }
    public class Field
    {
        public string name;
        public Type type;
    }
}
