using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationMachineModel : BaseModel
    {
        public long CostCalculationId { get; set; }
        
        [ForeignKey("CostCalculationId")]
        public virtual FinishingPrintingCostCalculationModel FinishingPrintingCostCalculation { get; set; }


        public int StepId { get; set; }
        [MaxLength(1024)]
        public string StepProcess { get; set; }
        [MaxLength(1024)]
        public string StepProcessArea { get; set; }
        public int Index { get; set; }
        
        public long MachineId { get; set; }
        [MaxLength(1024)]
        public string MachineName { get; set; }
        [MaxLength(1024)]
        public string MachineProcess { get; set; }
        public decimal MachineElectric { get; set; }
        public decimal MachineLPG { get; set; }
        public decimal MachineSolar { get; set; }
        public decimal MachineSteam { get; set; }
        public decimal MachineWater { get; set; }


        public ICollection<FinishingPrintingCostCalculationChemicalModel> Chemicals { get; set; }
       
        public decimal Depretiation { get; set; }
    }
}
