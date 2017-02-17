
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Exceptions;

namespace CSharpFormatter.Library.Statements
{
  public class UsingSt : AbsStatement<UsingSt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      if (psr.GetNextTextOrEmpty() == @"using" && psr.GetNextTypeOrUnknown(1) == TokenType.Identifier)
      {
        try
        {
          if (fi.DebugMode)
          {
            sb.Append(psr.IndentToken(@"UsingSt"));
          }
          sb.Append(psr.Consume());
          sb.Append(psr.SpaceToken());
          sb.Append(psr.Consume());

          if (psr.GetNextTextOrEmpty() == @"=")
          {
            sb.Append(psr.SpaceToken());
            sb.Append(psr.Consume());
            sb.Append(psr.SpaceToken());

            if (psr.GetNextTypeOrUnknown() == TokenType.Identifier)
            {
              sb.Append(psr.Consume());
            }
            else
            {
              throw new ParseFatalException(@"Fatal UsingSt!");
            }
          }

          while (psr.GetNextTextOrEmpty() == @".")
          {
            sb.Append(psr.Consume());
            if (psr.GetNextTypeOrUnknown() == TokenType.Identifier)
            {
              sb.Append(psr.Consume());
            }
            else
            {
              throw new ResetException();
            }
          }

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
        throw new ParseFatalException(@"Fatal UsingSt!");
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }
  }
}
