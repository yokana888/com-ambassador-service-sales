using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.DOStock;
using Com.Ambassador.Service.Sales.Lib.Models.DOSales;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.DOStock
{
    public class DOStockDataUtil : BaseDataUtil<DOStockFacade, DOSalesModel>
    {
        public DOStockDataUtil(DOStockFacade facade) : base(facade)
        {
        }

        public override Task<DOSalesModel> GetNewData()
        {
            return Task.FromResult(new DOSalesModel()
            {
                Code = "code",
                AutoIncreament = 1,
                DOSalesNo = "DOSalesNo",
                DOSalesType = "DOSalesType",
                Status = "Status",
                Accepted = false,
                Declined = false,
                Type = "Type",
                Date = DateTimeOffset.UtcNow,
                SalesContractId = 1,
                SalesContractNo = "SalesContractNo",
                MaterialWidth = "MaterialWidth",
                PieceLength = "PieceLength",
                OrderQuantity = 1,
                MaterialId = 1,
                MaterialCode = "MaterialCode",
                MaterialName = "MaterialName",
                MaterialPrice = 100,
                MaterialTags = "MaterialTags",
                MaterialConstructionId = 1,
                MaterialConstructionCode = "MaterialConstructionCode",
                MaterialConstructionName = "MaterialConstructionName",
                MaterialConstructionRemark = "MaterialConstructionRemark",
                ColorRequest = "ColorRequest",
                ColorTemplate = "ColorTemplate",
                CommodityId = 1,
                CommodityCode = "CommodityCode",
                CommodityName = "CommodityName",
                CommodityDescription = "CommodityDescription",
                BuyerId = 1,
                BuyerCode = "BuyerCode",
                BuyerName = "BuyerName",
                BuyerType = "BuyerType",
                BuyerAddress = "BuyerAddress",
                DestinationBuyerName = "DestinationBuyerName",
                DestinationBuyerAddress = "DestinationBuyerAddress",
                SalesName = "SalesName",
                HeadOfStorage = "HeadOfStorage",
                PackingUom = "PCS",
                LengthUom = "MTR",
                WeightUom = "KG",
                Disp = 1,
                Op = 1,
                Sc = 1,
                DoneBy = "DoneBy",
                FillEachBale = 1,
                DOSalesCategory = "STOCK",
                StorageId = 1,
                StorageCode = "StorageCode",
                StorageUnit = "StorageUnit",
                StorageName = "StorageName",
                Construction = "construction",
                Remark = "reamarl",
                DOSalesDetailItems = new List<DOSalesDetailModel>()
                {
                    new DOSalesDetailModel()
                    {
                        ProductionOrderId = 1,
                        ProductionOrderNo = "OrderNo",
                        MaterialId = 1,
                        MaterialCode = "MaterialCode",
                        MaterialName = "MaterialName",
                        MaterialPrice = 1,
                        MaterialTags = "MaterialTags",
                        MaterialConstructionId = 1,
                        MaterialConstructionName = "MaterialConstructionName",
                        MaterialConstructionCode = "MaterialConstructionCode",
                        MaterialConstructionRemark = "MaterialConstructionRemark",
                        MaterialWidth = "MaterialWidth",
                        ConstructionName = "ConstructionName",
                        ColorRequest = "ColorRequest",
                        ColorTemplate = "ColorTemplate",
                        UnitOrCode = "UnitOrCode",
                        Packing = 1,
                        Length = 1,
                        Weight = 1,
                        ConvertionValue = 10,
                        NoSOP="NoSOP",
                        ThreadNumber="ThreadNumber",
                        AvalType = "ty",
                        Grade="Grade",
                    }
                }
            });
        }
    }
}
