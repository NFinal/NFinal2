
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  partial class Test_DelegateMethodSt
  {
    [Test]
    public void T_00()
    {
      var fp = new ParseFunc[]{
        DelegateMethodSt.Singleton()
      };
      var input = @"private delegate void F([MarshalAs(UnmanagedType.LPStr)]Dictionary<List<String>,Tuple<List<String>,Int32>> ttt);";
      var expect = new String[]{
        @"private delegate void F([MarshalAs(UnmanagedType.LPStr)] Dictionary<List<String>, Tuple<List<String>, Int32>> ttt);",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_01()
    {
      var fp = new ParseFunc[]{
        DelegateMethodSt.Singleton()
      };
      var input = @"private delegate Dictionary<List<String>,Tuple<List<String>,Int32>> F();";
      var expect = new String[]{
        @"private delegate Dictionary<List<String>, Tuple<List<String>, Int32>> F();",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_04()
    {
      var fp = new ParseFunc[]{
        DelegateMethodSt.Singleton()
      };
      var input = @"private delegate String Hoge(Int32 a, Boolean b);";
      var expect = new String[]{
        @"private delegate String Hoge(Int32 a, Boolean b);",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_05()
    {
      var fp = new ParseFunc[]{
        DelegateMethodSt.Singleton()
      };
      var input = @"private delegate String Hoge(Int32 a, Boolean b = true);";
      var expect = new String[]{
        @"private delegate String Hoge(Int32 a, Boolean b = true);",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_06()
    {
      var fp = new ParseFunc[]{
        DelegateMethodSt.Singleton()
      };
      var input = @"private delegate String Hoge([In] Int32 a, Boolean b = true);";
      var expect = new String[]{
        @"private delegate String Hoge([In] Int32 a, Boolean b = true);",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_07()
    {
      var fp = new ParseFunc[]{
        DelegateMethodSt.Singleton()
      };
      var input = @"private delegate String Hoge([In] [Out] Int32 a, Boolean b = true);";
      var expect = new String[]{
        @"private delegate String Hoge([In][Out] Int32 a, Boolean b = true);",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_08()
    {
      var fp = new ParseFunc[]{
        DelegateMethodSt.Singleton()
      };
      var input = @"private delegate String Hoge([In,Out] Int32 a, Boolean b = true);";
      var expect = new String[]{
        @"private delegate String Hoge([In, Out] Int32 a, Boolean b = true);",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_09()
    {
      var fp = new ParseFunc[]{
        DelegateMethodSt.Singleton()
      };
      var input = @"private delegate extern String Hoge();";
      var expect = new String[]{
        @"private delegate extern String Hoge();",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_10()
    {
      var fp = new ParseFunc[]{
        DelegateMethodSt.Singleton()
      };
      var input = @"private delegate extern String Hoge(out String str);";
      var expect = new String[]{
        @"private delegate extern String Hoge(out String str);",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_11()
    {
      var fp = new ParseFunc[]{
        DelegateMethodSt.Singleton()
      };
      var input = @"private delegate extern String Hoge([Out]out String str);";
      var expect = new String[]{
        @"private delegate extern String Hoge([Out] out String str);",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
