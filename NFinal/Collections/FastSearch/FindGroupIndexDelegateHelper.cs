//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : FindGroupIndexDelegateHelper.cs
//        Description :查找到具有相同字符串长度的字符串信息组。
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
    /// 查找到具有相同字符串长度的字符串信息组
    /// </summary>
    public class FindGroupIndexDelegateHelper
    {
        /// <summary>
        /// 代码节点
        /// </summary>
        public class Node
        {
            /// <summary>
            /// 节点类型
            /// </summary>
            public NodeType nodeType;
            /// <summary>
            ///  比较值
            /// </summary>
            public int lessThanValue;
            /// <summary>
            /// 小于的节点
            /// </summary>
            public Node smallNode;
            /// <summary>
            /// 大于的节点
            /// </summary>
            public Node createNode;
            /// <summary>
            /// 组的索引
            /// </summary>
            public int arrayIndex;
        }
        /// <summary>
        /// 代码节点类型
        /// </summary>
        public enum NodeType
        {
            /// <summary>
            /// 比较
            /// </summary>
            Compare,
            /// <summary>
            /// 设置索引
            /// </summary>
            SetIndex
        }
        /// <summary>
        /// 组装并返回查找具有相同字符串长度的组的索引的函数代理
        /// </summary>
        /// <typeparam name="TValue">查找的值类型</typeparam>
        /// <param name="groupDataArray">具有相同字符串长度的组的数组</param>
        /// <returns></returns>
        public static FindGroupIndexDelegate GetFindGroupIndexDelegate<TValue>(GroupData<TValue>[] groupDataArray)
        {
            //Node rootNode = new Node();
            //FindGroupIndexDelegate(groupDataArray, rootNode, 0, groupDataArray.Length - 1);
            TypeBuilder typeBuilder = NFinal.Emit.UnSafeHelper.GetDynamicType();
            MethodBuilder dynamicMethod = typeBuilder.DefineMethod("FindGroupIndexDelegate", MethodAttributes.Public
                | MethodAttributes.Static | MethodAttributes.HideBySig, CallingConventions.Standard,
                typeof(int), new Type[] { typeof(int) });
            var methodIL = dynamicMethod.GetILGenerator();
            GenerateFindGroupIndexDelegate(methodIL, groupDataArray, 0, groupDataArray.Length - 1);
            return NFinal.Emit.UnSafeHelper.GetDelegate<FindGroupIndexDelegate>(typeBuilder, "FindGroupIndexDelegate");
        }
        /// <summary>
        /// 利用IL生成查找函数
        /// </summary>
        /// <typeparam name="TValue">查找的值</typeparam>
        /// <param name="methodIL">IL生成器</param>
        /// <param name="groupDataArray">具有相同字符串长度的组的数组</param>
        /// <param name="left">左指向位置</param>
        /// <param name="right">右指向位置</param>
        public static void GenerateFindGroupIndexDelegate<TValue>(ILGenerator methodIL,GroupData<TValue>[] groupDataArray, int left, int right)
        {
            int middle = (right + left + 1) >> 1;
            int lessThanValue = groupDataArray[middle].length;
            var elseStatement =  methodIL.DefineLabel();
            methodIL.Emit(OpCodes.Ldarg_0);
            methodIL.Emit(OpCodes.Ldc_I4,lessThanValue);
            methodIL.Emit(OpCodes.Bge_S, elseStatement);
            //if
            if (middle == left + 1)
            {
                methodIL.Emit(OpCodes.Ldc_I4, left);
                methodIL.Emit(OpCodes.Ret);
            }
            else
            {
                GenerateFindGroupIndexDelegate(methodIL, groupDataArray, left, middle - 1);
            }
            //else
            methodIL.MarkLabel(elseStatement);
            if (middle == right)
            {
                methodIL.Emit(OpCodes.Ldc_I4, right);
                methodIL.Emit(OpCodes.Ret);
            }
            else
            {
                GenerateFindGroupIndexDelegate(methodIL,groupDataArray,middle,right);
            }
        }
#if EMITDEBUG
        public static int SampleForIL<TValue>(GroupData<TValue>[] groupDataArray,int length)
        {
            if (length < 0)
            {
                return 0;
            }
            else
            {
                if (length < 2)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
        }
        public static void FindGroupIndexDelegate<TValue>(GroupData<TValue>[] groupDataArray,Node node, int left,int right)
        {
            int middle = (right + left + 1)>>1;
            node.nodeType = NodeType.Compare;
            node.lessThanValue = groupDataArray[middle].length;
            node.smallNode = new Node();
            if (middle == left + 1)
            {
                node.smallNode.nodeType = NodeType.SetIndex;
                node.smallNode.arrayIndex = left;
            }
            else
            {
                FindGroupIndexDelegate(groupDataArray, node.smallNode, left, middle - 1);
            }
            node.createNode = new Node();
            if (middle == right)
            {
                node.createNode.nodeType = NodeType.SetIndex;
                node.createNode.arrayIndex = right;
            }
            else
            {
                FindGroupIndexDelegate(groupDataArray, node.createNode, middle, right);
            }
        }
#endif
    }
}
