using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment
{
    public class DetailCMGarmentByUnitReportViewModel 
    {
        public int count { get; set; }
        public string RO_Number { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public string Article { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public string UnitName { get; set; }        
        public double Quantity { get; set; }
        public string UOMUnit { get; set; }
        public double FOB_Price { get; set; }
        public double ConfirmPrice { get; set; }
        public double CM { get; set; }
        public double CurrencyRate { get; set; }
        public double CMIDR { get; set; }
        public double OTL1 { get; set; }
        public double OTL2 { get; set; }
        public double SMV_Cutting { get; set; }
        public double SMV_Sewing { get; set; }
        public double SMV_Finishing { get; set; }
        public double SMV_Total { get; set; }
        public double Insurance { get; set; }
        public double Freight { get; set; }
        public double Commission { get; set; }
        public double BudgetAmount { get; set; }
        public double CMPrice { get; set; }
    }
}
