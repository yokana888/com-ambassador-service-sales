using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.Report
{
    public class ProductionOrderReportViewModel
    {
        public long id { get; set; }
        public string status { get; set; }
        public string detail { get; set; }
        public string orderNo { get; set; }
        public string NoSalesContract { get; set; }
        public string colorType { get; set; }
        public double Price { get; set; }
        public string CurrCode { get; set; }
        public double orderQuantity { get; set; }
        public string orderType { get; set; }
        public string processType { get; set; }
        public string construction { get; set; }
        public string designCode { get; set; }
        public string designNumber { get; set; }
        public string colorTemplate { get; set; }
        public string colorRequest { get; set; }
        public string buyer { get; set; }
        public string buyerType { get; set; }
        public string staffName { get; set; }
        public DateTimeOffset _createdDate { get; set; }
        public DateTimeOffset deliveryDate { get; set; }

    }
}
