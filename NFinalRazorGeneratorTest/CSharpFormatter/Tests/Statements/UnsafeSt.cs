
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_UnsafeSt
  {
    [Test]
    public void T_1()
    {
      var fp = new ParseFunc[]{
        UnsafeSt.Singleton()
      };
      var input = @"unsafe{f();}";
      var expect = new String[]{
        @"unsafe",
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
        UnsafeSt.Singleton()
      };
      var input = @"unsafe{}";
      var expect = new String[]{
        @"unsafe",
        @"{",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
