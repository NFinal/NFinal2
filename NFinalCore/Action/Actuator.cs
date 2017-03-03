using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using NFinal.Owin;
using System.Reflection;
using System.Reflection.Emit;

namespace NFinal.Action
{
    //public delegate void RunActionDelegate<TContext>(TContext context,NFinal.Middleware.ActionData<TContext> actionData);
    public class Actuator
    {
        public static FieldInfo actionDataEnvironmentFilters = typeof(NFinal.Middleware.ActionData<,>).GetField("IEnvironmentFilters");
        public static MethodInfo BaseFiltersMethodInfo = typeof(NFinal.Filter.FilterHelper).GetMethod("BaseFilter");//, new Type[] { typeof(NFinal.Filter.IEnvironmentFilter[]), typeof(IDictionary<string, object>) });
        public static FieldInfo actionDataRequestFilters = typeof(NFinal.Middleware.ActionData<,>).GetField("IRequestFilters");
        public static MethodInfo RequestFiltersMethodInfo = typeof(NFinal.Filter.FilterHelper).GetMethod("RequestFilter");//, new Type[] { typeof(NFinal.Filter.IRequestFilter[]), typeof(IDictionary<string, object>), typeof(NFinal.Owin.Request) });
        public static FieldInfo actionDataResponseFilters = typeof(NFinal.Middleware.ActionData<,>).GetField("IResponseFilters");
        public static MethodInfo ResponseFiltersMethodInfo = typeof(NFinal.Filter.FilterHelper).GetMethod("ResponseFilter");//, new Type[] { typeof(NFinal.Filter.IResponseFilter[]), typeof(NFinal.Owin.Response) });
        public static ConstructorInfo MemoryStreamConstructorInfo = typeof(System.IO.MemoryStream).GetConstructor(Type.EmptyTypes);
        //public static MethodInfo OwinActionInitializationMethodInfo = typeof(NFinal.OwinAction<,>).GetMethod("Initialization");//, new Type[] { typeof(IDictionary<string, object>), typeof(System.IO.Stream), typeof(NFinal.Owin.Request), typeof(NFinal.CompressMode) });
        //public static FieldInfo requestParametersFieldInfo = typeof(NFinal.Owin.Request).GetField("parameters");
        public static MethodInfo nameValueCollectionGetItemMethodInfo = typeof(NFinal.NameValueCollection).GetMethod("get_Item", new Type[] { typeof(string) });
        public static MethodInfo modelHelperGetModelMethodInfo = typeof(NFinal.Model.ModelHelper).GetMethod("GetModel");
        //public static MethodInfo getRequestMethodInfo = typeof(System.EnvironmentExtension).GetMethod("GetRequest", new Type[] { typeof(IDictionary<string,object>)});
        //public static MethodInfo getResponseBodyMethodInfo = typeof(System.EnvironmentExtension).GetMethod("GetResponseBody", new Type[] { typeof(IDictionary<string, object>) });
        //public static MethodInfo disposeMethodInfo = typeof(System.IDisposable).GetMethod("Dispose",Type.EmptyTypes);
        public static Dictionary<Type, System.Reflection.MethodInfo> StringContainerOpImplicitMethodInfoDic = null;
        public static ActionExecute<TContext,TRequest> GetRunActionDelegate<TContext,TRequest>(Assembly assembly,Type controllerType,System.Reflection.MethodInfo actionMethodInfo)
        {
            var TContextType = typeof(TContext);
            if (StringContainerOpImplicitMethodInfoDic == null)
            {
                StringContainerOpImplicitMethodInfoDic = new Dictionary<Type, System.Reflection.MethodInfo>();
                System.Reflection.MethodInfo[] methodInfos = typeof(StringContainer).GetMethods();
                foreach (var methodInfo in methodInfos)
                {
                    if (methodInfo.Name == "op_Implicit" && methodInfo.ReturnType != typeof(StringContainer))
                    {
                        StringContainerOpImplicitMethodInfoDic.Add(methodInfo.ReturnType, methodInfo);
                    }
                }
            }
            DynamicMethod method = new DynamicMethod("RunActionX", typeof(void), new Type[] { typeof(TContext),typeof(NFinal.Middleware.ActionData<TContext,TRequest>),typeof(TRequest),typeof(NameValueCollection)});
            ILGenerator methodIL = method.GetILGenerator();
            var methodEnd = methodIL.DefineLabel();
            var request= methodIL.DeclareLocal(typeof(TRequest));
            var controller = methodIL.DeclareLocal(controllerType);
            ////null
            //methodIL.Emit(OpCodes.Ldnull);
            ////request=null;
            //methodIL.Emit(OpCodes.Stloc,request);

            //var ifIEnvironmentFiltersEnd = methodIL.DefineLabel();
            ////actionData
            //methodIL.Emit(OpCodes.Ldarg_1);
            ////actionData.IEnvironmentFilters
            //methodIL.Emit(OpCodes.Ldfld, actionDataEnvironmentFilters);
            ////actionData.IEnvironmentFilters,IDictionary<string,object>
            //methodIL.Emit(OpCodes.Ldarg_0);
            ////FilterHelper.Filter(actionData.IBaseFilters,environment);
            //methodIL.Emit(OpCodes.Call, BaseFiltersMethodInfo.MakeGenericMethod(TContextType));
            //methodIL.Emit(OpCodes.Ldc_I4_0);
            //methodIL.Emit(OpCodes.Ceq);
            //methodIL.Emit(OpCodes.Brtrue_S, methodEnd);

            ////actionData
            //methodIL.Emit(OpCodes.Ldarg_1);
            ////actionData.IRequestFilters
            //methodIL.Emit(OpCodes.Ldfld, actionDataRequestFilters);
            ////actionData.IRequestFilters,IDictionary<string,object>
            //methodIL.Emit(OpCodes.Ldarg_0);
            ////actionData.IRequestFilters,IDictionary<string,object>,request
            //methodIL.Emit(OpCodes.Ldloca_S, request);
            ////FilterHelper.Filter(actionData.IEnvironmentFilters,environment,request);
            //methodIL.Emit(OpCodes.Call, RequestFiltersMethodInfo.MakeGenericMethod(TContextType));
            //methodIL.Emit(OpCodes.Ldc_I4_0);
            //methodIL.Emit(OpCodes.Ceq);
            //methodIL.Emit(OpCodes.Brtrue_S, methodEnd);

            var defaultConstructor = controllerType.GetConstructor(Type.EmptyTypes);
            methodIL.Emit(OpCodes.Newobj, defaultConstructor);
            methodIL.Emit(OpCodes.Stloc, controller);

            //0.context,1.actionData,2.request,3.parameters
            //try
            methodIL.BeginExceptionBlock();
            {
                //controller
                methodIL.Emit(OpCodes.Ldloc, controller);
                //controller,environment
                methodIL.Emit(OpCodes.Ldarg_0);
                //controller,environment,"methodName"
                methodIL.Emit(OpCodes.Ldstr, actionMethodInfo.Name);
                //controller,environment,"methodName",null
                methodIL.Emit(OpCodes.Ldnull);
                //controller,environment,"methodName",null,Request
                methodIL.Emit(OpCodes.Ldarg_2);
                //controller,environment,"methodName",null,Request,CompressMode
                methodIL.Emit(OpCodes.Ldc_I4, (int)NFinal.CompressMode.Deflate);
                methodIL.Emit(OpCodes.Callvirt, controllerType.GetMethod("Initialization",
                    new Type[] { typeof(TContext),
                        typeof(string),
                        typeof(System.IO.Stream),
                        typeof(TRequest),
                        typeof(NFinal.CompressMode) }));
                
                //controller
                methodIL.Emit(OpCodes.Ldloc, controller);
                //controller.Before();
                methodIL.Emit(OpCodes.Callvirt, controllerType.GetMethod("Before", Type.EmptyTypes));
                //bool,0
                methodIL.Emit(OpCodes.Ldc_I4_0);
                methodIL.Emit(OpCodes.Ceq);
                var BeforeEnd = methodIL.DefineLabel();
                methodIL.Emit(OpCodes.Brfalse_S, BeforeEnd);
                methodIL.Emit(OpCodes.Leave_S,methodEnd);
                methodIL.MarkLabel(BeforeEnd);

                //ViewBag初始化
                if (true)
                {
                    var ControllerViewBagFieldInfo = controllerType.GetField("ViewBag");
                    Type ViewBagType = null;
                    if (ControllerViewBagFieldInfo.FieldType == typeof(object))
                    {
                        string modelTypeName = controllerType.Namespace + "." + controllerType.Name + "_Model";
                        modelTypeName += "." + actionMethodInfo.Name;
                        ViewBagType = assembly.GetType(modelTypeName);
                        if (ViewBagType == null)
                        {
                            throw new NFinal.Exceptions.ModelNotFoundException(modelTypeName);
                        }
                    }
                    else
                    {
                        ViewBagType = ControllerViewBagFieldInfo.FieldType;
                    }
                    var ViewBagContructorInfo = ViewBagType.GetConstructor(Type.EmptyTypes);
                    var ViewBag = methodIL.DeclareLocal(ViewBagType);

                    //controller
                    methodIL.Emit(OpCodes.Newobj, ViewBagContructorInfo);
                    methodIL.Emit(OpCodes.Stloc, ViewBag);

                    //获取所有字段
                    var controllerFieldInfos = controllerType.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static|BindingFlags.FlattenHierarchy);
                    foreach (var controllerFieldInfo in controllerFieldInfos)
                    {
                        //查找Controller中具有ViewBagMember特性的字段
                        var viewBagMemberAttribute = controllerFieldInfo.GetCustomAttributes(typeof(ViewBagMemberAttribute), true);
                        if (viewBagMemberAttribute.Count() > 0)
                        {
                            var ViewBagFiledInfo = ViewBagType.GetField(
                                controllerFieldInfo.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                            //查找到ViewBag中具有相同名字的字段
                            if (ViewBagFiledInfo != null && ViewBagFiledInfo.FieldType == controllerFieldInfo.FieldType)
                            {
                                if (controllerFieldInfo.IsStatic)
                                {
                                    methodIL.Emit(OpCodes.Ldloc, ViewBag);
                                    methodIL.Emit(OpCodes.Ldsfld, controllerFieldInfo);
                                    methodIL.Emit(OpCodes.Stfld, ViewBagFiledInfo);
                                }
                                else
                                {
                                    //赋值操作
                                    methodIL.Emit(OpCodes.Ldloc, ViewBag);
                                    methodIL.Emit(OpCodes.Ldloc, controller);
                                    methodIL.Emit(OpCodes.Ldfld, controllerFieldInfo);
                                    methodIL.Emit(OpCodes.Stfld, ViewBagFiledInfo);
                                }
                            }
                        }
                    }
                    var controllerPropertyInfos = controllerType.GetProperties(BindingFlags.Public|BindingFlags.Instance|BindingFlags.Static| BindingFlags.FlattenHierarchy);
                    foreach (var controllerPropertyInfo in controllerPropertyInfos)
                    {
                        var viewBagMemberAttribute = controllerPropertyInfo.GetCustomAttributes(typeof(ViewBagMemberAttribute), true);
                        if (viewBagMemberAttribute.Count() > 0)
                        {
                            var viewBagPropertyInfo = ViewBagType.GetProperty(
                                controllerPropertyInfo.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                            if (viewBagPropertyInfo != null && viewBagPropertyInfo.PropertyType == controllerPropertyInfo.PropertyType)
                            {
                                MethodInfo controllerPropertyInfoGetGetMethod = controllerPropertyInfo.GetGetMethod();
                                MethodInfo viewBagPropertyInfoGetSetMethod = viewBagPropertyInfo.GetSetMethod();
                                if (controllerPropertyInfo.GetGetMethod().IsStatic)
                                {
                                    methodIL.Emit(OpCodes.Ldloc, ViewBag);
                                    methodIL.Emit(OpCodes.Call, controllerPropertyInfoGetGetMethod);
                                    methodIL.Emit(OpCodes.Callvirt, viewBagPropertyInfoGetSetMethod);
                                }
                                else
                                {
                                    methodIL.Emit(OpCodes.Ldloc, ViewBag);
                                    methodIL.Emit(OpCodes.Ldloc, controller);
                                    methodIL.Emit(OpCodes.Callvirt, controllerPropertyInfoGetGetMethod);
                                    methodIL.Emit(OpCodes.Callvirt, viewBagPropertyInfoGetSetMethod);
                                }
                            }
                        }
                    }
                    methodIL.Emit(OpCodes.Ldloc, controller);
                    methodIL.Emit(OpCodes.Ldloc, ViewBag);
                    methodIL.Emit(OpCodes.Stfld, ControllerViewBagFieldInfo);
                }

                //controller
                methodIL.Emit(OpCodes.Ldloc, controller);
                System.Reflection.ParameterInfo[] parameterInfos = actionMethodInfo.GetParameters();
                //添加参数
                if (parameterInfos.Length > 0)
                {
                    for (int i = 0; i < parameterInfos.Length; i++)
                    {
                        Type parameterType = parameterInfos[i].ParameterType;
                        if (parameterInfos[i].ParameterType
#if (NET40 || NET451 || NET461)
                            .IsGenericType
#endif
#if NETCORE
                            .GetTypeInfo().IsGenericType
#endif
                            && parameterInfos[i].ParameterType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            parameterType = parameterInfos[i].ParameterType.GetGenericArguments()[0];
                        }
                        if (parameterType == typeof(System.String) ||
                            parameterType == typeof(System.Int32) ||
                            parameterType == typeof(System.Int16) ||
                            parameterType == typeof(System.Int64) ||
                            parameterType == typeof(System.UInt32) ||
                            parameterType == typeof(System.UInt16) ||
                            parameterType == typeof(System.UInt64) ||
                            parameterType == typeof(System.Byte) ||
                            parameterType == typeof(System.SByte) ||
                            parameterType == typeof(System.Single) ||
                            parameterType == typeof(System.Double) ||
                            parameterType == typeof(System.Decimal) ||
                            parameterType == typeof(System.DateTime) ||
                            parameterType == typeof(System.DateTimeOffset) ||
                            parameterType == typeof(System.Char) ||
                            parameterType == typeof(System.Boolean) ||
                            parameterType == typeof(System.Guid))
                        {
                            //parameters
                            methodIL.Emit(OpCodes.Ldarg_3);
                            //parameters,name
                            methodIL.Emit(OpCodes.Ldstr, parameterInfos[i].Name);
                            //parameters[name]
                            methodIL.Emit(OpCodes.Callvirt, nameValueCollectionGetItemMethodInfo);
                            //ParameterType op_Implicit(request.parameters[name]);隐式转换
                            methodIL.Emit(OpCodes.Call, StringContainerOpImplicitMethodInfoDic[parameterInfos[i].ParameterType]);
                        }
                        else
                        {
                            //new ParameterModel();
                            //methodIL.Emit(OpCodes.Newobj, parameterInfos[i].ParameterType.GetConstructor(Type.EmptyTypes));
                            //parameters
                            methodIL.Emit(OpCodes.Ldarg_3);
                            //ModelHelper.GetModel(new ParameterModel(),parameters);
                            methodIL.Emit(OpCodes.Call, modelHelperGetModelMethodInfo.MakeGenericMethod(parameterType));
                        }
                    }
                }
                //controller.Action(par1,par2,par3.....);
                methodIL.Emit(OpCodes.Callvirt, actionMethodInfo);

                //controller
                methodIL.Emit(OpCodes.Ldloc, controller);
                //controller.After();
                methodIL.Emit(OpCodes.Callvirt, controllerType.GetMethod("After", Type.EmptyTypes));

                //actionData
                methodIL.Emit(OpCodes.Ldarg_1);
                //actionData.IResponseFilters
                methodIL.Emit(OpCodes.Ldfld, typeof(NFinal.Middleware.ActionData<TContext,TRequest>).GetField("IResponseFilters"));
                //actionData.IResponseFilters,controller
                methodIL.Emit(OpCodes.Ldloc, controller);
                //actionData.IResponseFilters,controller.response
                methodIL.Emit(OpCodes.Callvirt, controllerType.GetProperty("response").GetGetMethod());
                //FilterHelper.Filter(actionData.IResponseFilters,controller.response)
                methodIL.Emit(OpCodes.Call, ResponseFiltersMethodInfo);
                methodIL.Emit(OpCodes.Ldc_I4_0);
                methodIL.Emit(OpCodes.Ceq);
                var ifResponseFiltersEnd = methodIL.DefineLabel();
                methodIL.Emit(OpCodes.Brfalse_S, ifResponseFiltersEnd);
                methodIL.Emit(OpCodes.Leave_S, methodEnd);
                methodIL.MarkLabel(ifResponseFiltersEnd);

                //controller
                methodIL.Emit(OpCodes.Ldloc, controller);
                //controller.Close();
                methodIL.Emit(OpCodes.Callvirt, controllerType.GetMethod("Close"));
                //跳出异常
                methodIL.Emit(OpCodes.Leave_S, methodEnd);
            }
            //finally
            methodIL.BeginFinallyBlock();
            {
                var finallyEnd = methodIL.DefineLabel();
                //controller
                methodIL.Emit(OpCodes.Ldloc, controller);
                //controller==null
                methodIL.Emit(OpCodes.Brfalse_S, finallyEnd);
                //controller
                methodIL.Emit(OpCodes.Ldloc, controller);
                //controller.Dispose();
                methodIL.Emit(OpCodes.Callvirt, controllerType.GetMethod("Dispose", Type.EmptyTypes));
                methodIL.MarkLabel(finallyEnd);
            }
            //end
            methodIL.EndExceptionBlock();

            methodIL.MarkLabel(methodEnd);
            methodIL.Emit(OpCodes.Ret);
            ActionExecute<TContext,TRequest> getRunActionDelegate =(ActionExecute<TContext,TRequest>)method.CreateDelegate(typeof(ActionExecute<TContext,TRequest>));
            return getRunActionDelegate;
        }
    }
    public class ViewBag
    {
        public string a;
        public string b { get; set; }
        public string c { get; set; }
    }
    public class ViewBagDynamic : BaseController
    {
        public string a;
        public string b { get; set; }
        public static string c { get; set; }
        public void Show()
        {
        }
    }
    public class ViewBagDynamicInit
    {
        public void Run()
        {
            ViewBagDynamic controller = new ViewBagDynamic();
            ViewBag viewBag = new ViewBag();
            viewBag.a = controller.a;
            viewBag.b = controller.b;
            viewBag.c = ViewBagDynamic.c;
            controller.ViewBag = viewBag;
        }
    }
    public class RunAction:Index
    {
        public static void Run1<TContext,TRequest>(TContext context, NFinal.Middleware.ActionData<TContext,TRequest> actionData,TRequest request,NameValueCollection parameters)
        {
            using (Index controller = new Index())
            {
                Request request1 = null;
                IDictionary<string, object> context1=null;
                //初始化
                
                controller.Initialization(context1,"Index", null, request1, CompressMode.None);
                ViewModel ViewBag = new ViewModel();
                ViewBag.a = 2;
                ViewBag.b = "3";
                
                ViewBag.c = Index.c;
                ViewBag.c1 = Index.c1;
                controller.ViewBag = ViewBag;
                if (!controller.Before())
                {
                    return;
                }
                {
                    //添参数问题。。。
                    //string a = request.parameters["a"];
                    //int b = request.parameters["b"];
                    //int? b1 = request.parameters["b1"];
                    //ParameterModel c = new ParameterModel();
                    //c = NFinal.Model.ModelHelper.GetModel(new ParameterModel(), request.parameters);
                    controller.Index1(parameters["a"], parameters["b"], NFinal.Model.ModelHelper.GetModel<ParameterModel>(parameters));
                }
                controller.After();
                if (!NFinal.Filter.FilterHelper.ResponseFilter(actionData.IResponseFilters,controller.response))
                {
                    return;
                }
                controller.Close();
            }
        }
        public void Run2()
        {

        }
    }
    public class User
    {
        public int userId;
        public string userName;
    }
    public class BaseController : OwinAction<EmptyMasterPageModel, User>
    {
        [ViewBagMember]
        public string imageUrl;
        public override bool Before()
        {
            return base.Before();
        }
        public override void After()
        {
            base.After();
        }
    }
    [EnvFilter]
    [OwinReqFilter]
    [ResFilter]
    public class Index : BaseController
    {
        public static string c;
        public static string c1;
        [GetHtml("/Index.html")]
        public void Index1(string a,int b,ParameterModel c)
        {
            this.Write("hello!");
        }
        [GetJson("/Index2.php")]
        public void Index2(string a)
        {
            this.ViewBag.a = 1;
        }
        public void Index3(int a)
        {
            this.Redirect("sss");
        }
        public void Index4(ParameterModel c)
        {

        }
    }
    public class Index_Index1Action : BaseController
    {
        
        public void Index1()
        {
            this.ViewBag.b = "2";
           
        }
    }
    public class Index_Index2Action : BaseController
    {
        public void Index2()
        {
            this.ViewBag.a = 1;
        }
    }
    public class EnvFilter : EnvironmentFilterAttribute
    {
        public override bool BaseFilter(IDictionary<string, object> environment)
        {
            return true;
        }
    }
    public class OwinReqFilter : OwinRequestFilterAttribute
    {
        public override bool RequestFilter(Request request)
        {
            return true;
        }
    }
    public class ResFilter : ResponseFilterAttribute
    {
        public override bool ResponseFilter(Response response)
        {
            throw new NotImplementedException();
        }
    }
    public class ParameterModel
    {
        public int a;
        public string b;
    }
    public class ViewModel
    {
        public string c;
        public string c1 { get; set; }
        public int a;
        public string b;
    }
}
