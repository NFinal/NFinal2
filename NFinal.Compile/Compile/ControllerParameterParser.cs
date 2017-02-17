using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Compile
{
    public class ControllerParameterParser
    {
        /// <summary>
        /// 转换GET参数的代码
        /// </summary>
        /// <param name="type">参数中的类型</param>
        /// <param name="name">参数名</param>
        /// <returns></returns>
        public string BuildGetParameterCode(string app, string type, bool isArray, string name, bool hasDefaultValue, string defaultValue)
        {
            string code = "";
            if (type.StartsWith("System."))
            {
                //去除前面的System.
                type = type.Remove(0, 7);
            }
            if (!isArray)
            {
                switch (type)
                {
                    case "SByte": goto case "sbyte";
                    case "sbyte": code = string.Format("sbyte {0}=sbyte.TryParse(get[\"{0}\"],out {0})?{0}:{1};", name, hasDefaultValue ? defaultValue : "(sbyte)0"); break;
                    case "Int16": goto case "short";
                    case "short": code = string.Format("short {0}=short.TryParse(get[\"{0}\"],out {0})?{0}:{1};", name, hasDefaultValue ? defaultValue : "(short)0"); break;
                    case "Int32": goto case "int";
                    case "int": code = string.Format("int {0}=int.TryParse(get[\"{0}\"],out {0})?{0}:{1};", name, hasDefaultValue ? defaultValue : "0"); break;
                    case "Int64": goto case "long";
                    case "long": code = string.Format("long {0}=long.TryParse(get[\"{0}\"],out {0})?{0}:{1};", name, hasDefaultValue ? defaultValue : "0"); break;
                    case "Byte": goto case "byte";
                    case "byte": code = string.Format("byte {0}=byte.TryParse(get[\"{0}\"],out {0})?{0}:{1};", name, hasDefaultValue ? defaultValue : "(byte)0"); break;
                    case "UInt16": goto case "ushort";
                    case "ushort": code = string.Format("ushort {0}=ushort.TryParse(get[\"{0}\"],out {0})?{0}:{1};", name, hasDefaultValue ? defaultValue : "(ushort)0"); break;
                    case "UInt32": goto case "uint";
                    case "uint": code = string.Format("uint {0}=uint.TryParse(get[\"{0}\"],out {0})?{0}:{1};", name, hasDefaultValue ? defaultValue : "(uint)0"); break;
                    case "UInt64": goto case "ulong";
                    case "ulong": code = string.Format("ulong {0}=ulong.TryParse(get[\"{0}\"],out {0})?{0}:{1};", name, hasDefaultValue ? defaultValue : "0"); break;
                    case "Single": goto case "float";
                    case "float": code = string.Format("float {0}=float.TryParse(get[\"{0}\"],out {0})?{0}:{1};", name, hasDefaultValue ? defaultValue : "0"); break;
                    case "Double": goto case "double";
                    case "double": code = string.Format("double {0}=double.TryParse(get[\"{0}\"],out {0})?{0}:{1};", name, hasDefaultValue ? defaultValue : "0"); break;
                    case "Decimal": goto case "decimal";
                    case "decimal": code = string.Format("decimal {0}=decimal.TryParse(get[\"{0}\"],out {0})?{0}:{1};", name, hasDefaultValue ? defaultValue : "0"); break;
                    case "Boolean": goto case "bool";
                    case "bool": code = string.Format("bool {0}=bool.TryParse(get[\"{0}\"],out {0})?{0}:{1};", name, hasDefaultValue ? defaultValue : "false"); break;
                    case "Char": goto case "Char";
                    case "char": code = string.Format("char {0}=char.TryParse(get[\"{0}\"],out {0})?{0}:{1};", name, hasDefaultValue ? defaultValue : "\' \'"); break;
                    case "String": goto case "string";
                    case "string": code = string.Format("string {0}=get[\"{0}\"];", name, hasDefaultValue ? defaultValue : "null"); break;
                    case "DateTime": code = string.Format("DateTime {0}=DateTime.TryParse(get[\"{0}\"],out {0})?{0}:{1};", name, hasDefaultValue ? defaultValue : "DateTime.Now"); break;
                    case "TimeSpan": code = string.Format("TimeSpan {0}=TimeSpan.TryParse(get[\"{0}\"],out {0})?{0}:{1};", name, hasDefaultValue ? defaultValue : "TimeSpan.Zero"); break;
                    default:
                        if (type.StartsWith(app + ".Models.Entity"))
                        {
                            code = string.Format("{0} {1}={0}.TryParse(get,out {1})?{1}:{2};", type, name, hasDefaultValue ? defaultValue : "null"); break;
                        }
                        else if (type.StartsWith("Models.Entity"))
                        {
                            code = string.Format("{3}.{0} {1}={3}.{0}.TryParse(get,out {1})?{1}:{2};", type, name, hasDefaultValue ? defaultValue : "null", app); break;
                        }
                        else
                        {
                            break;
                        }
                }
            }
            else
            {
                switch (type)
                {
                    case "SByte": goto case "sbyte";
                    case "sbyte": code = string.Format("sbyte[] {0}=get[\"{0}\"]==null?{1}:Array.Convert.All<string,sbyte>(get[\"{0}\"].Split(','),delegate(string __s__){sbyte __temp__;sbyte.TryParse(__s__,out __temp__);return __temp__;});", name, hasDefaultValue ? defaultValue :"null"); break;
                    case "Int16": goto case "short";
                    case "short": code = string.Format("short[] {0}=get[\"{0}\"]==null?{1}:Array.Convert.All<string,short>(get[\"{0}\"].Split(','),delegate(string __s__){short __temp__;short.TryParse(__s__,out __temp__);return __temp__;});", name, hasDefaultValue ? defaultValue :"null"); break;
                    case "Int32": goto case "int";
                    case "int": code = string.Format("int[] {0}=get[\"{0}\"]==null?{1}:Array.Convert.All<string,int>(get[\"{0}\"].Split(','),delegate(string __s__){int __temp__;int.TryParse(__s__,out __temp__);return __temp__;});", name, hasDefaultValue ? defaultValue :"null"); break;
                    case "Int64": goto case "long";
                    case "long": code = string.Format("long[] {0}=get[\"{0}\"]==null?{1}:Array.Convert.All<string,long>(get[\"{0}\"].Split(','),delegate(string __s__){long __temp__;long.TryParse(__s__,out __temp__);return __temp__;});", name, hasDefaultValue ? defaultValue :"null"); break;
                    case "Byte": goto case "byte";
                    case "byte": code = string.Format("byte[] {0}=get[\"{0}\"]==null?{1}:Array.Convert.All<string,byte>(get[\"{0}\"].Split(','),delegate(string __s__){byte __temp__;byte.TryParse(__s__,out __temp__);return __temp__;});", name, hasDefaultValue ? defaultValue :"null"); break;
                    case "UInt16": goto case "ushort";
                    case "ushort": code = string.Format("ushort[] {0}=get[\"{0}\"]==null?{1}:Array.Convert.All<string,ushort>(get[\"{0}\"].Split(','),delegate(string __s__){ushort __temp__;ushort.TryParse(__s__,__temp__);return __temp__;});", name, hasDefaultValue ? defaultValue :"null"); break;
                    case "UInt32": goto case "uint";
                    case "uint": code = string.Format("uint[] {0}=get[\"{0}\"]==null?{1}:Array.Convert.All<string,ushort>(get[\"{0}\"].Split(','),delegate(string __s__){unit __temp__;uint.TryParse(__s__,__temp__);return __temp__;});", name, hasDefaultValue ? defaultValue : "null"); break;
                    case "UInt64": goto case "ulong";
                    case "ulong": code = string.Format("ulong[] {0}=get[\"{0}\"]==null?{1}:Array.Convert.All<string,ulong>(get[\"{0}\"].Split(','),delegate(string __s__){ulong __temp__;ulong.TryParse(__s__,out __temp__);return __temp__;});", name, hasDefaultValue ? defaultValue :"null"); break;
                    case "Single": goto case "float";
                    case "float": code = string.Format("float[] {0}=get[\"{0}\"]==null?{1}:Array.Convert.All<string,float>(get[\"{0}\"].Split(','),delegate(string __s__){float __temp__;float.TryParse(__s__,out __temp__);return __temp__;});", name, hasDefaultValue ? defaultValue :"null"); break;
                    case "Double": goto case "double";
                    case "double": code = string.Format("double[] {0}=get[\"{0}\"]==null?{1}:Array.Convert.All<string,double>(get[\"{0}\"].Split(','),delegate(string __s__){double __temp__;double.TryParse(__s__,out __temp__);return __temp__;});", name, hasDefaultValue ? defaultValue :"null"); break;
                    case "Decimal": goto case "decimal";
                    case "decimal": code = string.Format("decimal[] {0}=get[\"{0}\"]==null?{1}:Array.Convert.All<string,decimal>(get[\"{0}\"].Split(','),delegate(string __s__){decimal __temp__;decimal.TryParse(__s__,out __temp__);return __temp__;});", name, hasDefaultValue ? defaultValue : "null"); break;
                    case "Boolean": goto case "bool";
                    case "bool": code = string.Format("bool[] {0}==get[\"{0}\"]==null?{1}:Array.Convert.All<string,bool>(get[\"{0}\"].Split(','),delegate(string __s__){bool __temp__;bool.TryParse(__s__,out __temp__);return __temp__;});", name, hasDefaultValue ? defaultValue : "null"); break;
                    case "Char": goto case "Char";
                    case "char": code = string.Format("char[] {0}==get[\"{0}\"]==null?{1}:Array.Convert.All<string char>(get[\"{0}\"].Split(','),delegate(string __s__){char __temp__;char.TryParse(__s__,out __temp__);return __temp__;});", name, hasDefaultValue ? defaultValue : "null"); break;
                    case "String": goto case "string";
                    case "string": code = string.Format("string[] {0}=get[\"{0}\"]==null?{1}:get[\"{0}\"].Split(',');", name, hasDefaultValue ? defaultValue : "null"); break;
                    case "DateTime": code = string.Format("DateTime[] {0}=get[\"{0}\"]==null?{1}:Array.Convert.All<string,DateTime>(get[\"{0}\"].Split(','),delegate(string __s__){Datetime __temp__;DateTime.TryParse(__s__,out __temp__);return __temp__;});", name, hasDefaultValue ? defaultValue : "null"); break;
                    case "TimeSpan": code = string.Format("TimeSpan[] {0}=get[\"{0}\"]==null?{1}:Array.Convert.All<string,TimeSpan>(get[\"{0}\"].Split(','),delegate(string __s__){TimeSpan __temp__;TimeSpan.TryParse(__s__,out __temp__);return __temp__;});", name, hasDefaultValue ? defaultValue : "null"); break;
                    default: code = string.Format("{0}[] {1}=get[\"{1}\"]==null?{2}:Array.Convert.All<string,{0}>(get[\"{1}\"].Split(','),delegate(string __s__){{0} {1};{0}.TryParse(__s__,out __temp__);return __temp__;});", type, name, hasDefaultValue ? defaultValue : "null"); break;
                }
            }
            return code;
        }
    }
}
