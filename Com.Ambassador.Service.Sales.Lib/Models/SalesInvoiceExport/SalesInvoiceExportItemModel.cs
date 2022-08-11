using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.SalesInvoiceExport
{
    public class SalesInvoiceExportItemModel : BaseModel
    {
        public int ProductId { get; set; }
        [MaxLength(255)]
        public string ProductCode { get; set; }
        [MaxLength(255)]
        public string ProductName { get; set; }
        [MaxLength(255)]
        public double QuantityPacking { get; set; }
        [MaxLength(255)]
        public string PackingUom { get; set; }
        [MaxLength(255)]
        public string ItemUom { get; set; }
        public double QuantityItem { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }

        public virtual SalesInvoiceExportDetailModel SalesInvoiceExportDetailModel { get; set; }
    }
}
