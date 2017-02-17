using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NFinal.Compile
{
    class ModelsTips
    {
        public static string TipsReg = @"(Models\s*\.\s*Tips\s*\.\s*([_a-zA-Z0-9]+)\s*\.\s*([_a-zA-Z0-9]+)|var)\s+([_a-zA-Z0-9]+)\s*=\s*new\s+Models\s*\.\s*Tips\s*\.\s*([_a-zA-Z0-9]+)\s*\.\s*([_a-zA-Z0-9]+)\s*\(\s*\)\s*;";
        public static void Delete(string csharpCode)
        {
            Regex reg = new Regex(TipsReg);
            MatchCollection mac= reg.Matches(csharpCode);

            for (int i = 0; i < mac.Count; i++)
            {
                
            }
        }
    }
}
