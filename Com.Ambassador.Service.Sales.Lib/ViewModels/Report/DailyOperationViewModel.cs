using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.Report
{
    public class DailyOperationViewModel
    {
        public string orderNo { get; set; }
        public double orderQuantity { get; set; }
        public string color { get; set; }
        public string area { get; set; }
        public string machine { get; set; }
        public string step { get; set; }
    }

    public class DailyAPiResult
    {
        public string apiVersion { get; set; }
        public List<DailyOperationViewModel> data { get; set; }
        public object info { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
    }
}
