using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.GarmentSalesContractModel
{
    public class GarmentSalesContractRO : BaseModel
    {
        public int CostCalculationId { get; set; }
        [MaxLength(255)]
        public string RONumber { get; set; }
        [MaxLength(1000)]
        public string Article { get; set; }
        public int ComodityId { get; set; }
        [MaxLength(500)]
        public string ComodityName { get; set; }
        [MaxLength(500)]
        public string ComodityCode { get; set; }
        public double Quantity { get; set; }
        public string UomId { get; set; }
        public string UomUnit { get; set; }
        [MaxLength(3000)]
        public string Description { get; set; } //jenis product

        [MaxLength(3000)]
        public string Material { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public virtual long SalesContractId { get; set; }
        [ForeignKey("SalesContractId")]
        public virtual GarmentSalesContract GarmentSalesContract { get; set; }
        public virtual ICollection<GarmentSalesContractItem> Items { get; set; }
    }
}
