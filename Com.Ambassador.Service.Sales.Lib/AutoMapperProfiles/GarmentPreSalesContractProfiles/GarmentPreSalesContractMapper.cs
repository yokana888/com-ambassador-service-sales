using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentPreSalesContractModel;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentPreSalesContractViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.GarmentPreSalesContractProfiles
{
    public class GarmentPreSalesContractMapper : Profile
    {
        public GarmentPreSalesContractMapper()
        {
            CreateMap<GarmentPreSalesContract, GarmentPreSalesContractViewModel>()
            .ForPath(d => d.BuyerAgentId, opt => opt.MapFrom(s => s.BuyerAgentId))
            .ForPath(d => d.BuyerAgentCode, opt => opt.MapFrom(s => s.BuyerAgentCode))
            .ForPath(d => d.BuyerAgentName, opt => opt.MapFrom(s => s.BuyerAgentName))
            .ForPath(d => d.BuyerBrandId, opt => opt.MapFrom(s => s.BuyerBrandId))
            .ForPath(d => d.BuyerBrandCode, opt => opt.MapFrom(s => s.BuyerBrandCode))
            .ForPath(d => d.BuyerBrandName, opt => opt.MapFrom(s => s.BuyerBrandName))
            .ReverseMap();
        }
    }
}