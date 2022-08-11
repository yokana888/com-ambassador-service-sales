using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationMachineViewModel : BaseViewModel
    {
        public StepViewModel Step { get; set; }
        public long CostCalculationId { get; set; }
        public MachineViewModel Machine { get; set; }
        public decimal Depretiation { get; set; }
        public List<FinishingPrintingCostCalculationChemicalViewModel> Chemicals { get; set; }
        public int Index { get; set; }
        
    }
}
