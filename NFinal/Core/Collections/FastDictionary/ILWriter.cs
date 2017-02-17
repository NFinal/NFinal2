using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Permissions;
public delegate int GetKeyIndex(string key, int length);
namespace NFinal.Collections
{

    public class ILWriter
    {
        public void GetTemp(ILGenerator iLGenerator, LocalBuilder pt, int charIndex)
        {

            Label charIndexLessThanLengthEnd = iLGenerator.DefineLabel();
            Label charIndexLessThanLengthElse = iLGenerator.DefineLabel();
            //charIndex<Length;
            iLGenerator.Emit(OpCodes.Ldc_I4, charIndex);
            iLGenerator.Emit(OpCodes.Ldarg_1);
            //if(charIndex<Length)
            iLGenerator.Emit(OpCodes.Clt);
            iLGenerator.Emit(OpCodes.Brfalse_S, charIndexLessThanLengthElse);
            //ifCase Content
            {
                //length-charIndex
                iLGenerator.Emit(OpCodes.Ldarg_1);
                iLGenerator.Emit(OpCodes.Ldc_I4, charIndex);
                iLGenerator.Emit(OpCodes.Sub);
                Label switch_default = iLGenerator.DefineLabel();
                Label switch_1 = iLGenerator.DefineLabel();
                Label switch_2 = iLGenerator.DefineLabel();
                Label switch_3 = iLGenerator.DefineLabel();
                Label switch_end = iLGenerator.DefineLabel();
                //switch(length-charIndex)
                iLGenerator.Emit(OpCodes.Switch, new Label[] { switch_1, switch_2, switch_3 });
                iLGenerator.Emit(OpCodes.Br_S, switch_default);
                {

                    //case 1:
                    iLGenerator.MarkLabel(switch_1);
                    {
                        iLGenerator.Emit(OpCodes.Ldloc_S, pt);
                        //charIndex<<1=得到字节数
                        iLGenerator.Emit(OpCodes.Ldc_I4, charIndex << 1);
                        //pt+(charIndex<<1)
                        iLGenerator.Emit(OpCodes.Add);
                        //*(short*)(pt+(charIndex<<1))
                        iLGenerator.Emit(OpCodes.Ldind_I2);
                        //temp=*(short*)(pt+(charIndex<<1))
                        iLGenerator.Emit(OpCodes.Conv_I8);
                        iLGenerator.Emit(OpCodes.Stloc_1);
                        //break;
                        iLGenerator.Emit(OpCodes.Br_S, switch_end);
                    }
                    //case 2:
                    iLGenerator.MarkLabel(switch_2);
                    {
                        iLGenerator.Emit(OpCodes.Ldloc_S, pt);
                        //charIndex<<1=得到字节数
                        iLGenerator.Emit(OpCodes.Ldc_I4, charIndex << 1);
                        //pt+(charIndex<<1)
                        iLGenerator.Emit(OpCodes.Add);
                        //*(int*)(pt+(charIndex<<1))
                        iLGenerator.Emit(OpCodes.Ldind_I4);
                        //temp=*(int*)(pt+(charIndex<<1))
                        iLGenerator.Emit(OpCodes.Conv_I8);
                        iLGenerator.Emit(OpCodes.Stloc_1);
                        //break;
                        iLGenerator.Emit(OpCodes.Br_S, switch_end);
                    }
                    //case 3:
                    iLGenerator.MarkLabel(switch_3);
                    {
                        iLGenerator.Emit(OpCodes.Ldloc_S, pt);
                        //charIndex<<1=得到字节数
                        iLGenerator.Emit(OpCodes.Ldc_I4, charIndex << 1);
                        //pt+(charIndex<<1)
                        iLGenerator.Emit(OpCodes.Add);
                        //*(int*)(pt+(charIndex<<1))
                        iLGenerator.Emit(OpCodes.Ldind_I4);
                        //(long)*(int*)(pt+(charIndex<<1))
                        iLGenerator.Emit(OpCodes.Conv_I8);
                        //((long)*(int*)(pt+(charIndex<<1)))<<16
                        iLGenerator.Emit(OpCodes.Ldc_I4_S, 16);
                        iLGenerator.Emit(OpCodes.Shl);


                        //pt+charIndex+2
                        iLGenerator.Emit(OpCodes.Ldloc_S, pt);
                        iLGenerator.Emit(OpCodes.Ldc_I4, (charIndex + 2) << 1);
                        iLGenerator.Emit(OpCodes.Add);
                        //*(short*)(pt+charIndex+2)
                        iLGenerator.Emit(OpCodes.Ldind_I2);
                        //(long)*(short*)(pt+charIndex+2)
                        iLGenerator.Emit(OpCodes.Conv_I8);

                        //temp=(((long)*(int*)(pt + 4))<<16)+(long)(*(int*)(pt+4+1))
                        iLGenerator.Emit(OpCodes.Add);
                        iLGenerator.Emit(OpCodes.Stloc_1);
                        //break;
                        iLGenerator.Emit(OpCodes.Br_S, switch_end);
                    }
                    //default:
                    iLGenerator.MarkLabel(switch_default);
                    {
                        iLGenerator.Emit(OpCodes.Ldloc_S, pt);
                        //charIndex<<1=得到字节数
                        iLGenerator.Emit(OpCodes.Ldc_I4, charIndex << 1);
                        //pt+(charIndex<<1)
                        iLGenerator.Emit(OpCodes.Add);
                        //*(long*)(pt+(charIndex<<1))
                        iLGenerator.Emit(OpCodes.Ldind_I8);
                        //temp=*(long*)(pt+(charIndex<<1))
                        iLGenerator.Emit(OpCodes.Stloc_1);
                        //break;
                        iLGenerator.Emit(OpCodes.Br_S, switch_end);
                    }
                    iLGenerator.MarkLabel(switch_end);
                }
            }
            iLGenerator.Emit(OpCodes.Br_S, charIndexLessThanLengthEnd);
            iLGenerator.MarkLabel(charIndexLessThanLengthElse);
            //else //Case charIndex>=Length
            {
                iLGenerator.Emit(OpCodes.Ldc_I8, 0L);
                iLGenerator.Emit(OpCodes.Stloc_1);
            }
            iLGenerator.MarkLabel(charIndexLessThanLengthEnd);


        }
        public GetKeyIndex Generate(CodeNode rootNode)
        {
            AssemblyName assamblyName = new AssemblyName("GenerateAssembly");
            AssemblyBuilder assambly = AppDomain.CurrentDomain.DefineDynamicAssembly(assamblyName, AssemblyBuilderAccess.Run);
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
            ModuleBuilder dynamicModule = assambly.DefineDynamicModule(assamblyName.Name);
            dynamicModule.SetCustomAttribute(unverifiableCodeAttributeBuilder);

            //class GenerateType
            TypeBuilder dynamicType = dynamicModule.DefineType("GenerateType" + Guid.NewGuid(),
                TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout);

            //public GenerateType(){}
            ConstructorBuilder constructorBuilder = dynamicType.DefineConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.RTSpecialName, CallingConventions.Standard, Type.EmptyTypes);
            var constructorILGenerator = constructorBuilder.GetILGenerator();
            constructorILGenerator.Emit(OpCodes.Ldarg_0);
            ConstructorInfo conObj = typeof(object).GetConstructor(new Type[0]);
            constructorILGenerator.Emit(OpCodes.Call, conObj);
            constructorILGenerator.Emit(OpCodes.Ret);

