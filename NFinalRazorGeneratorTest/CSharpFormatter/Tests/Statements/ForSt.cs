
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_ForSt
  {
    [Test]
    public void T_01()
    {
      var fp = new ParseFunc[]{
        ForSt.Singleton()
      };
      var input = @" for(Int32 idx = 0;idx<10;idx++  )  { } ";
      var expect = new String[]{
        @"for (Int32 idx = 0; idx < 10; idx++)",
        @"{",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_02()
    {
      var fp = new ParseFunc[]{
        ForSt.Singleton()
      };
      var input = @" for(Int32 idx = 0; idx < 10;)  { } ";
      var expect = new String[]{
        @"for (Int32 idx = 0; idx < 10;)",
        @"{",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_03()
    {
      var fp = new ParseFunc[]{
        ForSt.Singleton()
      };
      var input = @" for( idx = 0; idx < 10;)  { } ";
      var expect = new String[]{
        @"for ( idx = 0; idx < 10;)",
        @"{",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_04()
    {
      var fp = new ParseFunc[]{
        ForSt.Singleton()
      };
      var input = @" for(; idx < 10;)  { } ";
      var expect = new String[]{
        @"for (; idx < 10;)",
        @"{",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_05()
    {
      var fp = new ParseFunc[]{
        ForSt.Singleton()
      };
      var input = @" for(; idx < 10;) idx++; ";
      var expect = new String[]{
        @"for (; idx < 10;)",
        @"  idx++;",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
