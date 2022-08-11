using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel
{
    public class InstructionViewModel
    {
        public string Code { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<StepViewModel> Steps { get; set; }
    }

    public class StepViewModel
    {
        public int Id { get; set; }
        public string Process { get; set; }
        public string ProcessArea { get; set; }
    }
}
