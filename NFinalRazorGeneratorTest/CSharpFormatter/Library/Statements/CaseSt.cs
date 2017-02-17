
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Exceptions;

namespace CSharpFormatter.Library.Statements
{
  public class CaseSt : AbsStatement<CaseSt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      if (psr.GetNextTextOrEmpty() == @"case")
      {
        try
        {
          sb.Append(psr.IndentToken(@"CaseSt.Parse"));
          sb.Append(psr.Consume());
          sb.Append(psr.SpaceToken());
          sb.Append(ParserUtils.Expr(psr));
          if (psr.GetNextTextOrEmpty() == @":")
          {
            sb.Append(psr.Consume());
            sb.Append(psr.LineBreakToken());

            psr.IndentDown();

            sb = psr.WithComments(status.IndentLevel, sb);

            sb.Append(psr.DefaultManyStatement());

            psr.IndentUp();

            return sb;
          }
        }
        catch (ResetException)
        {
        }
        throw new ParseFatalException(@"Fatal CaseSt!");
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }
  }
}
