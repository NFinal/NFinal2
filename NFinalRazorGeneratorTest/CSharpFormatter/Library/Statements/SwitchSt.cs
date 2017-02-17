
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Exceptions;

namespace CSharpFormatter.Library.Statements
{
  public class SwitchSt : AbsStatement<SwitchSt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      if (psr.GetNextTextOrEmpty() == @"switch")
      {
        try
        {
          sb.Append(psr.IndentToken(@"SwitchSt.Parse"));
          sb.Append(psr.Consume());
          if (fi.SpaceBetweenKeywordAndParan)
          {
            sb.Append(psr.SpaceToken());
          }
          if (psr.GetNextTypeOrUnknown() == TokenType.ParenthesesOpen)
          {
            sb.Append(psr.Consume());
            sb.Append(ParserUtils.Expr(psr));
            if (psr.GetNextTypeOrUnknown() == TokenType.ParenthesesClose)
            {
              sb.Append(psr.Consume());
              sb.Append(psr.LineBreakToken());

              if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketOpen)
              {
                sb.Append(psr.IndentToken(@"SwitchSt.Parse"));
                sb.Append(psr.Consume());
                sb.Append(psr.LineBreakToken());
                psr.IndentDown();

                sb = psr.WithComments(status.IndentLevel, sb);

                if (psr.GetNextTypeOrUnknown() != TokenType.CurlyBracketClose)
                {
                  sb.Append(psr.ManyStatement(new ParseFunc[]{
                    DirectiveIfSt.Singleton(),
                    CaseSt.Singleton(),
                    DefaultSt.Singleton(),
                  }));
                }

                if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketClose)
                {
                  psr.IndentUp();
                  sb.Append(psr.IndentToken(@"SwitchSt.Parse"));
                  sb.Append(psr.Consume());
                  sb.Append(psr.LineBreakToken());
                  return sb;
                }
              }
            }
          }
        }
        catch (ResetException)
        {
        }
        throw new ParseFatalException(@"Fatal SwitchSt!");
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }
  }
}
