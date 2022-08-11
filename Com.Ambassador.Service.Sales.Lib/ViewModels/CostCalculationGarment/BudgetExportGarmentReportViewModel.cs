using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment
{
    public class BudgetExportGarmentReportViewModel 
    {
        public int count { get; set; }
        public string RO_Number { get; set; }
        public string UnitName { get; set; }
        public string Section { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string Article { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public string PONumber { get; set; }
        public string CategoryName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public double BudgetQuantity { get; set; }
        public string BudgetUOM { get; set; }
        public double BudgetPrice { get; set; }
        public double BudgetAmount { get; set; }
    }
}
