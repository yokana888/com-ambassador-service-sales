using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.Garment
{
    public class GarmentProductionOrderReportViewModel
    {
        public string Week { get; set; }
        public List<GarmentProductionOrderReportBuyerViewModel> Buyers { get; set; }
    }

    public class GarmentProductionOrderReportBuyerViewModel
    {
        public string Buyer { get; set; }
        public double Quantities { get; set; }
        public double Amounts { get; set; }
        public List<GarmentProductionOrderReportDetailViewModel> Details { get; set; }
    }

    public class GarmentProductionOrderReportDetailViewModel
    {
        public string Section { get; set; }
        public string Commodity { get; set; }
        public string Article { get; set; }
        public string RONo { get; set; }
        public DateTime Date { get; set; }
        public double Quantity { get; set; }
        public string Uom { get; set; }
        public double ConfirmPrice { get; set; }
        public double Amount { get; set; }
        public DateTime ConfirmDate { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string ValidationPPIC { get; set; }
    }
}
