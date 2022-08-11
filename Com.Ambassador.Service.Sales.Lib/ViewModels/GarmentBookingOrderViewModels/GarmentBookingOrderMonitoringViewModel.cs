using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels
{
    public class GarmentBookingOrderMonitoringViewModel : BaseViewModel
    {
        public string BookingOrderNo { get; set; }
        public DateTimeOffset? BookingOrderDate { get; set; }
        public string BuyerName { get; set; }
        public string SectionName { get; set; }
        public double? OrderQuantity { get; set; }
        public DateTimeOffset? DeliveryDate { get; set; }
        public string ComodityName { get; set; }
        public double? ConfirmQuantity { get; set; }
        public DateTimeOffset? DeliveryDateItems { get; set; }
        public DateTimeOffset? ConfirmDate { get; set; }
        public string Remark { get; set; }
        public string StatusConfirm { get; set; }
        public string StatusBooking { get; set; }
        public string OrderLeft { get; set; }
        public string DateDiff { get; set; }
        public int row_count { get; set; }
        public double NotConfirmedQuantity { get; set; }
        public string SurplusQuantity { get; set; }
    }
}
