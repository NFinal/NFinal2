
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Exceptions;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Library.Parsers
{
  public class Parser
  {
    private readonly FormatterInfo fi;
    private List<Token> tokens;
    private Int32 tokenIndex;
    private Int32 indentLevel;
    private List<String> comments;
    private List<TokenType> ignoreTypes;

    public Parser(List<Token> tokens)
    {
      this.fi = new FormatterInfo();
      this.tokens = tokens;
      this.tokenIndex = 0;
      this.indentLevel = 0;
      this.comments = new List<String>();
      this.ignoreTypes = new List<TokenType>(){
        TokenType.Comment,
        TokenType.Empty,
      };
    }

    public Parser(List<Token> tokens, FormatterInfo fi) : this(tokens)
    {
      this.fi = fi;
      this.tokens = tokens;
    }

    public StringBuilder WithComments(Int32 indentLevel, StringBuilder data)
    {
      StringBuilder sb = new StringBuilder();
      foreach (var cmt in this.comments)
      {
        if (String.IsNullOrEmpty(cmt))
        {
          sb.Append(this.IndentToken(0, @"Parser.WithComments"));
        }
        else
        {
          sb.Append(this.IndentToken(indentLevel, @"Parser.WithComments"));
          sb.Append(cmt);
        }
        sb.Append(this.LineBreakToken());
      }
      this.comments.Clear();
      sb.Append(data);
      return sb;
    }

    public Token IndentToken(String debugText = @"")
    {
      return IndentToken(this.indentLevel, debugText);
    }

    private Token IndentToken(Int32 indentLevel, String debugText = @"")
    {
      StringBuilder sb = new StringBuilder();
      if (fi.DebugMode)
      {
        sb.Append(String.Format(@"[{0,-50}]", debugText));
      }
      if (this.fi.UseTab)
      {
        sb.Append(new String('\t', indentLevel));
      }
      else
      {
        sb.Append(new String(' ', indentLevel * this.fi.Indent));
      }
      return new Token(sb.ToString());
    }

    public void IndentUp()
    {
      if (0 < this.indentLevel)
      {
        this.indentLevel--;
      }
    }

    public void IndentDown()
    {
      this.indentLevel++;
    }

    public ParserStatus SaveStatus()
    {
      return new ParserStatus(this.tokenIndex, this.indentLevel, new List<String>(this.comments));
    }

    public void LoadStatus(ParserStatus status)
    {
      this.tokenIndex = status.TokenIndex;
      this.indentLevel = status.IndentLevel;
      this.comments = status.Comments;
    }

    public Token SpaceToken()
    {
      return new Token(@" ");
    }

    public Token LineBreakToken()
    {
      return new Token("\n");
    }

    public Boolean End()
    {
      try
      {
        GetToken();
      }
      catch (EOFException)
      {
        return true;
      }
      return false;
    }

    public Token Consume()
    {
      while (true)
      {
        if (this.tokens.Count <= this.tokenIndex)
        {
          throw new EOFException();
        }
        else if (this.ignoreTypes.Contains(this.tokens[this.tokenIndex].Type))
        {
          this.comments.Add(this.tokens[this.tokenIndex].ToString());
          this.tokenIndex++;
        }
        else
        {
          break;
        }
      }
      var t = this.tokens[this.tokenIndex];
      this.tokenIndex++;
      return t;
    }

    public String GetNextTextOrEmpty(Int32 n = 0)
    {
      try
      {
        return GetToken(n).Text;
      }
      catch (ResetException)
      {
        return @"";
      }
    }

    public TokenType GetNextTypeOrUnknown(Int32 n = 0)
    {
      try
      {
        return GetToken(n).Type;
      }
      catch (ResetException)
      {
        return TokenType.Unknown;
      }
    }

    private Token GetToken(Int32 n = 0)
    {
      var cnt = (0 <= n) ? n : (-n);
      var idx = 0;
      while (true)
      {
        if (this.tokens.Count <= this.tokenIndex + idx)
        {
          throw new EOFException();
        }
        else if (this.ignoreTypes.Contains(this.tokens[this.tokenIndex + idx].Type))
        {
          if (0 <= n)
          {
            idx++;
          }
          else
          {
            idx--;
          }
        }
        else
        {
          if (cnt == 0)
          {
            break;
          }
          else
          {
            cnt--;
            if (0 <= n)
            {
              idx++;
            }
            else
            {
              idx--;
            }
          }
        }
      }
      return this.tokens[this.tokenIndex + idx];
    }

    public ParserResult Evalute()
    {
      return this.Evalute(new ParseFunc[]{
        AttributeSt.Singleton(),
        InterfaceSt.Singleton(),
        ClassSt.Singleton(),
        DirectiveIfSt.Singleton(),
        EnumSt.Singleton(),
        NamespaceSt.Singleton(),
        UsingSt.Singleton(),
      });
    }

    public ParserResult Evalute(ParseFunc[] fs)
    {
      var output = @"";
      var ok = true;
      try
      {
        output = this.ManyStatement(fs).ToString();
        if (!this.End())
        {
          throw new ParseFatalException(@"Can not parse until EOF!");
        }
      }
      catch (ParseFatalException ex)
      {
        ok = false;
        try
        {
          var t = this.GetToken();
          output = String.Join("\n", new String[]{
            String.Format(@"{0}({1},{2}): {3}", t.Path, t.Lnum, t.Col, ex.Message),
            String.Format(@"  {0}", t.Line),
            String.Format(@"  {0}", (new String(' ', t.Col)) + @"// "),
          });
        }
        catch (ResetException)
        {
          output = ex.Message;
        }
      }
      return new ParserResult(output, ok);
    }

    private readonly ParseFunc[] DefaultParseFuncions = new ParseFunc[]{
      DefineLocalVariableSt.Singleton(),
      AssignSt.Singleton(),
      BreakSt.Singleton(),
      CheckedSt.Singleton(),
      ContinueSt.Singleton(),
      DirectiveIfSt.Singleton(),
      ExprSt.Singleton(),
      ForSt.Singleton(),
      ForeachSt.Singleton(),
      IfSt.Singleton(),
      ReturnSt.Singleton(),
      SwitchSt.Singleton(),
      ThrowSt.Singleton(),
      TrySt.Singleton(),
      UnCheckedSt.Singleton(),
      UnsafeSt.Singleton(),
      UsingBlockSt.Singleton(),
      WhileSt.Singleton(),
    };

    public StringBuilder DefaultOneStatement()
    {
      return this.OneStatement(DefaultParseFuncions);
    }

    public StringBuilder DefaultManyStatement()
    {
      return this.ManyStatement(DefaultParseFuncions);
    }

    public StringBuilder ManyStatement(ParseFunc[] fs)
    {
      var sb = new StringBuilder();
      try
      {
        while (true)
        {
          sb.Append(OneStatement(fs));
        }
      }
      catch (ResetException)
      {
      }
      return sb;
    }

    public StringBuilder OneStatement(ParseFunc[] fs)
    {
      foreach (var f in fs)
      {
        try
        {
          return f(this, fi);
        }
        catch (ResetException)
        {
        }
      }
      throw new ResetException();
    }
  }
}
