using System.Linq;

namespace Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass
{
    public abstract class BaseMonitoringLogic<TViewModel>
    {
        public abstract IQueryable<TViewModel> GetQuery(string filterString);
    }
}
