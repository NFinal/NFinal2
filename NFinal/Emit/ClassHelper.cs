//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : StructHelper.cs
//        Description :利用EMIT快速创建自定义类型的帮助类
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace NFinal.Emit
{
    /// <summary>
    /// 利用EMIT快速创建自定义类型的帮助类
    /// </summary>
    public class ClassHelper
    {
        private TypeBuilder tb;
        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="typeName">类型名称</param>
        public ClassHelper(string assemblyName, string moduleName, string typeName)
        {
            this.tb = CreateTypeBuilder(assemblyName,moduleName,typeName);
 
        }
        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        public void AddAttribute(string name, Type type)
        {
            CreateAutoImplementedProperty(this.tb, name, type);
        }
        /// <summary>
        /// 添加静态变量
        /// </summary>
        /// <param name="name">变量名</param>
        /// <param name="type">变量类型</param>
        public FieldBuilder AddStaticField(string name, Type type)
        {
            FieldBuilder fieldBuilder = this.tb.DefineField(name, type, FieldAttributes.Public|FieldAttributes.Static);
            return fieldBuilder;
        }
        /// <summary>
        /// 添加字段
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public void AddField(string name, Type type)
        {
            CreateAutoImplementedField(this.tb,name,type);
        }
        /// <summary>
        /// 返回类型
        /// </summary>
        /// <returns></returns>

#if (NET40 || NET451 || NET461)
        public Type ToType()
        {
            return this.tb.CreateType();
        }
#endif
#if NETCORE
        public Type ToType()
        {
            return this.tb.CreateTypeInfo().AsType();
        }
#endif

        private static TypeBuilder CreateTypeBuilder(string assemblyName, string moduleName, string typeName)
        {
            TypeBuilder typeBuilder =
#if (NET40 || NET451 || NET461)
                AppDomain.CurrentDomain
#endif
#if NETCORE
                AssemblyBuilder
#endif
                .DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.Run).DefineDynamicModule(moduleName).DefineType(typeName, TypeAttributes.Public|TypeAttributes.Class);
            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
            //typeBuilder.DefineConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, CallingConventions.Standard, new Type[] { });
            return typeBuilder;
        }
        private static void CreateAutoImplementedField(TypeBuilder builder, string fieldName, Type fieldType)
        {
            FieldBuilder fieldBuilder = builder.DefineField(fieldName,fieldType,FieldAttributes.Public);
        }
        private static void CreateAutoImplementedProperty(TypeBuilder builder, string propertyName, Type propertyType)
        {
            const string PrivateFieldPrefix = "m_";
            const string GetterPrefix = "get_";
            const string SetterPrefix = "set_";

            FieldBuilder fieldBuilder = builder.DefineField(string.Concat(PrivateFieldPrefix, propertyName), propertyType, FieldAttributes.Private);
            PropertyBuilder propertyBuilder = builder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, Type.EmptyTypes);
            MethodAttributes propertyMethodAttributes = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
            MethodBuilder getterMethod = builder.DefineMethod(string.Concat(GetterPrefix, propertyName), propertyMethodAttributes, propertyType, Type.EmptyTypes);

            ILGenerator getterILCode = getterMethod.GetILGenerator();
            getterILCode.Emit(OpCodes.Ldarg_0);
            getterILCode.Emit(OpCodes.Ldfld, fieldBuilder);
            getterILCode.Emit(OpCodes.Ret);

            MethodBuilder setterMethod = builder.DefineMethod(string.Concat(SetterPrefix, propertyName), propertyMethodAttributes, propertyType, Type.EmptyTypes);

            ILGenerator setterILCode = setterMethod.GetILGenerator();
            setterILCode.Emit(OpCodes.Ldarg_0);
            setterILCode.Emit(OpCodes.Ldarg_1);
            setterILCode.Emit(OpCodes.Stfld, fieldBuilder);
            setterILCode.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getterMethod);
            propertyBuilder.SetSetMethod(setterMethod);
        }
    }
}
