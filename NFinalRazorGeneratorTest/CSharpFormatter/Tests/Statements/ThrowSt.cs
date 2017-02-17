
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_Throw
  {
    [Test]
    public void T_1()
    {
      var fp = new ParseFunc[]{
        ThrowSt.Singleton()
      };
      var input = @"throw;";
      var expect = new String[]{
        @"throw;",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_2()
    {
      var fp = new ParseFunc[]{
        ThrowSt.Singleton()
      };
      var input = @"throw new Exception(@""hoge"");";
      var expect = new String[]{
        @"throw new Exception(@""hoge"");",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
