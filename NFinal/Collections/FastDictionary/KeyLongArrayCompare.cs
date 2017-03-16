using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFinal.Collections
{
    public class KeyLongArrayCompare : IComparer<int>
    {
        public long[][] KeyLongArray;
        public int[][] ColumnSortArray;
        public int column;
        public KeyLongArrayCompare(long[][] keyLongArray, int column)
        {
            this.KeyLongArray = keyLongArray;
            this.column = column;
        }
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
