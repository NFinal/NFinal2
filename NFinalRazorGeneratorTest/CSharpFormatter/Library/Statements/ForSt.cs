
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Exceptions;

namespace CSharpFormatter.Library.Statements
{
  public class ForSt : AbsStatement<ForSt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      if (psr.GetNextTextOrEmpty() == @"for")
      {
        try
        {
          sb.Append(psr.IndentToken(@"ForSt.Parse"));
          sb.Append(psr.Consume());
          if (fi.SpaceBetweenKeywordAndParan)
          {
            sb.Append(psr.SpaceToken());
          }
          if (psr.GetNextTypeOrUnknown() == TokenType.ParenthesesOpen)
          {
            sb.Append(psr.Consume());
            if (psr.GetNextTextOrEmpty() != @";")
            {
              var sb2 = new StringBuilder();
              var status2 = psr.SaveStatus();
              try
              {
                if (psr.GetNextTextOrEmpty() == @"var")
                {
                  sb2.Append(psr.Consume());
                }
                else
                {
                  sb2.Append(ParserUtils.Type(psr));
                }
                if (psr.GetNextTypeOrUnknown() == TokenType.Identifier)
                {
                  sb2.Append(psr.SpaceToken());
                  sb2.Append(psr.Consume());
                  sb.Append(sb2);
                }
                else
                {
                  throw new ResetException();
                }
              }
              catch (ResetException)
              {
                psr.LoadStatus(status2);
                if (psr.GetNextTypeOrUnknown() == TokenType.Identifier)
                {
                  sb.Append(psr.SpaceToken());
                  sb.Append(psr.Consume());
                }
                else
                {
                  throw new ResetException();
                }
              }
              if (psr.GetNextTextOrEmpty() == @"=")
              {
                sb.Append(psr.SpaceToken());
                sb.Append(psr.Consume());
                sb.Append(psr.SpaceToken());
                sb.Append(ParserUtils.Expr(psr));
              }
            }
            if (psr.GetNextTextOrEmpty() == @";")
            {
              sb.Append(psr.Consume());
              sb.Append(psr.SpaceToken());
              sb.Append(ParserUtils.Expr(psr));
              if (psr.GetNextTextOrEmpty() == @";")
              {
                sb.Append(psr.Consume());
                if (psr.GetNextTypeOrUnknown() != TokenType.ParenthesesClose)
                {
                  sb.Append(psr.SpaceToken());
                  sb.Append(ParserUtils.Expr(psr));
                }
                if (psr.GetNextTypeOrUnknown() == TokenType.ParenthesesClose)
                {
                  sb.Append(psr.Consume());
                  sb.Append(psr.LineBreakToken());

                  if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketOpen)
                  {
                    sb.Append(psr.IndentToken(@"ForSt.Parse"));
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
                      sb.Append(psr.IndentToken(@"ForSt.Parse"));
                      sb.Append(psr.Consume());
                      sb.Append(psr.LineBreakToken());
                      return sb;
                    }
                  }
                  else
                  {
                    psr.IndentDown();
                    sb.Append(psr.DefaultOneStatement());
                    psr.IndentUp();
                    return sb;
                  }
                }
              }
            }
          }
        }
        catch (ResetException)
        {
        }
        throw new ParseFatalException(@"Fatal ForSt!");
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }
  }
}
