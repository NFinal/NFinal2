using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace NFinalModelGeneratorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = AppContext.BaseDirectory;
            fileName=Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(fileName))),"Models","raincol.sql");
            StreamReader streamReader= File.OpenText(fileName);
            string sqlContent= streamReader.ReadToEnd();
            NFinalModelGenerator.SqlDocument doc = new NFinalModelGenerator.SqlDocument(fileName,"NFinalModelGeneratorTest",sqlContent);
            StreamWriter sw = null;
            foreach (var model in doc.modelFileDataList)
            {
                sw = new StreamWriter(model.fileName, false, System.Text.Encoding.UTF8);
                sw.Write(model.content);
                sw.Dispose();
            }
            Console.WriteLine(fileName);
            Console.ReadKey();
        }
    }
}
