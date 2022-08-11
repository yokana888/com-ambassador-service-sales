using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.FinisihingPrintingSalesContract
{
    public class FinisihingPrintingSalesContractDataUtil : BaseDataUtil<FinishingPrintingSalesContractFacade, FinishingPrintingSalesContractModel>
    {
        public FinisihingPrintingSalesContractDataUtil(FinishingPrintingSalesContractFacade facade) : base(facade)
        {
        }

        public override Task<FinishingPrintingSalesContractModel> GetNewData()
        {
            return Task.FromResult(new FinishingPrintingSalesContractModel() {
                AccountBankAccountName = "a",
                AccountBankCode = "a",
                AccountBankCurrencyCode = "a",
                AccountBankCurrencyID = 1,
                AccountBankCurrencyRate = 1,
                AccountBankCurrencySymbol ="a",
                AccountBankID = 1,
                AccountBankName = "name",
                AccountBankNumber = "nm",
                Details = new List<FinishingPrintingSalesContractDetailModel>()
                {
                    new FinishingPrintingSalesContractDetailModel()
                    {
                        Color = "ColorType",
                        UseIncomeTax = true,
                        Price = 1,
                        ScreenCost = 1
                    }
                },
                BuyerType = "type"
            });

        }
    }
}
