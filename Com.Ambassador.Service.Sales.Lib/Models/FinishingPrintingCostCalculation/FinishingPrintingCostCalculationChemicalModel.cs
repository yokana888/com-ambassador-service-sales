using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationChemicalModel : BaseModel
    {
        public long CostCalculationId { get; set; }
        
        public long CostCalculationMachineId { get; set; }
        
        [ForeignKey("CostCalculationMachineId")]
        public virtual FinishingPrintingCostCalculationMachineModel FinishingPrintingCostCalculationMachine { get; set; }
        
        public long ChemicalId { get; set; }
        [MaxLength(1024)]
        public string ChemicalName { get; set; }
        public double ChemicalPrice { get; set; }
        [MaxLength(64)]
        public string ChemicalUom { get; set; }
        [MaxLength(64)]
        public string ChemicalCurrency { get; set; }

        public int ChemicalQuantity { get; set; }
    }
}
