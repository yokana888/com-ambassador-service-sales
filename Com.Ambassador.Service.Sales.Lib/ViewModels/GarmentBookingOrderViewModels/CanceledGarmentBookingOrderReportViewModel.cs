using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels
{
    public class CanceledGarmentBookingOrderReportViewModel : BaseViewModel
    {
        public string BookingOrderNo { get; set; }
        public DateTimeOffset? BookingOrderDate { get; set; }
        public DateTimeOffset? DeliveryDate { get; set; }
        public string BuyerName { get; set; }
        public double? OrderQuantity { get; set; }
        public double? CanceledQuantity { get; set; }
        public DateTimeOffset? CanceledDate { get; set; }
        public DateTimeOffset? ExpiredBookingDate { get; set; }
        public double? ExpiredBookingQuantity { get; set; }

        public string ComodityName { get; set; }
        public double? ConfirmQuantity { get; set; }
        public DateTimeOffset? DeliveryDateItem { get; set; }
        public DateTimeOffset? ConfirmDate { get; set; }
        public string Remark { get; set; }
        public bool? IsCanceled { get; set; }
        public DateTimeOffset? CanceledDateItem { get; set; }
        public long? BookingOrderItemId { get; set; }

        public double? TotalBeginningQuantity { get; set; }

        public string CancelStatus { get; set; }
        public int row_count { get; set; }
    }
}
