using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Razor;
using System.Web.Razor.Text;
using NFinalRazorGenerator;

namespace NFinalRazorGeneratorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectRootPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppContext.BaseDirectory)));
            string InputFilePath = Path.Combine(projectRootPath, "Sample", "Sample.cshtml");
            StreamReader reader = File.OpenText(InputFilePath);
            string inputFileContent = reader.ReadToEnd();
            reader.Dispose();
         
            string FileNameSpace = "SampleNameSpace";
            if (InputFilePath.EndsWith(".cshtml"))
            {
                string nameSpace = FileNameSpace;
                string className = Path.GetFileNameWithoutExtension(InputFilePath);
                string outPutFilePath = Path.Combine(Path.GetDirectoryName(InputFilePath), className+".cs");
                RazorWriter razorWriter = new NFinalRazorGeneratorTest.RazorWriter(inputFileContent);
                StreamWriter sw = new StreamWriter(outPutFilePath, false, System.Text.Encoding.UTF8);
                razorWriter.WriteTemplate(sw, nameSpace, className);
                sw.Dispose();
            }
        }
    }
}
