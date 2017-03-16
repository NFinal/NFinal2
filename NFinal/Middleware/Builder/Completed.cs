using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NFinal.Owin.Builder
{
    public class Completed
    {
        private static readonly Task completed = CreateCompletedTask();

        private static Task CreateCompletedTask()
        {
            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(null);
            return tcs.Task;
        }

        public Task Invoke(IDictionary<string, object> env)
        {
            env["owin.ResponseStatusCode"] = 200;
            return completed;
        }
    }
}
