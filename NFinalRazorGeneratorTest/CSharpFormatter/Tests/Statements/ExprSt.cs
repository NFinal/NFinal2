
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_ExprSt
  {
    [Test]
    public void T_01()
    {
      var fp = new ParseFunc[]{
        ExprSt.Singleton()
      };
      var input = @"1 + 2++;";
      var expect = new String[]{
        @"1 + 2++;",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_02()
    {
      var fp = new ParseFunc[]{
        ExprSt.Singleton()
      };
      var input = @"3 + 4--;";
      var expect = new String[]{
        @"3 + 4--;",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_03()
    {
      var fp = new ParseFunc[]{
        ExprSt.Singleton()
      };
      var input = @"5++ + 6;";
      var expect = new String[]{
        @"5++ + 6;",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_04()
    {
      var fp = new ParseFunc[]{
        ExprSt.Singleton()
      };
      var input = @"7-- + 8;";
      var expect = new String[]{
        @"7-- + 8;",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_05()
    {
      var fp = new ParseFunc[]{
        ExprSt.Singleton()
      };
      var input = @"8 + 8 - 8 * 8 / 8 < 8 > 8 << 8 >> 8 % 8 & 8 && 8 || 8 | 8;";
      var expect = new String[]{
        @"8 + 8 - 8 * 8 / 8 < 8 > 8 << 8 >> 8 % 8 & 8 && 8 || 8 | 8;",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_06()
    {
      var fp = new ParseFunc[]{
        ExprSt.Singleton()
      };
      var input = @"8 + (8 - 8);";
      var expect = new String[]{
        @"8 + (8 - 8);",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_07()
    {
      var fp = new ParseFunc[]{
        ExprSt.Singleton()
      };
      var input = @"new List<String>(){};";
      var expect = new String[]{
        @"new List<String>(){};",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_08()
    {
      var fp = new ParseFunc[]{
        ExprSt.Singleton()
      };
      var input = @"new List<List<String>,Dictionary<String,String>>();";
      var expect = new String[]{
        @"new List<List<String>, Dictionary<String, String>>();",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_09()
    {
      var fp = new ParseFunc[]{
        ExprSt.Singleton()
      };
      var input = @"(new Int32[]{ 0, 1, 2, 3, 4, })[0];";
      var expect = new String[]{
        @"(new Int32[]{",
        @"  0,",
        @"  1,",
        @"  2,",
        @"  3,",
        @"  4,",
        @"})[0];",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_10()
    {
      var fp = new ParseFunc[]{
        ExprSt.Singleton()
      };
      var input = @"new Int32[]{ 0, 1, 2, 3, 4, }[0];";
      var expect = new String[]{
        @"new Int32[]{",
        @"  0,",
        @"  1,",
        @"  2,",
        @"  3,",
        @"  4,",
        @"}[0];",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_11()
    {
      var fp = new ParseFunc[]{
        ExprSt.Singleton()
      };
      var input = @"new List<Int32[]>[]{new Int32[]{0,1,},new Int32[]{2,3,},};";
      var expect = new String[]{
        @"new List<Int32[]>[]{",
        @"  new Int32[]{",
        @"    0,",
        @"    1,",
        @"  },",
        @"  new Int32[]{",
        @"    2,",
        @"    3,",
        @"  },",
        @"};",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_12()
    {
      var fp = new ParseFunc[]{
        ExprSt.Singleton()
      };
      var input = @"new{ hoge = 12, foo = 34, };";
      var expect = new String[]{
        @"new{",
        @"  hoge = 12,",
        @"  foo = 34,",
        @"};",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_13()
    {
      var fp = new ParseFunc[]{
        ExprSt.Singleton()
      };
      var input = @"new[]{ new{ X = 0, Y = 1,},new{ X = 2, Y = 3,},new{ X = 4, Y = 5,},new{ X = 6, Y = 7,}, };";
      var expect = new String[]{
        @"new[]{",
        @"  new{",
        @"    X = 0,",
        @"    Y = 1,",
        @"  },",
        @"  new{",
        @"    X = 2,",
        @"    Y = 3,",
        @"  },",
        @"  new{",
        @"    X = 4,",
        @"    Y = 5,",
        @"  },",
        @"  new{",
        @"    X = 6,",
        @"    Y = 7,",
        @"  },",
        @"};",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_14()
    {
      var fp = new ParseFunc[]{
        ExprSt.Singleton()
      };
      var input = @"idx++;";
      var expect = new String[]{
        @"idx++;",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
