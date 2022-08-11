using Com.Ambassador.Service.Sales.Lib.Models.SalesInvoice;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseInterface;
using Com.Ambassador.Service.Sales.Lib.ViewModels.SalesInvoice;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.SalesInvoice
{
    public interface ISalesInvoiceContract : IBaseFacade<SalesInvoiceModel>
    {
        List<SalesInvoiceModel> ReadByBuyerId(int buyerId);
        Task<int> UpdateFromSalesReceiptAsync(int id, SalesInvoiceUpdateModel model);
        Task<List<SalesInvoiceReportViewModel>> GetReport(int buyerId, long salesInvoiceId, bool? isPaidOff, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet);
        Task<MemoryStream> GenerateExcel(int buyerId, long salesInvoiceId, bool? isPaidOff, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet);
    }
}
