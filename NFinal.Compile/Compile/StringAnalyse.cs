using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Compile
{
    public class StringAnalyse
    {
        public static string[] GetStringFromCode(string csharpCode, bool withDoubleQuotes)
        {
            System.Collections.Generic.List<string> stringList = new System.Collections.Generic.List<string>();
            char[] csharpCodeArray = csharpCode.ToCharArray();
            int start = 0;
            int end = 0;
            int num = 0;
            if (csharpCodeArray[0] == '\"')
            {
                start = 0;
                num++;
            }
            for (int i = 1; i < csharpCodeArray.Length; i++)
            {
                if (csharpCodeArray[i] == '\"' && csharpCodeArray[i - 1] != '\\')
                {
                    num++;
                    if ((num & 1) == 0 && num != 0)
                    {
                        end = i;
                        if (withDoubleQuotes)
                        {
                            stringList.Add(csharpCode.Substring(start, end - start));
                        }
                        else
                        {
                            stringList.Add(csharpCode.Substring(start + 1, end - start - 1));
                        }
                    }
                    else
                    {
                        start = i;
                    }
                }
            }
            return stringList.ToArray();
        }
    }
}
