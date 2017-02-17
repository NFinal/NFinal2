
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Exceptions;

namespace CSharpFormatter.Library.Statements
{
  public class ContinueSt : AbsStatement<ContinueSt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      if (psr.GetNextTextOrEmpty() == @"continue")
      {
        try
        {
          sb.Append(psr.IndentToken(@"ContinueSt.Parse"));
          sb.Append(psr.Consume());
          if (psr.GetNextTypeOrUnknown() == TokenType.Semicolon)
          {
            sb.Append(psr.Consume());
            sb.Append(psr.LineBreakToken());
            return psr.WithComments(status.IndentLevel, sb);
          }
        }
        catch (ResetException)
        {
        }
        throw new ParseFatalException(@"Fatal ContinueSt!");
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }
  }
}
