//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :Table.cs
//        Description :表信息类
//
//        created by Lucas at  2015-6-30`
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Compile.DB.Coding
{

    /// <summary>
    /// 表信息
    /// </summary>
    public class Table
    {
        public string name;
        public string nameCs;
        public string nameJs;
        public bool hasId;
        public Field id=null;
        public System.Collections.Generic.List<Field> fields = null;

        public Table()
        {
            name = "";
        }

        public Table(string name)
        {
            this.name = name;
        }
    }
}
