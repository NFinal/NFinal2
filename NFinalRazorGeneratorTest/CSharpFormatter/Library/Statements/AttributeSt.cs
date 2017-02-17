
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Exceptions;

namespace CSharpFormatter.Library.Statements
{
  public class AttributeSt : AbsStatement<AttributeSt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      if (psr.GetNextTextOrEmpty() == @"[")
      {
        try
        {
          sb.Append(psr.IndentToken(@"AttributeSt.Parse"));
          sb.Append(ParserUtils.Attribute(psr));
          sb.Append(psr.LineBreakToken());
          return psr.WithComments(status.IndentLevel, sb);
        }
        catch (ResetException)
        {
        }
        throw new ParseFatalException(@"Fatal AttributeSt.Parse!");
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }
  }
}
