using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFormatter.Library;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Parsers;

namespace CSharpFormatter
{
    public class FastFormat
    {
        public static string Format(string fileName)
        {
            
            var defaultFI = new FormatterInfo();
            Encoding enc = defaultFI.Enc;
            var indent = defaultFI.Indent;
            var useTab = defaultFI.UseTab;
            var overwrite = true;
            var crlf = defaultFI.Crlf;
            var sbkp = defaultFI.SpaceBetweenKeywordAndParan;
            var debugMode = defaultFI.DebugMode;

            FormatterInfo fi = new FormatterInfo(fileName,enc,indent,useTab,overwrite,crlf,sbkp,debugMode);
            var ts = Lexer.LexerFile(fi);
            var psr = new Parser(ts, fi);
            var result = psr.Evalute();
            IO.WriteFileCRLF(fileName, result.Output, fi.Enc);
            return result.Output;
        }
    }
}
