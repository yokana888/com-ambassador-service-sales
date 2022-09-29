using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.Garment.GarmentMerchandiser;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentSalesContractFacades;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentSalesContractModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentSalesContractDataUtils
{
    public class GarmentSalesContractDataUtil : BaseDataUtil<GarmentSalesContractFacade, GarmentSalesContract>
    {
        private readonly CostCalculationGarmentDataUtil costCalculationGarmentDataUtil;

        public GarmentSalesContractDataUtil(GarmentSalesContractFacade facade, CostCalculationGarmentDataUtil costCalculationGarmentDataUtil) : base(facade)
        {
            this.costCalculationGarmentDataUtil = costCalculationGarmentDataUtil;
        }

        public override async Task<GarmentSalesContract> GetNewData()
        {
            var costCalculationGarmentData = await costCalculationGarmentDataUtil.GetTestData();

            var data = await base.GetNewData();

            data.SalesContractNo = "SC/AG/001";
            data.CostCalculationId = (int)costCalculationGarmentData.Id;
            data.RONumber = costCalculationGarmentData.RO_Number;
            data.DeliveryDate =  costCalculationGarmentData.DeliveryDate;
            data.Article = costCalculationGarmentData.Article;
            data.BuyerBrandId = costCalculationGarmentData.BuyerBrandId;
            data.BuyerBrandCode = costCalculationGarmentData.BuyerBrandCode;
            data.BuyerBrandName = costCalculationGarmentData.BuyerBrandName;
            data.ComodityId = 1;
            data.ComodityCode = "XX";
            data.ComodityName = "Test Comodity";
            data.Packing = "Text Packing";
            data.Quantity = 1;
            data.UomId = "1";
            data.UomUnit = "Test UOM";
            data.Description = "Test Description";
            data.Material = "Test Material";
            data.DocPresented = "Test DocPresented";
            data.FOB = "Test";
            data.Price = 8.7;
            data.Amount = 8.7;
            data.Delivery = "Test";
            data.Country = "Test";
            data.NoHS = "Test No HS";
            data.IsMaterial = false;
            data.IsTrimming = false;
            data.IsEmbrodiary = false;
            data.IsPrinted = false;
            data.IsTTPayment = false;
            data.PaymentDetail = "Test Payment";
            data.AccountBankId = 1;
            data.AccountName = "Test Account Name";
            data.DocPrinted = false;

            data.Items = new List<GarmentSalesContractItem>()
            {
                new GarmentSalesContractItem
                {
                    Quantity = 1,
                    Price    = 8.7,
                    Description = "Test",
                }
            };
            return data;
        }
    }
}