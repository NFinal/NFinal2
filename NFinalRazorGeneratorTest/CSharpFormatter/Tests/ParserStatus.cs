
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_LoadStatus
  {
    [Test]
    public void T_1()
    {
      var expr = @"a String 1 + 2 - 3;";
      var psr = new Parser(Lexer.LexerString(expr));
      var s1 = psr.SaveStatus();
      try
      {
        psr.Consume();
        ParserUtils.Type(psr);
        ParserUtils.Expr(psr);
      }
      catch (Exception)
      {
      }
      psr.LoadStatus(s1);
      var s2 = psr.SaveStatus();
      Assert.AreEqual(s1.ToString(), s2.ToString());
    }
  }
}
