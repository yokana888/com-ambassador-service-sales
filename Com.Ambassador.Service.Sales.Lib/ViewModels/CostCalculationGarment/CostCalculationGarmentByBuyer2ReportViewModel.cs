using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment
{
    public class CostCalculationGarmentByBuyer2ReportViewModel 
    {
        public int count { get; set; }
        public string RO_Number { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public string SalesContractNo { get; set; }
        public string Article { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public double ConfirmPrice { get; set; }
        public double Quantity { get; set; }
        public string UOMUnit { get; set; }
        public double Amount { get; set; }
    }
}
