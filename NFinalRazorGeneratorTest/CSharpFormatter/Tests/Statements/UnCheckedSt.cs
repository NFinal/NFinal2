
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_UnChecked
  {
    [Test]
    public void T_1()
    {
      var fp = new ParseFunc[]{
        UnCheckedSt.Singleton()
      };
      var input = @"unchecked{f();}";
      var expect = new String[]{
        @"unchecked",
        @"{",
        @"  f();",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_2()
    {
      var fp = new ParseFunc[]{
        UnCheckedSt.Singleton()
      };
      var input = @"unchecked{}";
      var expect = new String[]{
        @"unchecked",
        @"{",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