            MethodBuilder methodBuilder = dynamicType.DefineMethod("GetIndex", MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.HideBySig, CallingConventions.Standard, typeof(int), new Type[] { typeof(string), typeof(int) });
            var iLGenerator = methodBuilder.GetILGenerator();
            //DynamicMethod dyMethod = new DynamicMethod("GetIndex2345", typeof(int), new Type[] { typeof(string), typeof(int) },true);;
            //var iLGenerator = dyMethod.GetILGenerator();
            Label lbLoadPointerFalse = iLGenerator.DefineLabel();
            //int i;loc_0
            iLGenerator.DeclareLocal(typeof(int));
            //long temp;loc_1
            iLGenerator.DeclareLocal(typeof(long));
            //char* p;loc_2
            iLGenerator.DeclareLocal(typeof(char*));
            //stringPinned keyPinned;loc_3
            iLGenerator.DeclareLocal(typeof(string), true);
            //char* pt;loc_4
            var pt = iLGenerator.DeclareLocal(typeof(char*));
            //i=0;
            iLGenerator.Emit(OpCodes.Ldc_I4_0);
            iLGenerator.Emit(OpCodes.Stloc_0);
            //keyPinned=key;
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Stloc_3);

            ////Char* p=&keyPinned;
            ////http://blog.csdn.net/sqlchen/article/details/8903857
            iLGenerator.Emit(OpCodes.Ldloc_3);
            iLGenerator.Emit(OpCodes.Conv_I);
            iLGenerator.Emit(OpCodes.Stloc_2);
            iLGenerator.Emit(OpCodes.Ldloc_2);
            iLGenerator.Emit(OpCodes.Brfalse_S, lbLoadPointerFalse);

