using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.SalesInvoiceExport
{
    public class SalesInvoiceExportDetailModel : BaseModel
    {
        public int BonId { get; set; }
        [MaxLength(255)]
        public string BonNo { get; set; }
        [MaxLength(255)]
        public string ContractNo { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [MaxLength(255)]
        public string WeightUom { get; set; }
        [MaxLength(255)]
        public string TotalUom { get; set; }
        public double GrossWeight { get; set; }
        public double NetWeight { get; set; }
        public double TotalMeas { get; set; }

        public virtual SalesInvoiceExportModel SalesInvoiceExportModel { get; set; }
        public virtual ICollection<SalesInvoiceExportItemModel> SalesInvoiceExportItems { get; set; }
    }
}
