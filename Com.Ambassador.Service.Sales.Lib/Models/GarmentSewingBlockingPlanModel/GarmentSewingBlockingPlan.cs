using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel
{
    public class GarmentSewingBlockingPlan : BaseModel
    {
        public long BookingOrderId { get; set; }
        [MaxLength(255)]
        public string BookingOrderNo { get; set; }
        public DateTimeOffset BookingOrderDate { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public long BuyerId { get; set; }
        [MaxLength(255)]
        public string BuyerCode { get; set; }
        [MaxLength(255)]
        public string BuyerName { get; set; }
        public double OrderQuantity { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public string BookingItems { get; set; }

        public virtual ICollection<GarmentSewingBlockingPlanItem> Items { get; set; }
    }
}
