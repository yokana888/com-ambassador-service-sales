using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.FinishingPrintingCostCalculation;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.FinishingPrintingPreSalesContract;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.FinisihingPrintingSalesContract
{
    public class ShinFinisihingPrintingSalesContractDataUtil : BaseDataUtil<ShinFinishingPrintingSalesContractFacade, FinishingPrintingSalesContractModel>
    {
        private readonly FinishingPrintingPreSalesContractFacade finishingPrintingPreSalesContractFacade;
        public ShinFinisihingPrintingSalesContractDataUtil(ShinFinishingPrintingSalesContractFacade facade, FinishingPrintingPreSalesContractFacade preSalesContractFacade) : base(facade)
        {
            finishingPrintingPreSalesContractFacade = preSalesContractFacade;
        }

        public override async Task<FinishingPrintingSalesContractModel> GetNewData()
        {
            FinishingPrintingPreSalesContractDataUtil ccDU = new FinishingPrintingPreSalesContractDataUtil(finishingPrintingPreSalesContractFacade);
            var ccData = await ccDU.GetTestData();

            return new FinishingPrintingSalesContractModel()
            {
                AgentCode = "c",
                AgentID = 1,
                AgentName = "name",
                Amount = 1,
                //CostCalculationId = ccData.Id,
                PreSalesContractId = ccData.Id,
                PreSalesContractNo = ccData.No,
                DesignMotiveID = 1,
                SalesContractNo = "np",
                UnitName = "np",
                BuyerName = "a00",
                //ProductionOrderNo = "no",
                AccountBankAccountName = "a",
                AccountBankCode = "a",
                AccountBankCurrencyCode = "a",
                AccountBankCurrencyID = 1,
                AccountBankCurrencyRate = 1,
                AccountBankCurrencySymbol = "a",
                AccountBankID = 1,
                AccountBankName = "name",
                AccountBankNumber = "nm",
                Details = new List<FinishingPrintingSalesContractDetailModel>()
                {
                    new FinishingPrintingSalesContractDetailModel()
                    {
                        Color = "c",
                        UseIncomeTax = true,
                        Price = 1,
                        ScreenCost = 1
                    }
                },
                BuyerType = "type"
            };

        }
    }
}
