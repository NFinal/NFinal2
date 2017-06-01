//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : KeyLongArrayCompare.cs
//        Description :字符串Key数组转成long二维数组之后的比较函数。
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

namespace NFinal.Collections.FastSearch
{
    /// <summary>
    /// 二维long数组的比较函数,用于快速排序
    /// </summary>
    public class KeyLongArrayCompare : IComparer<int>
    {
        /// <summary>
        /// 二维long数组
        /// </summary>
        public long[][] KeyLongArray;
        /// <summary>
        /// 列排序之后的数组
        /// </summary>
        public int[][] ColumnSortArray;
        /// <summary>
        /// 列索引
        /// </summary>
        public int column;
        /// <summary>
        /// 二维long数组元素比较
        /// </summary>
        /// <param name="keyLongArray">二维long数组</param>
        /// <param name="column">列索引</param>
        public KeyLongArrayCompare(long[][] keyLongArray, int column)
        {
            this.KeyLongArray = keyLongArray;
            this.column = column;
        }
        /// <summary>
        /// 比较long元素
        /// </summary>
        /// <param name="row1">行索引1</param>
        /// <param name="row2">行索引2</param>
        /// <returns></returns>
        public int Compare(int row1, int row2)
        {
            long temp = GetKeyLong(row1, column) - GetKeyLong(row2, column);
            if (temp > 0)
            {
                return 1;
            }
            else if (temp < 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 获取行列索引所对应的值
        /// </summary>
        /// <param name="RowIndex">行索引</param>
        /// <param name="ColumnIndex">列索引</param>
        /// <returns></returns>
        public long GetKeyLong(int RowIndex, int ColumnIndex)
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
    }
}
