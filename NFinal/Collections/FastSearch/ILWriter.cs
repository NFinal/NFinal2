//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ILWriter.cs
//        Description :根据代码节点生成IL代码。
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

namespace NFinal.Collections.FastSearch
{
    /// <summary>
    /// 根据代码节点生成IL代码
    /// </summary>
    public class ILWriter
    {
        /// <summary>
        /// 根据代码节点生成IL代码
        /// </summary>
        /// <param name="rootNode">代码根节点</param>
        /// <param name="length">字符串长度</param>
        /// <param name="columnLength">第几列</param>
        /// <returns></returns>
        public FindDelegate Generate(FastFindSameLengthStringHelper.Node rootNode, int length, int columnLength)
        {
            var typeBuilder = NFinal.Emit.UnSafeHelper.GetDynamicType();
            MethodBuilder dynamicMethod = typeBuilder.DefineMethod("Compare", MethodAttributes.Public
               | MethodAttributes.Static | MethodAttributes.HideBySig, CallingConventions.Standard,
               typeof(int), new Type[] { typeof(char*) });
            ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
            WriteCode(iLGenerator, ref rootNode, length, columnLength);
            return NFinal.Emit.UnSafeHelper.GetDelegate<FindDelegate>(typeBuilder, "Compare");
        }
        /// <summary>
        /// 利用IL书写比较函数
        /// </summary>
        /// <param name="iLGenerator">IL代码发生器</param>
        /// <param name="codeNode">代码节点</param>
        /// <param name="remain">剩余长度</param>
        public void WriteCompare(ILGenerator iLGenerator, ref FastFindSameLengthStringHelper.Node codeNode, int remain)
        {
            if (remain == 0)
            {
                if (codeNode.charIndex > 0)
                {
                    iLGenerator.Emit(OpCodes.Ldc_I4, codeNode.charIndex * 2);
                    iLGenerator.Emit(OpCodes.Add);
                }
                iLGenerator.Emit(OpCodes.Ldind_I8);
                iLGenerator.Emit(OpCodes.Ldc_I8, codeNode.compareValue);
            }
            else if (remain == 1)
            {
                if (codeNode.charIndex > 0)
                {
                    iLGenerator.Emit(OpCodes.Ldc_I4, codeNode.charIndex * 2);
                    iLGenerator.Emit(OpCodes.Add);
                }
                iLGenerator.Emit(OpCodes.Ldind_I2);//不需要转换short和int字节数相同
                //iLGenerator.Emit(OpCodes.Conv_I4);
                iLGenerator.Emit(OpCodes.Ldc_I4, (short)codeNode.compareValue);
            }
            else if (remain == 2)
            {
                if (codeNode.charIndex > 0)
                {
                    iLGenerator.Emit(OpCodes.Ldc_I4, codeNode.charIndex * 2);
                    iLGenerator.Emit(OpCodes.Add);
                }
                iLGenerator.Emit(OpCodes.Ldind_I4);
                iLGenerator.Emit(OpCodes.Ldc_I4, (int)codeNode.compareValue);
            }
            else if (remain == 3)
            {
                if (codeNode.charIndex > 0)
                {
                    iLGenerator.Emit(OpCodes.Ldc_I4, codeNode.charIndex * 2 - 1);
                    iLGenerator.Emit(OpCodes.Add);
                }
                iLGenerator.Emit(OpCodes.Ldind_I8);
                iLGenerator.Emit(OpCodes.Ldc_I8, codeNode.compareValue);
            }
        }
        /// <summary>
        /// 利用IL书写查找函数
        /// </summary>
        /// <param name="iLGenerator">IL发生器</param>
        /// <param name="codeNode">代码节点</param>
        /// <param name="length">字符串长度</param>
        /// <param name="columnLength">列长度</param>
        public void WriteCode(ILGenerator iLGenerator, ref FastFindSameLengthStringHelper.Node codeNode, int length, int columnLength)
        {
            if (codeNode.nodeType == FastFindSameLengthStringHelper.NodeType.CompareCreaterThan
                || codeNode.nodeType == FastFindSameLengthStringHelper.NodeType.CompareLessThan)
            {
                var elseCase = iLGenerator.DefineLabel();
                var endIfCase = iLGenerator.DefineLabel();
                iLGenerator.Emit(OpCodes.Ldarg_0);
                if (codeNode.charIndex == columnLength - 1)
                {
                    int remain = length & 3;
                    WriteCompare(iLGenerator, ref codeNode, remain);
                }
                else
                {
                    if (codeNode.charIndex > 0)
                    {
                        iLGenerator.Emit(OpCodes.Ldc_I4, codeNode.charIndex * 2);
                        iLGenerator.Emit(OpCodes.Add);
                    }
                    iLGenerator.Emit(OpCodes.Ldind_I8);
                    iLGenerator.Emit(OpCodes.Ldc_I8, codeNode.compareValue);
                }
                if (codeNode.nodeType == FastFindSameLengthStringHelper.NodeType.CompareCreaterThan)
                {
                    iLGenerator.Emit(OpCodes.Ble, elseCase);
                }
                if (codeNode.nodeType == FastFindSameLengthStringHelper.NodeType.CompareLessThan)
                {
                    iLGenerator.Emit(OpCodes.Bge, elseCase);
                }
                #region If
                if (codeNode.ifCase != null)
                {
                    WriteCode(iLGenerator, ref codeNode.ifCase, length, columnLength);
                }
                #endregion
                iLGenerator.MarkLabel(elseCase);
                #region Else
                if (codeNode.elseCase != null)
                {
                    WriteCode(iLGenerator, ref codeNode.elseCase, length, columnLength);
                }
                #endregion
                iLGenerator.MarkLabel(endIfCase);
            }
            else
            {
                iLGenerator.Emit(OpCodes.Ldc_I4, codeNode.arrayIndex);
                iLGenerator.Emit(OpCodes.Ret);
            }
        }
#if EMITDEBUG
        public unsafe int FindSample(char* pKey, int length)
        {
            int a = 23;
            short b = 1;
            if (a < b)
            {
                return 1;
            }
            if (*(long*)(pKey) > 324234)
            {
                return 0;
            }
            else
            {
                return 1;
            }

            if (*(long*)(pKey) < 3424234)
            {
                return 0;
            }
            else
            {
                return 1;
            }
            return 0;
        }
#endif
    }
}
