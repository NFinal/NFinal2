
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Exceptions;

namespace CSharpFormatter.Library.Statements
{
  public class ClassMemberSt : AbsStatement<ClassMemberSt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      try
      {
        sb.Append(psr.IndentToken(@"ClassMemberSt.Parse"));
        sb.Append(ParserUtils.AccessModifiers(psr));
        var doExit = false;
        while (!doExit)
        {
          switch (psr.GetNextTextOrEmpty())
          {
            case @"readonly":
            case @"static":
            case @"const":
              sb.Append(psr.Consume());
              sb.Append(psr.SpaceToken());
              break;
            default:
              doExit = true;
              break;
          }
        }
        sb.Append(ParserUtils.Type(psr));

        if (psr.GetNextTypeOrUnknown() == TokenType.Identifier)
        {
          sb.Append(psr.SpaceToken());
          sb.Append(psr.Consume());
        }

        if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketOpen)
        {
          sb.Append(psr.LineBreakToken());
          sb.Append(psr.IndentToken(@"ClassMemberSt.Parse"));
          sb.Append(psr.Consume());
          sb.Append(psr.LineBreakToken());
          psr.IndentDown();

          while (psr.GetNextTypeOrUnknown() != TokenType.CurlyBracketClose)
          {
            sb.Append(psr.IndentToken(@"ClassMemberSt.Parse"));
            sb.Append(ParserUtils.AccessModifiers(psr));
            if (psr.GetNextTextOrEmpty() == @"get" || psr.GetNextTextOrEmpty() == @"set")
            {
              sb.Append(psr.Consume());
              if (psr.GetNextTypeOrUnknown() == TokenType.Semicolon)
              {
                sb.Append(psr.Consume());
                sb.Append(psr.LineBreakToken());
              }
              else if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketOpen)
              {
                sb.Append(psr.LineBreakToken());
                sb.Append(psr.IndentToken(@"ClassMemberSt.Parse"));
                sb.Append(psr.Consume());
                sb.Append(psr.LineBreakToken());
                psr.IndentDown();

                sb = psr.WithComments(status.IndentLevel, sb);

                if (psr.GetNextTypeOrUnknown() != TokenType.CurlyBracketClose)
                {
                  sb.Append(psr.DefaultManyStatement());

                  if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketClose)
                  {
                    psr.IndentUp();
                    sb.Append(psr.IndentToken(@"ClassMemberSt.Parse"));
                    sb.Append(psr.Consume());
                    sb.Append(psr.LineBreakToken());
                  }
                  else
                  {
                    throw new ParseFatalException(@"Fatal ClassMemberSt!");
                  }
                }
              }
            }
          }

          if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketClose)
          {
            psr.IndentUp();
            sb.Append(psr.IndentToken(@"ClassMemberSt.Parse"));
            sb.Append(psr.Consume());
            sb.Append(psr.LineBreakToken());
            return psr.WithComments(status.IndentLevel, sb);
          }
          else
          {
            throw new ParseFatalException(@"Fatal ClassMemberSt!");
          }
        }
        else
        {
          if (psr.GetNextTextOrEmpty() == @"=")
          {
            sb.Append(psr.SpaceToken());
            sb.Append(psr.Consume());
            sb.Append(psr.SpaceToken());
            sb.Append(ParserUtils.Expr(psr));
          }
          if (psr.GetNextTypeOrUnknown() == TokenType.Semicolon)
          {
            sb.Append(psr.Consume());
            sb.Append(psr.LineBreakToken());
            return psr.WithComments(status.IndentLevel, sb);
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
