﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NFinal.Compile.SqlTemplate.oracle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class Open : RazorGenerator.Templating.RazorTemplateBase
    {
#line hidden
        #line 3 "..\..\SqlTemplate\oracle\Open.cshtml"
           
    public NFinal.Compile.SqlTemplate.Model.Open Model { get; set; }

        #line default
        #line hidden
        
        public override void Execute()
        {
WriteLiteral("\r\n");

WriteLiteral("var ");

            
            #line 6 "..\..\SqlTemplate\oracle\Open.cshtml"
      
            
            #line default
            #line hidden
            
            #line 6 "..\..\SqlTemplate\oracle\Open.cshtml"
 Write(Model.connectionVarName);

            
            #line default
            #line hidden
            
            #line 6 "..\..\SqlTemplate\oracle\Open.cshtml"
                              
            
            #line default
            #line hidden
WriteLiteral(" = new Oracle.ManagedDataAccess.Client.OracleConnection(NFinal.Config.Configurati" +
"onManager.ConnectionStrings[");

            
            #line 6 "..\..\SqlTemplate\oracle\Open.cshtml"
                                                                                                                                              
            
            #line default
            #line hidden
            
            #line 6 "..\..\SqlTemplate\oracle\Open.cshtml"
                                                                                                                                         Write(Model.dbName);

            
            #line default
            #line hidden
            
            #line 6 "..\..\SqlTemplate\oracle\Open.cshtml"
                                                                                                                                                           
            
            #line default
            #line hidden
WriteLiteral("].ConnectionString);\r\n");

            
            #line 7 "..\..\SqlTemplate\oracle\Open.cshtml"
			
            
            #line default
            #line hidden
            
            #line 7 "..\..\SqlTemplate\oracle\Open.cshtml"
     
            
            #line default
            #line hidden
            
            #line 7 "..\..\SqlTemplate\oracle\Open.cshtml"
Write(Model.connectionVarName);

            
            #line default
            #line hidden
            
            #line 7 "..\..\SqlTemplate\oracle\Open.cshtml"
                             
            
            #line default
            #line hidden
WriteLiteral(".Open();\r\n");

        }
    }
}
#pragma warning restore 1591
