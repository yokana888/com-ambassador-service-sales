using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MonitoringViewModels
{
    public class SewingBlockingPlanReportViewModel
    {
        public string buyer { get; set; }
        public string unit { get; set; }
        public short year { get; set; }
        public byte weekSewingBlocking { get; set; }
        public double SMVSewing { get; set; }
        public double UsedEH { get; set; }
        public bool isConfirmed { get; set; }
        public double bookingQty { get; set; }
        public double bookingOrderQty { get; set; }
        public byte weekBookingOrder { get; set; }
        public double bookingOrderConfirmedQty { get; set; }
        public long bookingId { get; set; }
        public DateTimeOffset bookingDate { get; set; }
        public List<BOItemViewModel> bookingOrderItems { get; set; }
        public List<SewingBlockingPlanReportItemViewModel> items { get; set; }
        //public List<double> efficiency { get; set; }
        //public List<double> workingHours { get; set; }
        //public List<double> AHTotal { get; set; }
        //public List<int> EHTotal { get; set; }
        //public List<int> remainingEH { get; set; }
        //public List<int> usedTotal { get; set; }
        //public List<byte> weekNumber { get; set; }
        //public List<DateTimeOffset> weekEndDate { get; set; }
        //public List<int> head { get; set; }

    }

    public class BOItemViewModel
    {
        public byte weekConfirm { get; set; }
        public double ConfirmQuantity { get; set; }
        public DateTimeOffset ConfirmDate { get; set; }
    }

    public class SewingBlockingPlanReportItemViewModel
    {
        public double efficiency { get; set; }
        public double workingHours { get; set; }
        public double AHTotal { get; set; }
        public int EHTotal { get; set; }
        public int remainingEH { get; set; }
        public double WHBooking { get; set; }
        public double WHConfirm { get; set; }
        public int usedTotal { get; set; }
        public byte weekNumber { get; set; }
        public DateTimeOffset weekEndDate { get; set; }
        public int head { get; set; }
    }
}
