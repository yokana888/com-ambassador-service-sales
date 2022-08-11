using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel
{
    public class MachineViewModel : BaseViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Process { get; set; }
        public decimal Electric { get; set; }
        public decimal LPG { get; set; }
        public decimal Solar { get; set; }
        public decimal Steam { get; set; }
        public decimal Water { get; set; }
    }
}
