//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : CompareDelegateHelper.cs
//        Description :字符串比较函数帮助类，用于生成基于一定长度的字符串比较函数
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
    //public delegate bool CompareDelegate(char* keyChar, char* CompareKeyChar, int length);
    /// <summary>
    /// 字符串比较函数帮助类，用于生成基于一定长度的字符串比较函数
    /// </summary>
    public unsafe class CompareDelegateHelper
    {
        /// <summary>
        /// 获取字符串比较函数
        /// </summary>
        /// <param name="length">两个字符串的长度</param>
        /// <returns></returns>
        public static CompareDelegate GetCompareDelegate(int length)
        {
            TypeBuilder typeBuilder = NFinal.Emit.UnSafeHelper.GetDynamicType();
            MethodBuilder dynamicMethod= typeBuilder.DefineMethod("Compare", MethodAttributes.Public 
                | MethodAttributes.Static | MethodAttributes.HideBySig, CallingConventions.Standard, 
                typeof(bool),new Type[]{ typeof(char*),typeof(char*),typeof(int) });
            ILGenerator methodIL= dynamicMethod.GetILGenerator();

            #region
            var fourCount = methodIL.DeclareLocal(typeof(int));
            //var position = methodIL.DeclareLocal(typeof(int));
            var remain = methodIL.DeclareLocal(typeof(int));
            
            int position = 0;
            var fourEnd = methodIL.DefineLabel();
            var methodEnd = methodIL.DefineLabel();
            while (position < (length - 3))
            {
                position += 4;
                methodIL.Emit(OpCodes.Ldarg_0);
                methodIL.Emit(OpCodes.Ldc_I4, position*2);
                methodIL.Emit(OpCodes.Add);
                methodIL.Emit(OpCodes.Ldind_I8);

                methodIL.Emit(OpCodes.Ldarg_1);
                methodIL.Emit(OpCodes.Ldc_I4, position * 2);
                methodIL.Emit(OpCodes.Add);
                methodIL.Emit(OpCodes.Ldind_I8);

                methodIL.Emit(OpCodes.Beq_S, fourEnd);
                methodIL.Emit(OpCodes.Ldc_I4_0);//false
                methodIL.Emit(OpCodes.Ret);
                
            }
            methodIL.MarkLabel(fourEnd);
            switch (length & 3)
            {
                case 1: {
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Ldc_I4, position*2);
                        methodIL.Emit(OpCodes.Add);
                        methodIL.Emit(OpCodes.Ldind_I2);

                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldc_I4, position*2);
                        methodIL.Emit(OpCodes.Add);
                        methodIL.Emit(OpCodes.Ldind_I2);

                        methodIL.Emit(OpCodes.Beq_S, methodEnd);
                        methodIL.Emit(OpCodes.Ldc_I4_0);
                        methodIL.Emit(OpCodes.Ret);
                    } break;
                case 2: {
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Ldc_I4, position * 2);
                        methodIL.Emit(OpCodes.Add);
                        methodIL.Emit(OpCodes.Ldind_I4);

                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldc_I4, position * 2);
                        methodIL.Emit(OpCodes.Add);
                        methodIL.Emit(OpCodes.Ldind_I4);

                        methodIL.Emit(OpCodes.Beq_S, methodEnd);
                        methodIL.Emit(OpCodes.Ldc_I4_0);
                        methodIL.Emit(OpCodes.Ret);
                    } break;
                case 3: {
                        methodIL.Emit(OpCodes.Ldarg_0);
                        methodIL.Emit(OpCodes.Ldc_I4, position * 2-1);
                        methodIL.Emit(OpCodes.Add);
                        methodIL.Emit(OpCodes.Ldind_I8);

                        methodIL.Emit(OpCodes.Ldarg_1);
                        methodIL.Emit(OpCodes.Ldc_I4, position * 2-1);
                        methodIL.Emit(OpCodes.Add);
                        methodIL.Emit(OpCodes.Ldind_I8);

                        methodIL.Emit(OpCodes.Beq_S, methodEnd);
                        methodIL.Emit(OpCodes.Ldc_I4_0);
                        methodIL.Emit(OpCodes.Ret);
                    } break;
            }
            #endregion
            methodIL.MarkLabel(methodEnd);
            methodIL.Emit(OpCodes.Ldc_I4_1);
            methodIL.Emit(OpCodes.Ret);

            return NFinal.Emit.UnSafeHelper.GetDelegate<CompareDelegate>(typeBuilder, "Compare");
        }
#if EMITDEBUG
        /// <summary>
        /// 所要生成函数的样子
        /// </summary>
        /// <param name="ptA">字符串指针A</param>
        /// <param name="ptB">字符串指针B</param>
        /// <param name="length">两字符串长度</param>
        /// <returns></returns>
        public unsafe static bool CompareA(char* ptA, char* ptB, int length)
        {
            int position = 0;
            while (position < (length-3))
            {
                if (*(long*)(ptA + position) != *(long*)(ptB + position))
                {
                    return false;
                }
                position += 4;
            }
            switch (length & 3)
            {
                case 1:
                    if (*(short*)(ptA + position) != *(short*)(ptB + position))
                    {
                        return false;
                    }break;
                case 2:
                    if (*(int*)(ptA + position) != *(int*)(ptB + position))
                    {
                        return false;
                    }break;
                case 3:
                    if (*(long*)(ptA + position-1) != *(long*)(ptB + position-1))
                    {
                        return false;
                    }break;
            }
            return true;
        }
        /// <summary>
        /// 测试函数有效性
        /// </summary>
        /// <returns></returns>
        public unsafe static bool Compare()
        {
            string strA = "abbdcc";
            string strB = "abbdcd";
            int length = strA.Length;
            fixed (char* ptA = strA) fixed (char* ptB = strB)
            {
                int fourCount = unchecked(length & 0xFFFFFFC);
                int position = 0;
                while (position < fourCount)
                {
                    if (*(long*)(ptA+ position) != *(long*)(ptB+ position))
                    {
                        return false;
                    }
                    position += 4;
                }
                int remain = length & 3;
                if (remain==1)
                {
                    if (*(short*)(ptA+ position) != *(short*)(ptB+ position))
                    {
                        return false;
                    }
                }
                if (remain == 2)
                {
                    if (*(int*)(ptA+ position) != *(int*)(ptB+ position))
                    {
                        return false;
                    }
                }
                if (remain == 3)
                {
                    position--;
                    if (*(long*)(ptA+ position) != *(long*)(ptB + position))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
#endif
    }
}
