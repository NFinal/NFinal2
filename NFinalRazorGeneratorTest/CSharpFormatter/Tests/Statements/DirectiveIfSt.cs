
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Parsers;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_DirectiveIfSt
  {
    [Test]
    public void T_if()
    {
      var pf = new ParseFunc[]{
        IfSt.Singleton(),
        DirectiveIfSt.Singleton()
      };
      var input = @"if(true){#if true}";
      var expect = new String[]{
        @"if (true)",
        @"{",
        @"#if true",
        @"}",
      };
      TestUtils.Eq(pf, input, expect);
    }

    [Test]
    public void T_elif()
    {
      var fp = new ParseFunc[]{
        IfSt.Singleton(),
        DirectiveIfSt.Singleton()
      };
      var input = @"if(true){#elif false}";
      var expect = new String[]{
        @"if (true)",
        @"{",
        @"#elif false",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_else()
    {
      var fp = new ParseFunc[]{
        DirectiveIfSt.Singleton()
      };
      var input = @"#else";
      var expect = new String[]{
        @"#else",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_endif()
    {
      var fp = new ParseFunc[]{
        DirectiveIfSt.Singleton()
      };
      var input = @"#endif";
      var expect = new String[]{
        @"#endif",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
