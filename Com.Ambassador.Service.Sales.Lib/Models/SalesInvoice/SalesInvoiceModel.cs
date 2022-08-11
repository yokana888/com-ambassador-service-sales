using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Ambassador.Service.Sales.Lib.Models.SalesInvoice
{
    public class SalesInvoiceModel : BaseModel
    {
        [MaxLength(255)]
        public string Code { get; set; }
        public long AutoIncreament { get; set; }
        [MaxLength(255)]
        public string SalesInvoiceNo { get; set; }
        [MaxLength(255)]
        public string SalesInvoiceType { get; set; }
        [MaxLength(255)]
        public string SalesInvoiceCategory { get; set; }
        public DateTimeOffset SalesInvoiceDate { get; set; }
        public DateTimeOffset DueDate { get; set; }
        [MaxLength(255)]
        public string DeliveryOrderNo { get; set; }

        #region Buyer
        public int BuyerId { get; set; }
        [MaxLength(255)]
        public string BuyerName { get; set; }
        [MaxLength(255)]
        public string BuyerCode { get; set; }
        [MaxLength(1000)]
        public string BuyerAddress { get; set; }
        [MaxLength(255)]
        public string BuyerNPWP { get; set; }
        [MaxLength(255)]
        public string BuyerNIK { get; set; }
        #endregion

        #region Currency
        public int CurrencyId { get; set; }
        [MaxLength(255)]
        public string CurrencyCode { get; set; }
        [MaxLength(255)]
        public string CurrencySymbol { get; set; }
        public double CurrencyRate { get; set; }
        #endregion

        [MaxLength(255)]
        public string PaymentType { get; set; }
        [MaxLength(255)]
        public string VatType { get; set; }
        //TotalPayment => jumlah yang ditangguhkan
        public double TotalPayment { get; set; }
        //TotalPaid => jumlah yang sudah dibayar
        public double TotalPaid { get; set; }
        //IsPaidOff => status lunas/tidak
        public bool IsPaidOff { get; set; }
        [MaxLength(1000)]
        public string Remark { get; set; }

        [MaxLength(256)]
        public string Sales { get; set; }

        [MaxLength(100)]
        public string UnitId { get; set; }

        [MaxLength(1000)]
        public string UnitCode { get; set; }

        [MaxLength(1000)]
        public string UnitName { get; set; }

        public virtual ICollection<SalesInvoiceDetailModel> SalesInvoiceDetails { get; set; }
    }
}
