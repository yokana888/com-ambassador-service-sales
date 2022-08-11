using System;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.Garment
{
    public class AvailableROReportViewModel
    {
        public DateTime AcceptedDate { get; set; }
        public DateTime AvailableDate { get; set; }
        public DateTime ValidatedMDDate { get; set; }
        public DateTime ValidatedSampleDate { get; set; }
        public string RONo { get; set; }
        public string Article { get; set; }
        public int DateDiff { get; set; }
        public string BuyerCode { get; set; }
        public string Buyer { get; set; }
        public DateTime DeliveryDate { get; set; }
        public double Quantity { get; set; }
        public string Uom { get; set; }
        public string AvailableBy { get; set; }
    }
}
