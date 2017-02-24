using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace NFinal.Json
{
    public delegate void GetJsonDelegate<TModel>(TModel model, NFinal.IO.IWriter writer, DateTimeFormat dateTimeFormat);
    public enum DateTimeFormat
    {
        UTCTimeNumber,
        LocalTimeNumber,
        UTCTimeString,
        LocalTimeString
    }
    public class JsonHelper
    {
        public static long GetUTCTimeNumber(DateTime dateTime)
        {
            DateTime utcDate = dateTime;
            if (dateTime.Kind != DateTimeKind.Utc)
            {
                utcDate = dateTime.ToUniversalTime();
            }
            long ms = (long)utcDate.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            return ms;
        }
        public static readonly System.Reflection.MethodInfo GetUTCTimeNumberMethodInfo=typeof(JsonHelper).GetMethod("GetUTCTimeNumber",new Type[] {typeof(DateTime)});
        public static long GetLocalTimeNumber(DateTime dateTime)
        {
            DateTime localDate = dateTime;
            if (dateTime.Kind != DateTimeKind.Local)
            {
                localDate = dateTime.ToLocalTime();
            }
            long ms = (long)localDate.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local)).TotalMilliseconds;
            return ms;
        }
        public static readonly System.Reflection.MethodInfo GetLocalTimeNumberMethodInfo = typeof(JsonHelper).GetMethod("GetLocalTimeNumber", new Type[] { typeof(DateTime) });
        public static string GetLocalTimeString(DateTime dateTime)
        {
            DateTime localDate = dateTime;
            if (dateTime.Kind != DateTimeKind.Local)
            {
                localDate = dateTime.ToLocalTime();
            }
            
            return localDate.ToLongTimeString();
        }
        public static readonly System.Reflection.MethodInfo GetLocalTimeStringMethodInfo = typeof(JsonHelper).GetMethod("GetLocalTimeString", new Type[] { typeof(DateTime) });
        public static string GetUTCTimeString(DateTime dateTime)
        {
            DateTime utcDate = dateTime;
            if (dateTime.Kind != DateTimeKind.Utc)
            {
                utcDate = dateTime.ToUniversalTime();
            }
            return utcDate.ToLongTimeString();
        }
        public static readonly System.Reflection.MethodInfo GetUTCTimeStringMethodInfo = typeof(JsonHelper).GetMethod("GetUTCTimeString", new Type[] { typeof(DateTime) });
        public struct GetJsonDelegateData
        {
            public Type modelType;
            public Delegate getJsonDelegate;
        }
        public static Dictionary<Type, GetJsonDelegateData> GetJsonDictionary = new Dictionary<Type, GetJsonDelegateData>();
        public static readonly System.Reflection.MethodInfo toBase64StringMethodInfo = typeof(System.Convert).GetMethod("ToBase64String",new Type[] { typeof(byte[])});
        public static readonly System.Reflection.MethodInfo writeMethodInfo = typeof(NFinal.IO.IWriter).GetMethod("Write", new Type[] { typeof(string) });
        public static readonly System.Reflection.MethodInfo WriteJsonReverseStringMethodInfo = typeof(System.WriterExtension).GetMethod("WriteJsonReverseString", new Type[] {typeof(NFinal.IO.IWriter), typeof(string) });
        public static void GetJson<TModel>(TModel model,NFinal.IO.IWriter writer, DateTimeFormat format)
        {
            GetJsonDelegateData getJsonDelegateData;
            if (!GetJsonDictionary.TryGetValue(model.GetType(), out getJsonDelegateData))
            {
                getJsonDelegateData.modelType = model.GetType();
                getJsonDelegateData.getJsonDelegate = GetDelegate(model, format);
                GetJsonDictionary.Add(model.GetType(), getJsonDelegateData);
            }
            ((GetJsonDelegate<TModel>)getJsonDelegateData.getJsonDelegate)(model,writer, format);
        }
        public static void WriteComma(ILGenerator methodIL,ref bool isFirst)
        {
            if (isFirst)
            {
                isFirst = false;
            }
            else
            {
                //writer
                methodIL.Emit(OpCodes.Ldarg_1);
                //writer,","
                methodIL.Emit(OpCodes.Ldstr, ",");
                //
                methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
            }
        }
        public static void WriteString(ILGenerator methodIL, string str)
        {
            //writer
            methodIL.Emit(OpCodes.Ldarg_1);
            //writer,","
            methodIL.Emit(OpCodes.Ldstr, str);
            //
            methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
        }
        public static Delegate GetDelegate<TModel>(TModel model, DateTimeFormat dateTimeFormat)
        {
            Delegate getModelDelegate = null;
            Type modelType = typeof(TModel);
            DynamicMethod method = new DynamicMethod("GetJsonX", typeof(void), new Type[] { modelType, typeof(NFinal.IO.IWriter),typeof(DateTimeFormat) });
            ILGenerator methodIL = method.GetILGenerator();
            methodIL.Emit(OpCodes.Nop);

            Type parametersType = typeof(NameValueCollection);
            var parametersGetItem = parametersType.GetMethod("get_Item");
            WriteString(methodIL, "{");
            bool isFirst = true;
            System.Reflection.PropertyInfo[] modelProperties = modelType.GetProperties();
            for (int i = 0; i < modelProperties.Length; i++)
            {
                WriteProperty(methodIL, modelProperties[i],ref isFirst, dateTimeFormat);
            }
            System.Reflection.FieldInfo[] modelFieldInfos = modelType.GetFields();
            for (int i = 0; i < modelFieldInfos.Length; i++)
            {
                WriteField(methodIL, modelFieldInfos[i],ref isFirst, dateTimeFormat);
            }
            WriteString(methodIL, "}");

            methodIL.Emit(OpCodes.Ret);
            getModelDelegate = method.CreateDelegate(typeof(GetJsonDelegate<>).MakeGenericType(typeof(TModel)));
            return getModelDelegate;
        }
        public static void WriteProperty(ILGenerator methodIL, System.Reflection.PropertyInfo propertyInfo, ref bool isFirst, DateTimeFormat dateTimeFormat)
        {
            //不解析静态属性和未设置Get的属性。
            if (propertyInfo.GetGetMethod() == null || propertyInfo.GetGetMethod().IsStatic == true)
            {
                return;
            }
            Type propertyType = propertyInfo.PropertyType;
            bool isNullable = false;
            if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                isNullable = true;
                propertyType = propertyInfo.PropertyType.GetGenericArguments()[0];
            }
            if (propertyType == typeof(System.String))
            {
                #region
                var elseCase = methodIL.DefineLabel();
                var endCase = methodIL.DefineLabel();
                WriteComma(methodIL, ref isFirst);
                WriteString(methodIL, "\"" + propertyInfo.Name + "\"");
                //model
                methodIL.Emit(OpCodes.Ldarg_0);
                //model.property
                methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                //model.property,null
                methodIL.Emit(OpCodes.Ldnull);
                //bool
                methodIL.Emit(OpCodes.Ceq);
                //
                methodIL.Emit(OpCodes.Brfalse_S, elseCase);
                WriteString(methodIL, "null");
                methodIL.Emit(OpCodes.Br_S, endCase);
                methodIL.MarkLabel(elseCase);
                WriteString(methodIL, "\"");
                //writer,model
                methodIL.Emit(OpCodes.Ldarg_1);
                //writer,model
                methodIL.Emit(OpCodes.Ldarg_0);
                //writer,model.property
                methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                //
                methodIL.Emit(OpCodes.Call, WriteJsonReverseStringMethodInfo);
                WriteString(methodIL, "\"");
                methodIL.MarkLabel(endCase);
                #endregion
            }
            else if (propertyType == typeof(System.Byte[]))
            {
                var elseCase = methodIL.DefineLabel();
                var endCase = methodIL.DefineLabel();
                WriteComma(methodIL, ref isFirst);
                WriteString(methodIL, "\"" + propertyInfo.Name + "\":");
                methodIL.Emit(OpCodes.Ldarg_0);
                methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                methodIL.Emit(OpCodes.Ldnull);
                methodIL.Emit(OpCodes.Ceq);
                methodIL.Emit(OpCodes.Brfalse_S, elseCase);
                WriteString(methodIL, "null");
                methodIL.Emit(OpCodes.Br_S, endCase);
                methodIL.MarkLabel(elseCase);
                WriteString(methodIL, "\"");
                methodIL.Emit(OpCodes.Ldarg_1);
                methodIL.Emit(OpCodes.Ldarg_0);
                methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                methodIL.Emit(OpCodes.Call, toBase64StringMethodInfo);
                methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                WriteString(methodIL, "\"");
                methodIL.MarkLabel(endCase);
            }
            else if (propertyType == typeof(System.Char) ||
                propertyType == typeof(System.Guid))
            {
                #region
                if (isNullable)
                {
                    var elseCase = methodIL.DefineLabel();
                    var endCase = methodIL.DefineLabel();
                    var temp1 = methodIL.DeclareLocal(propertyInfo.PropertyType);
                    var temp = methodIL.DeclareLocal(propertyType);
                    WriteComma(methodIL, ref isFirst);
                    WriteString(methodIL, "\"" + propertyInfo.Name + "\":");
                    methodIL.Emit(OpCodes.Ldarg_0);
                    methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                    methodIL.Emit(OpCodes.Stloc_S, temp1);
                    methodIL.Emit(OpCodes.Ldloca_S, temp1);
                    methodIL.Emit(OpCodes.Call, propertyInfo.PropertyType.GetMethod("get_HasValue", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Ldc_I4_0);
                    methodIL.Emit(OpCodes.Ceq);
                    methodIL.Emit(OpCodes.Brfalse_S, elseCase);
                    WriteString(methodIL, "null");
                    methodIL.Emit(OpCodes.Br_S, endCase);
                    methodIL.MarkLabel(elseCase);
                    WriteString(methodIL, "\"");
                    methodIL.Emit(OpCodes.Ldarg_1);
                    methodIL.Emit(OpCodes.Ldarg_0);
                    methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                    methodIL.Emit(OpCodes.Stloc_S, temp1);
                    methodIL.Emit(OpCodes.Ldloca_S, temp1);
                    methodIL.Emit(OpCodes.Call, propertyInfo.PropertyType.GetMethod("get_Value", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Stloc, temp);
                    methodIL.Emit(OpCodes.Ldloca_S, temp);
                    methodIL.Emit(OpCodes.Call, propertyType.GetMethod("ToString", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                    WriteString(methodIL, "\"");
                    methodIL.MarkLabel(endCase);
                }
                else
                {
                    var temp = methodIL.DeclareLocal(propertyType);
                    WriteComma(methodIL, ref isFirst);
                    WriteString(methodIL, "\"" + propertyInfo.Name + "\":");
                    WriteString(methodIL, "\"");
                    methodIL.Emit(OpCodes.Ldarg_1);
                    methodIL.Emit(OpCodes.Ldarg_0);
                    methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                    methodIL.Emit(OpCodes.Stloc, temp);
                    methodIL.Emit(OpCodes.Ldloca_S, temp);
                    methodIL.Emit(OpCodes.Call, propertyType.GetMethod("ToString", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                    WriteString(methodIL, "\"");
                }
                #endregion
            }
            else if (propertyType == typeof(System.Int32) ||
                propertyType == typeof(System.Int16) ||
                propertyType == typeof(System.Int64) ||
                propertyType == typeof(System.UInt32) ||
                propertyType == typeof(System.UInt16) ||
                propertyType == typeof(System.UInt64) ||
                propertyType == typeof(System.Byte) ||
                propertyType == typeof(System.SByte) ||
                propertyType == typeof(System.Single) ||
                propertyType == typeof(System.Double) ||
                propertyType == typeof(System.Decimal))
            {
                #region
                if (isNullable)
                {
                    var elseCase = methodIL.DefineLabel();
                    var endCase = methodIL.DefineLabel();
                    var temp = methodIL.DeclareLocal(propertyType);
                    var temp1 = methodIL.DeclareLocal(propertyInfo.PropertyType);
                    WriteComma(methodIL, ref isFirst);
                    WriteString(methodIL, "\"" + propertyInfo.Name + "\":");
                    methodIL.Emit(OpCodes.Ldarg_0);
                    methodIL.Emit(OpCodes.Call, propertyInfo.GetGetMethod());
                    methodIL.Emit(OpCodes.Stloc,temp1);
                    methodIL.Emit(OpCodes.Ldloca_S,temp1);
                    methodIL.Emit(OpCodes.Call, propertyInfo.PropertyType.GetMethod("get_HasValue", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Ldc_I4_0);
                    methodIL.Emit(OpCodes.Ceq);
                    methodIL.Emit(OpCodes.Brfalse_S, elseCase);
                    WriteString(methodIL, "null");
                    methodIL.Emit(OpCodes.Br_S, endCase);
                    methodIL.MarkLabel(elseCase);
                    methodIL.Emit(OpCodes.Ldarg_1);
                    methodIL.Emit(OpCodes.Ldarg_0);
                    methodIL.Emit(OpCodes.Callvirt,propertyInfo.GetGetMethod());
                    methodIL.Emit(OpCodes.Stloc,temp1);
                    methodIL.Emit(OpCodes.Ldloca_S,temp1);
                    methodIL.Emit(OpCodes.Call, propertyInfo.PropertyType.GetMethod("get_Value", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Stloc, temp);
                    methodIL.Emit(OpCodes.Ldloca_S, temp);
                    methodIL.Emit(OpCodes.Call, propertyType.GetMethod("ToString", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                    methodIL.MarkLabel(endCase);
                }
                else
                {
                    var temp = methodIL.DeclareLocal(propertyInfo.PropertyType);
                    WriteComma(methodIL, ref isFirst);
                    WriteString(methodIL, "\"" + propertyInfo.Name + "\":");
                    methodIL.Emit(OpCodes.Ldarg_1);
                    methodIL.Emit(OpCodes.Ldarg_0);
                    methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                    methodIL.Emit(OpCodes.Stloc,temp);
                    methodIL.Emit(OpCodes.Ldloca_S,temp);
                    methodIL.Emit(OpCodes.Call, propertyType.GetMethod("ToString", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                }
                #endregion
            }
            else if (propertyType == typeof(System.DateTime)
                )
            {
                #region
                if (isNullable)
                {
                    var elseCase = methodIL.DefineLabel();
                    var endCase = methodIL.DefineLabel();
                    var longTime = methodIL.DeclareLocal(typeof(System.Int64));
                    var temp = methodIL.DeclareLocal(propertyType);
                    var temp1 = methodIL.DeclareLocal(propertyInfo.PropertyType);
                    WriteComma(methodIL, ref isFirst);
                    WriteString(methodIL, "\"" + propertyInfo.Name + "\":");
                    methodIL.Emit(OpCodes.Ldarg_0);
                    methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                    methodIL.Emit(OpCodes.Stloc, temp1);
                    methodIL.Emit(OpCodes.Ldloca_S, temp1);
                    methodIL.Emit(OpCodes.Call, propertyInfo.PropertyType.GetMethod("get_HasValue", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Ldc_I4_0);
                    methodIL.Emit(OpCodes.Ceq);
                    methodIL.Emit(OpCodes.Brfalse_S, elseCase);
                    WriteString(methodIL, "null");
                    methodIL.Emit(OpCodes.Br_S, endCase);
                    methodIL.MarkLabel(elseCase);

                    if (dateTimeFormat == DateTimeFormat.LocalTimeNumber)
                    {
                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                        methodIL.Emit(OpCodes.Stloc, temp1);
                        methodIL.Emit(OpCodes.Ldloca_S, temp1);
                        methodIL.Emit(OpCodes.Call, propertyInfo.PropertyType.GetMethod("get_Value", Type.EmptyTypes));
                        methodIL.Emit(OpCodes.Call, GetLocalTimeNumberMethodInfo);
                        methodIL.Emit(OpCodes.Stloc, longTime);
                        methodIL.Emit(OpCodes.Ldloca_S, longTime);
                        methodIL.Emit(OpCodes.Call, typeof(System.Int64).GetMethod("ToString", Type.EmptyTypes));
                        methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                    }
                    else if (dateTimeFormat == DateTimeFormat.UTCTimeNumber)
                    {
                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                        methodIL.Emit(OpCodes.Stloc, temp1);
                        methodIL.Emit(OpCodes.Ldloca_S, temp1);
                        methodIL.Emit(OpCodes.Call, propertyInfo.PropertyType.GetMethod("get_Value", Type.EmptyTypes));
                        methodIL.Emit(OpCodes.Call, GetUTCTimeNumberMethodInfo);
                        methodIL.Emit(OpCodes.Stloc, longTime);
                        methodIL.Emit(OpCodes.Ldloca_S, longTime);
                        methodIL.Emit(OpCodes.Call, typeof(System.Int64).GetMethod("ToString", Type.EmptyTypes));
                        methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                    }
                    else if (dateTimeFormat == DateTimeFormat.LocalTimeString)
                    {
                        WriteString(methodIL, "\"");
                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                        methodIL.Emit(OpCodes.Stloc, temp1);
                        methodIL.Emit(OpCodes.Ldloca_S, temp1);
                        methodIL.Emit(OpCodes.Call, propertyInfo.PropertyType.GetMethod("get_Value", Type.EmptyTypes));
                        methodIL.Emit(OpCodes.Call, GetLocalTimeStringMethodInfo);
                        methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                        WriteString(methodIL, "\"");
                    }
                    else if (dateTimeFormat == DateTimeFormat.UTCTimeString)
                    {
                        WriteString(methodIL, "\"");
                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                        methodIL.Emit(OpCodes.Stloc, temp1);
                        methodIL.Emit(OpCodes.Ldloca_S, temp1);
                        methodIL.Emit(OpCodes.Call, propertyInfo.PropertyType.GetMethod("get_Value", Type.EmptyTypes));
                        methodIL.Emit(OpCodes.Call, GetUTCTimeStringMethodInfo);
                        methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                        WriteString(methodIL, "\"");
                    }
                    methodIL.MarkLabel(endCase);
                }
                else
                {
                    var longTime = methodIL.DeclareLocal(typeof(System.Int64));
                    WriteComma(methodIL, ref isFirst);
                    WriteString(methodIL, "\"" + propertyInfo.Name + "\":");
                   
                    if (dateTimeFormat == DateTimeFormat.LocalTimeNumber)
                    {
                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                        methodIL.Emit(OpCodes.Call, GetLocalTimeNumberMethodInfo);
                        methodIL.Emit(OpCodes.Stloc_S, longTime);
                        methodIL.Emit(OpCodes.Ldloca_S, longTime);
                        methodIL.Emit(OpCodes.Call, typeof(System.Int64).GetMethod("ToString", Type.EmptyTypes));
                        methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                    }
                    else if (dateTimeFormat == DateTimeFormat.UTCTimeNumber)
                    {
                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                        methodIL.Emit(OpCodes.Call, GetUTCTimeNumberMethodInfo);
                        methodIL.Emit(OpCodes.Stloc_S, longTime);
                        methodIL.Emit(OpCodes.Ldloca_S, longTime);
                        methodIL.Emit(OpCodes.Call, typeof(System.Int64).GetMethod("ToString", Type.EmptyTypes));
                        methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                    }
                    else if (dateTimeFormat == DateTimeFormat.LocalTimeString)
                    {
                        WriteString(methodIL, "\"");
                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                        methodIL.Emit(OpCodes.Call, GetLocalTimeStringMethodInfo);
                        methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                        WriteString(methodIL, "\"");
                    }
                    else if (dateTimeFormat == DateTimeFormat.UTCTimeString)
                    {
                        WriteString(methodIL, "\"");
                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                        methodIL.Emit(OpCodes.Call, GetUTCTimeStringMethodInfo);
                        methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                        WriteString(methodIL, "\"");
                    }

                }
                #endregion
            }
            else if (propertyType == typeof(System.Boolean))
            {
                var temp = methodIL.DeclareLocal(propertyInfo.PropertyType);
                WriteComma(methodIL, ref isFirst);
                WriteString(methodIL, "\"" + propertyInfo.Name + "\":");
                if (isNullable)
                {
                    var isNullElseCase = methodIL.DefineLabel();
                    var isNullEndCase = methodIL.DefineLabel();
                    methodIL.Emit(OpCodes.Ldarg_0);
                    methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                    methodIL.Emit(OpCodes.Stloc_S,temp);
                    methodIL.Emit(OpCodes.Ldloca_S,temp);
                    methodIL.Emit(OpCodes.Call, propertyInfo.PropertyType.GetMethod("get_HasValue", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Ldc_I4_0);
                    methodIL.Emit(OpCodes.Ceq);
                    methodIL.Emit(OpCodes.Brfalse_S, isNullElseCase);
                    {
                        WriteString(methodIL, "null");
                    }
                    methodIL.Emit(OpCodes.Br_S, isNullEndCase);
                    methodIL.MarkLabel(isNullElseCase);
                    {
                        var elseCase = methodIL.DefineLabel();
                        var endCase = methodIL.DefineLabel();
                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                        methodIL.Emit(OpCodes.Stloc_S, temp);
                        methodIL.Emit(OpCodes.Ldloca_S, temp);
                        methodIL.Emit(OpCodes.Call, propertyInfo.PropertyType.GetMethod("get_Value", Type.EmptyTypes));
                        methodIL.Emit(OpCodes.Brtrue_S, elseCase);
                        {
                            methodIL.Emit(OpCodes.Ldstr, "false");
                        }
                        methodIL.Emit(OpCodes.Br_S, endCase);
                        methodIL.MarkLabel(elseCase);
                        {
                            methodIL.Emit(OpCodes.Ldstr, "true");
                        }
                        methodIL.MarkLabel(endCase);
                        methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                    }
                    methodIL.MarkLabel(isNullEndCase);
                }
                else
                {
                    var elseCase = methodIL.DefineLabel();
                    var endCase = methodIL.DefineLabel();
                    methodIL.Emit(OpCodes.Ldarg_1);
                    methodIL.Emit(OpCodes.Ldarg_0);
                    methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                    methodIL.Emit(OpCodes.Brtrue_S, elseCase);
                    methodIL.Emit(OpCodes.Ldstr, "false");
                    methodIL.Emit(OpCodes.Br_S, endCase);
                    methodIL.MarkLabel(elseCase);
                    methodIL.Emit(OpCodes.Ldstr, "true");
                    methodIL.MarkLabel(endCase);
                    methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                }
            }
        }
        public static void WriteField(ILGenerator methodIL, System.Reflection.FieldInfo fieldInfo,ref bool isFirst, DateTimeFormat dateTimeFormat)
        {
            Type fieldType = fieldInfo.FieldType;
            bool isNullable = false;
            if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                isNullable = true;
                fieldType = fieldInfo.FieldType.GetGenericArguments()[0];
            }
            if (fieldType == typeof(System.String))
            {
                WriteComma(methodIL, ref isFirst);
                var elseCase = methodIL.DefineLabel();
                var endCase = methodIL.DefineLabel();
                WriteString(methodIL, "\"" + fieldInfo.Name + "\":");
                methodIL.Emit(OpCodes.Ldarg_0);
                methodIL.Emit(OpCodes.Ldfld, fieldInfo);
                methodIL.Emit(OpCodes.Ldnull);
                methodIL.Emit(OpCodes.Ceq);
                methodIL.Emit(OpCodes.Brfalse_S, elseCase);
                WriteString(methodIL, "null");
                methodIL.Emit(OpCodes.Br_S, endCase);
                methodIL.MarkLabel(elseCase);
                WriteString(methodIL, "\"");
                methodIL.Emit(OpCodes.Ldarg_1);
                methodIL.Emit(OpCodes.Ldarg_0);
                methodIL.Emit(OpCodes.Ldfld, fieldInfo);
                methodIL.Emit(OpCodes.Call, WriteJsonReverseStringMethodInfo);
                WriteString(methodIL, "\"");
                methodIL.MarkLabel(endCase);
            }
            else if (fieldType == typeof(System.Byte[]))
            {
                var elseCase = methodIL.DefineLabel();
                var endCase = methodIL.DefineLabel();
                WriteComma(methodIL,ref isFirst);
                WriteString(methodIL,"\""+fieldInfo.Name+"\":");
                methodIL.Emit(OpCodes.Ldarg_0);
                methodIL.Emit(OpCodes.Ldfld,fieldInfo);
                methodIL.Emit(OpCodes.Ldnull);
                methodIL.Emit(OpCodes.Ceq);
                methodIL.Emit(OpCodes.Brfalse_S,elseCase);
                WriteString(methodIL,"null");
                methodIL.Emit(OpCodes.Br_S,endCase);
                methodIL.MarkLabel(elseCase);
                WriteString(methodIL,"\"");
                methodIL.Emit(OpCodes.Ldarg_1);
                methodIL.Emit(OpCodes.Ldarg_0);
                methodIL.Emit(OpCodes.Ldfld,fieldInfo);
                methodIL.Emit(OpCodes.Call, toBase64StringMethodInfo);
                methodIL.Emit(OpCodes.Callvirt,writeMethodInfo);
                WriteString(methodIL, "\"");
                methodIL.MarkLabel(endCase);
            }
            else if (fieldType == typeof(System.Char) ||
                fieldType == typeof(System.Guid))
            {
                if (isNullable)
                {
                    var elseCase = methodIL.DefineLabel();
                    var endCase = methodIL.DefineLabel();
                    var temp = methodIL.DeclareLocal(fieldType);
                    WriteComma(methodIL, ref isFirst);
                    WriteString(methodIL, "\"" + fieldInfo.Name + "\":");
                    methodIL.Emit(OpCodes.Ldarg_0);
                    methodIL.Emit(OpCodes.Ldflda, fieldInfo);
                    methodIL.Emit(OpCodes.Call, fieldInfo.FieldType.GetMethod("get_HasValue", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Ldc_I4_0);
                    methodIL.Emit(OpCodes.Ceq);
                    methodIL.Emit(OpCodes.Brfalse_S, elseCase);
                    WriteString(methodIL, "null");
                    methodIL.Emit(OpCodes.Br_S, endCase);
                    methodIL.MarkLabel(elseCase);
                    WriteString(methodIL, "\"");
                    methodIL.Emit(OpCodes.Ldarg_1);
                    methodIL.Emit(OpCodes.Ldarg_0);
                    methodIL.Emit(OpCodes.Ldflda, fieldInfo);
                    methodIL.Emit(OpCodes.Call, fieldInfo.FieldType.GetMethod("get_Value", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Stloc, temp);
                    methodIL.Emit(OpCodes.Ldloca_S, temp);
                    methodIL.Emit(OpCodes.Call, fieldType.GetMethod("ToString", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                    WriteString(methodIL, "\"");
                    methodIL.MarkLabel(endCase);
                }
                else
                {
                    WriteComma(methodIL, ref isFirst);
                    WriteString(methodIL, "\"" + fieldInfo.Name + "\":");
                    WriteString(methodIL, "\"");
                    methodIL.Emit(OpCodes.Ldarg_1);
                    methodIL.Emit(OpCodes.Ldarg_0);
                    methodIL.Emit(OpCodes.Ldflda, fieldInfo);
                    methodIL.Emit(OpCodes.Call, fieldType.GetMethod("ToString", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                    WriteString(methodIL, "\"");
                }
            }
            else if (fieldType == typeof(System.Int32) ||
                fieldType == typeof(System.Int16) ||
                fieldType == typeof(System.Int64) ||
                fieldType == typeof(System.UInt32) ||
                fieldType == typeof(System.UInt16) ||
                fieldType == typeof(System.UInt64) ||
                fieldType == typeof(System.Byte) ||
                fieldType == typeof(System.SByte) ||
                fieldType == typeof(System.Single) ||
                fieldType == typeof(System.Double) ||
                fieldType == typeof(System.Decimal))
            {
                if (isNullable)
                {
                    var elseCase = methodIL.DefineLabel();
                    var endCase = methodIL.DefineLabel();
                    var temp = methodIL.DeclareLocal(fieldType);
                    WriteComma(methodIL, ref isFirst);
                    WriteString(methodIL, "\"" + fieldInfo.Name + "\":");
                    methodIL.Emit(OpCodes.Ldarg_0);
                    methodIL.Emit(OpCodes.Ldflda, fieldInfo);
                    methodIL.Emit(OpCodes.Call, fieldInfo.FieldType.GetMethod("get_HasValue", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Ldc_I4_0);
                    methodIL.Emit(OpCodes.Ceq);
                    methodIL.Emit(OpCodes.Brfalse_S, elseCase);
                    WriteString(methodIL, "null");
                    methodIL.Emit(OpCodes.Br_S, endCase);
                    methodIL.MarkLabel(elseCase);
                    methodIL.Emit(OpCodes.Ldarg_1);
                    methodIL.Emit(OpCodes.Ldarg_0);
                    methodIL.Emit(OpCodes.Ldflda, fieldInfo);
                    methodIL.Emit(OpCodes.Call, fieldInfo.FieldType.GetMethod("get_Value", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Stloc, temp);
                    methodIL.Emit(OpCodes.Ldloca_S, temp);
                    methodIL.Emit(OpCodes.Call, fieldType.GetMethod("ToString", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                    methodIL.MarkLabel(endCase);
                }
                else
                {
                    WriteComma(methodIL, ref isFirst);
                    WriteString(methodIL, "\"" + fieldInfo.Name + "\":");
                    methodIL.Emit(OpCodes.Ldarg_1);
                    methodIL.Emit(OpCodes.Ldarg_0);
                    methodIL.Emit(OpCodes.Ldflda, fieldInfo);
                    methodIL.Emit(OpCodes.Call, fieldType.GetMethod("ToString", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                }
            }
            else if (fieldType == typeof(System.DateTime)
                )
            {
                if (isNullable)
                {
                    var elseCase = methodIL.DefineLabel();
                    var endCase = methodIL.DefineLabel();
                    var longTime = methodIL.DeclareLocal(typeof(System.Int64));
                    var temp = methodIL.DeclareLocal(fieldType);
                    WriteComma(methodIL, ref isFirst);
                    WriteString(methodIL, "\"" + fieldInfo.Name + "\":");
                    methodIL.Emit(OpCodes.Ldarg_0);
                    methodIL.Emit(OpCodes.Ldflda, fieldInfo);
                    methodIL.Emit(OpCodes.Call, fieldInfo.FieldType.GetMethod("get_HasValue", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Ldc_I4_0);
                    methodIL.Emit(OpCodes.Ceq);
                    methodIL.Emit(OpCodes.Brfalse_S, elseCase);
                    WriteString(methodIL, "null");
                    methodIL.Emit(OpCodes.Br_S, endCase);
                    methodIL.MarkLabel(elseCase);

                    if (dateTimeFormat == DateTimeFormat.LocalTimeNumber)
                    {
                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Ldflda, fieldInfo);
                        methodIL.Emit(OpCodes.Call, fieldInfo.FieldType.GetMethod("get_Value", Type.EmptyTypes));
                        methodIL.Emit(OpCodes.Call, GetLocalTimeNumberMethodInfo);
                        methodIL.Emit(OpCodes.Stloc, longTime);
                        methodIL.Emit(OpCodes.Ldloca_S, longTime);
                        methodIL.Emit(OpCodes.Call, typeof(System.Int64).GetMethod("ToString", Type.EmptyTypes));
                        methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                    }
                    else if (dateTimeFormat == DateTimeFormat.UTCTimeNumber)
                    {
                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Ldflda, fieldInfo);
                        methodIL.Emit(OpCodes.Call, fieldInfo.FieldType.GetMethod("get_Value", Type.EmptyTypes));
                        methodIL.Emit(OpCodes.Call, GetUTCTimeNumberMethodInfo);
                        methodIL.Emit(OpCodes.Stloc, longTime);
                        methodIL.Emit(OpCodes.Ldloca_S, longTime);
                        methodIL.Emit(OpCodes.Call, typeof(System.Int64).GetMethod("ToString", Type.EmptyTypes));
                        methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                    }
                    else if (dateTimeFormat == DateTimeFormat.LocalTimeString)
                    {
                        WriteString(methodIL, "\"");
                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Ldflda, fieldInfo);
                        methodIL.Emit(OpCodes.Call, fieldInfo.FieldType.GetMethod("get_Value", Type.EmptyTypes));
                        methodIL.Emit(OpCodes.Call, GetLocalTimeStringMethodInfo);
                        methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                        WriteString(methodIL, "\"");
                    }
                    else if (dateTimeFormat == DateTimeFormat.UTCTimeString)
                    {
                        WriteString(methodIL, "\"");
                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Ldflda, fieldInfo);
                        methodIL.Emit(OpCodes.Call, fieldInfo.FieldType.GetMethod("get_Value", Type.EmptyTypes));
                        methodIL.Emit(OpCodes.Call, GetUTCTimeStringMethodInfo);
                        methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                        WriteString(methodIL, "\"");
                    }
                    methodIL.MarkLabel(endCase);
                }
                else
                {
                    var longTime = methodIL.DeclareLocal(typeof(System.Int64));
                    WriteComma(methodIL, ref isFirst);
                    WriteString(methodIL, "\"" + fieldInfo.Name + "\":");

                    if (dateTimeFormat == DateTimeFormat.LocalTimeNumber)
                    {
                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Ldfld, fieldInfo);
                        methodIL.Emit(OpCodes.Call, GetLocalTimeNumberMethodInfo);
                        methodIL.Emit(OpCodes.Stloc_S, longTime);
                        methodIL.Emit(OpCodes.Ldloca_S, longTime);
                        methodIL.Emit(OpCodes.Call, typeof(System.Int64).GetMethod("ToString", Type.EmptyTypes));
                        methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                    }
                    else if (dateTimeFormat == DateTimeFormat.UTCTimeNumber)
                    {
                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Ldfld, fieldInfo);
                        methodIL.Emit(OpCodes.Call, GetUTCTimeNumberMethodInfo);
                        methodIL.Emit(OpCodes.Stloc_S, longTime);
                        methodIL.Emit(OpCodes.Ldloca_S, longTime);
                        methodIL.Emit(OpCodes.Call, typeof(System.Int64).GetMethod("ToString", Type.EmptyTypes));
                        methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                    }
                    else if (dateTimeFormat == DateTimeFormat.LocalTimeString)
                    {
                        WriteString(methodIL, "\"");
                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Ldfld, fieldInfo);
                        methodIL.Emit(OpCodes.Call, GetLocalTimeStringMethodInfo);
                        methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                        WriteString(methodIL, "\"");
                    }
                    else if (dateTimeFormat == DateTimeFormat.UTCTimeString)
                    {
                        WriteString(methodIL, "\"");
                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Ldfld, fieldInfo);
                        methodIL.Emit(OpCodes.Call, GetUTCTimeStringMethodInfo);
                        methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                        WriteString(methodIL, "\"");
                    }

                }
            }
            else if (fieldType == typeof(System.Boolean))
            {
                WriteComma(methodIL, ref isFirst);
                WriteString(methodIL, "\"" + fieldInfo.Name + "\":");
                if (isNullable)
                {
                    var isNullElseCase = methodIL.DefineLabel();
                    var isNullEndCase = methodIL.DefineLabel();
                    methodIL.Emit(OpCodes.Ldarg_0);
                    methodIL.Emit(OpCodes.Ldflda, fieldInfo);
                    methodIL.Emit(OpCodes.Call, fieldInfo.FieldType.GetMethod("get_HasValue", Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Ldc_I4_0);
                    methodIL.Emit(OpCodes.Ceq);
                    methodIL.Emit(OpCodes.Brfalse_S, isNullElseCase);
                    {
                        WriteString(methodIL, "null");
                    }
                    methodIL.Emit(OpCodes.Br_S, isNullEndCase);
                    methodIL.MarkLabel(isNullElseCase);
                    {
                        var elseCase = methodIL.DefineLabel();
                        var endCase = methodIL.DefineLabel();
                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Ldflda, fieldInfo);
                        methodIL.Emit(OpCodes.Call, fieldInfo.FieldType.GetMethod("get_Value", Type.EmptyTypes));
                        methodIL.Emit(OpCodes.Brtrue_S, elseCase);
                        {
                            methodIL.Emit(OpCodes.Ldstr, "false");
                        }
                        methodIL.Emit(OpCodes.Br_S, endCase);
                        methodIL.MarkLabel(elseCase);
                        {
                            methodIL.Emit(OpCodes.Ldstr, "true");
                        }
                        methodIL.MarkLabel(endCase);
                        methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                    }
                    methodIL.MarkLabel(isNullEndCase);
                }
                else
                {
                    var elseCase = methodIL.DefineLabel();
                    var endCase = methodIL.DefineLabel();
                    methodIL.Emit(OpCodes.Ldarg_1);
                    methodIL.Emit(OpCodes.Ldarg_0);
                    methodIL.Emit(OpCodes.Ldfld, fieldInfo);
                    methodIL.Emit(OpCodes.Brtrue_S, elseCase);
                    methodIL.Emit(OpCodes.Ldstr, "false");
                    methodIL.Emit(OpCodes.Br_S, endCase);
                    methodIL.MarkLabel(elseCase);
                    methodIL.Emit(OpCodes.Ldstr, "true");
                    methodIL.MarkLabel(endCase);
                    methodIL.Emit(OpCodes.Callvirt, writeMethodInfo);
                }
            }
        }
    }
    public class Model
    {
        public string a { get; set; }
        public byte[] b4 { get; set; }
        public int b { get; set; }
        public char b1 { get; set; }
        public DateTime b2 { get; set; }
        public bool b3 { get; set; }
        public int? c { get; set; }
        public char? c1 { get; set; }
        public DateTime? c2 { get; set; }
        public bool? c3 { get; set;}
    }
    public class JsonTest
    {
        public static void Write(Model model,NFinal.IO.IWriter writer,DateTimeFormat dateTimeFormat)
        {
            writer.Write("{");
            writer.Write("\"a\":");
            if (model.a == null)
            {
                writer.Write("null");
            }
            else
            {
                writer.WriteJsonReverseString(model.a);
            }
            writer.Write(",");
            writer.Write("\"b\":");
            writer.Write(model.b.ToString());
            writer.Write(",");
            writer.Write("\"c\":");
            if (model.c == null)
            {
                writer.Write("null");
            }
            else
            {
                writer.Write(((Int32)model.c).ToString());
            }
            writer.Write(",");
            writer.Write("\"b1\":");
            writer.Write("\"");
            writer.Write(model.b1.ToString());
            writer.Write("\"");

            writer.Write(",");
            writer.Write("\"c1\":");
            if (model.c1 == null)
            {
                writer.Write("null");
            }
            else
            {
                writer.Write("\"");
                writer.Write(((Char)model.c1).ToString());
                writer.Write("\"");
            }
            writer.Write(",");
            writer.Write("\"b2\":");
            writer.Write(JsonHelper.GetLocalTimeString(model.b2));
            writer.Write(",");
            writer.Write("\"c2\":");
            if (model.c2 == null)
            {
                writer.Write("null");
            }
            else
            {
                writer.Write(JsonHelper.GetLocalTimeString((DateTime)model.c2));
            }
            writer.Write(",");
            writer.Write("\"b3\":");
            writer.Write(model.b3?"true":"false");
            if (model.c3 == null)
            {
                writer.Write("null");
            }
            else
            {
                writer.Write((System.Boolean)model.c3?"true":"false");
            }
            writer.Write(",");
            writer.Write("\"b4\":");
            if (model.b4 == null)
            {
                writer.Write("null");
            }
            else
            {
                writer.Write("\"");
                writer.Write(Convert.ToBase64String(model.b4));
                writer.Write("\"");
            }
            writer.Write("}");
        }
    }
}
