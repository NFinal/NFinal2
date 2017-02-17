
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_DefaultSt
  {
    [Test]
    public void T_1()
    {
      var fp = new ParseFunc[]{
        DefaultSt.Singleton()
      };
      var input = @"default:";
      var expect = new String[]{
        @"default:",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
