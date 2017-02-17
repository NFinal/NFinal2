
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_IfSt
  {
    [Test]
    public void T_1()
    {
      var fp = new ParseFunc[]{
        IfSt.Singleton()
      };
      var input = @" if(true  )  { break; } ";
      var expect = new String[]{
        @"if (true)",
        @"{",
        @"  break;",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_2()
    {
      var fp = new ParseFunc[]{
        IfSt.Singleton()
      };
      var input = @" if(true  )  {} ";
      var expect = new String[]{
        @"if (true)",
        @"{",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_3()
    {
      var fp = new ParseFunc[]{
        IfSt.Singleton()
      };
      var input = @" if(true  )  {} else if(true){}else{}";
      var expect = new String[]{
        @"if (true)",
        @"{",
        @"}",
        @"else if (true)",
        @"{",
        @"}",
        @"else",
        @"{",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_4()
    {
      var fp = new ParseFunc[]{
        IfSt.Singleton()
      };
      var input = @" if(true  ) idx++; else if (false)idx++; else idx--;";
      var expect = new String[]{
        @"if (true)",
        @"  idx++;",
        @"else if (false)",
        @"  idx++;",
        @"else",
        @"  idx--;",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
