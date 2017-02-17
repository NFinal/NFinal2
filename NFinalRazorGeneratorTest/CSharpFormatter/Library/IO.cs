
using System;
using System.IO;
using System.Text;

namespace CSharpFormatter.Library
{
  public class IO
  {
    public static Boolean ExistsFile(String path)
    {
      return File.Exists(path);
    }

    public static String[] ReadFile(String path, Encoding enc)
    {
      return File.ReadAllLines(path, enc);
    }

    public static void WriteFileCRLF(String path, String output, Encoding enc)
    {
      File.WriteAllLines(path, output.Split('\n'), enc);
    }

    public static void WriteFileLF(String path, String output, Encoding enc)
    {
      File.WriteAllText(path, output, enc);
    }

    public static void PrintNormal(String line)
    {
      Console.ResetColor();
      Console.WriteLine(line);
      Console.ResetColor();
    }

    public static void PrintRed(String line)
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine(line);
      Console.ResetColor();
    }

    public static void PrintDarkGray(String line)
    {
      Console.ForegroundColor = ConsoleColor.DarkGray;
      Console.WriteLine(line);
      Console.ResetColor();
    }

    public static void PrintYellow(String line)
    {
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine(line);
      Console.ResetColor();
    }

    public static void PrintGreen(String line)
    {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine(line);
      Console.ResetColor();
    }

    public static void PrintHide(String line)
    {
      Console.ForegroundColor = Console.BackgroundColor;
      Console.WriteLine(line);
      Console.ResetColor();
    }
  }
}
