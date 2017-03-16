using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace NFinal.Emit
{
    public class StructHelper
    {
        private TypeBuilder tb;
        public StructHelper(string assemblyName, string moduleName, string typeName)
        {
            this.tb = CreateTypeBuilder(assemblyName,moduleName,typeName);
 
        }
        public void AddAttribute(string name, Type type)
        {
            CreateAutoImplementedProperty(this.tb, name, type);
        }
        public void AddField(string name, Type type)
        {
            CreateAutoImplementedField(this.tb,name,type);
        }
        public Type ToType()
        {
            return this.tb.GetType();
        }
        private static TypeBuilder CreateTypeBuilder(string assemblyName, string moduleName, string typeName)
        {
            TypeBuilder typeBuilder =
#if (NET40 || NET451 || NET461)
                AppDomain.CurrentDomain
#endif
#if NETCORE
                AssemblyBuilder
#endif
                .DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.Run).DefineDynamicModule(moduleName).DefineType(typeName, TypeAttributes.Public);
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
