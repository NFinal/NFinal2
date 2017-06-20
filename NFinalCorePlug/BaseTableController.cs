using System;
using System.Collections.Generic;
using System.Text;
using NFinal;

namespace NFinalCorePlug
{
    public class BaseTableController<TModel> : BaseController where TModel : class
    {
        public void Insert()
        {
            this.Insert<TModel>();
        }
        public bool Update()
        {
            return this.Update<TModel>();
        }
        public bool Delete()
        {
            return this.Delete<TModel>();
        }
        public IEnumerable<TModel> Page(int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            return this.GetAll<TModel>($" limit {(pageIndex - 1) * pageSize},{pageSize}");
        }
    }
}
