using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.Weaving;
using Com.Ambassador.Service.Sales.Lib.Models.Weaving;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.Weaving
{
    public class WeavingSalesContractDataUtil : BaseDataUtil<WeavingSalesContractFacade, WeavingSalesContractModel>
    {
        public WeavingSalesContractDataUtil(WeavingSalesContractFacade facade) : base(facade)
        {
        }

        public override Task<WeavingSalesContractModel> GetNewData()
        {
            return Task.FromResult(new WeavingSalesContractModel()
            {
                AccountBankCode = "code",
                AccountBankId = 1,
                AccountBankName = "name",
                AccountBankNumber = "123",
                AccountCurrencyCode = "idr",
                AccountCurrencyId = "1",
                AgentCode = "c",
                AgentId = 1,
                AgentName = "ma",
                AutoIncrementNumber = 1,
                BankName = "name",
                BuyerCode = "c",
                BuyerId = 1,
                BuyerName = "name",
                BuyerType = "te",
                Code = "c",
                Comission = "c",
                ComodityCode = "code",
                ComodityDescription = "re",
                ComodityId = 1,
                ComodityName = "naem",
                ComodityType = "tye",
                Condition = "s",
                DeliveredTo = "ds",
                DeliverySchedule = DateTimeOffset.UtcNow,
                DispositionNumber = "123",
                FromStock = true,
                IncomeTax = "1230",
                MaterialConstructionCode = "c",
                MaterialConstructionId = 1,
                MaterialConstructionName = "n",
                MaterialConstructionRemark = "r",
                MaterialWidth = "1",
                OrderQuantity = 1,
                Packing = "pac",
                PieceLength= "1",
                Price = 1,
                ProductCode = "c",
                ProductId = 1,
                ProductName  = "n",
                ProductPrice = 1,
                ProductTags = "ta",
                QualityCode = "c",
                QualityId = 1,
                QualityName = "n",
                Remark = "r",
                SalesContractNo = "ad12",
                ShipmentDescription = "de",
                ShippingQuantityTolerance = 1,
                TermOfPaymentCode = "c",
                TermOfPaymentId = 1,
                TermOfPaymentIsExport = true,
                TermOfPaymentName ="name",
                TermOfShipment = "sk",
                TransportFee = "123120",
                UomId = 1,
                UomUnit = "a0",
                YarnMaterialCode = "c",
                YarnMaterialId = 1,
                YarnMaterialName = "n",
                YarnMaterialRemark = "r"
            });
        }
    }
}
