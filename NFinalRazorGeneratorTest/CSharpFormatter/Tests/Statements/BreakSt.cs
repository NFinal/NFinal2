
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_BreakSt
  {
    [Test]
    public void T_1()
    {
      var fp = new ParseFunc[]{
        BreakSt.Singleton()
      };
      var input = @"break;";
      var expect = new String[]{
        @"break;",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_2()
    {
      var fp = new ParseFunc[]{
        BreakSt.Singleton()
      };
      var input = @"yield break;";
      var expect = new String[]{
        @"yield break;",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
