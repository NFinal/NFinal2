using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Collections
{

    public class FastDictionary<TValue>
    {
        /// <summary>
        /// 存储Key字符串的数组
        /// </summary>
        public string[] keyArray;
        /// <summary>
        /// Value的数组
        /// </summary>
        public TValue[] valueArray;
        /// <summary>
        /// 转换为Long数组之后列的最大长度
        /// </summary>
        private int maxColumnCount = 0;
        /// <summary>
        /// 二分节点,用于构造代码
        /// </summary>
        private CodeNode rootNode;
        private GetKeyIndex getKeyIndex;

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="kvList"></param>
        /// <param name="count"></param>
        public unsafe FastDictionary(IEnumerable<KeyValuePair<string, TValue>> kvList, int count)
        {
            int i = 0;
            keyArray = new string[count];
            valueArray = new TValue[count];
            long[][] KeyLongArray = new long[count][];
            foreach (var kv in kvList)
            {
                keyArray[i] = kv.Key;
                valueArray[i] = kv.Value;
                KeyLongArray[i] = FastDictionaryUtility.GetArray(keyArray[i]);
                if (KeyLongArray[i].Length > maxColumnCount)
                {
                    maxColumnCount = KeyLongArray[i].Length;
                }
                i++;
            }
            // 列排序索引，用于对Long二维数组列进行排序
            int[][] ColumnSortArray = new int[maxColumnCount][];
            //初始化列索引数组
            for (int column = 0; column < maxColumnCount; column++)
            {
                ColumnSortArray[column] = new int[count];
                for (int row = 0; row < count; row++)
                {
                    ColumnSortArray[column][row] = row;
                }
            }
            rootNode = new CodeNode();
            Init(KeyLongArray, KeyLongArray, ColumnSortArray, ref rootNode);
            //StringBuilder sb = new StringBuilder();
            //WriteCode(sb, rootNode, 0);
            //string result = sb.ToString();
            ILWriter writer = new ILWriter();
            getKeyIndex = writer.Generate(rootNode);
        }

        /// <summary>
        /// 获取LongArray二维数组中的值
        /// </summary>
        /// <param name="KeyLongArray"></param>
        /// <param name="RowIndex"></param>
        /// <param name="ColumnIndex"></param>
        /// <returns></returns>
        public long GetKeyLong(long[][] KeyLongArray, int RowIndex, int ColumnIndex)
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
        public bool TryGetValue(string key, int length, out TValue value)
        {
            int index = getKeyIndex(key, length);
            if (string.Equals(keyArray[index], key,StringComparison.Ordinal))
            {
                value = valueArray[index];
                return true;
            }
            else
            {
                value = default(TValue);
                return false;
            }
        }
        public bool TryGetValue(string key, out TValue value)
        {
            int index = getKeyIndex(key, key.Length);
            if (string.Equals(keyArray[index], key,StringComparison.Ordinal))
            {
                value = valueArray[index];
                return true;
            }
            else
            {
                value = default(TValue);
                return false;
            }
        }
        public TValue this[int index]
        {
            get { return valueArray[index]; }
            set { valueArray[index] = value; }
        }
        public TValue this[string key, int length]
        {
            get
            {
                int index = getKeyIndex(key, length);
                if (string.Equals(keyArray[index], key))
                {
                    return valueArray[index];
                }
                else
                {
                    return default(TValue);
                }
            }
            set
            {
                int index = getKeyIndex(key, length);
                if (string.Equals(keyArray[index], key))
                {
                    valueArray[index] = value;
                }
            }
        }
        public TValue this[string key]
        {
            get
            {
                int index = getKeyIndex(key, key.Length);
                if (string.Equals(keyArray[index], key))
                {
                    return valueArray[index];
                }
                else
                {
                    return default(TValue);
                }
            }
            set
            {
                int index = getKeyIndex(key, key.Length);
                if (string.Equals(keyArray[index], key))
                {
                    valueArray[index] = value;
                }
            }
        }
        public void Init(long[][] FullKeyLongArray, long[][] KeyLongArray, int[][] ColumnSortArray, ref CodeNode codeNode)
        {
            if (KeyLongArray == null) return;
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
            ColumnCompareData[] columnCompareData = new ColumnCompareData[maxColumnCount]; ;
            //一列一列的解析出比较结果，如二分值，索引等
            for (int column = 0; column < maxColumnCount; column++)
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
                for (int column = 0; column < maxColumnCount; column++)
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
                    ColumnSortArraySmall = new int[maxColumnCount][];
                    for (int column = 0; column < maxColumnCount; column++)
                    {
                        ColumnSortArraySmall[column] = new int[KeyLongArraySmallLength];
                    }
                }
                KeyLongArrayBigLength = arrayLength - columnCompareData[BestColumnCompareDataIndex].midIndex - 1;
                if (KeyLongArrayBigLength > 0)
                {
                    KeyLongArrayBig = new long[KeyLongArrayBigLength][];
                    ColumnSortArrayBig = new int[maxColumnCount][];
                    for (int column = 0; column < maxColumnCount; column++)
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
                        for (int column = 0; column < maxColumnCount; column++)
                        {
                            ColumnSortArrayBig[column][bigIndex] = ColumnSortArray[BestColumnCompareDataIndex][row];
                        }
                        bigIndex++;
                    }
                    else
                    {
                        KeyLongArraySmall[smallIndex] = FullKeyLongArray[ColumnSortArray[BestColumnCompareDataIndex][row]];
                        for (int column = 0; column < maxColumnCount; column++)
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
                    ColumnSortArraySmall = new int[maxColumnCount][];
                    for (int column = 0; column < maxColumnCount; column++)
                    {
                        ColumnSortArraySmall[column] = new int[KeyLongArraySmallLength];
                    }
                }
                KeyLongArrayBigLength = arrayLength - columnCompareData[BestColumnCompareDataIndex].midIndex;
                if (KeyLongArrayBigLength > 0)
                {
                    KeyLongArrayBig = new long[KeyLongArrayBigLength][];
                    ColumnSortArrayBig = new int[maxColumnCount][];
                    for (int column = 0; column < maxColumnCount; column++)
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
                        for (int column = 0; column < maxColumnCount; column++)
                        {
                            ColumnSortArraySmall[column][smallIndex] = ColumnSortArray[BestColumnCompareDataIndex][row];
                        }
                        smallIndex++;
                    }
                    else
                    {
                        KeyLongArrayBig[bigIndex] = FullKeyLongArray[ColumnSortArray[BestColumnCompareDataIndex][row]];
                        for (int column = 0; column < maxColumnCount; column++)
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
                codeNode.nodeType = CodeNodeType.CompareCreaterThan;
                codeNode.compareValue = columnCompareData[BestColumnCompareDataIndex].midValue;
            }
            else
            {
                codeNode.nodeType = CodeNodeType.CompareLessThan;
                codeNode.compareValue = columnCompareData[BestColumnCompareDataIndex].midValue;
            }
            codeNode.ifCase = new CodeNode();
            codeNode.elseCase = new CodeNode();
            codeNode.charIndex = BestColumnCompareDataIndex << 2;
            //[1][2][mid]|[4] if(x>mid){ i=3}else{i=4}
            //[1][2][mid]|[4][5]
            if (codeNode.nodeType == CodeNodeType.CompareCreaterThan)
            {

                if (KeyLongArrayBigLength == 1)
                {
                    codeNode.ifCase.nodeType = CodeNodeType.SetIndex;
                    codeNode.ifCase.arrayIndex = ColumnSortArray[BestColumnCompareDataIndex][columnCompareData[BestColumnCompareDataIndex].midIndex + 1];
                    codeNode.ifCase.charIndex = codeNode.charIndex;
                }
                else
                {
                    Init(FullKeyLongArray, KeyLongArrayBig, ColumnSortArrayBig, ref codeNode.ifCase);
                }
                if (KeyLongArraySmallLength == 1)
                {
                    codeNode.elseCase.nodeType = CodeNodeType.SetIndex;
                    codeNode.elseCase.arrayIndex = ColumnSortArray[BestColumnCompareDataIndex][columnCompareData[BestColumnCompareDataIndex].midIndex];
                    codeNode.elseCase.charIndex = codeNode.charIndex;

                }
                else
                {
                    Init(FullKeyLongArray, KeyLongArraySmall, ColumnSortArraySmall, ref codeNode.elseCase);
                }
            }
            //[1][2]|[mid][4][5] if(x<mid){}else{}
            else if (codeNode.nodeType == CodeNodeType.CompareLessThan)
            {
                if (KeyLongArraySmallLength == 1)
                {
                    codeNode.ifCase.nodeType = CodeNodeType.SetIndex;
                    codeNode.ifCase.arrayIndex = ColumnSortArray[BestColumnCompareDataIndex][columnCompareData[BestColumnCompareDataIndex].midIndex - 1];
                    codeNode.ifCase.charIndex = codeNode.charIndex;
                }
                else
                {
                    Init(FullKeyLongArray, KeyLongArraySmall, ColumnSortArraySmall, ref codeNode.ifCase);
                }
                if (KeyLongArrayBigLength == 1)
                {
                    codeNode.elseCase.nodeType = CodeNodeType.SetIndex;
                    codeNode.elseCase.arrayIndex = ColumnSortArray[BestColumnCompareDataIndex][columnCompareData[BestColumnCompareDataIndex].midIndex];
                    codeNode.elseCase.charIndex = codeNode.charIndex;
                }
                else
                {
                    Init(FullKeyLongArray, KeyLongArrayBig, ColumnSortArrayBig, ref codeNode.elseCase);
                }
            }
        }
    }
}
