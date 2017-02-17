
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Exceptions;

namespace CSharpFormatter.Library.Statements
{
  public class CheckedSt : AbsStatement<CheckedSt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      if (psr.GetNextTextOrEmpty() == @"checked")
      {
        try
        {
          sb.Append(psr.IndentToken(@"CheckedSt.Parse"));
          sb.Append(psr.Consume());
          sb.Append(psr.LineBreakToken());

          if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketOpen)
          {
            sb.Append(psr.IndentToken(@"CheckedSt.Parse"));
            sb.Append(psr.Consume());
            sb.Append(psr.LineBreakToken());
            psr.IndentDown();

            sb = psr.WithComments(status.IndentLevel, sb);

            if (psr.GetNextTypeOrUnknown() != TokenType.CurlyBracketClose)
            {
              sb.Append(psr.DefaultManyStatement());
            }

            if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketClose)
            {
              psr.IndentUp();
              sb.Append(psr.IndentToken(@"CheckedSt.Parse"));
              sb.Append(psr.Consume());
              sb.Append(psr.LineBreakToken());
              return sb;
            }
          }
        }
        catch (ResetException)
        {
        }
        throw new ParseFatalException(@"Fatal CheckedSt!");
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }
  }
}
