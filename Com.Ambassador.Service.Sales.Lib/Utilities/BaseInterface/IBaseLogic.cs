using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.Utilities.BaseInterface
{
    public interface IBaseLogic<TModel>
    {
        ReadResponse<TModel> Read(int page, int size, string order, List<string> select, string keyword, string filter);
        void Create(TModel model);
        Task<TModel> ReadByIdAsync(long id);
        void UpdateAsync(long id, TModel model);
        Task DeleteAsync(long id);
    }
}
