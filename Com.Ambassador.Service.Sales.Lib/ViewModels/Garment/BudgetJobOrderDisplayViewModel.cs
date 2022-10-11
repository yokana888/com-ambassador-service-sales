using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.Garment
{
    public class BudgetJobOrderDisplayViewModel
    {
        public string RO_Number { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public string Article { get; set; }
        public string ComodityCode { get; set; }
        public double Quantity { get; set; }
        public string UOMUnit { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string ProductRemark { get; set; }
        public double BudgetQuantity { get; set; }
        public string UomPriceName { get; set; }
        public double Price { get; set; }
        public string POSerialNumber { get; set; }
        public string Type { get; set; }
    }
}
