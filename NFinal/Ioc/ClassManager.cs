using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using Dapper;

namespace NFinal.Ioc
{
#if EMITDEBUG
    public class MethodData
    {
        public Type classType;
        public MethodInfo methodInfo;
        public ParameterInfo[] parameters;
        public Type returnType;
        public Delegate executeMethodDelegate;
    }
    public class PropertyData
    {

    }
    public class MemberData
    {

    }
    public delegate TReturn MethodDelegate<TReturn, TParam>(TParam param);
    public delegate TReturn SimpleMethodDelegate<TReturn>();
    public delegate TValue GetPropertyDelegate<TValue>();
    public delegate TValue SetPropertyDelegate<TValue>(TValue value);

    public class ClassA
    {
        public string A { get; set; }
        public void Main()
        {

        }
    }
    public class VirtualInstance
    {
        public object value;
        public string virtualInstance;
        public TReturn Execute<TReturn,TParam>(string method, TParam param)
        {
            return default(TReturn);
        }
    }
    public class VirtualInterfaceManager
    {

        public static Dictionary<string, MethodData> classMethodDataDictionary = new Dictionary<string, MethodData>();
        public static Dictionary<Type, Dictionary<string, Delegate>> classGetPropertyDictionary = new Dictionary<Type, Dictionary<string, Delegate>>();
        public static Dictionary<Type, Dictionary<string, Delegate>> classSetPropertyDictionary = new Dictionary<Type, Dictionary<string, Delegate>>();
        public object GetInstance(string virtualInterface, object param)
        {
            
            return null;            
        }
        public TReturn ExecuteMethod<TReturn, TParam>(string virtualInterfaceAndMethodName, TParam param)
        {
            Type paramType = typeof(TParam);
            //if (paramType.)
            //{
            //    throw new ArgumentException("参数类型不能为匿名类型或者泛型！");
            //}
            MethodData methodData;
            if (classMethodDataDictionary.TryGetValue(virtualInterfaceAndMethodName, out methodData))
            {
                if (methodData.executeMethodDelegate == null)
                {
                    Type classType = methodData.classType;
                    
                    DynamicMethod dynamicMethod = new DynamicMethod("Invoke", methodData.returnType, new Type[] { paramType });
                    var methodIL = dynamicMethod.GetILGenerator();
                    var localClass = methodIL.DeclareLocal(classType);
                    methodIL.Emit(OpCodes.Newobj, classType.GetConstructor(Type.EmptyTypes));
                    methodIL.Emit(OpCodes.Stloc, localClass);
                    methodIL.Emit(OpCodes.Ldloc,localClass);
                    
                    var properties = paramType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    IDictionary<string, PropertyInfo> propertyDictionary = new Dictionary<string, PropertyInfo>(StringComparer.CurrentCultureIgnoreCase);
                    foreach (var property in properties)
                    {
                        propertyDictionary.Add(property.Name, property);
                    }
                    var fields = paramType.GetFields(BindingFlags.Instance | BindingFlags.Public);
                    IDictionary<string, FieldInfo> fieldDictionary = new Dictionary<string, FieldInfo>(StringComparer.CurrentCultureIgnoreCase);
                    foreach (var field in fields)
                    {
                        fieldDictionary.Add(field.Name, field);
                    }
                    PropertyInfo propertyInfo;
                    FieldInfo fieldInfo;
                    for (int i = 0; i < methodData.parameters.Length; i++)
                    {
                        string name = methodData.parameters[i].Name;
                        bool findProperty;
                        if (findProperty = propertyDictionary.TryGetValue(name, out propertyInfo))
                        {
                            methodIL.Emit(OpCodes.Ldarg_1);
                            methodIL.Emit(OpCodes.Callvirt, propertyInfo.GetGetMethod());
                        }
                        else if (fieldDictionary.TryGetValue(name, out fieldInfo))
                        {
                            methodIL.Emit(OpCodes.Ldarg_1);
                            methodIL.Emit(OpCodes.Ldfld,fieldInfo);
                        }
                        else
                        {
                            throw new ArgumentNullException($"类型为：{methodData.parameters[i].ParameterType},名称为：{name}的参数不存在");
                        }
                    }
                    methodIL.Emit(OpCodes.Callvirt,methodData.methodInfo);
                    methodIL.Emit(OpCodes.Ret);
                    methodData.executeMethodDelegate = dynamicMethod.CreateDelegate(typeof(MethodDelegate<,>).MakeGenericType(methodData.returnType, paramType));
                }
                return ((MethodDelegate<TReturn,TParam>)methodData.executeMethodDelegate)(param);
            }
            else
            {
                throw new ArgumentException($"指定的虚拟接口中的函数不存在！");
            }
            

        }
        public TValue GetProperty<TValue>(string propertyName)
        {
            return default(TValue);
        }
        public void SetProperty<TValue>(string propertyName, TValue value)
        {
           
        }
        public void Regist<TClass>(string virtualInterface)
        {
            Type classType= typeof(TClass);
            MethodInfo[] methods= classType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            Dictionary<string, MethodData> methodDataDictionary = new Dictionary<string, MethodData>();
            Delegate methodDelegate = null;
            foreach (var method in methods)
            {
                var parameters= method.GetParameters();
                if (parameters.Length == 1)
                {
                    MethodData methodData = new MethodData();
                    methodData.classType = classType;
                    methodData.methodInfo = method;
                    methodData.parameters = method.GetParameters();
                    methodData.returnType = method.ReturnType;

                    methodDataDictionary.Add(virtualInterface+":"+method.Name, methodData);
                }
            }
        }
    }
    public class par
    {
        public string a = "1";
        public int b { get; set; }
        public int c = 1;
        public string d { get; set; } = "2";
    }
    public class Test
    {
        public void Paa(Test test, par p)
        {
            test.Invoke(p.c, p.d);
            test.Invoke(p.b, p.a);
        }
        public void New(string virtualInstance,par p)
        {
            Test test = new Test();
            test.Invoke(p.c, p.d);
        }
        public void Invoke(int c, string d)
        {

        }
    }
#endif
}
