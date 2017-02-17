
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Exceptions;

namespace CSharpFormatter.Library.Statements
{
  public class DirectiveIfSt : AbsStatement<DirectiveIfSt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      try
      {
        switch (psr.GetNextTextOrEmpty())
        {
          case @"#if":
          case @"#elif":
            if (fi.DebugMode)
            {
              sb.Append(psr.IndentToken(@"DirectiveIfSt.Parse"));
            }
            sb.Append(psr.Consume());
            sb.Append(psr.SpaceToken());
            sb.Append(ParserUtils.Expr(psr));
            sb.Append(psr.LineBreakToken());
            return psr.WithComments(status.IndentLevel, sb);

          case @"#else":
          case @"#endif":
            if (fi.DebugMode)
            {
              sb.Append(psr.IndentToken(@"DirectiveIfSt.Parse"));
            }
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
