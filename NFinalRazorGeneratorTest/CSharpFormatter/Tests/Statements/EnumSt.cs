
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_EnumSt
  {
    [Test]
    public void T_01()
    {
      var fp = new ParseFunc[]{
        EnumSt.Singleton()
      };
      var input = @"public enum Hoge{}";
      var expect = new String[]{
        @"public enum Hoge",
        @"{",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_02()
    {
      var fp = new ParseFunc[]{
        EnumSt.Singleton()
      };
      var input = @"public enum Hoge{A,}";
      var expect = new String[]{
        @"public enum Hoge",
        @"{",
        @"  A,",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_03()
    {
      var fp = new ParseFunc[]{
        EnumSt.Singleton()
      };
      var input = @"public enum Hoge{A,B,}";
      var expect = new String[]{
        @"public enum Hoge",
        @"{",
        @"  A,",
        @"  B,",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_04()
    {
      var fp = new ParseFunc[]{
        EnumSt.Singleton()
      };
      var input = @"public enum Hoge{A,B}";
      var expect = new String[]{
        @"public enum Hoge",
        @"{",
        @"  A,",
        @"  B",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_05()
    {
      var fp = new ParseFunc[]{
        EnumSt.Singleton()
      };
      var input = @"public enum Hoge{A=2,B}";
      var expect = new String[]{
        @"public enum Hoge",
        @"{",
        @"  A = 2,",
        @"  B",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_06()
    {
      var fp = new ParseFunc[]{
        EnumSt.Singleton()
      };
      var input = @"public enum Hoge{A=2,B=4}";
      var expect = new String[]{
        @"public enum Hoge",
        @"{",
        @"  A = 2,",
        @"  B = 4",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
