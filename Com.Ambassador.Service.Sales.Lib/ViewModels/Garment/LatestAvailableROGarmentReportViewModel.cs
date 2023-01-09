using System;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.Garment
{
    public class LatestAvailableROGarmentReportViewModel
    {
        public DateTime ApprovedSampleDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string RONo { get; set; }
        public string Article { get; set; }
        public int DateDiff { get; set; }
        public string BuyerCode { get; set; }
        public string Buyer { get; set; }
        public string Type { get; set; }
        public double Quantity { get; set; }
        public string Uom { get; set; }
        public double LeadTime { get; set; }
        public string SizeRange { get; set; }
        public string Fabric { get; set; }
        public int RO_GarmentId { get; set; }
    }
}
