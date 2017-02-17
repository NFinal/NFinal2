
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_ForeachSt
  {
    [Test]
    public void T_1()
    {
      var fp = new ParseFunc[]{
        ForeachSt.Singleton()
      };
      var input = @" foreach(var b in new Boolean[]{ true,false,false,false,true}  )  { break; } ";
      var expect = new String[]{
        @"foreach (var b in new Boolean[]{",
        @"  true,",
        @"  false,",
        @"  false,",
        @"  false,",
        @"  true",
        @"})",
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
        ForeachSt.Singleton()
      };
      var input = @" foreach(var n in new{1,2}) idx++; ";
      var expect = new String[]{
        @"foreach (var n in new{",
        @"  1,",
        @"  2",
        @"})",
        @"  idx++;",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
