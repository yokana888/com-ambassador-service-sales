using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.SalesInvoice;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.DOReturn
{
    public class DOReturnItemViewModel : BaseViewModel
    {
        //public SalesInvoiceDetailViewModel SalesInvoiceDetail { get; set; }
        //public SalesInvoiceItemViewModel SalesInvoiceItem { get; set; }

        public int? ShippingOutId { get; set; }
        public string BonNo { get; set; }
        public int? ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public double? QuantityPacking { get; set; }
        public string PackingUom { get; set; }
        public string ItemUom { get; set; }
        public double? QuantityItem { get; set; }
    }
}
