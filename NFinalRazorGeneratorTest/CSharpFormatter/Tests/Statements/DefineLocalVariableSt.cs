
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_DefineLocalVariableSt
  {
    [Test]
    public void T_01()
    {
      var fp = new ParseFunc[]{
        DefineLocalVariableSt.Singleton()
      };
      var input = @"String[] ss;";
      var expect = new String[]{
        @"String[] ss;",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_02()
    {
      var fp = new ParseFunc[]{
        DefineLocalVariableSt.Singleton()
      };
      var input = @"String str = @""hi"";";
      var expect = new String[]{
        @"String str = @""hi"";",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_03()
    {
      var fp = new ParseFunc[]{
        DefineLocalVariableSt.Singleton()
      };
      var input = @"const String str = @""hi"";";
      var expect = new String[]{
        @"const String str = @""hi"";",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_04()
    {
      var fp = new ParseFunc[]{
        DefineLocalVariableSt.Singleton()
      };
      var input = @"var str = @""hi"";";
      var expect = new String[]{
        @"var str = @""hi"";",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_05()
    {
      var fp = new ParseFunc[]{
        DefineLocalVariableSt.Singleton()
      };
      var input = @"List<String> a = new List<String>();";
      var expect = new String[]{
        @"List<String> a = new List<String>();",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_06()
    {
      var fp = new ParseFunc[]{
        DefineLocalVariableSt.Singleton()
      };
      var input = @"List<String, List<String, List<String, String>>> a = new List<String, List<String, List<String, String>>>();";
      var expect = new String[]{
        @"List<String, List<String, List<String, String>>> a = new List<String, List<String, List<String, String>>>();",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
