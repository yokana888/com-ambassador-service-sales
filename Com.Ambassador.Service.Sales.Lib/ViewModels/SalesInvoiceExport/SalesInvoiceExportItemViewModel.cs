using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.SalesInvoiceExport
{
    public class SalesInvoiceExportItemViewModel : BaseViewModel
    {
        public int? ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public double? QuantityPacking { get; set; }
        public string PackingUom { get; set; }
        public string ItemUom { get; set; }
        public double? QuantityItem { get; set; }
        public double? Price { get; set; }
        public double? Amount { get; set; }
    }
}