            ////计算Char* p地址
            var runtimeHelpers = typeof(System.Runtime.CompilerServices.RuntimeHelpers);
            PropertyInfo offsetToStringData = runtimeHelpers.GetProperty("OffsetToStringData", BindingFlags.Static | BindingFlags.Public);
            iLGenerator.Emit(OpCodes.Ldloc_2);
            iLGenerator.Emit(OpCodes.Call, offsetToStringData.GetGetMethod());
            iLGenerator.Emit(OpCodes.Add);
            iLGenerator.Emit(OpCodes.Stloc_2);

            ////Char* pt=p;
            iLGenerator.MarkLabel(lbLoadPointerFalse);
            iLGenerator.Emit(OpCodes.Ldloc_2);
            iLGenerator.Emit(OpCodes.Stloc_S, pt);

            //开始
            {
                WriteCode(ref iLGenerator, pt, ref rootNode);
            }
            //结束
            //keyPinned=null,用于释放资源
            iLGenerator.Emit(OpCodes.Ldnull);
            iLGenerator.Emit(OpCodes.Stloc_3);
            //return i;
            iLGenerator.Emit(OpCodes.Ldloc_0);
            iLGenerator.Emit(OpCodes.Ret);
            Type t = dynamicType.CreateType();

            //assambly.Save("GenerateAssembly.dll");
            MethodInfo GM = t.GetMethod("GetIndex");
            GetKeyIndex delete = (GetKeyIndex)Delegate.CreateDelegate(typeof(GetKeyIndex), GM);
            return delete;
        }
        public void WriteCode(ref ILGenerator iLGenerator, LocalBuilder pt, ref CodeNode codeNode)
        {
            if (codeNode != null)
            {
                if (codeNode.nodeType == CodeNodeType.CompareCreaterThan)
                {
                    Label ifCompareCreaterThanElse = iLGenerator.DefineLabel();
                    Label ifCompareCreaterThanEnd = iLGenerator.DefineLabel();
                    GetTemp(iLGenerator, pt, codeNode.charIndex);
                    //temp>compareValue
                    iLGenerator.Emit(OpCodes.Ldloc_1);
                    iLGenerator.Emit(OpCodes.Ldc_I8, codeNode.compareValue);
                    //if(temp>compareValue)
                    iLGenerator.Emit(OpCodes.Cgt);
                    //把结果转化为bool类型
                    iLGenerator.Emit(OpCodes.Brfalse, ifCompareCreaterThanElse);
                    {
                        //ifContent
                        WriteCode(ref iLGenerator, pt, ref codeNode.ifCase);
                    }
                    iLGenerator.Emit(OpCodes.Br, ifCompareCreaterThanEnd);
                    iLGenerator.MarkLabel(ifCompareCreaterThanElse);
                    {
                        //elseContent
                        WriteCode(ref iLGenerator, pt, ref codeNode.elseCase);
                    }
                    iLGenerator.MarkLabel(ifCompareCreaterThanEnd);
                }
                else if (codeNode.nodeType == CodeNodeType.CompareLessThan)
                {
                    Label ifCompareLessThanElse = iLGenerator.DefineLabel();
                    Label ifCompareLessThanEnd = iLGenerator.DefineLabel();
                    GetTemp(iLGenerator, pt, codeNode.charIndex);
                    //temp<compareValue
                    iLGenerator.Emit(OpCodes.Ldloc_1);
                    iLGenerator.Emit(OpCodes.Ldc_I8, codeNode.compareValue);
                    //if(temp<compareValue)
                    iLGenerator.Emit(OpCodes.Clt);
                    //把结果转化为bool类型
                    iLGenerator.Emit(OpCodes.Brfalse, ifCompareLessThanElse);
                    {
                        //ifContent
                        WriteCode(ref iLGenerator, pt, ref codeNode.ifCase);
                    }
                    iLGenerator.Emit(OpCodes.Br, ifCompareLessThanEnd);
                    iLGenerator.MarkLabel(ifCompareLessThanElse);
                    {
                        //elseContent
                        WriteCode(ref iLGenerator, pt, ref codeNode.elseCase);
                    }
                    iLGenerator.MarkLabel(ifCompareLessThanEnd);
                }
                else if (codeNode.nodeType == CodeNodeType.SetIndex)
                {
                    //i=arrayIndex;
                    iLGenerator.Emit(OpCodes.Ldc_I4, codeNode.arrayIndex);
                    iLGenerator.Emit(OpCodes.Stloc_0);
                }
            }

        }
    }
}
