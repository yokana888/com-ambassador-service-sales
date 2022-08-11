using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment
{
    public class CostCalculationGarmentApprovalReportViewModel
    {
        public int count { get; set; }
        public string RO_Number { get; set; }
        public string StaffName { get; set; }
        public string UnitName { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public DateTimeOffset ConfirmDate { get; set; }
        public string Section { get; set; }
        public string Comodity { get; set; }
        public string Article { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public double Quantity { get; set; }
        public string UOMUnit { get; set; }
        public string ValidatedKadiv { get; set; }
        public DateTimeOffset ValidatedDate { get; set; }
    }
}
