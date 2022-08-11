using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.Spinning
{
    public class SpinningSalesContractReportViewModel : BaseViewModel
    {
        public string salesContractNo { get; set; }
        public string buyerName { get; set; }
        public string buyerType { get; set; }
        public string dispositionNo { get; set; }
        public string comodityName { get; set; }
        public double orderQuantity { get; set; }
        public string uomUnit { get; set; }
        public double shippingQuantityTolerance { get; set; }
        public string qualityName { get; set; }
        public double price { get; set; }
        public string accountCurrencyCode { get; set; }
        public string termOfPaymentName { get; set; }
        public string paymentTo { get; set; }
        public DateTimeOffset? deliverySchedule { get; set; }
        public string agentName { get; set; }
        public string comission { get; set; }
    }
}
