using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Collections.FastSearch
{
    public class FastFindSameLengthStringHelper
    {
        public class Node
        {
            /// <summary>
            /// 对比的数字
            /// </summary>
            public long compareValue;
            /// <summary>
            /// if 语句节点
            /// </summary>
            public Node ifCase;
            /// <summary>
            /// else语句节点
            /// </summary>
            public Node elseCase;
            /// <summary>
            /// 节点的类型
            /// </summary>
            public NodeType nodeType;
            /// <summary>
            /// 对比的列数
            /// </summary>
            public int charIndex;
            /// <summary>
            /// 当只有一个元素时，返回该元素的索引
            /// </summary>
            public int arrayIndex;
        }
        public enum NodeType
        {
            CompareCreaterThan,
            CompareLessThan,
            SetIndex
        }
        public struct ColumnCompareData
        {
            public int[] ColumnSort;
            public bool hasMidValue;
            public long midValue;
            public int midValueIndex;
            public int midIndex;
            public LongCompare compare;
            public int[] ColumnBig;
            public int[] ColumnSmall;
            public int midDistance;
        }
        public enum LongCompare
        {
            LessThan,
            CreaterThan
        }
        public unsafe static FindDelegate GetFastFindSameLengthStringDelegate<TValue>(List<KV<TValue>> list, int length)
        {
            int rowCount = list.Count;
            int columnLength = (length + 3) >> 2;
            long[][] KeyLongArray = new long[rowCount][];
            for (int row = 0; row < rowCount; row++)
            {
                fixed (char* pKey = list[row].key)
                {
                    KeyLongArray[row] = new long[columnLength];
                    for (int col = 0; col < columnLength; col++)
                    {
                        if (col == columnLength - 1)
                        {
                            int remain = length & 3;
                            if (remain == 0)
                            {
                                KeyLongArray[row][col] = *(long*)(pKey + col * 4);
                            }
                            else if (remain == 1)
                            {
                                KeyLongArray[row][col] = *(short*)(pKey + col * 4);
                            }
                            else if (remain == 2)
                            {
                                KeyLongArray[row][col] = *(int*)(pKey + col * 4);
                            }
                            else if (remain == 3)
                            {
                                KeyLongArray[row][col] = *(long*)(pKey + col * 4 - 1);
                            }
                        }
                        else
                        {
                            KeyLongArray[row][col] = *(long*)(pKey + col * 4);
                        }
                    }
                }
            }
            // 列排序索引，用于对Long二维数组列进行排序
            int[][] ColumnSortArray = new int[columnLength][];
            //初始化列索引数组
            for (int column = 0; column < columnLength; column++)
            {
                ColumnSortArray[column] = new int[rowCount];
                for (int row = 0; row < rowCount; row++)
                {
                    ColumnSortArray[column][row] = row;
                }
            }
            Node rootNode = new Node();
            Init(KeyLongArray,KeyLongArray,columnLength, ColumnSortArray, ref rootNode);
            ILWriter iLWriter = new ILWriter();
            return iLWriter.Generate(rootNode, length, columnLength);
        }
        public static void Init(long[][] FullKeyLongArray, long[][] KeyLongArray,int columnLength, int[][] ColumnSortArray, ref Node codeNode)
        {
            if (KeyLongArray == null) return;
            if (FullKeyLongArray.Length == 1)
            {
                codeNode.nodeType = NodeType.SetIndex;
                codeNode.arrayIndex = 0;
                return;
            }
            // KeyValue数组长度
            int arrayLength = KeyLongArray.Length;
            //是否为奇数数组
            bool isEvenArray = (arrayLength & 1) == 0;
            //找到的最好中分的那个列
            int BestColumnCompareDataIndex = 0;
            //数组为奇数长度时，最中间的索引值
            int midIndex = 0;
            //数组为偶数长度时，中间左右两边的值
            int midLeftIndex, midRightIndex = 0;
            //距中间值的距离
            int midDistance = 0;
            //距中间值的最长距离
            int midDistanceLength = 0;
            //是否具有中间值，如果一个列中所有元素均相同，则无中间值
            bool hasMidValue = false;
            //是否找到了直接二分值
            bool hasFindBestColumnCompareDataIndex = false;
            //数组中每列比较的结果
            ColumnCompareData[] columnCompareData = new ColumnCompareData[columnLength]; ;
            //一列一列的解析出比较结果，如二分值，索引等
            for (int column = 0; column < columnLength; column++)
            {
                //对列索引数组进行排序
                Array.Sort<int>(ColumnSortArray[column], new KeyLongArrayCompare(FullKeyLongArray, column));
                //是否有二分值
                hasMidValue = false;
                //如果数组长度为偶数
                if (isEvenArray)
                {
                    //中间距两边的距离
                    midDistanceLength = arrayLength >> 1;
                    //中间靠左边的索引值
                    midLeftIndex = (arrayLength >> 1) - 1;
                    //中间靠右边的索引值
                    midRightIndex = midLeftIndex + 1;
                    //找到与中间值值不同的行，并记录下来。
                    long LeftValue = GetKeyLong(FullKeyLongArray, ColumnSortArray[column][midLeftIndex], column);
                    long RightValue = GetKeyLong(FullKeyLongArray, ColumnSortArray[column][midRightIndex], column);
                    //[1][2][3][4]
                    //[1][Left]|[Right][4]
                    if (LeftValue != RightValue)
                    {
                        BestColumnCompareDataIndex = column;
                        hasFindBestColumnCompareDataIndex = true;
                        hasMidValue = true;
                        columnCompareData[column].hasMidValue = hasMidValue;
                        columnCompareData[column].midValue = RightValue;
                        columnCompareData[column].midValueIndex = ColumnSortArray[column][midRightIndex];
                        columnCompareData[column].compare = LongCompare.LessThan;
                        columnCompareData[column].midIndex = midRightIndex;
                        columnCompareData[column].midDistance = 0;
                        break;
                    }
                    else
                    {
                        //[1][2][3][4]
                        for (midDistance = 1; midDistance < midDistanceLength; midDistance++)
                        {
                            //[1]|[Left][3][4],2
                            if (LeftValue != GetKeyLong(FullKeyLongArray, ColumnSortArray[column][midLeftIndex - midDistance], column))
                            {
                                hasMidValue = true;
                                columnCompareData[column].hasMidValue = hasMidValue;
                                columnCompareData[column].midValue = LeftValue;
                                columnCompareData[column].midValueIndex = ColumnSortArray[column][midLeftIndex];
                                columnCompareData[column].compare = LongCompare.LessThan;
                                columnCompareData[column].midIndex = midLeftIndex;
                                columnCompareData[column].midDistance = midDistance;
                            }
                            //[1][2][Right]|[4]
                            if (RightValue != GetKeyLong(FullKeyLongArray, ColumnSortArray[column][midRightIndex + midDistance], column))
                            {
                                hasMidValue = true;
                                columnCompareData[column].hasMidValue = hasMidValue;
                                columnCompareData[column].midValue = RightValue;
                                columnCompareData[column].midValueIndex = ColumnSortArray[column][midRightIndex];
                                columnCompareData[column].compare = LongCompare.CreaterThan;
                                columnCompareData[column].midIndex = midRightIndex;
                                columnCompareData[column].midDistance = midDistance;
                            }
                        }
                    }
                }
                //如果数组长度为奇数
                else
                {
                    //中间到两边的距离
                    midDistanceLength = arrayLength >> 1;
                    //中间值的索引
                    midIndex = (arrayLength >> 1);
                    //找出与中间值不同的行
                    //[1][2][mid][3][4]
                    long midValue = GetKeyLong(FullKeyLongArray, ColumnSortArray[column][midIndex], column);
                    for (midDistance = 0; midDistance < midDistanceLength; midDistance++)
                    {
                        //[1][2]|[mid][3][4]
                        if (midValue != GetKeyLong(FullKeyLongArray, ColumnSortArray[column][midIndex - midDistance - 1], column))
                        {
                            hasMidValue = true;
                            columnCompareData[column].hasMidValue = hasMidValue;
                            columnCompareData[column].midValue = midValue;
                            columnCompareData[column].midValueIndex = ColumnSortArray[column][midIndex - midDistance - 1];
                            columnCompareData[column].compare = LongCompare.LessThan;
                            columnCompareData[column].midIndex = midIndex;
                            columnCompareData[column].midDistance = midDistance;
                            if (midDistance == 0)
                            {
                                BestColumnCompareDataIndex = column;
                                hasFindBestColumnCompareDataIndex = true;
                                break;
                            }
                        }
                        //[1][2][mid]|[3][4]
                        if (midValue != GetKeyLong(FullKeyLongArray, ColumnSortArray[column][midIndex + midDistance + 1], column))
                        {
                            hasMidValue = true;
                            columnCompareData[column].hasMidValue = hasMidValue;
                            columnCompareData[column].midValue = midValue;
                            columnCompareData[column].compare = LongCompare.CreaterThan;
                            columnCompareData[column].midValueIndex = ColumnSortArray[column][midIndex + midDistance + 1];
                            columnCompareData[column].midIndex = midIndex;
                            columnCompareData[column].midDistance = midDistance;
                            if (midDistance == 0)
                            {
                                BestColumnCompareDataIndex = column;
                                hasFindBestColumnCompareDataIndex = true;
                                break;
                            }
                        }
                    }
                }
            }
            //如果没有直接找到了二分索引
            if (!hasFindBestColumnCompareDataIndex)
            {
                //距离中间最小的距离
                int minMidDistance = midDistanceLength;
                //找到最合适的二分索引，即距两边距离最短的
                for (int column = 0; column < columnLength; column++)
                {
                    if (columnCompareData[column].midDistance < minMidDistance)
                    {
                        minMidDistance = columnCompareData[column].midDistance;
                        BestColumnCompareDataIndex = column;
                    }
                }
            }
            //两个数组Big和Small
            long[][] KeyLongArrayBig = null;
            long[][] KeyLongArraySmall = null;
            int[][] ColumnSortArrayBig = null;
            int[][] ColumnSortArraySmall = null;
            int KeyLongArrayBigLength = 0;
            int KeyLongArraySmallLength = 0;
            //[1][2][midValue]|[3][4]
            if (columnCompareData[BestColumnCompareDataIndex].compare == LongCompare.CreaterThan)
            {
                KeyLongArraySmallLength = columnCompareData[BestColumnCompareDataIndex].midIndex + 1;
                if (KeyLongArraySmallLength > 0)
                {
                    KeyLongArraySmall = new long[KeyLongArraySmallLength][];
                    //初始化排序数组
                    ColumnSortArraySmall = new int[columnLength][];
                    for (int column = 0; column < columnLength; column++)
                    {
                        ColumnSortArraySmall[column] = new int[KeyLongArraySmallLength];
                    }
                }
                KeyLongArrayBigLength = arrayLength - columnCompareData[BestColumnCompareDataIndex].midIndex - 1;
                if (KeyLongArrayBigLength > 0)
                {
                    KeyLongArrayBig = new long[KeyLongArrayBigLength][];
                    ColumnSortArrayBig = new int[columnLength][];
                    for (int column = 0; column < columnLength; column++)
                    {
                        ColumnSortArrayBig[column] = new int[KeyLongArrayBigLength];
                    }
                }
                int smallIndex = 0;
                int bigIndex = 0;
                for (int row = 0; row < arrayLength; row++)
                {
                    if (row > columnCompareData[BestColumnCompareDataIndex].midIndex)
                    {
                        KeyLongArrayBig[bigIndex] = FullKeyLongArray[ColumnSortArray[BestColumnCompareDataIndex][row]];
                        for (int column = 0; column < columnLength; column++)
                        {
                            ColumnSortArrayBig[column][bigIndex] = ColumnSortArray[BestColumnCompareDataIndex][row];
                        }
                        bigIndex++;
                    }
                    else
                    {
                        KeyLongArraySmall[smallIndex] = FullKeyLongArray[ColumnSortArray[BestColumnCompareDataIndex][row]];
                        for (int column = 0; column < columnLength; column++)
                        {
                            ColumnSortArraySmall[column][smallIndex] = ColumnSortArray[BestColumnCompareDataIndex][row];
                        }
                        smallIndex++;
                    }
                }
            }
            //[1][2]|[midValue][3][4]
            else if (columnCompareData[BestColumnCompareDataIndex].compare == LongCompare.LessThan)
            {
                KeyLongArraySmallLength = columnCompareData[BestColumnCompareDataIndex].midIndex;
                if (KeyLongArraySmallLength > 0)
                {
                    KeyLongArraySmall = new long[KeyLongArraySmallLength][];
                    ColumnSortArraySmall = new int[columnLength][];
                    for (int column = 0; column < columnLength; column++)
                    {
                        ColumnSortArraySmall[column] = new int[KeyLongArraySmallLength];
                    }
                }
                KeyLongArrayBigLength = arrayLength - columnCompareData[BestColumnCompareDataIndex].midIndex;
                if (KeyLongArrayBigLength > 0)
                {
                    KeyLongArrayBig = new long[KeyLongArrayBigLength][];
                    ColumnSortArrayBig = new int[columnLength][];
                    for (int column = 0; column < columnLength; column++)
                    {
                        ColumnSortArrayBig[column] = new int[KeyLongArrayBigLength];
                    }
                }
                int smallIndex = 0;
                int bigIndex = 0;
                for (int row = 0; row < arrayLength; row++)
                {
                    if (row < columnCompareData[BestColumnCompareDataIndex].midIndex)
                    {
                        KeyLongArraySmall[smallIndex] = FullKeyLongArray[ColumnSortArray[BestColumnCompareDataIndex][row]];
                        for (int column = 0; column < columnLength; column++)
                        {
                            ColumnSortArraySmall[column][smallIndex] = ColumnSortArray[BestColumnCompareDataIndex][row];
                        }
                        smallIndex++;
                    }
                    else
                    {
                        KeyLongArrayBig[bigIndex] = FullKeyLongArray[ColumnSortArray[BestColumnCompareDataIndex][row]];
                        for (int column = 0; column < columnLength; column++)
                        {
                            ColumnSortArrayBig[column][bigIndex] = ColumnSortArray[BestColumnCompareDataIndex][row];
                        }
                        bigIndex++;
                    }
                }
            }
            //解析最终结果到CodeNode节点中，用于生成代码用
            if (columnCompareData[BestColumnCompareDataIndex].compare == LongCompare.CreaterThan)
            {
                codeNode.nodeType = NodeType.CompareCreaterThan;
                codeNode.compareValue = columnCompareData[BestColumnCompareDataIndex].midValue;
            }
            if(columnCompareData[BestColumnCompareDataIndex].compare==LongCompare.LessThan)
            {
                codeNode.nodeType = NodeType.CompareLessThan;
                codeNode.compareValue = columnCompareData[BestColumnCompareDataIndex].midValue;
            }
            //codeNode.ifCase = new Node();
            //codeNode.elseCase = new Node();
            codeNode.charIndex = BestColumnCompareDataIndex << 2;
            //[1][2][mid]|[4] if(x>mid){ i=3}else{i=4}
            //[1][2][mid]|[4][5]
            if (codeNode.nodeType == NodeType.CompareCreaterThan)
            {
                if (KeyLongArrayBigLength == 0)
                {
                    codeNode.ifCase = null;
                }
                else
                {
                    codeNode = new Node();
                }
                if (KeyLongArrayBigLength == 1)
                {
                    codeNode.ifCase.nodeType = NodeType.SetIndex;
                    codeNode.ifCase.arrayIndex = ColumnSortArray[BestColumnCompareDataIndex][columnCompareData[BestColumnCompareDataIndex].midIndex + 1];
                    codeNode.ifCase.charIndex = codeNode.charIndex;
                }
                else
                {
                    Init(FullKeyLongArray, KeyLongArrayBig,columnLength, ColumnSortArrayBig, ref codeNode.ifCase);
                }
                if (KeyLongArraySmallLength == 0)
                {
                    codeNode.elseCase = null;
                }
                else
                {
                    codeNode.elseCase = new Node();
                }
                if (KeyLongArraySmallLength == 1)
                {
                    codeNode.elseCase.nodeType = NodeType.SetIndex;
                    codeNode.elseCase.arrayIndex = ColumnSortArray[BestColumnCompareDataIndex][columnCompareData[BestColumnCompareDataIndex].midIndex];
                    codeNode.elseCase.charIndex = codeNode.charIndex;

                }
                else
                {
                    Init(FullKeyLongArray, KeyLongArraySmall,columnLength, ColumnSortArraySmall, ref codeNode.elseCase);
                }
            }
            //[1][2]|[mid][4][5] if(x<mid){}else{}
            else if (codeNode.nodeType == NodeType.CompareLessThan)
            {
                if (KeyLongArraySmallLength == 0)
                {
                    codeNode.ifCase = null;
                }
                else
                {
                    codeNode.ifCase = new Node();
                }
                if (KeyLongArraySmallLength == 1)
                {
                    codeNode.ifCase.nodeType = NodeType.SetIndex;
                    codeNode.ifCase.arrayIndex = ColumnSortArray[BestColumnCompareDataIndex][columnCompareData[BestColumnCompareDataIndex].midIndex - 1];
                    codeNode.ifCase.charIndex = codeNode.charIndex;
                }
                else
                {
                    Init(FullKeyLongArray, KeyLongArraySmall,columnLength, ColumnSortArraySmall, ref codeNode.ifCase);
                }
                if (KeyLongArrayBigLength == 0)
                {
                    codeNode.elseCase = null;
                }
                else
                {
                    codeNode.elseCase = new Node();
                }
                if (KeyLongArrayBigLength == 1)
                {
                    codeNode.elseCase.nodeType = NodeType.SetIndex;
                    codeNode.elseCase.arrayIndex = ColumnSortArray[BestColumnCompareDataIndex][columnCompareData[BestColumnCompareDataIndex].midIndex];
                    codeNode.elseCase.charIndex = codeNode.charIndex;
                }
                else
                {
                    Init(FullKeyLongArray, KeyLongArrayBig,columnLength, ColumnSortArrayBig, ref codeNode.elseCase);
                }
            }
        }
        public static long GetKeyLong(long[][] KeyLongArray, int RowIndex, int ColumnIndex)
        {
            if (ColumnIndex < KeyLongArray[RowIndex].Length)
            {
                return KeyLongArray[RowIndex][ColumnIndex];
            }
            else
            {
                return 0;
            }
        }
        public static unsafe long GetLong(char* pKey, int index,int length)
        {
            int positionLength = (length + 3) >> 2;
            if (index == positionLength - 1)
            {
                int remain = length & 3;
                if (remain == 0)
                {
                    return *(long*)(pKey + index * 4);
                }
                else if (remain == 1)
                {
                    return *(short*)(pKey + index * 4);
                }
                else if (remain == 2)
                {
                    return *(int*)(pKey + index * 4);
                }
                else if (remain == 3)
                {
                    return *(long*)(pKey + index * 4 - 1);
                }
                else
                {
                    return 0;//没用。
                }
            }
            else
            {
                return *(long*)(pKey + index * 4);
            }
        }
        public unsafe static void GetFastFindSameLengthStringSample(char* pt,int length)
        {
            if (*(long*)(pt) < 343)
            {
                if (*(short*)pt > 4545)
                {

                }
                else
                {

                }
            }
            else
            {

            }
        }
    }
}
