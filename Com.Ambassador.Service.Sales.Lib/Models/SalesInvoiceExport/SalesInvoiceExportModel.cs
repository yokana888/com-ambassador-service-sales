using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.SalesInvoiceExport
{
    public class SalesInvoiceExportModel : BaseModel
    {
        [MaxLength(255)]
        public string Code { get; set; }
        public long AutoIncreament { get; set; }
        [MaxLength(255)]
        public string SalesInvoiceNo { get; set; }
        [MaxLength(255)]
        public string SalesInvoiceCategory { get; set; }
        [MaxLength(255)]
        public string LetterOfCreditNumberType { get; set; }
        public DateTimeOffset SalesInvoiceDate { get; set; }
        [MaxLength(255)]
        public string FPType { get; set; }
        [MaxLength(255)]
        public string BuyerName { get; set; }
        [MaxLength(1000)]
        public string BuyerAddress { get; set; }
        [MaxLength(255)]
        public string Authorized { get; set; }
        [MaxLength(1000)]
        public string ShippedPer { get; set; }
        public DateTimeOffset SailingDate { get; set; }
        [MaxLength(255)]
        public string LetterOfCreditNumber { get; set; }
        public DateTimeOffset LCDate { get; set; }
        [MaxLength(255)]
        public string IssuedBy { get; set; }
        [MaxLength(255)]
        public string From { get; set; }
        [MaxLength(255)]
        public string To { get; set; }
        [MaxLength(255)]
        public string HSCode { get; set; }
        [MaxLength(255)]
        public string TermOfPaymentType { get; set; }
        [MaxLength(255)]
        public string TermOfPaymentRemark { get; set; }
        public string ShippingRemark { get; set; }
        [MaxLength(1000)]
        public string Remark { get; set; }

        public virtual ICollection<SalesInvoiceExportDetailModel> SalesInvoiceExportDetails { get; set; }
    }
}
