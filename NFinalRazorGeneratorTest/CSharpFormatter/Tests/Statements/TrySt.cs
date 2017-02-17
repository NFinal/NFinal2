
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_TrySt
  {
    [Test]
    public void T_1()
    {
      var fp = new ParseFunc[]{
        TrySt.Singleton()
      };
      var input = @"  try {hoge.foo(1,2+3);} finally{throw new Exception(@""fail"");} ";
      var expect = new String[]{
        @"try",
        @"{",
        @"  hoge.foo(1, 2 + 3);",
        @"}",
        @"finally",
        @"{",
        @"  throw new Exception(@""fail"");",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_2()
    {
      var fp = new ParseFunc[]{
        TrySt.Singleton()
      };
      var input = @"  try {} catch(Exception ex){}catch(Exception){}finally{} ";
      var expect = new String[]{
        @"try",
        @"{",
        @"}",
        @"catch (Exception ex)",
        @"{",
        @"}",
        @"catch (Exception)",
        @"{",
        @"}",
        @"finally",
        @"{",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
