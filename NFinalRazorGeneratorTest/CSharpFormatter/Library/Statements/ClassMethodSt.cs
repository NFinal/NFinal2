
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Exceptions;

namespace CSharpFormatter.Library.Statements
{
  public class ClassMethodSt : AbsStatement<ClassMethodSt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      try
      {
        sb.Append(psr.IndentToken(@"ClassMethodSt.Parse"));
        sb.Append(ParserUtils.AccessModifiers(psr));
        var doExit = false;
        while (!doExit)
        {
          switch (psr.GetNextTextOrEmpty())
          {
            case @"virtual":
            case @"override":
            case @"extern":
            case @"static":
              sb.Append(psr.Consume());
              sb.Append(psr.SpaceToken());
              break;
            default:
              doExit = true;
              break;
          }
        }
        // constractor
        if (psr.GetNextTypeOrUnknown() == TokenType.Identifier && psr.GetNextTypeOrUnknown(1) == TokenType.ParenthesesOpen)
        {
          sb.Append(psr.Consume());
        }
        // destractor
        else if (psr.GetNextTextOrEmpty() == @"~" && psr.GetNextTypeOrUnknown(1) == TokenType.Identifier && psr.GetNextTypeOrUnknown(2) == TokenType.ParenthesesOpen)
        {
          sb.Append(psr.Consume());
          sb.Append(psr.Consume());
        }
        else
        {
          sb.Append(ParserUtils.Type(psr));
          if (psr.GetNextTypeOrUnknown() == TokenType.Identifier)
          {
            sb.Append(psr.SpaceToken());
            sb.Append(psr.Consume());
          }
        }
        if (psr.GetNextTypeOrUnknown() == TokenType.ParenthesesOpen)
        {
          sb.Append(psr.Consume());
          while (psr.GetNextTypeOrUnknown() != TokenType.ParenthesesClose)
          {
            var attrCount = 0;
            while (true)
            {
              try
              {
                sb.Append(ParserUtils.Attribute(psr));
                attrCount++;
              }
              catch (ResetException)
              {
                break;
              }
            }
            if (0 < attrCount)
            {
              sb.Append(psr.SpaceToken());
            }
            sb.Append(ParserUtils.RefOutIn(psr));
            sb.Append(ParserUtils.Type(psr));
            sb.Append(psr.SpaceToken());
            if (psr.GetNextTypeOrUnknown() == TokenType.Identifier)
            {
              sb.Append(psr.Consume());
              if (psr.GetNextTextOrEmpty() == @"=")
              {
                sb.Append(psr.SpaceToken());
                sb.Append(psr.Consume());
                sb.Append(psr.SpaceToken());
                sb.Append(ParserUtils.Expr(psr));
              }
              if (psr.GetNextTextOrEmpty() == @",")
              {
                sb.Append(psr.Consume());
                sb.Append(psr.SpaceToken());
              }
            }
            else
            {
              throw new ParseFatalException(@"Fatal ClassMethodSt!");
            }
          }
          if (psr.GetNextTypeOrUnknown() == TokenType.ParenthesesClose)
          {
            sb.Append(psr.Consume());

            if (psr.GetNextTextOrEmpty() == @":")
            {
              sb.Append(psr.SpaceToken());
              sb.Append(psr.Consume());
              sb.Append(psr.SpaceToken());
              sb.Append(ParserUtils.Expr(psr));
            }

            if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketOpen)
            {
              sb.Append(psr.LineBreakToken());
              sb.Append(psr.IndentToken(@"ClassMethodSt.Parse"));
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
                sb.Append(psr.IndentToken(@"ClassMethodSt.Parse"));
                sb.Append(psr.Consume());
                sb.Append(psr.LineBreakToken());
                return sb;
              }
            }
            else if (psr.GetNextTypeOrUnknown() == TokenType.Semicolon)
            {
              sb.Append(psr.Consume());
              sb.Append(psr.LineBreakToken());
              return sb;
            }
          }
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
