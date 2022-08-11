using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.Report
{
    public class FabricQualityControlViewModel
    {
        public string orderNo { get; set; }
        public double orderQuantity { get; set; }
        public string grade { get; set; }
    }

    public class FabricAPiResult
    {
        public string apiVersion { get; set; }
        public List<FabricQualityControlViewModel> data { get; set; }
        public object info { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
    }
}
