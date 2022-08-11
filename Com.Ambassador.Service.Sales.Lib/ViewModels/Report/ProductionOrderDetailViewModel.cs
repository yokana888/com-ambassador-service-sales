using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.Report
{
    public class ProductionOrderDetailViewModel
    {
        public string orderNo { get; set; }
        public double orderQuantity { get; set; }
        public string uom { get; set; }
    }
}
