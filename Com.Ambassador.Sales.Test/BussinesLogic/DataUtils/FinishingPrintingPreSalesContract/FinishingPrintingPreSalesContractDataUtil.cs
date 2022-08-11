using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.FinishingPrintingPreSalesContract
{
    public class FinishingPrintingPreSalesContractDataUtil : BaseDataUtil<FinishingPrintingPreSalesContractFacade, FinishingPrintingPreSalesContractModel>
    {
        public FinishingPrintingPreSalesContractDataUtil(FinishingPrintingPreSalesContractFacade facade) : base(facade)
        {
        }

        public override Task<FinishingPrintingPreSalesContractModel> GetNewData()
        {
            return Task.FromResult(new FinishingPrintingPreSalesContractModel()
            {
                BuyerCode = "code",
                BuyerId = 1,
                BuyerName  = "name",
                Date = DateTimeOffset.UtcNow,
                No = "no",
                OrderQuantity = 1,
                UnitName = "name",
                ProcessTypeName = "name",
                ProcessTypeId = 1,
                UnitId = 1,
                UnitCode = "cpe",
                ProcessTypeCode = "code",
                Remark = "remark",
                Type = "type",
                OrderTypeName = "name",
                OrderTypeCode = "cpde",
                OrderTypeId = 1
            });
        }
    }
}
