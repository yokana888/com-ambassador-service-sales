using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.SalesInvoiceExport
{
    public class SalesInvoiceExportDetailViewModel : BaseViewModel
    {
        public int? BonId { get; set; }
        public string BonNo { get; set; }
        public string ContractNo { get; set; }
        public string Description { get; set; }
        public string WeightUom { get; set; }
        public string TotalUom { get; set; }
        public double? GrossWeight { get; set; }
        public double? NetWeight { get; set; }
        public double? TotalMeas { get; set; }

        public ICollection<SalesInvoiceExportItemViewModel> SalesInvoiceExportItems { get; set; }
    }
}
