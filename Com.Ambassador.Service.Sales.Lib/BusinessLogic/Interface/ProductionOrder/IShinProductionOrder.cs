using Com.Ambassador.Service.Sales.Lib.Models.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseInterface;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Report;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Report.OrderStatusReport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.ProductionOrder
{
    public interface IShinProductionOrder : IBaseFacade<ProductionOrderModel>
    {
        Task<Tuple<List<ProductionOrderReportViewModel>, int>> GetReport(string salesContractNo, string orderNo, string orderTypeId, string processTypeId, string buyerId, string accountId, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order, int offset);
        Task<MemoryStream> GenerateExcel(string salesContractNo, string orderNo, string orderTypeId, string processTypeId, string buyerId, string accountId, DateTime? dateFrom, DateTime? dateTo, int offset);
        Task<ProductionOrderReportDetailViewModel> GetDetailReport(long no);
        Task<int> UpdateRequestedTrue(List<int> ids);
        Task<int> UpdateRequestedFalse(List<int> ids);
        Task<int> UpdateIsCompletedTrue(int id);
        Task<int> UpdateIsCalculated(int id, bool flag);
        Task<int> UpdateIsCompletedFalse(int id);
        Task<int> UpdateDistributedQuantity(List<int> id, List<double> distributedQuantity);
        List<YearlyOrderQuantity> GetMonthlyOrderQuantityByYearAndOrderType(int year, int orderTypeId, int timeoffset);
        List<MonthlyOrderQuantity> GetMonthlyOrderIdsByOrderType(int year, int month, int orderTypeId, int timeoffset);
        double GetTotalQuantityBySalesContractId(long id);
        Task<int> ApproveByMD(long id);
        //List<ProductionOrderModel> ReadBySalesContractNo(string salesContractNo);

        Task<int> ApproveBySample(long id);
    }
}
