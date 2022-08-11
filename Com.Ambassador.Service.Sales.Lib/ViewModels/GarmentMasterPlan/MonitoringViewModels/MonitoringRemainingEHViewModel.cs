using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MonitoringViewModels
{
    public class MonitoringRemainingEHViewModel
    {
        public string Unit { get; set; }

        public List<MonitoringRemainingEHItemViewModel> Items { get; set; }
    }

    public class MonitoringRemainingEHItemViewModel
    {
        public byte WeekNumber { get; set; }

        public int Operator { get; set; }
        public int RemainingEH { get; set; }
    }
}
