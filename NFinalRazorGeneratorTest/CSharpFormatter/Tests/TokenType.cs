
using NUnit.Framework;
using CSharpFormatter.Library.Lexers;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_TokenType
  {

    [Test]
    public void T_Comment()
    {
      var ts = Lexer.LexerString(@"// using A _ _a_1 abc123");
      Assert.AreEqual(ts[0].Type, TokenType.Comment);
    }

    [Test]
    public void T_Number()
    {
      var ts = Lexer.LexerString(@"123");
      Assert.AreEqual(ts[0].Type, TokenType.Number);
    }

    [Test]
    public void T_Boolean()
    {
      foreach (var t in Lexer.LexerString(@"true false"))
      {
        Assert.AreEqual(t.Type, TokenType.Boolean);
      }
    }

    [Test]
    public void T_Semicolon()
    {
      var ts = Lexer.LexerString(@";");
      Assert.AreEqual(ts[0].Type, TokenType.Semicolon);
    }

    [Test]
    public void T_String()
    {
      foreach (var t in Lexer.LexerString(@"  ""hoge""  @""foo"" "))
      {
        Assert.AreEqual(t.Type, TokenType.String);
      }
    }

    [Test]
    public void T_Char()
    {
      var ts = Lexer.LexerString(@" 'h' ");
      Assert.AreEqual(ts[0].Type, TokenType.Char);
    }

    [Test]
    public void T_ParenthesesOpen()
    {
      var ts = Lexer.LexerString(@" ( ");
      Assert.AreEqual(ts[0].Type, TokenType.ParenthesesOpen);
    }

    [Test]
    public void T_ParenthesesClose()
    {
      var ts = Lexer.LexerString(@" ) ");
      Assert.AreEqual(ts[0].Type, TokenType.ParenthesesClose);
    }

    [Test]
    public void T_CurlyBracketOpen()
    {
      var ts = Lexer.LexerString(@" { ");
      Assert.AreEqual(ts[0].Type, TokenType.CurlyBracketOpen);
    }

    [Test]
    public void T_CurlyBracketClose()
    {
      var ts = Lexer.LexerString(@" } ");
      Assert.AreEqual(ts[0].Type, TokenType.CurlyBracketClose);
    }

    [Test]
    public void T_SquareBracketOpen()
    {
      var ts = Lexer.LexerString(@" [ ");
      Assert.AreEqual(ts[0].Type, TokenType.SquareBracketOpen);
    }

    [Test]
    public void T_SquareBracketClose()
    {
      var ts = Lexer.LexerString(@" ] ");
      Assert.AreEqual(ts[0].Type, TokenType.SquareBracketClose);
    }

    [Test]
    public void T_Operator()
    {
      foreach (var t in Lexer.LexerString(@" + ! - , : ? "))
      {
        Assert.AreEqual(t.Type, TokenType.Operator);
      }
    }

    [Test]
    public void T_Identifier()
    {
      var ts = Lexer.LexerString(@"_abc123");
      Assert.AreEqual(ts[0].Type, TokenType.Identifier);
    }

    [Test]
    public void T_Keyword()
    {
      foreach (var t in Lexer.LexerString(@"case default"))
      {
        Assert.AreEqual(t.Type, TokenType.Keyword);
      }
    }
  }
}
