
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Exceptions;

namespace CSharpFormatter.Library.Statements
{
  public class ParserUtils
  {
    public static StringBuilder RefOutIn(Parser psr)
    {
      var status = psr.SaveStatus();
      try
      {
        var sb = new StringBuilder();
        switch (psr.GetNextTextOrEmpty())
        {
          case @"ref":
          case @"out":
          case @"in":
            sb.Append(psr.Consume());
            sb.Append(psr.SpaceToken());
            break;
        }
        return sb;
      }
      catch (ResetException)
      {
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }

    public static StringBuilder AccessModifiers(Parser psr)
    {
      var status = psr.SaveStatus();
      try
      {
        var sb = new StringBuilder();
        switch (psr.GetNextTextOrEmpty())
        {
          case @"protected":
            sb.Append(psr.Consume());
            sb.Append(psr.SpaceToken());
            if (psr.GetNextTextOrEmpty() == @"internal")
            {
              sb.Append(psr.Consume());
              sb.Append(psr.SpaceToken());
            }
            break;
          case @"public":
          case @"private":
          case @"internal":
            sb.Append(psr.Consume());
            sb.Append(psr.SpaceToken());
            break;
        }
        return sb;
      }
      catch (ResetException)
      {
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }

    public static StringBuilder ConditionalOprator(Parser psr)
    {
      var status = psr.SaveStatus();
      var sb = new StringBuilder();
      try
      {
        if (psr.GetNextTextOrEmpty() == @"?")
        {
          sb.Append(psr.SpaceToken());
          sb.Append(psr.Consume());
          sb.Append(psr.SpaceToken());
          sb.Append(Expr(psr));
          if (psr.GetNextTextOrEmpty() == @":")
          {
            sb.Append(psr.SpaceToken());
            sb.Append(psr.Consume());
            sb.Append(psr.SpaceToken());
            sb.Append(Expr(psr));
            return sb;
          }
        }
      }
      catch (ResetException)
      {
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }

    public static StringBuilder AsIs(Parser psr)
    {
      var status = psr.SaveStatus();
      var sb = new StringBuilder();
      try
      {
        switch (psr.GetNextTextOrEmpty())
        {
          case @"as":
          case @"is":
            sb.Append(psr.SpaceToken());
            sb.Append(psr.Consume());
            sb.Append(psr.SpaceToken());
            sb.Append(Type(psr));
            return sb;
        }
      }
      catch (ResetException)
      {
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }

    public static StringBuilder _Expr(Parser psr)
    {
      var status = psr.SaveStatus();
      try
      {
        var sb = new StringBuilder();
        sb.Append(psr.SpaceToken());

        switch (psr.GetNextTextOrEmpty())
        {
          case @"!":
            switch (psr.GetNextTextOrEmpty(1))
            {
              case @"=":
                sb.Append(psr.Consume());
                sb.Append(psr.Consume());
                break;
            }
            break;

          case @"=":
            switch (psr.GetNextTextOrEmpty(1))
            {
              case @"=":
              case @">":
                sb.Append(psr.Consume());
                sb.Append(psr.Consume());
                break;
            }
            break;

          case @"-":
            sb.Append(psr.Consume());
            if (psr.GetNextTextOrEmpty() == @">")
            {
              sb.Append(psr.Consume());
            }
            break;

          case @"+":
          case @"*":
          case @"/":
          case @"%":
            sb.Append(psr.Consume());
            break;

          case @"|":
            sb.Append(psr.Consume());
            if (psr.GetNextTextOrEmpty() == @"|")
            {
              sb.Append(psr.Consume());
            }
            break;

          case @"&":
            sb.Append(psr.Consume());
            if (psr.GetNextTextOrEmpty() == @"&")
            {
              sb.Append(psr.Consume());
            }
            break;

          case @"<":
            sb.Append(psr.Consume());
            switch (psr.GetNextTextOrEmpty())
            {
              case @"<":
              case @"=":
                sb.Append(psr.Consume());
                break;
            }
            break;

          case @">":
            sb.Append(psr.Consume());
            switch (psr.GetNextTextOrEmpty())
            {
              case @">":
              case @"=":
                sb.Append(psr.Consume());
                break;
            }
            break;

          case @"?":
            switch (psr.GetNextTextOrEmpty(1))
            {
              case @"?":
              case @".":
                sb.Append(psr.Consume());
                sb.Append(psr.Consume());
                break;
            }
            break;

          default:
            throw new ResetException();
        }

        sb.Append(psr.SpaceToken());
        sb.Append(Term(psr));

        return sb;
      }
      catch (ResetException)
      {
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }

    public static StringBuilder Expr(Parser psr)
    {
      var status = psr.SaveStatus();
      try
      {
        var sb = new StringBuilder();
        sb.Append(Term(psr));

        while (true)
        {
          try
          {
            sb.Append(_Expr(psr));
          }
          catch (ResetException)
          {
            break;
          }
        }

        try
        {
          sb.Append(ConditionalOprator(psr));
        }
        catch (ResetException)
        {
        }

        try
        {
          sb.Append(AsIs(psr));
        }
        catch (ResetException)
        {
        }

        return sb;
      }
      catch (ResetException)
      {
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }

    public static StringBuilder Type(Parser psr)
    {
      var status = psr.SaveStatus();
      try
      {
        var sb = new StringBuilder();
        if (psr.GetNextTypeOrUnknown() == TokenType.Identifier)
        {
          sb.Append(psr.Consume());
          while (true)
          {
            if (psr.GetNextTextOrEmpty() == @".")
            {
              sb.Append(psr.Consume());
            }
            else
            {
              break;
            }
            if (psr.GetNextTypeOrUnknown() == TokenType.Identifier)
            {
              sb.Append(psr.Consume());
            }
            else
            {
              throw new ResetException();
            }
          }
        }
        else
        {
          throw new ResetException();
        }
        if (psr.GetNextTextOrEmpty() == @"<")
        {
          sb.Append(psr.Consume());
          if (psr.GetNextTextOrEmpty() != @">")
          {
            sb.Append(Type(psr));
            while (psr.GetNextTextOrEmpty() == @",")
            {
              var status2 = psr.SaveStatus();
              try
              {
                var sb2 = new StringBuilder();
                sb2.Append(psr.Consume());
                sb2.Append(psr.SpaceToken());
                sb2.Append(Type(psr));
                sb.Append(sb2);
              }
              catch (ResetException)
              {
                psr.LoadStatus(status2);
                break;
              }
            }
          }
          if (psr.GetNextTextOrEmpty() == @">")
          {
            sb.Append(psr.Consume());
          }
          else
          {
            throw new ResetException();
          }
        }
        if (psr.GetNextTextOrEmpty() == @"[" && psr.GetNextTextOrEmpty(1) == @"]")
        {
          sb.Append(psr.Consume());
          sb.Append(psr.Consume());
        }
        if (psr.GetNextTextOrEmpty() == @"?")
        {
          sb.Append(psr.Consume());
        }
        return sb;
      }
      catch (ResetException)
      {
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }

    private static StringBuilder New(Parser psr)
    {
      var status = psr.SaveStatus();
      try
      {
        var sb = new StringBuilder();
        if (psr.GetNextTextOrEmpty() == @"new")
        {
          sb.Append(psr.Consume());

          if (psr.GetNextTextOrEmpty() == @"[")
          {
            sb.Append(SquareBrackets(psr));
            sb.Append(CurlyBrackets_Exprs(psr));
          }
          else
          {
            try
            {
              var t = Type(psr);
              sb.Append(psr.SpaceToken());
              sb.Append(t);
            }
            catch (ResetException)
            {
            }
            if (psr.GetNextTypeOrUnknown() == TokenType.ParenthesesOpen)
            {
              sb.Append(FuncArgs(psr));
            }
            if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketOpen)
            {
              sb.Append(CurlyBrackets_Exprs(psr));
            }
          }
          return sb;
        }
      }
      catch (ResetException)
      {
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }

    private static StringBuilder ExprWithParenthese(Parser psr)
    {
      var status = psr.SaveStatus();
      try
      {
        var sb = new StringBuilder();
        if (psr.GetNextTypeOrUnknown() == TokenType.ParenthesesOpen)
        {
          sb.Append(psr.Consume());
          sb.Append(Expr(psr));
          if (psr.GetNextTypeOrUnknown() == TokenType.ParenthesesClose)
          {
            sb.Append(psr.Consume());
            return sb;
          }
        }
      }
      catch (ResetException)
      {
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }

    private static StringBuilder SquareBrackets(Parser psr)
    {
      var status = psr.SaveStatus();
      try
      {
        var sb = new StringBuilder();
        while (psr.GetNextTextOrEmpty() == @"[")
        {
          sb.Append(psr.Consume());
          if (psr.GetNextTextOrEmpty() != @"]")
          {
            sb.Append(ParserUtils.Expr(psr));
          }
          if (psr.GetNextTextOrEmpty() == @"]")
          {
            sb.Append(psr.Consume());
            return sb;
          }
        }
      }
      catch (ResetException)
      {
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }

    private static StringBuilder CurlyBrackets_Exprs(Parser psr)
    {
      var status = psr.SaveStatus();
      try
      {
        var sb = new StringBuilder();
        while (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketOpen)
        {
          sb.Append(psr.Consume());
          if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketClose)
          {
            sb.Append(psr.Consume());
            return sb;
          }
          else
          {
            sb.Append(psr.LineBreakToken());
            psr.IndentDown();
            while (psr.GetNextTypeOrUnknown() != TokenType.CurlyBracketClose)
            {
              sb.Append(psr.IndentToken(@"ParserUtils.CurlyBrackets_Exprs"));
              sb.Append(Expr(psr));
              if (psr.GetNextTextOrEmpty() == @"=")
              {
                sb.Append(psr.SpaceToken());
                sb.Append(psr.Consume());
                sb.Append(psr.SpaceToken());
                sb.Append(Expr(psr));
              }
              if (psr.GetNextTextOrEmpty() == @",")
              {
                sb.Append(psr.Consume());
              }
              sb.Append(psr.LineBreakToken());
            }
            if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketClose)
            {
              psr.IndentUp();
              sb.Append(psr.IndentToken(@"ParserUtils.CurlyBrackets_Exprs"));
              sb.Append(psr.Consume());
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

    private static StringBuilder CurlyBracket_Statements(Parser psr)
    {
      var status = psr.SaveStatus();
      try
      {
        var sb = new StringBuilder();
        if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketOpen)
        {
          sb.Append(psr.Consume());
          sb.Append(psr.LineBreakToken());
          psr.IndentDown();
          sb.Append(psr.DefaultManyStatement());
          if (psr.GetNextTypeOrUnknown() == TokenType.CurlyBracketClose)
          {
            psr.IndentUp();
            sb.Append(psr.IndentToken(@"ParserUtils.CurlyBracket_Statements"));
            sb.Append(psr.Consume());
            return sb;
          }
        }
      }
      catch (ResetException)
      {
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }

    private static StringBuilder Parentheses_Tuple(Parser psr)
    {
      var status = psr.SaveStatus();
      try
      {
        var sb = new StringBuilder();
        if (psr.GetNextTypeOrUnknown() == TokenType.ParenthesesOpen)
        {
          sb.Append(psr.Consume());
          if (psr.GetNextTextOrEmpty(1) != @"," && psr.GetNextTypeOrUnknown(1) != TokenType.ParenthesesClose)
          {
            sb.Append(Type(psr));
            sb.Append(psr.SpaceToken());
          }
          if (psr.GetNextTypeOrUnknown() == TokenType.Identifier)
          {
            sb.Append(psr.Consume());
            while (psr.GetNextTextOrEmpty() == @",")
            {
              sb.Append(psr.Consume());
              sb.Append(psr.SpaceToken());
              if (psr.GetNextTextOrEmpty(1) != @"," && psr.GetNextTypeOrUnknown(1) != TokenType.ParenthesesClose)
              {
                sb.Append(Type(psr));
                sb.Append(psr.SpaceToken());
              }
              if (psr.GetNextTypeOrUnknown() == TokenType.Identifier)
              {
                sb.Append(psr.Consume());
              }
            }
            if (psr.GetNextTypeOrUnknown() == TokenType.ParenthesesClose)
            {
              sb.Append(psr.Consume());
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

    private static StringBuilder Query(Parser psr)
    {
      var status = psr.SaveStatus();
      try
      {
        var sb = new StringBuilder();
        var doExit = false;
        var n = 0;
        while (!doExit)
        {
          switch (psr.GetNextTextOrEmpty())
          {
            case @"let":
              if (0 < n)
              {
                sb.Append(psr.SpaceToken());
              }
              sb.Append(psr.Consume());
              sb.Append(psr.SpaceToken());
              if (psr.GetNextTypeOrUnknown() == TokenType.Identifier && psr.GetNextTextOrEmpty(1) == @"=")
              {
                sb.Append(psr.Consume());
                sb.Append(psr.SpaceToken());
                sb.Append(psr.Consume());
                sb.Append(psr.SpaceToken());
                sb.Append(Expr(psr));
              }
              break;
            case @"where":
              if (0 < n)
              {
                sb.Append(psr.SpaceToken());
              }
              sb.Append(psr.Consume());
              sb.Append(psr.SpaceToken());
              sb.Append(Expr(psr));
              break;
            case @"orderby":
              if (0 < n)
              {
                sb.Append(psr.SpaceToken());
              }
              sb.Append(psr.Consume());
              sb.Append(psr.SpaceToken());
              while (true)
              {
                sb.Append(Expr(psr));
                switch (psr.GetNextTextOrEmpty())
                {
                  case @"desceding":
                  case @"ascending":
                    sb.Append(psr.SpaceToken());
                    sb.Append(psr.Consume());
                    break;
                }
                if (psr.GetNextTextOrEmpty() == @",")
                {
                  sb.Append(psr.Consume());
                  sb.Append(psr.SpaceToken());
                }
                else
                {
                  break;
                }
              }
              break;
            case @"select":
              if (0 < n)
              {
                sb.Append(psr.SpaceToken());
              }
              sb.Append(psr.Consume());
              sb.Append(psr.SpaceToken());
              sb.Append(Expr(psr));
              if (psr.GetNextTextOrEmpty() == @"into")
              {
                sb.Append(psr.SpaceToken());
                sb.Append(psr.Consume());
                sb.Append(psr.SpaceToken());
                sb.Append(Expr(psr));
              }
              break;
            case @"join":
              if (0 < n)
              {
                sb.Append(psr.SpaceToken());
              }
              sb.Append(psr.Consume());
              sb.Append(psr.SpaceToken());
              sb.Append(Expr(psr));
              if (psr.GetNextTextOrEmpty() == @"in")
              {
                sb.Append(psr.SpaceToken());
                sb.Append(psr.Consume());
                sb.Append(psr.SpaceToken());
                sb.Append(Expr(psr));
                if (psr.GetNextTextOrEmpty() == @"on")
                {
                  sb.Append(psr.SpaceToken());
                  sb.Append(psr.Consume());
                  sb.Append(psr.SpaceToken());
                  sb.Append(Expr(psr));
                  if (psr.GetNextTextOrEmpty() == @"equals")
                  {
                    sb.Append(psr.SpaceToken());
                    sb.Append(psr.Consume());
                    sb.Append(psr.SpaceToken());
                    sb.Append(Expr(psr));
                    if (psr.GetNextTextOrEmpty() == @"into")
                    {
                      sb.Append(psr.SpaceToken());
                      sb.Append(psr.Consume());
                      sb.Append(psr.SpaceToken());
                      sb.Append(Expr(psr));
                    }
                  }
                }
              }
              break;
            case @"group":
              if (0 < n)
              {
                sb.Append(psr.SpaceToken());
              }
              sb.Append(psr.Consume());
              sb.Append(psr.SpaceToken());
              sb.Append(Expr(psr));
              if (psr.GetNextTextOrEmpty() == @"by")
              {
                sb.Append(psr.SpaceToken());
                sb.Append(psr.Consume());
                sb.Append(psr.SpaceToken());
                sb.Append(Expr(psr));
                if (psr.GetNextTextOrEmpty() == @"into")
                {
                  sb.Append(psr.SpaceToken());
                  sb.Append(psr.Consume());
                  sb.Append(psr.SpaceToken());
                  sb.Append(Expr(psr));
                }
              }
              break;
            case @"from":
              if (0 < n)
              {
                sb.Append(psr.SpaceToken());
              }
              sb.Append(psr.Consume());
              sb.Append(psr.SpaceToken());
              sb.Append(Expr(psr));
              if (psr.GetNextTextOrEmpty() == @"in")
              {
                sb.Append(psr.SpaceToken());
                sb.Append(psr.Consume());
                sb.Append(psr.SpaceToken());
                sb.Append(Expr(psr));
              }
              break;
            default:
              doExit = true;
              break;
          }
          n++;
        }
        return sb;
      }
      catch (ResetException)
      {
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }

    private static StringBuilder Atom(Parser psr)
    {
      var status = psr.SaveStatus();
      try
      {
        var sb = new StringBuilder();
        switch (psr.GetNextTypeOrUnknown())
        {
          case TokenType.Keyword:
            switch (psr.GetNextTextOrEmpty())
            {
              case @"new":
                sb.Append(New(psr));
                break;
              case @"select":
              case @"from":
              case @"where":
              case @"join":
              case @"orderby":
              case @"group":
                sb.Append(Query(psr));
                break;
              default:
                throw new ResetException();
            }
            break;
          case TokenType.Number:
          case TokenType.Boolean:
          case TokenType.String:
          case TokenType.Char:
            sb.Append(psr.Consume());
            break;
          case TokenType.Identifier:
            sb.Append(psr.Consume());
            break;
          case TokenType.ParenthesesOpen:
            try
            {
              sb.Append(Parentheses_Tuple(psr));
            }
            catch (ResetException)
            {
              sb.Append(ExprWithParenthese(psr));
            }
            break;
          case TokenType.CurlyBracketOpen:
            sb.Append(CurlyBracket_Statements(psr));
            break;
          default:
            throw new ResetException();
        }

        while (true)
        {
          try
          {
            if (psr.GetNextTypeOrUnknown() == TokenType.ParenthesesOpen)
            {
              sb.Append(FuncArgs(psr));
            }
            else if (psr.GetNextTextOrEmpty() == @"[")
            {
              sb.Append(SquareBrackets(psr));
            }
            else
            {
              break;
            }
          }
          catch (ResetException)
          {
            break;
          }
        }
        return sb;
      }
      catch (ResetException)
      {
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }

    private static StringBuilder Cast(Parser psr)
    {
      var status = psr.SaveStatus();
      try
      {
        var sb = new StringBuilder();
        if (psr.GetNextTypeOrUnknown() == TokenType.ParenthesesOpen)
        {
          sb.Append(psr.Consume());
          sb.Append(Type(psr));
          if (psr.GetNextTypeOrUnknown() == TokenType.ParenthesesClose)
          {
            sb.Append(psr.Consume());
            sb.Append(psr.SpaceToken());
            return sb;
          }
        }
      }
      catch (ResetException)
      {
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }

    private static StringBuilder Term(Parser psr)
    {
      var status = psr.SaveStatus();
      try
      {
        var sb = new StringBuilder();
        try
        {
          sb.Append(Cast(psr));
        }
        catch (ResetException)
        {
        }
        switch (psr.GetNextTextOrEmpty())
        {
          case @"+":
          case @"-":
          case @"*":
          case @"&":
          case @"!":
          case @"~":
            sb.Append(psr.Consume());
            break;
        }
        sb.Append(Atom(psr));
        while (psr.GetNextTextOrEmpty() == @".")
        {
          var status2 = psr.SaveStatus();
          try
          {
            var sb2 = new StringBuilder();
            sb2.Append(psr.Consume());
            sb2.Append(Atom(psr));
            sb.Append(sb2);
          }
          catch (ResetException)
          {
            psr.LoadStatus(status2);
            break;
          }
        }
        try
        {
          sb.Append(PostOperator(psr));
        }
        catch (ResetException)
        {
        }
        return sb;
      }
      catch (ResetException)
      {
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }

    private static StringBuilder PostOperator(Parser psr)
    {
      var status = psr.SaveStatus();
      try
      {
        var sb = new StringBuilder();
        if (psr.GetNextTextOrEmpty() == @"+" && psr.GetNextTextOrEmpty(1) == @"+")
        {
          sb.Append(psr.Consume());
          sb.Append(psr.Consume());
          return sb;
        }
        else if (psr.GetNextTextOrEmpty() == @"-" && psr.GetNextTextOrEmpty(1) == @"-")
        {
          sb.Append(psr.Consume());
          sb.Append(psr.Consume());
          return sb;
        }
      }
      catch (ResetException)
      {
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }

    private static StringBuilder FuncArgs(Parser psr)
    {
      var status = psr.SaveStatus();
      try
      {
        var sb = new StringBuilder();
        while (psr.GetNextTypeOrUnknown() == TokenType.ParenthesesOpen)
        {
          sb.Append(psr.Consume());
          while (psr.GetNextTypeOrUnknown() != TokenType.ParenthesesClose)
          {
            sb.Append(RefOutIn(psr));
            sb.Append(Expr(psr));
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
          if (psr.GetNextTypeOrUnknown() == TokenType.ParenthesesClose)
          {
            sb.Append(psr.Consume());
            return sb;
          }
        }
      }
      catch (ResetException)
      {
      }
      psr.LoadStatus(status);
      throw new ResetException();
    }

    public static StringBuilder Attribute(Parser psr)
    {
      var sb = new StringBuilder();
      var status = psr.SaveStatus();
      try
      {
        if (psr.GetNextTextOrEmpty() == @"[")
        {
          sb.Append(psr.Consume());
          if (psr.GetNextTextOrEmpty(1) == @":")
          {
            switch (psr.GetNextTextOrEmpty())
            {
              case @"assembly":
              case @"module":
              case @"type":
              case @"field":
              case @"method":
              case @"event":
              case @"property":
              case @"param":
              case @"return":
                sb.Append(psr.Consume());
                sb.Append(psr.Consume());
                sb.Append(psr.SpaceToken());
                break;
            }
          }
          while (psr.GetNextTextOrEmpty() != @"]")
          {
            sb.Append(Expr(psr));
            if (psr.GetNextTextOrEmpty() == @",")
            {
              sb.Append(psr.Consume());
              sb.Append(psr.SpaceToken());
            }
            else if (psr.GetNextTextOrEmpty() != @"]")
            {
              throw new ResetException();
            }
          }
          if (psr.GetNextTextOrEmpty() == @"]")
          {
            sb.Append(psr.Consume());
            return sb;
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
