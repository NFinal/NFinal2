
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Exceptions;

namespace CSharpFormatter.Library.Statements
{
  public class TrySt : AbsStatement<TrySt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      if (psr.GetNextTextOrEmpty() == @"try")
      {
        try
        {
          sb.Append(psr.IndentToken(@"TrySt.Parse"));
          sb.Append(psr.Consume());
          sb.Append(psr.LineBreakToken());

          if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketOpen)
          {
            sb.Append(psr.IndentToken(@"TrySt.Parse"));
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
              sb.Append(psr.IndentToken(@"TrySt.Parse"));
              sb.Append(psr.Consume());
              sb.Append(psr.LineBreakToken());

              sb.Append(psr.ManyStatement(new ParseFunc[]{
                DirectiveIfSt.Singleton(),
                CatchSt.Singleton(),
              }));

              sb.Append(psr.ManyStatement(new ParseFunc[]{
                DirectiveIfSt.Singleton(),
                FinallySt.Singleton(),
              }));

              return sb;
            }
          }
        }
        catch (ResetException)
        {
        }
        throw new ParseFatalException(@"Fatal TrySt!");
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }
  }

  public class CatchSt : AbsStatement<CatchSt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      if (psr.GetNextTextOrEmpty() == @"catch")
      {
        try
        {
          sb.Append(psr.IndentToken(@"CatchSt.Parse"));
          sb.Append(psr.Consume());
          if (fi.SpaceBetweenKeywordAndParan)
          {
            sb.Append(psr.SpaceToken());
          }
          if (psr.GetNextTypeOrUnknown() == TokenType.ParenthesesOpen)
          {
            sb.Append(psr.Consume());
            sb.Append(ParserUtils.Expr(psr));
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
                sb.Append(psr.IndentToken(@"CatchSt.Parse"));
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
                  sb.Append(psr.IndentToken(@"CatchSt.Parse"));
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
        throw new ParseFatalException(@"Fatal CatchSt!");
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }
  }

  public class FinallySt : AbsStatement<FinallySt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      if (psr.GetNextTextOrEmpty() == @"finally")
      {
        try
        {
          sb.Append(psr.IndentToken(@"FinallySt.Parse"));
          sb.Append(psr.Consume());
          sb.Append(psr.LineBreakToken());

          if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketOpen)
          {
            sb.Append(psr.IndentToken(@"FinallySt.Parse"));
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
              sb.Append(psr.IndentToken(@"FinallySt.Parse"));
              sb.Append(psr.Consume());
              sb.Append(psr.LineBreakToken());
              return sb;
            }
          }
        }
        catch (ResetException)
        {
        }
        throw new ParseFatalException(@"Fatal FinallySt!");
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }
  }
}
