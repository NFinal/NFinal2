using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFinal.Extension;

namespace NFinal.Validation
{
    /// <summary>
    /// 验证结果
    /// </summary>
    public class ValidResult : List<ValidateStatus>
    {
        /// <summary>
        /// 验证未通过
        /// </summary>
        public bool isDirty = false;
        public static bool operator true(ValidResult result)
        {
            return !result.isDirty;
        }
        public static bool operator false(ValidResult result)
        {
            return result.isDirty;
        }
        public static ValidResult operator +(ValidResult result1, ValidResult result2)
        {
            ValidResult result = new ValidResult();
            foreach (var r in result1)
            {
                if (r.isDirty)
                {
                    result.Add(r);
                }
            }
            foreach (var r in result2)
            {
                if (r.isDirty)
                {
                    result.Add(r);
                }
            }
            if (result.Count > 0)
            {
                result.isDirty = true;
            }
            return result;
        }
        public void Write<TContext,TRequest>(NFinal.IAction<TContext, TRequest> action)
        {
            if (this.isDirty)
            {
                action.SetResponseHeader("Content-Type", "text/html");
                foreach (var r in this)
                {
                    action.Write(r.message);
                }
            }
        }
    }
}
