using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting
{
    public class FinishingPrintingSalesContractReportViewModel : BaseViewModel
	{
        public string salesContractNo { get; set; }
        public string buyerName { get; set; }
        public string buyerType { get; set; }
        public string dispositionNo { get; set; }
        public string orderType { get; set; }
        public string comodityName { get; set; }
        public string materialName { get; set; }
        public string materialConstructionName { get; set; }
        public string yarnMaterialName { get; set; }
        public string materialWidth { get; set; }
        public double orderQuantity { get; set; }
        public double productionOrderQuantity { get; set; }
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
        public string color { get; set; }
        public string useIncomeTax { get; set; }
        public string status { get; set; }
        public string sppOrderNo { get; set; }
        public DateTimeOffset? sppDate { get; set; }
    }
}
