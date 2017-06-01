//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : 任务完成.cs
//        Description :任务完成，正常输出Http流
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NFinal.Owin.Builder
{
    /// <summary>
    /// 任务完成，正常输出Http流
    /// </summary>
    public class Completed
    {
        private static readonly Task completed = CreateCompletedTask();

        private static Task CreateCompletedTask()
        {
            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(null);
            return tcs.Task;
        }
        /// <summary>
        /// 引入完成任务
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public Task Invoke(IDictionary<string, object> env)
        {
            env["owin.ResponseStatusCode"] = 200;
            return completed;
        }
    }
}
