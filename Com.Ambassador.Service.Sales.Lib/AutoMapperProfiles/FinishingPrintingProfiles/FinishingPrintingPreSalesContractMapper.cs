using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.FinishingPrintingProfiles
{
    public class FinishingPrintingPreSalesContractMapper : Profile
    {
        public FinishingPrintingPreSalesContractMapper()
        {
            CreateMap<FinishingPrintingPreSalesContractModel, FinishingPrintingPreSalesContractViewModel>()

                .ForPath(d => d.Buyer.Id, opt => opt.MapFrom(s => s.BuyerId))
                .ForPath(d => d.Buyer.Code, opt => opt.MapFrom(s => s.BuyerCode))
                .ForPath(d => d.Buyer.Name, opt => opt.MapFrom(s => s.BuyerName))
                .ForPath(d => d.Buyer.Type, opt => opt.MapFrom(s => s.BuyerType))

                .ForPath(d => d.Unit.Id, opt => opt.MapFrom(s => s.UnitId))
                .ForPath(d => d.Unit.Code, opt => opt.MapFrom(s => s.UnitCode))
                .ForPath(d => d.Unit.Name, opt => opt.MapFrom(s => s.UnitName))

                .ForPath(d => d.ProcessType.Id, opt => opt.MapFrom(s => s.ProcessTypeId))
                .ForPath(d => d.ProcessType.Code, opt => opt.MapFrom(s => s.ProcessTypeCode))
                .ForPath(d => d.ProcessType.Name, opt => opt.MapFrom(s => s.ProcessTypeName))

                .ForPath(d => d.ProcessType.OrderType.Id, opt => opt.MapFrom(s => s.OrderTypeId))
                .ForPath(d => d.ProcessType.OrderType.Code, opt => opt.MapFrom(s => s.OrderTypeCode))
                .ForPath(d => d.ProcessType.OrderType.Name, opt => opt.MapFrom(s => s.OrderTypeName))

                .ReverseMap();
        }
    }
}
