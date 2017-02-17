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
    public partial class IdIncrement : RazorGenerator.Templating.RazorTemplateBase
    {
#line hidden
        #line 3 "..\..\SqlTemplate\oracle\IdIncrement.cshtml"
           
    public NFinal.Compile.SqlTemplate.Model.IdIncrement Model { get; set; }

        #line default
        #line hidden
        
        public override void Execute()
        {
WriteLiteral("\r\n");

WriteLiteral("--oracle数据库专用增加id自增的sql语句\r\n");

            
            #line 7 "..\..\SqlTemplate\oracle\IdIncrement.cshtml"
  foreach(var table in Model.tables){

            
            #line default
            #line hidden
WriteLiteral("--为表");

            
            #line 8 "..\..\SqlTemplate\oracle\IdIncrement.cshtml"
        
            
            #line default
            #line hidden
            
            #line 8 "..\..\SqlTemplate\oracle\IdIncrement.cshtml"
   Write(table.name);

            
            #line default
            #line hidden
            
            #line 8 "..\..\SqlTemplate\oracle\IdIncrement.cshtml"
                   
            
            #line default
            #line hidden
WriteLiteral("增加自增字段\r\n");

WriteLiteral("--创建自增序列\r\n");

WriteLiteral("CREATE SEQUENCE ");

            
            #line 10 "..\..\SqlTemplate\oracle\IdIncrement.cshtml"
                    
            
            #line default
            #line hidden
            
            #line 10 "..\..\SqlTemplate\oracle\IdIncrement.cshtml"
               Write(table.name);

            
            #line default
            #line hidden
            
            #line 10 "..\..\SqlTemplate\oracle\IdIncrement.cshtml"
                               
            
            #line default
            #line hidden
WriteLiteral("_id_sequence\r\n");

WriteLiteral("INCREMENT BY 1 -- 每次加几个\r\n");

WriteLiteral("START WITH 1 -- 从1开始计数\r\n");

WriteLiteral("NOMAXVALUE -- 不设置最大值\r\n");

WriteLiteral("NOCYCLE -- 一直累加，不循环\r\n");

WriteLiteral("NOCACHE; -- 不建缓冲区\r\n");

WriteLiteral("--创建插入触发器，更新id字段\r\n");

WriteLiteral("CREATE OR REPLACE TRIGGER ");

            
            #line 17 "..\..\SqlTemplate\oracle\IdIncrement.cshtml"
                              
            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\oracle\IdIncrement.cshtml"
                         Write(table.name);

            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\oracle\IdIncrement.cshtml"
                                         
            
            #line default
            #line hidden
WriteLiteral("_id_triger BEFORE\r\n");

WriteLiteral("INSERT ON ");

            
            #line 18 "..\..\SqlTemplate\oracle\IdIncrement.cshtml"
              
            
            #line default
            #line hidden
            
            #line 18 "..\..\SqlTemplate\oracle\IdIncrement.cshtml"
         Write(table.name);

            
            #line default
            #line hidden
            
            #line 18 "..\..\SqlTemplate\oracle\IdIncrement.cshtml"
                         
            
            #line default
            #line hidden
WriteLiteral(" FOR EACH ROW WHEN (new.id is null)--只有在id为空时，启动该触发器生成id号\r\n");

WriteLiteral("begin\r\n");

WriteLiteral("select ");

            
            #line 20 "..\..\SqlTemplate\oracle\IdIncrement.cshtml"
           
            
            #line default
            #line hidden
            
            #line 20 "..\..\SqlTemplate\oracle\IdIncrement.cshtml"
      Write(table.name);

            
            #line default
            #line hidden
            
            #line 20 "..\..\SqlTemplate\oracle\IdIncrement.cshtml"
                      
            
            #line default
            #line hidden
WriteLiteral("_id_sequence.nextval into: new.id from dual;\r\n");

WriteLiteral("end;\r\n");

            
            #line 22 "..\..\SqlTemplate\oracle\IdIncrement.cshtml"
}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
