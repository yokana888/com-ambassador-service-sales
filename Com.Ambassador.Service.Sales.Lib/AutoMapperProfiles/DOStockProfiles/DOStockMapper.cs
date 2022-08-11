using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.DOSales;
using Com.Ambassador.Service.Sales.Lib.ViewModels.DOStock;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.DOStockProfiles
{
    public class DOStockMapper : Profile
    {
        public DOStockMapper()
        {
            CreateMap<DOSalesModel, DOStockViewModel>()
                .ForPath(d => d.Buyer.Id, opt => opt.MapFrom(s => s.BuyerId))
                .ForPath(d => d.Buyer.Code, opt => opt.MapFrom(s => s.BuyerCode))
                .ForPath(d => d.Buyer.Name, opt => opt.MapFrom(s => s.BuyerName))
                .ForPath(d => d.Buyer.Type, opt => opt.MapFrom(s => s.BuyerType))
                .ForPath(d => d.Buyer.Address, opt => opt.MapFrom(s => s.BuyerAddress))
                .ForPath(d => d.DOStockNo, opt => opt.MapFrom(s => s.DOSalesNo))
                .ForPath(d => d.DOStockCategory, opt => opt.MapFrom(s => s.DOSalesCategory))
                .ForPath(d => d.DOStockType, opt => opt.MapFrom(s => s.DOSalesType))
                .ForPath(d => d.DOStockItems, opt => opt.MapFrom(s => s.DOSalesDetailItems))
                .ReverseMap();

            CreateMap<DOSalesDetailModel, DOStockItemViewModel>()
                .ForPath(d => d.ProductionOrder.Id, opt => opt.MapFrom(s => s.ProductionOrderId))
                .ForPath(d => d.ProductionOrder.OrderNo, opt => opt.MapFrom(s => s.ProductionOrderNo))
                .ForPath(d => d.ProductionOrder.Material.Id, opt => opt.MapFrom(s => s.MaterialId))
                .ForPath(d => d.ProductionOrder.Material.Code, opt => opt.MapFrom(s => s.MaterialCode))
                .ForPath(d => d.ProductionOrder.Material.Name, opt => opt.MapFrom(s => s.MaterialName))
                .ForPath(d => d.ProductionOrder.Material.Price, opt => opt.MapFrom(s => s.MaterialPrice))
                .ForPath(d => d.ProductionOrder.Material.Tags, opt => opt.MapFrom(s => s.MaterialTags))
                .ForPath(d => d.ProductionOrder.MaterialConstruction.Id, opt => opt.MapFrom(s => s.MaterialConstructionId))
                .ForPath(d => d.ProductionOrder.MaterialConstruction.Code, opt => opt.MapFrom(s => s.MaterialConstructionCode))
                .ForPath(d => d.ProductionOrder.MaterialConstruction.Name, opt => opt.MapFrom(s => s.MaterialConstructionName))
                .ForPath(d => d.ProductionOrder.MaterialConstruction.Remark, opt => opt.MapFrom(s => s.MaterialConstructionRemark))
                .ForPath(d => d.ProductionOrder.MaterialWidth, opt => opt.MapFrom(s => s.MaterialWidth))
                .ReverseMap();
        }
    }
}
