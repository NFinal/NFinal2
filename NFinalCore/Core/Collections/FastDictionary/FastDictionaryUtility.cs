using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Collections
{
    public class FastDictionaryUtility
    {
        public unsafe static long[] GetArray(string key)
        {
            int length = key.Length;
            int remain = length;
            int longCount = 0;
            if ((length & 3) == 0)
            {
                longCount = length >> 2;
            }
            else
            {
                longCount = (length >> 2) + 1;
            }
            long[] longArray = new long[longCount];
            fixed (char* p = key)
            {
                char* pt = p;
                int i = 0;
                long value = 0;
                while (remain != 0)
                {
                    if (remain > 3)
                    {
                        value = *(long*)pt;
                        pt += 4;
                        remain -= 4;
                    }
                    else if (remain == 3)
                    {
                        value = (((long)(*(int*)pt)) << 16) + (*(short*)(pt + 2));
                        pt += 3;
                        remain -= 3;
                    }
                    else if (remain == 2)
                    {
                        value = *(int*)(pt);
                        pt += 2;
                        remain -= 2;
                    }
                    else if (remain > 0)
                    {
                        value = *pt;
                        pt++;
                        remain--;
                    }
                    longArray[i] = value;
                    i++;
                }
            }
            return longArray;
        }
    }
}