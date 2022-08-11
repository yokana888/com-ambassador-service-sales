using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentSewingBlockingPlanViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.GarmentSewingBlockingPlanProfiles
{
    public class GarmentSewingBlockingPlanMapper : Profile
    {
        public GarmentSewingBlockingPlanMapper()
        {
            CreateMap<GarmentSewingBlockingPlan, GarmentSewingBlockingPlanViewModel>()
                .ForPath(d => d.Buyer.Id, opt => opt.MapFrom(s => s.BuyerId))
                .ForPath(d => d.Buyer.Code, opt => opt.MapFrom(s => s.BuyerCode))
                .ForPath(d => d.Buyer.Name, opt => opt.MapFrom(s => s.BuyerName))
                .ReverseMap();

            CreateMap<GarmentSewingBlockingPlanItem, GarmentSewingBlockingPlanItemViewModel>()
                .ForPath(d => d.Unit.Id, opt => opt.MapFrom(s => s.UnitId))
                .ForPath(d => d.Unit.Code, opt => opt.MapFrom(s => s.UnitCode))
                .ForPath(d => d.Unit.Name, opt => opt.MapFrom(s => s.UnitName))
                .ForPath(d => d.Comodity.Id, opt => opt.MapFrom(s => s.ComodityId))
                .ForPath(d => d.Comodity.Code, opt => opt.MapFrom(s => s.ComodityCode))
                .ForPath(d => d.Comodity.Name, opt => opt.MapFrom(s => s.ComodityName))
                .ReverseMap();
        }
    }
}
