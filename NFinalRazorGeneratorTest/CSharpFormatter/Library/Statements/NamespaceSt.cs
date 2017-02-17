
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Exceptions;

namespace CSharpFormatter.Library.Statements
{
  public class NamespaceSt : AbsStatement<NamespaceSt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      if (psr.GetNextTextOrEmpty() == @"namespace")
      {
        try
        {
          sb.Append(psr.IndentToken(@"NamespaceSt.Parse"));
          sb.Append(psr.Consume());
          sb.Append(psr.SpaceToken());
          if (psr.GetNextTypeOrUnknown() == TokenType.Identifier)
          {
            sb.Append(psr.Consume());
            while (psr.GetNextTextOrEmpty() == @".")
            {
              sb.Append(psr.Consume());
              if (psr.GetNextTypeOrUnknown() == TokenType.Identifier)
              {
                sb.Append(psr.Consume());
              }
              else
              {
                throw new ParseFatalException(@"Fatal NamespaceSt!");
              }
            }
            sb.Append(psr.LineBreakToken());

            if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketOpen)
            {
              sb.Append(psr.IndentToken(@"NamespaceSt.Parse"));
              sb.Append(psr.Consume());
              sb.Append(psr.LineBreakToken());
              psr.IndentDown();

              sb = psr.WithComments(status.IndentLevel, sb);

              if (psr.GetNextTypeOrUnknown() != TokenType.CurlyBracketClose)
              {
                sb.Append(psr.ManyStatement(new ParseFunc[]{
                  DirectiveIfSt.Singleton(),
                  DelegateMethodSt.Singleton(),
                  AttributeSt.Singleton(),
                  InterfaceSt.Singleton(),
                  ClassSt.Singleton(),
                  NamespaceSt.Singleton(),
                  EnumSt.Singleton(),
                }));
              }

              if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketClose)
              {
                psr.IndentUp();
                sb.Append(psr.IndentToken(@"NamespaceSt.Parse"));
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
        throw new ParseFatalException(@"Fatal NamespaceSt!");
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }
  }
}
