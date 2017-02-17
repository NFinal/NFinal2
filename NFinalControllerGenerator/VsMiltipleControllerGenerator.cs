using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.ComponentModelHost;

namespace NFinalControllerGenerator
{
    public class VsMiltipleControllerGenerator : VsMultipleFileGenerator<StructAction>
    {
        public override int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".txt";
            return 0;
        }

        public override byte[] GenerateContent(StructAction element)
        {
            throw new NotImplementedException();
        }

        public override byte[] GenerateSummaryContent()
        {
            return new byte[0];
        }

        public override IEnumerator<StructAction> GetEnumerator()
        {

            IComponentModel componentModel =
        (IComponentModel)Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(SComponentModel));

            var visualStuioWorkspace = componentModel.GetService<VisualStudioWorkspace>();
            //var componentModel = (IComponentModel)this.GetService(typeof(SComponentModel));
            //if (componentModel != null)
            //{
            //    visualStuioWorkspace = componentModel.GetService<Microsoft.VisualStudio.LanguageServices.VisualStudioWorkspace>();
            //}
            
            var currentDoc = visualStuioWorkspace.CurrentSolution.GetDocumentIdsWithFilePath(this.InputFilePath).FirstOrDefault();
            var proj = visualStuioWorkspace.CurrentSolution.GetProject(currentDoc.ProjectId);

            StringWriter sw = new StringWriter();
            //if (msbw == null)
            //{
            //    msbw = MSBuildWorkspace.Create(new Dictionary<string, string> { { "DesignTimeBuild", "true" },
            //    { "IntelliSenseBuild", "true" },
            //    { "BuildingInsideVisualStudio", "true" }});
            //}
            //if (proj == null)
            //{
            //    proj = msbw.OpenProjectAsync(project.FileName).Result;
            //}
            CSharpCompilation cSharpCompilation = null;
            Compilation compilation = null;
            if (cSharpCompilation == null)
            {
                if (proj.TryGetCompilation(out compilation))
                {
                    cSharpCompilation = (CSharpCompilation)compilation;
                }
                else
                {
                    cSharpCompilation = (CSharpCompilation)proj.GetCompilationAsync().Result;
                }
            }
            var document = proj.Documents.Single(doc => { return doc.FilePath == this.InputFilePath; });
            List<StructAction> structActionList = new List<StructAction>();
            StructAction structAction = null;
            StructModel model = new StructModel();
            byte[] buffer = null;// model.GetDocument(sw, document, cSharpCompilation);
            return structActionList.GetEnumerator();
        }

        protected override string GetFileName(StructAction element)
        {
            return element.fileName;
        }
    }
}
