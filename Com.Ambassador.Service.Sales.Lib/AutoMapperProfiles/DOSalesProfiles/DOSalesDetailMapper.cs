using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.DOSales;
using Com.Ambassador.Service.Sales.Lib.ViewModels.DOSales;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.DOSalesProfiles
{
    public class DOSalesDetailMapper : Profile
    {
        public DOSalesDetailMapper()
        {
            CreateMap<DOSalesDetailModel, DOSalesDetailViewModel>()

                .ForPath(d => d.ProductionOrder.Id, opt => opt.MapFrom(s => s.ProductionOrderId))
                .ForPath(d => d.ProductionOrder.OrderNo, opt => opt.MapFrom(s => s.ProductionOrderNo))
                .ForPath(d => d.ProductionOrder.MaterialWidth, opt => opt.MapFrom(s => s.MaterialWidth))

                .ForPath(d => d.ProductionOrder.Material.Id, opt => opt.MapFrom(s => s.MaterialId))
                .ForPath(d => d.ProductionOrder.Material.Code, opt => opt.MapFrom(s => s.MaterialCode))
                .ForPath(d => d.ProductionOrder.Material.Name, opt => opt.MapFrom(s => s.MaterialName))
                .ForPath(d => d.ProductionOrder.Material.Price, opt => opt.MapFrom(s => s.MaterialPrice))
                .ForPath(d => d.ProductionOrder.Material.Tags, opt => opt.MapFrom(s => s.MaterialTags))

                .ForPath(d => d.ProductionOrder.MaterialConstruction.Id, opt => opt.MapFrom(s => s.MaterialConstructionId))
                .ForPath(d => d.ProductionOrder.MaterialConstruction.Name, opt => opt.MapFrom(s => s.MaterialConstructionName))
                .ForPath(d => d.ProductionOrder.MaterialConstruction.Code, opt => opt.MapFrom(s => s.MaterialConstructionCode))
                .ForPath(d => d.ProductionOrder.MaterialConstruction.Remark, opt => opt.MapFrom(s => s.MaterialConstructionRemark))

                .ReverseMap();
        }
    }
}
