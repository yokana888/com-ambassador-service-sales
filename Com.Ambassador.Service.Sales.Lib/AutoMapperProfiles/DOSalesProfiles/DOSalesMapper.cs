using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.DOSales;
using Com.Ambassador.Service.Sales.Lib.ViewModels.DOSales;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.DOSalesProfiles
{
    public class DOSalesMapper : Profile
    {
        public DOSalesMapper()
        {
            CreateMap<DOSalesModel, DOSalesViewModel>()

                .ForPath(d => d.SalesContract.Id, opt => opt.MapFrom(s => s.SalesContractId))
                .ForPath(d => d.SalesContract.SalesContractNo, opt => opt.MapFrom(s => s.SalesContractNo))
                .ForPath(d => d.SalesContract.MaterialWidth, opt => opt.MapFrom(s => s.MaterialWidth))
                .ForPath(d => d.SalesContract.OrderQuantity, opt => opt.MapFrom(s => s.OrderQuantity))
                .ForPath(d => d.SalesContract.PieceLength, opt => opt.MapFrom(s => s.PieceLength))

                .ForPath(d => d.SalesContract.Material.Id, opt => opt.MapFrom(s => s.MaterialId))
                .ForPath(d => d.SalesContract.Material.Code, opt => opt.MapFrom(s => s.MaterialCode))
                .ForPath(d => d.SalesContract.Material.Name, opt => opt.MapFrom(s => s.MaterialName))
                .ForPath(d => d.SalesContract.Material.Tags, opt => opt.MapFrom(s => s.MaterialTags))

                .ForPath(d => d.SalesContract.MaterialConstruction.Id, opt => opt.MapFrom(s => s.MaterialConstructionId))
                .ForPath(d => d.SalesContract.MaterialConstruction.Code, opt => opt.MapFrom(s => s.MaterialConstructionCode))
                .ForPath(d => d.SalesContract.MaterialConstruction.Name, opt => opt.MapFrom(s => s.MaterialConstructionName))
                .ForPath(d => d.SalesContract.MaterialConstruction.Remark, opt => opt.MapFrom(s => s.MaterialConstructionRemark))

                .ForPath(d => d.SalesContract.Commodity.Id, opt => opt.MapFrom(s => s.CommodityId))
                .ForPath(d => d.SalesContract.Commodity.Code, opt => opt.MapFrom(s => s.CommodityCode))
                .ForPath(d => d.SalesContract.Commodity.Name, opt => opt.MapFrom(s => s.CommodityName))
                .ForPath(d => d.SalesContract.CommodityDescription, opt => opt.MapFrom(s => s.CommodityDescription))

                .ForPath(d => d.SalesContract.Buyer.Id, opt => opt.MapFrom(s => s.BuyerId))
                .ForPath(d => d.SalesContract.Buyer.Code, opt => opt.MapFrom(s => s.BuyerCode))
                .ForPath(d => d.SalesContract.Buyer.Name, opt => opt.MapFrom(s => s.BuyerName))
                .ForPath(d => d.SalesContract.Buyer.Type, opt => opt.MapFrom(s => s.BuyerType))
                .ForPath(d => d.SalesContract.Buyer.Address, opt => opt.MapFrom(s => s.BuyerAddress))

                .ForPath(d => d.Storage._id, opt => opt.MapFrom(s => s.StorageId))
                .ForPath(d => d.Storage.name, opt => opt.MapFrom(s => s.StorageName))
                .ForPath(d => d.Storage.code, opt => opt.MapFrom(s => s.StorageCode))
                .ForPath(d => d.Storage.unit.name, opt => opt.MapFrom(s => s.StorageUnit))

                .ReverseMap();
        }
    }
}
