using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationDataUtils : BaseDataUtil<FinishingPrintingCostCalculationFacade, FinishingPrintingCostCalculationModel>
    {
        public FinishingPrintingCostCalculationDataUtils(FinishingPrintingCostCalculationFacade facade) : base(facade)
        {
        }

        public override Task<FinishingPrintingCostCalculationModel> GetNewData()
        {
            return Task.FromResult(new FinishingPrintingCostCalculationModel()
            {
                ActualPrice = 1,
                Code = "code",
                CurrencyRate = 1,
                Date = DateTimeOffset.UtcNow,
                GreigeId = 1,
                GreigeName = "name",
                InstructionId = 1,
                InstructionName ="name",
                PreparationFabricWeight = 1,
                ProductionOrderNo = "np",
                ProductionUnitValue = 1,
                Remark = "r",
                RFDFabricWeight = 1,
                Color = "color",
                ConfirmPrice = 1,
                GreigePrice = 1,
                FreightCost = 1,
                MaterialId = 1,
                MaterialName = "name",
                PreSalesContractId = 1,
                PreSalesContractNo = "no",
                SalesFirstName = "name",
                SalesId = 1,
                SalesLastName = "last",
                SalesUserName = "user",
                UnitId = 1,
                UnitName = "name",
                UomId = 1,
                UomUnit = "unit",
                ImageFile = "file",
                ImagePath = "earere",
                Machines = new List<FinishingPrintingCostCalculationMachineModel>()
                {
                    new FinishingPrintingCostCalculationMachineModel()
                    {
                        Chemicals = new List<FinishingPrintingCostCalculationChemicalModel>()
                        {
                            new FinishingPrintingCostCalculationChemicalModel()
                            {
                                ChemicalId = 1,
                                ChemicalQuantity = 1,
                                ChemicalPrice = 1,
                                ChemicalCurrency = "rp",
                                ChemicalName = "name",
                                ChemicalUom = "m"
                                
                            }
                        },
                        MachineElectric = 1,
                        MachineLPG = 1,
                        MachineName = "name",
                        MachineProcess = "process",
                        MachineSolar = 1,
                        MachineSteam = 1,
                        MachineWater = 1,
                        StepId = 1,
                        StepProcess = "prec",
                        StepProcessArea = "area",
                        Depretiation = 1,
                        MachineId = 1
                    }
                }
            });
        }
    }
}
