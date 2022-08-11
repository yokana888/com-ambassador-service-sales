using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment
{
    public class ProfitGarmentBySectionReportViewModel
    {
        public int count { get; set; }
        public string RO_Number { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public string Article { get; set; }
        public string Section { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public string UnitName { get; set; }
        public string Comodity { get; set; }
        public string ComodityDescription { get; set; }
        public double Profit { get; set; }
        public double ProfitIDR { get; set; }
        public double ProfitUSD { get; set; }
        public double ProfitFOB { get; set; }
        public double Commision { get; set; }
        public double Quantity { get; set; }
        public string UOMUnit { get; set; }
        public double ConfirmPrice { get; set; }
        public double CurrencyRate { get; set; }
        public double CMPrice { get; set; }
        public double FOBPrice { get; set; }
        public double FabAllow { get; set; }
        public double AccAllow { get; set; }
        public double Amount { get; set; }
    }
}