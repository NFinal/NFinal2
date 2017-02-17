
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_WhileSt
  {
    [Test]
    public void T_1()
    {
      var fp = new ParseFunc[]{
        WhileSt.Singleton()
      };
      var input = @" while ( 1==4+2  )  { a == 23;continue; } ";
      var expect = new String[]{
        @"while (1 == 4 + 2)",
        @"{",
        @"  a == 23;",
        @"  continue;",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_2()
    {
      var fp = new ParseFunc[]{
        WhileSt.Singleton()
      };
      var input = @" while ( 1==4+2  )  {} ";
      var expect = new String[]{
        @"while (1 == 4 + 2)",
        @"{",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_3()
    {
      var fp = new ParseFunc[]{
        WhileSt.Singleton()
      };
      var input = @" while ( idx < 10 ) idx++; ";
      var expect = new String[]{
        @"while (idx < 10)",
        @"  idx++;",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
