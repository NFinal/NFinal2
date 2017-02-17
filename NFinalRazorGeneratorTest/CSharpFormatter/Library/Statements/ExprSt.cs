
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Exceptions;

namespace CSharpFormatter.Library.Statements
{
  public class ExprSt : AbsStatement<ExprSt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      try
      {
        sb.Append(psr.IndentToken(@"ExprSt.Parse"));
        sb.Append(ParserUtils.Expr(psr));
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
      psr.LoadStatus(status);
      throw new ResetException();
    }
  }
}
