//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : UnSafeHelper.cs
//        Description :利用EMIT创建可以使用指针的自定义类型的帮助类
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
#if !NETCORE
using System.Security.Permissions;
#endif

namespace NFinal.Emit
{
    /// <summary>
    /// 利用EMIT创建可以使用指针的自定义类型的帮助类
    /// </summary>
    public class UnSafeHelper
    {
        /// <summary>
        /// 获取代理
        /// </summary>
        /// <typeparam name="TDelegate"></typeparam>
        /// <param name="typeBuilder"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public static TDelegate GetDelegate<TDelegate>(TypeBuilder typeBuilder,string methodName)
        {
#if NETCORE
            TypeInfo t = typeBuilder.CreateTypeInfo();
#else
            Type t = typeBuilder.CreateType();
#endif
            //assambly.Save("GenerateAssembly.dll");
            MethodInfo GM = t.GetMethod(methodName);
#if NETCORE
            TDelegate delete = (TDelegate)(object)GM.CreateDelegate(typeof(TDelegate));
#else
            TDelegate delete = (TDelegate)(object)Delegate.CreateDelegate(typeof(TDelegate), GM);
#endif
            return delete;
        }
        /// <summary>
        /// 获取动态类型
        /// </summary>
        /// <returns></returns>
        public static TypeBuilder GetDynamicType()
        {
            AssemblyName assamblyName = new AssemblyName("GenerateAssembly");

            AssemblyBuilder assambly =
#if NETCORE
                AssemblyBuilder
#else
                AppDomain.CurrentDomain
#endif
                .DefineDynamicAssembly(assamblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder dynamicModule = assambly.DefineDynamicModule(assamblyName.Name);
#if !NETCORE
            //设置/unsafe选项
            //[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
            Type securityPermissionType = typeof(System.Security.Permissions.SecurityPermissionAttribute);
            ConstructorInfo securityPermissionConstructor = securityPermissionType.GetConstructor(new Type[] { typeof(System.Security.Permissions.SecurityAction) });
            PropertyInfo skipVerificationPropertyInfo = securityPermissionType.GetProperty("SkipVerification");
            CustomAttributeBuilder securityPermissionAttributeBuilder = new CustomAttributeBuilder(securityPermissionConstructor, new object[] { SecurityAction.RequestMinimum }, new PropertyInfo[] { skipVerificationPropertyInfo }, new object[] { true });
            assambly.SetCustomAttribute(securityPermissionAttributeBuilder);

            //[module: UnverifiableCode]
            Type unverifiableCodeType = typeof(System.Security.UnverifiableCodeAttribute);
            ConstructorInfo unverifiableCodeConstructor = unverifiableCodeType.GetConstructor(Type.EmptyTypes);
            CustomAttributeBuilder unverifiableCodeAttributeBuilder = new CustomAttributeBuilder(unverifiableCodeConstructor, new object[] { });
            dynamicModule.SetCustomAttribute(unverifiableCodeAttributeBuilder);
#endif
            //class GenerateType
            TypeBuilder dynamicType = dynamicModule.DefineType("GenerateType" + Guid.NewGuid(),
                TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout,typeof(object));

            //public GenerateType(){}
            ConstructorBuilder constructorBuilder = dynamicType.DefineConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.RTSpecialName, CallingConventions.Standard, Type.EmptyTypes);
            var constructorILGenerator = constructorBuilder.GetILGenerator();
            constructorILGenerator.Emit(OpCodes.Ldarg_0);
            ConstructorInfo conObj = typeof(object).GetConstructor(new Type[0]);
            constructorILGenerator.Emit(OpCodes.Call, conObj);
            constructorILGenerator.Emit(OpCodes.Ret);
            return dynamicType;
        }
    }
}
