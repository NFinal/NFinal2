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

namespace NFinal.Compile.SqlTemplate.sqlite
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class QueryObject : RazorGenerator.Templating.RazorTemplateBase
    {
#line hidden
        #line 3 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
           
    public NFinal.Compile.SqlTemplate.Model.QueryObject Model { get; set; }

        #line default
        #line hidden
        
        public override void Execute()
        {
WriteLiteral("\r\n");

WriteLiteral("#region\tvar ");

            
            #line 6 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
              
            
            #line default
            #line hidden
            
            #line 6 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
         Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 6 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                            
            
            #line default
            #line hidden
WriteLiteral("; 选取首行首列的值\r\n\t\t\tvar __");

            
            #line 7 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
           
            
            #line default
            #line hidden
            
            #line 7 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
      Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 7 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                              
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 7 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                  
            
            #line default
            #line hidden
            
            #line 7 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                             Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 7 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                
            
            #line default
            #line hidden
WriteLiteral("_command__ = new System.Data.SQLite.SQLiteCommand(\"");

            
            #line 7 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                      
            
            #line default
            #line hidden
            
            #line 7 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                 Write(Model.sql);

            
            #line default
            #line hidden
            
            #line 7 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                                
            
            #line default
            #line hidden
WriteLiteral("\", ");

            
            #line 7 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                                      
            
            #line default
            #line hidden
            
            #line 7 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                                 Write(Model.connectionVarName);

            
            #line default
            #line hidden
            
            #line 7 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                                                              
            
            #line default
            #line hidden
WriteLiteral(");\r\n");

            
            #line 8 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
			
            
            #line default
            #line hidden
            
            #line 8 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
    if(Model.isTransaction){

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

WriteLiteral("__");

            
            #line 9 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
         
            
            #line default
            #line hidden
            
            #line 9 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
    Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 9 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                            
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 9 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                
            
            #line default
            #line hidden
            
            #line 9 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                           Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 9 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                              
            
            #line default
            #line hidden
WriteLiteral("_command__.Transaction=");

            
            #line 9 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                        
            
            #line default
            #line hidden
            
            #line 9 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                   Write(Model.transactionVarName);

            
            #line default
            #line hidden
            
            #line 9 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                 
            
            #line default
            #line hidden
WriteLiteral(";\r\n");

            
            #line 10 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
			}

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

            
            #line 11 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
    if(Model.sqlVarParameters.Count>0){

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

WriteLiteral("var __");

            
            #line 12 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
             
            
            #line default
            #line hidden
            
            #line 12 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
        Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 12 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 12 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                    
            
            #line default
            #line hidden
            
            #line 12 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                               Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 12 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                  
            
            #line default
            #line hidden
WriteLiteral("_parameters__=new System.Data.SQLite.SQLiteParameter[");

            
            #line 12 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                          
            
            #line default
            #line hidden
            
            #line 12 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                     Write(Model.sqlVarParameters.Count);

            
            #line default
            #line hidden
            
            #line 12 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                                                       
            
            #line default
            #line hidden
WriteLiteral("];\r\n");

            
            #line 13 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
			}

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

            
            #line 14 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
     int i=0;
            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 15 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
			
            
            #line default
            #line hidden
            
            #line 15 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
    foreach(var sqlVarParameter in Model.sqlVarParameters){
			if(sqlVarParameter.field.length>0){

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

WriteLiteral("__");

            
            #line 17 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
         
            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
    Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                            
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 17 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                
            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                           Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                              
            
            #line default
            #line hidden
WriteLiteral("_parameters__[");

            
            #line 17 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                        Write(i);

            
            #line default
            #line hidden
WriteLiteral("] = new System.Data.SQLite.SQLiteParameter(\"");

WriteLiteral("@");

            
            #line 17 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                               
            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                          Write(sqlVarParameter.name);

            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                                                    
            
            #line default
            #line hidden
WriteLiteral("\",System.Data.DbType.");

            
            #line 17 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                                                                            
            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                                                                       Write(sqlVarParameter.field.dbType);

            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                                                                                                         
            
            #line default
            #line hidden
WriteLiteral(",");

            
            #line 17 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                                                                                                             
            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                                                                                                        Write(sqlVarParameter.field.length);

            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                                                                                                                                          
            
            #line default
            #line hidden
WriteLiteral(");\r\n");

WriteLiteral("\t\t\t");

WriteLiteral("__");

            
            #line 18 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
         
            
            #line default
            #line hidden
            
            #line 18 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
    Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 18 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                            
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 18 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                
            
            #line default
            #line hidden
            
            #line 18 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                           Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 18 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                              
            
            #line default
            #line hidden
WriteLiteral("_parameters__[");

            
            #line 18 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                        Write(i);

            
            #line default
            #line hidden
WriteLiteral("].Value = ");

            
            #line 18 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                           
            
            #line default
            #line hidden
            
            #line 18 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                      Write(sqlVarParameter.csharpName);

            
            #line default
            #line hidden
            
            #line 18 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                      
            
            #line default
            #line hidden
WriteLiteral(".WithDBNull();\r\n");

            
            #line 19 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
			}else{

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

WriteLiteral("__");

            
            #line 20 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
         
            
            #line default
            #line hidden
            
            #line 20 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
    Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 20 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                            
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 20 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                
            
            #line default
            #line hidden
            
            #line 20 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                           Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 20 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                              
            
            #line default
            #line hidden
WriteLiteral("_parameters__[");

            
            #line 20 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                        Write(i);

            
            #line default
            #line hidden
WriteLiteral("] = new System.Data.SQLite.SQLiteParameter(\"");

WriteLiteral("@");

            
            #line 20 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                               
            
            #line default
            #line hidden
            
            #line 20 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                          Write(sqlVarParameter.name);

            
            #line default
            #line hidden
            
            #line 20 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                                                    
            
            #line default
            #line hidden
WriteLiteral("\",System.Data.DbType.");

            
            #line 20 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                                                                            
            
            #line default
            #line hidden
            
            #line 20 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                                                                       Write(sqlVarParameter.field.dbType);

            
            #line default
            #line hidden
            
            #line 20 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                                                                                                         
            
            #line default
            #line hidden
WriteLiteral(");\r\n");

WriteLiteral("\t\t\t");

WriteLiteral("__");

            
            #line 21 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
         
            
            #line default
            #line hidden
            
            #line 21 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
    Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 21 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                            
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 21 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                
            
            #line default
            #line hidden
            
            #line 21 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                           Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 21 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                              
            
            #line default
            #line hidden
WriteLiteral("_parameters__[");

            
            #line 21 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                        Write(i);

            
            #line default
            #line hidden
WriteLiteral("].Value = ");

            
            #line 21 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                           
            
            #line default
            #line hidden
            
            #line 21 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                      Write(sqlVarParameter.csharpName);

            
            #line default
            #line hidden
            
            #line 21 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                      
            
            #line default
            #line hidden
WriteLiteral(".WithDBNull();\r\n");

            
            #line 22 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
			}
			i++;
			}

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

            
            #line 25 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
    if(Model.sqlVarParameters.Count>0){

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

WriteLiteral("__");

            
            #line 26 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
         
            
            #line default
            #line hidden
            
            #line 26 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
    Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 26 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                            
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 26 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                
            
            #line default
            #line hidden
            
            #line 26 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                           Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 26 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                              
            
            #line default
            #line hidden
WriteLiteral("_command__.Parameters.AddRange(__");

            
            #line 26 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                  
            
            #line default
            #line hidden
            
            #line 26 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                             Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 26 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                     
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 26 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                         
            
            #line default
            #line hidden
            
            #line 26 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                    Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 26 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                                                       
            
            #line default
            #line hidden
WriteLiteral("_parameters__);\r\n");

            
            #line 27 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
			}

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

            
            #line 28 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
    if(Model.hasGenericType){
			if(Model.isDeclaration){

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

            
            #line 30 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
       
            
            #line default
            #line hidden
            
            #line 30 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
  Write(Model.type);

            
            #line default
            #line hidden
            
            #line 30 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                  
            
            #line default
            #line hidden
WriteLiteral(" ");

            
            #line 30 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                      
            
            #line default
            #line hidden
            
            #line 30 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                 Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 30 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                    
            
            #line default
            #line hidden
WriteLiteral("= __");

            
            #line 30 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                           
            
            #line default
            #line hidden
            
            #line 30 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                      Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 30 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                              
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 30 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                  
            
            #line default
            #line hidden
            
            #line 30 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                             Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 30 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                                
            
            #line default
            #line hidden
WriteLiteral("_command__.ExecuteScalar().AsVar();\r\n");

            
            #line 31 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
			}
			}else{

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

WriteLiteral("var ");

            
            #line 33 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
           
            
            #line default
            #line hidden
            
            #line 33 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
      Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 33 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                         
            
            #line default
            #line hidden
WriteLiteral(" = __");

            
            #line 33 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                 
            
            #line default
            #line hidden
            
            #line 33 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                            Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 33 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                    
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 33 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                        
            
            #line default
            #line hidden
            
            #line 33 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                   Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 33 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                                                      
            
            #line default
            #line hidden
WriteLiteral("_command__.ExecuteScalar().AsVar();\r\n");

            
            #line 34 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("\t\t\t__");

            
            #line 35 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
       
            
            #line default
            #line hidden
            
            #line 35 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
  Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 35 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                          
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 35 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                              
            
            #line default
            #line hidden
            
            #line 35 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                         Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 35 "..\..\SqlTemplate\sqlite\QueryObject.cshtml"
                                            
            
            #line default
            #line hidden
WriteLiteral("_command__.Dispose();\r\n\t\t\t#endregion\r\n\t\t\t");

        }
    }
}
#pragma warning restore 1591
