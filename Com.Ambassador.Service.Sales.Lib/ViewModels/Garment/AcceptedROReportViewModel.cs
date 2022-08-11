using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.Garment
{
    public class AcceptedROReportViewModel
    {
        public DateTime AcceptedDate { get; set; }
        public string RONo { get; set; }
        public string Article { get; set; }
        public string Buyer { get; set; }
        public string BuyerName { get; set; }
        public DateTime DeliveryDate { get; set; }
        public double Quantity { get; set; }
        public string Uom { get; set; }
        public string AcceptedBy { get; set; }
    }
}
