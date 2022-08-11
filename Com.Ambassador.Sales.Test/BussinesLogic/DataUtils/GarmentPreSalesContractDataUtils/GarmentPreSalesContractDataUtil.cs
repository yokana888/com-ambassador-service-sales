using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentPreSalesContractFacades;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentPreSalesContractModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentPreSalesContractDataUtils
{
    public class GarmentPreSalesContractDataUtil : BaseDataUtil<GarmentPreSalesContractFacade, GarmentPreSalesContract>
    {
        public GarmentPreSalesContractDataUtil(GarmentPreSalesContractFacade facade) : base(facade)
        {
        }

        public override Task<GarmentPreSalesContract> GetNewData()
        {
            return Task.FromResult(new GarmentPreSalesContract()
            {
                SCNo = "",
                SCDate = new DateTimeOffset(),
                SCType = "SAMPLE",
                SectionId = 1,
                SectionCode = "Test",
                BuyerAgentId = 1,
                BuyerAgentCode = "Test",
                BuyerAgentName = "Test",
                BuyerBrandId = 1,
                BuyerBrandCode = "Test",
                BuyerBrandName = "Test",
                OrderQuantity = 1,
                Remark = "Test",
                IsCC = false,
                IsPR = false,
                IsPosted = false,
            });
        }

    }
}