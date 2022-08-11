using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MonitoringViewModels
{
    public class OverScheduleMonitoringViewModel
    {
        public string bookingCode { get; set; }
        public DateTimeOffset bookingDate { get; set; }
        public string buyer { get; set; }
        public double bookingOrderQty { get; set; }
        public double confirmQty { get; set; }
        public DateTimeOffset bookingDeliveryDate { get; set; }
        public string comodity { get; set; }
        public double smv { get; set; }
        public string unit { get; set; }
        public int year { get; set; }
        public byte weekNum { get; set; }
        public DateTimeOffset startDate { get; set; }
        public DateTimeOffset endDate { get; set; }
        public double quantity { get; set; }
        public string remark { get; set; }
        public DateTimeOffset deliveryDate { get; set; }
        public double usedEH { get; set; }
    }
}
