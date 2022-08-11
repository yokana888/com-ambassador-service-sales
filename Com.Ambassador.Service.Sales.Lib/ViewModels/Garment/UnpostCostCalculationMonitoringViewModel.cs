using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.Garment
{
    public class MonitoringUnpostCostCalculationViewModel
    {
        public string Section { get; set; }
        public string RONo { get; set; }
        public string Article { get; set; }
        public string PreSCNo { get; set; }
        public string Unit { get; set; }
        public double Quantity { get; set; }
        public List<MonitoringUnpostCostCalculationReasonsViewModel> UnpostReasons { get; set; }
    }

    public class MonitoringUnpostCostCalculationReasonsViewModel
    {
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public string Creator { get; set; }
    }
}
