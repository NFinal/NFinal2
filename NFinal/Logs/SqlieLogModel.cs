using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Logs
{
    public class SqlieLogModel
    {
        public int id { get; set; }
        public string actionKey { get; set; }
        public string controllerName { get; set; }
        public string methodName { get; set; }
        //处理时间
        public int processPeriod;
        public bool isSuccess;
        public string parameters;
        public DateTime requestTime;
        //页面输出时间
        public int outPutPeriod;
    }
}
