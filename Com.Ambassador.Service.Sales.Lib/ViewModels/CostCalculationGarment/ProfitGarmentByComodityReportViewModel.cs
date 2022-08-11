using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment
{
    public class ProfitGarmentByComodityReportViewModel
    {
        public int count { get; set; }
        public string ComodityCode { get; set; }
        public string ComodityName { get; set; }
        public double Quantity { get; set; }
        public string UOMUnit { get; set; }
        public double Amount { get; set; }
        public double ProfitIDR { get; set; }
        public double ProfitUSD { get; set; }
        public double ProfitFOB { get; set; }
    }
}
