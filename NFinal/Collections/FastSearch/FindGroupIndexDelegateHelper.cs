using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace NFinal.Collections.FastSearch
{
    public class FindGroupIndexDelegateHelper
    {
        public class Node
        {
            public NodeType nodeType;
            public int lessThanValue;
            public Node smallNode;
            public Node createNode;
            public int arrayIndex;
        }
        public enum NodeType
        {
            Compare,
            SetIndex
        }
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
    }
}
