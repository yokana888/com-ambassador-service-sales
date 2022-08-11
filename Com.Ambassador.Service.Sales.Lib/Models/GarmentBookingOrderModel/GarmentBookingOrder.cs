using Com.Ambassador.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.GarmentBookingOrderModel
{
    public class GarmentBookingOrder : BaseModel
    {
        [MaxLength(50)]
        public string BookingOrderNo { get; set; }
        public DateTimeOffset BookingOrderDate { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public long BuyerId { get; set; }
        [MaxLength(25)]
        public string BuyerCode { get; set; }
        [MaxLength(100)]
        public string BuyerName { get; set; }
        public long SectionId { get; set; }
        [MaxLength(25)]
        public string SectionCode { get; set; }
        [MaxLength(100)]
        public string SectionName { get; set; }
        public double OrderQuantity { get; set; }
        public string Remark { get; set; }
        public bool IsBlockingPlan { get; set; }
        public bool IsCanceled { get; set; }
        public DateTimeOffset? CanceledDate { get; set; }
        public double CanceledQuantity { get; set; }
        public DateTimeOffset? ExpiredBookingDate { get; set; }
        public double ExpiredBookingQuantity { get; set; }
        public double ConfirmedQuantity { get; set; }
        public bool HadConfirmed { get; set; }
        public virtual ICollection<GarmentBookingOrderItem> Items { get; set; }

    }
}