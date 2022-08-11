using Com.Ambassador.Service.Sales.Lib.Utilities.BaseInterface;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface
{
    public interface ICanceledGarmentBookingOrderReportFacade
    {
        Tuple<List<CanceledGarmentBookingOrderReportViewModel>, int> Read(string no, string buyerCode, string statusCancel, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order, int offset);
        MemoryStream GenerateExcel(string no, string buyerCode, string statusCancel, DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
