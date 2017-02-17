
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Exceptions;

namespace CSharpFormatter.Library.Statements
{
  public class EnumSt : AbsStatement<EnumSt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      try
      {
        sb.Append(psr.IndentToken(@"EnumSt.Parse"));
        sb.Append(ParserUtils.AccessModifiers(psr));
        if (psr.GetNextTextOrEmpty() == @"enum")
        {
          sb.Append(psr.Consume());
          sb.Append(psr.SpaceToken());
          if (psr.GetNextTypeOrUnknown() == TokenType.Identifier)
          {
            sb.Append(psr.Consume());
            sb.Append(psr.LineBreakToken());

            if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketOpen)
            {
              sb.Append(psr.IndentToken(@"EnumSt.Parse"));
              sb.Append(psr.Consume());
              sb.Append(psr.LineBreakToken());
              psr.IndentDown();

              sb = psr.WithComments(status.IndentLevel, sb);

              if (psr.GetNextTypeOrUnknown() != TokenType.CurlyBracketClose)
              {
                sb.Append(psr.ManyStatement(new ParseFunc[]{
                  DirectiveIfSt.Singleton(),
                  EnumMemberSt.Singleton(),
                }));
              }

              if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketClose)
              {
                psr.IndentUp();
                sb.Append(psr.IndentToken(@"EnumSt.Parse"));
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
      psr.LoadStatus(status);
      throw new ResetException();
    }
  }


  public class EnumMemberSt : AbsStatement<EnumMemberSt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      try
      {
        if (psr.GetNextTypeOrUnknown() == TokenType.Identifier)
        {
          sb.Append(psr.IndentToken(@"EnumMemberSt.Parse"));
          sb.Append(psr.Consume());
          if (psr.GetNextTextOrEmpty() == @"=")
          {
            sb.Append(psr.SpaceToken());
            sb.Append(psr.Consume());
            sb.Append(psr.SpaceToken());
            if (psr.GetNextTypeOrUnknown() == TokenType.Number)
            {
              sb.Append(psr.Consume());
            }
            else
            {
              throw new ParseFatalException(@"Fatal EnumMemberSt!");
            }
          }
          if (psr.GetNextTextOrEmpty() == @",")
          {
            sb.Append(psr.Consume());
          }
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
