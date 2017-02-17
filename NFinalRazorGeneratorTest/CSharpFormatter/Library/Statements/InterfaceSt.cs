
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Exceptions;

namespace CSharpFormatter.Library.Statements
{
  public class InterfaceSt : AbsStatement<InterfaceSt>
  {
    protected override StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();

      sb.Append(psr.IndentToken(@"InterfaceSt.Parse"));
      sb.Append(ParserUtils.AccessModifiers(psr));

      if (psr.GetNextTextOrEmpty() == @"interface")
      {
        try
        {
          sb.Append(psr.Consume());
          sb.Append(psr.SpaceToken());
          if (psr.GetNextTypeOrUnknown() == TokenType.Identifier)
          {
            sb.Append(psr.Consume());
            if (psr.GetNextTextOrEmpty() == @"<")
            {
              sb.Append(psr.Consume());
              if (psr.GetNextTypeOrUnknown() == TokenType.Identifier)
              {
                sb.Append(psr.Consume());
                if (psr.GetNextTextOrEmpty() == @">")
                {
                  sb.Append(psr.Consume());
                }
                else
                {
                  throw new ParseFatalException(@"Fatal InterfaceSt!");
                }
              }
              else
              {
                throw new ParseFatalException(@"Fatal InterfaceSt!");
              }
            }
          }
          else
          {
            throw new ParseFatalException(@"Fatal InterfaceSt!");
          }

          if (psr.GetNextTextOrEmpty() == @"where")
          {
            sb.Append(psr.SpaceToken());
            sb.Append(psr.Consume());
            if (psr.GetNextTypeOrUnknown() == TokenType.Identifier)
            {
              sb.Append(psr.SpaceToken());
              sb.Append(psr.Consume());
            }
            else
            {
              throw new ParseFatalException(@"Fatal InterfaceSt!");
            }
          }

          sb.Append(psr.LineBreakToken());

          if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketOpen)
          {
            sb.Append(psr.IndentToken(@"InterfaceSt.Parse"));
            sb.Append(psr.Consume());
            sb.Append(psr.LineBreakToken());
            psr.IndentDown();

            sb = psr.WithComments(status.IndentLevel, sb);

            if (psr.GetNextTypeOrUnknown() != TokenType.CurlyBracketClose)
            {
              sb.Append(psr.ManyStatement(new ParseFunc[]{
                DelegateMethodSt.Singleton(),
                DirectiveIfSt.Singleton(),
                AttributeSt.Singleton(),
                InterfaceSt.Singleton(),
                ClassSt.Singleton(),
                EnumSt.Singleton(),
                ClassMemberSt.Singleton(),
                ClassMethodSt.Singleton(),
              }));
            }

            if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketClose)
            {
              psr.IndentUp();
              sb.Append(psr.IndentToken(@"InterfaceSt.Parse"));
              sb.Append(psr.Consume());
              sb.Append(psr.LineBreakToken());
              return sb;
            }
          }
        }
        catch (ResetException)
        {
        }
        throw new ParseFatalException(@"Fatal InterfaceSt!");
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }
  }
}
