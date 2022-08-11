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
        public int CostCalculationId { get; set; }
        [MaxLength(255)]
        public string RONumber { get; set; }
        public int BuyerBrandId { get; set; }
        public string BuyerBrandCode { get; set; }
        public string BuyerBrandName { get; set; }
        public int ComodityId { get; set; }
        [MaxLength(500)]
        public string ComodityName { get; set; }
        [MaxLength(500)]
        public string ComodityCode { get; set; }
        [MaxLength(1000)]
        public string Packing { get; set; }
        [MaxLength(1000)]
        public string Article { get; set; }
        public double Quantity { get; set; }
        public string UomId { get; set; }
        public string UomUnit { get; set; }
        [MaxLength(3000)]
        public string Description { get; set; }
        [MaxLength(3000)]
        public string Material { get; set; }
        [MaxLength(3000)]
        public string DocPresented { get; set; }
        [MaxLength(3000)]
        public string FOB { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
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
        public virtual ICollection<GarmentSalesContractItem> Items { get; set; }
    }
}
