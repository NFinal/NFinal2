
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_ReturnSt
  {
    [Test]
    public void T_1()
    {
      var fp = new ParseFunc[]{
        ReturnSt.Singleton()
      };
      var input = @"return;";
      var expect = new String[]{
        @"return;",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_2()
    {
      var fp = new ParseFunc[]{
        ReturnSt.Singleton()
      };
      var input = @"return new String[]{};";
      var expect = new String[]{
        @"return new String[]{};",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
