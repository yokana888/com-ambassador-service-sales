using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.Garment
{
    public class MonitoringPreSalesContractViewModel
    {
        public string Section { get; set; }
        public string SCNo { get; set; }
        public DateTime SCDate { get; set; }
        public string SCType { get; set; }
        public string BuyerAgent { get; set; }
        public string BuyerBrand { get; set; }
        public string Type { get; set; }
        public int OrderQuantity { get; set; }
        public List<MonitoringPreSalesContractGPRViewModel> GarmentPurchaseRequests { get; set; }
        public List<MonitoringPreSalesContractCCViewModel> CostCalculations { get; set; }
    }

    public class MonitoringPreSalesContractGPRViewModel
    {
        public string PRNo { get; set; }
        public string RONo { get; set; }
        public string PRType { get; set; }
        public string Unit { get; set; }
        public DateTime Date { get; set; }
        public string Article { get; set; }
    }

    public class MonitoringPreSalesContractCCViewModel
    {
        public DateTime Date { get; set; }
        public string RONo { get; set; }
        public string Article { get; set; }
        public string Unit { get; set; }
        public double Quantity { get; set; }
        public string Uom { get; set; }
        public double ConfirmPrice { get; set; }
    }
}
