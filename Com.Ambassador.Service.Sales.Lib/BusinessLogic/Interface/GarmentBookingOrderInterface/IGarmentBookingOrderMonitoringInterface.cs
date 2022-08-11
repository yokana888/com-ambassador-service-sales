using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface
{
    public interface IGarmentBookingOrderMonitoringInterface
    {
        Tuple<List<GarmentBookingOrderMonitoringViewModel>, int> Read(string section, string no, string buyerCode, string comodityCode, string statusConfirm, string statusBookingOrder, DateTime? dateFrom, DateTime? dateTo, DateTime? dateDeliveryFrom, DateTime? dateDeliveryTo, int page, int size, string Order, int offset);
        MemoryStream GenerateExcel(string section, string no, string buyerCode, string comodityCode, string statusConfirm, string statusBookingOrder, DateTime? dateFrom, DateTime? dateTo, DateTime? dateDeliveryFrom, DateTime? dateDeliveryTo, int offset);
    }
}
