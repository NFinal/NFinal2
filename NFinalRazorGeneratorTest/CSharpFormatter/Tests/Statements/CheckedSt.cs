
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_Checked
  {
    [Test]
    public void T_1()
    {
      var fp = new ParseFunc[]{
        CheckedSt.Singleton()
      };
      var input = @"checked{f();}";
      var expect = new String[]{
        @"checked",
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
        CheckedSt.Singleton()
      };
      var input = @"checked{}";
      var expect = new String[]{
        @"checked",
        @"{",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
