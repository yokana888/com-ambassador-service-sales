using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MonitoringViewModels
{
    public class WeeklyWorkingScheduleMonitoringViewModel
    {
        public string bookingOrderNo { get; set; }
        public DateTimeOffset bookingOrderDate { get; set; }
        public string buyer { get; set; }
        public double orderQuantity { get; set; }
        public double confirmQty { get; set; }
        public DateTimeOffset deliveryDate { get; set; }
        public string confirmComodity { get; set; }
        public string confirmQuantity { get; set; }
        public string confirmDeliveryDate { get; set; }
        public string workingComodity { get; set; }
        public double smv { get; set; }
        public string unit { get; set; }
        public int year { get; set; }
        public string week { get; set; }
        public double quantity { get; set; }
        public string remark { get; set; }
        public DateTimeOffset workingDeliveryDate { get; set; }
        public string status { get; set; }
        public bool isConfirmed { get; set; }
        public List<booking> booking { get; set; }
    }

    public class booking
    {
        public string confirmComodity { get; set; }
        public double confirmQuantity { get; set; }
        public string confirmDeliveryDate { get; set; }
    }
}
