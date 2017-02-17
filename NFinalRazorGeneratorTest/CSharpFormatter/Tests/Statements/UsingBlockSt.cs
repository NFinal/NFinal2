
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_UsingBlockSt
  {
    [Test]
    public void T_1()
    {
      var fp = new ParseFunc[]{
        UsingBlockSt.Singleton()
      };
      var input = @" using (var a = new String[]{}  )  { x = f(); } ";
      var expect = new String[]{
        @"using (var a = new String[]{})",
        @"{",
        @"  x = f();",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_2()
    {
      var fp = new ParseFunc[]{
        UsingBlockSt.Singleton()
      };
      var input = @" using (var a = new String[]{}  )  {} ";
      var expect = new String[]{
        @"using (var a = new String[]{})",
        @"{",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_3()
    {
      var fp = new ParseFunc[]{
        UsingBlockSt.Singleton()
      };
      var input = @" using (var a = new String[]{}  )  idx++; ";
      var expect = new String[]{
        @"using (var a = new String[]{})",
        @"  idx++;",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
