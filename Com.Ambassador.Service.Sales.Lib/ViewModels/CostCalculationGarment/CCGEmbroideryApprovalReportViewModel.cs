using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment
{
    public class CCGEmbroideryApprovalReportViewModel 
    {
        public int count { get; set; }
        public string RO_Number { get; set; }
        public string UnitName { get; set; }
        public string Section { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string Article { get; set; }
        public double Quantity { get; set; }
        public string UOMUnit { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public DateTimeOffset ValidatedDate { get; set; }
        public string PONumber { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public double BudgetQuantity { get; set; }
        public string BudgetUOM { get; set; }
    }
}
