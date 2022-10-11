using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.GarmentSalesContractModel
{
    public class GarmentSalesContract : BaseModel
    {
        [MaxLength(255)]
        public string SalesContractNo { get; set; }
        public int BuyerBrandId { get; set; }
        public string BuyerBrandCode { get; set; }
        public string BuyerBrandName { get; set; }
        [MaxLength(20)]
        public string SCType { get; set; }
        [MaxLength(1000)]
        public string Packing { get; set; }
        [MaxLength(3000)]
        public string DocPresented { get; set; }
        [MaxLength(3000)]
        public string FOB { get; set; }
        public double Amount { get; set; }
        [MaxLength(255)]
        public string Delivery { get; set; }
        [MaxLength(255)]
        public string Country { get; set; }
        [MaxLength(3000)]
        public string NoHS { get; set; }
        public bool IsMaterial { get; set; }
        public bool IsTrimming { get; set; }
        public bool IsEmbrodiary { get; set; }
        public bool IsPrinted { get; set; }
        public bool IsWash { get; set; }
        public bool IsTTPayment { get; set; }
        public string PaymentDetail { get; set; }
        public long AccountBankId { get; set; }
        [MaxLength(500)]
        public string AccountBankName { get; set; }
        [MaxLength(500)]
        public string AccountName { get; set; }
        public bool DocPrinted { get; set; }
        public double FreightCost { get; set; }
        [MaxLength(500)]
        public string PaymentMethod { get; set; }
        [MaxLength(500)]
        public string DownPayment { get; set; }
        public int LatePayment { get; set; }
        public int LateReturn { get; set; }
        public double? Claim { get; set; }

        [MaxLength(255)]
        public string RecipientName { get; set; }
        [MaxLength(3000)]
        public string RecipientAddress { get; set; }
        [MaxLength(100)]
        public string RecipientJob { get; set; }
        public virtual ICollection<GarmentSalesContractRO> SalesContractROs { get; set; }
    }
}
