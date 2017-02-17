
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Exceptions;

namespace CSharpFormatter.Library.Statements
{
  public class DefineLocalVariableSt : AbsStatement<DefineLocalVariableSt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      try
      {
        sb.Append(psr.IndentToken(@"DefineLocalVariableSt.Parse"));
        if (psr.GetNextTextOrEmpty() == @"var")
        {
          sb.Append(psr.Consume());
        }
        else
        {
          if (psr.GetNextTextOrEmpty() == @"const")
          {
            sb.Append(psr.Consume());
            sb.Append(psr.SpaceToken());
          }
          sb.Append(ParserUtils.Type(psr));
        }
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
