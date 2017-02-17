
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_Case
  {
    [Test]
    public void T_1()
    {
      var fp = new ParseFunc[]{
        CaseSt.Singleton()
      };
      var input = @"case   1  :";
      var expect = new String[]{
        @"case 1:",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_2()
    {
      var fp = new ParseFunc[]{
        CaseSt.Singleton()
      };
      var input = @"  case   @""""  :";
      var expect = new String[]{
        @"case @"""":",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_3()
    {
      var fp = new ParseFunc[]{
        CaseSt.Singleton()
      };
      var input = @"  case   Hoge.Foo  :";
      var expect = new String[]{
        @"case Hoge.Foo:",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
