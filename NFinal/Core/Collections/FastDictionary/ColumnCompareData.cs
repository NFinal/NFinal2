using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFinal.Collections
{
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
}
