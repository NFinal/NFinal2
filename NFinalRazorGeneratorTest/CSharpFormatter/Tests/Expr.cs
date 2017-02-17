
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_Expr
  {
    [Test]
    public void T_01()
    {
      var input = @"1+2++";
      var expect = @"1 + 2++";
      TestUtils.EqExpr(input, expect);
    }

    [Test]
    public void T_02()
    {
      var input = @"1+2-3";
      var expect = @"1 + 2 - 3";
      TestUtils.EqExpr(input, expect);
    }

    [Test]
    public void T_03()
    {
      var input = @"1+2-(4*5-3)%3/2";
      var expect = @"1 + 2 - (4 * 5 - 3) % 3 / 2";
      TestUtils.EqExpr(input, expect);
    }

    [Test]
    public void T_04()
    {
      var input = @"select x * 2";
      var expect = @"select x * 2";
      TestUtils.EqExpr(input, expect);
    }

    [Test]
    public void T_05()
    {
      var input = @"from x in new String[]{1,2,3}";
      var expect = "from x in new String[]{\n  1,\n  2,\n  3\n}";
      TestUtils.EqExpr(input, expect);
    }

    [Test]
    public void T_06()
    {
      var input = @"select x * 2 into x";
      var expect = @"select x * 2 into x";
      TestUtils.EqExpr(input, expect);
    }

    [Test]
    public void T_07()
    {
      var input = @"group x * 2 by x";
      var expect = @"group x * 2 by x";
      TestUtils.EqExpr(input, expect);
    }

    [Test]
    public void T_08()
    {
      var input = @"group x * 2 by x into g";
      var expect = @"group x * 2 by x into g";
      TestUtils.EqExpr(input, expect);
    }

    [Test]
    public void T_09()
    {
      var input = @"select x * 2 from x in a";
      var expect = @"select x * 2 from x in a";
      TestUtils.EqExpr(input, expect);
    }

    [Test]
    public void T_10()
    {
      var input = @"select x * y let y = 2*3*4 from x in a";
      var expect = @"select x * y let y = 2 * 3 * 4 from x in a";
      TestUtils.EqExpr(input, expect);
    }

    [Test]
    public void T_11()
    {
      var input = @"select new{X=x,Y=y,Z=z} orderby x ascending, y desceding, z";
      var expect = "select new{\n  X = x,\n  Y = y,\n  Z = z\n} orderby x ascending, y desceding, z";
      TestUtils.EqExpr(input, expect);
    }

    [Test]
    public void T_12()
    {
      var input = @"join prod in products on category.ID equals prod.CategoryID";
      var expect = "join prod in products on category.ID equals prod.CategoryID";
      TestUtils.EqExpr(input, expect);
    }
  }
}
