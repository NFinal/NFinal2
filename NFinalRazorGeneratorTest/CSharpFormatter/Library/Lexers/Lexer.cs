
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace CSharpFormatter.Library.Lexers
{
  public class Lexer
  {
    private static readonly Tuple<TokenType, Regex>[] patterns = new Tuple<TokenType, Regex>[]{
      new Tuple<TokenType, Regex>(TokenType.Comment, new Regex(@"^(//.*)$")),
      new Tuple<TokenType, Regex>(TokenType.WhiteSpaces, new Regex(@"^(\s+)")),
      new Tuple<TokenType, Regex>(TokenType.Number, new Regex(@"^(0\.[0-9]+|[1-9][0-9]*\.[0-9]+)")),
      new Tuple<TokenType, Regex>(TokenType.Number, new Regex(@"^(0x[0-9a-fA-F]+)")),
      new Tuple<TokenType, Regex>(TokenType.Number, new Regex(@"^(0|[1-9][0-9]*)")),
      new Tuple<TokenType, Regex>(TokenType.Boolean, new Regex(@"^(true|false)")),
      new Tuple<TokenType, Regex>(TokenType.String, new Regex(@"^(""(\\""|[^""])*"")")),
      new Tuple<TokenType, Regex>(TokenType.String, new Regex(@"^(@""(""""|[^""])*"")")),
      new Tuple<TokenType, Regex>(TokenType.Char, new Regex(@"^('.')")),
      new Tuple<TokenType, Regex>(TokenType.Char, new Regex(@"^('\\.')")),
      new Tuple<TokenType, Regex>(TokenType.ParenthesesOpen, new Regex(@"^(\()")),
      new Tuple<TokenType, Regex>(TokenType.ParenthesesClose, new Regex(@"^(\))")),
      new Tuple<TokenType, Regex>(TokenType.CurlyBracketOpen, new Regex(@"^({)")),
      new Tuple<TokenType, Regex>(TokenType.CurlyBracketClose, new Regex(@"^(})")),
      new Tuple<TokenType, Regex>(TokenType.SquareBracketOpen, new Regex(@"^(\[)")),
      new Tuple<TokenType, Regex>(TokenType.SquareBracketClose, new Regex(@"^(\])")),
      new Tuple<TokenType, Regex>(TokenType.Operator, new Regex(@"^(~|,|:|\?|!|&|<|>|\+|-|\*|/|\.|=|%|\|)")),
      new Tuple<TokenType, Regex>(TokenType.Semicolon, new Regex(@"^(;)")),
      new Tuple<TokenType, Regex>(TokenType.Directive, new Regex(@"^(#[a-zA-Z]+)")),
      new Tuple<TokenType, Regex>(TokenType.Identifier, new Regex(@"^([a-zA-Z_][a-zA-Z_0-9]*\??)")),
      new Tuple<TokenType, Regex>(TokenType.Unknown, new Regex(@"^(.)")),
    };

    private static List<Token> LexerLine(String line, String path, Int32 lnum)
    {
      var ts = new List<Token>(){};
      var idx = 0;
      while (idx < line.Length)
      {
        foreach (var x in patterns)
        {
          var m = x.Item2.Match(line.Substring(idx));
          if (m.Success)
          {
            var type = x.Item1;
            var matchedText = m.Groups[1].Value;
            if (type == TokenType.Identifier)
            {
              // https://msdn.microsoft.com/ja-jp/library/x53a06bb.aspx
              switch (matchedText)
              {
                case @"abstract":
                case @"as":
                case @"break":
                case @"case":
                case @"catch":
                case @"class":
                case @"const":
                case @"continue":
                case @"default":
                case @"else":
                case @"event":
                case @"extern":
                case @"finally":
                case @"for":
                case @"foreach":
                case @"from":
                case @"get":
                case @"goto":
                case @"group":
                case @"if":
                case @"in":
                case @"interface":
                case @"internal":
                case @"is":
                case @"join":
                case @"namespace":
                case @"new":
                case @"orderby":
                case @"into":
                case @"by":
                case @"let":
                case @"out":
                case @"override":
                case @"partial":
                case @"private":
                case @"protected":
                case @"public":
                case @"readonly":
                case @"ref":
                case @"select":
                case @"set":
                case @"struct":
                case @"throw":
                case @"try":
                case @"using":
                case @"var":
                case @"while":
                case @"yield":
                  type = TokenType.Keyword;
                  break;
              }
            }
            var t = new Token(type, matchedText, path, lnum, idx, line);
            if (0 < t.Text.Length)
            {
              idx += t.Text.Length;
              ts.Add(t);
              break;
            }
          }
        }
      }
      return ts;
    }

    private static List<Token> LexerLines(String[] lines, String path)
    {
      var ts = new List<Token>(){};
      var lnum = 0;
      foreach (var line in lines)
      {
        lnum++;
        var whitespacesOnly = true;
        foreach (var t in LexerLine(line, path, lnum))
        {
          if (t.Type != TokenType.WhiteSpaces)
          {
            whitespacesOnly = false;
            ts.Add(t);
          }
        }
        if (whitespacesOnly)
        {
          ts.Add(new Token(TokenType.Empty, @"", path, lnum, 0, line));
        }
      }
      return ts;
    }

    public static List<Token> LexerString(String str)
    {
      return LexerLines(str.Split('\n'), @"");
    }

    public static List<Token> LexerFile(FormatterInfo fi)
    {
      return LexerLines(IO.ReadFile(fi.Path, fi.Enc), fi.Path);
    }
  }
}
