using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationChemicalViewModel : BaseViewModel
    {
        public long CostCalculationId { get; set; }
        public long CostCalculationMachineId { get; set; }
        public ProductViewModel Chemical { get; set; }
        public int ChemicalQuantity { get; set; }
        
    }
}
