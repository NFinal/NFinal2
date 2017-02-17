
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_ContinueSt
  {
    [Test]
    public void T_1()
    {
      var fp = new ParseFunc[]{
        ContinueSt.Singleton()
      };
      var input = @"continue;";
      var expect = new String[]{
        @"continue;",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
