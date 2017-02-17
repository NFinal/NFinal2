
using System;
using System.Text;
using NUnit.Framework;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  public class TestUtils
  {
    public static void Eq(ParseFunc[] pf, String input, String[] expect)
    {
      var psr = new Parser(Lexer.LexerString(input));
      var result = psr.Evalute(pf);
      var joinedExpect = @"";
      foreach (var line in expect)
      {
        joinedExpect += line + "\n";
      }
      Assert.AreEqual(result.Output, joinedExpect);
      Assert.True(result.Success);
    }

    public static void EqExpr(String input, String expect)
    {
      var psr = new Parser(Lexer.LexerString(input));
      var result = ParserUtils.Expr(psr).ToString();
      Assert.AreEqual(result, expect);
    }
  }
}
