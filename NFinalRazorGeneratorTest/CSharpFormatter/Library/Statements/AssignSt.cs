
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Exceptions;

namespace CSharpFormatter.Library.Statements
{
  public class AssignSt : AbsStatement<AssignSt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      try
      {
        sb.Append(psr.IndentToken(@"AssignSt.Parse"));
        sb.Append(ParserUtils.Expr(psr));
        sb.Append(psr.SpaceToken());
        if (psr.GetNextTextOrEmpty() == @"=")
        {
          sb.Append(psr.Consume());
        }
        else if (psr.GetNextTextOrEmpty(1) == @"=")
        {
          switch (psr.GetNextTextOrEmpty())
          {
            case @"+":
            case @"-":
            case @"*":
            case @"/":
            case @"%":
            case @"^":
            case @"&":
            case @"|":
              sb.Append(psr.Consume());
              sb.Append(psr.Consume());
              break;
            default:
              throw new ResetException();
          }
        }
        else if (psr.GetNextTextOrEmpty() == @"<" && psr.GetNextTextOrEmpty(1) == @"<" && psr.GetNextTextOrEmpty(2) == @"=")
        {
          sb.Append(psr.Consume());
          sb.Append(psr.Consume());
          sb.Append(psr.Consume());
        }
        else if (psr.GetNextTextOrEmpty() == @">" && psr.GetNextTextOrEmpty(1) == @">" && psr.GetNextTextOrEmpty(2) == @"=")
        {
          sb.Append(psr.Consume());
          sb.Append(psr.Consume());
          sb.Append(psr.Consume());
        }
        else
        {
          throw new ResetException();
        }
        sb.Append(psr.SpaceToken());
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
