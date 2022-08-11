using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.GarmentBookingOrderProfiles
{
    public class GarmentBookingOrderMapper : Profile
    {
        public GarmentBookingOrderMapper()
        {
            CreateMap<GarmentBookingOrder, GarmentBookingOrderViewModel>()
                .ForPath(d => d.BuyerId, opt => opt.MapFrom(s => s.BuyerId))
                .ForPath(d => d.BuyerCode, opt => opt.MapFrom(s => s.BuyerCode))
                .ForPath(d => d.BuyerName, opt => opt.MapFrom(s => s.BuyerName))

                .ForPath(d => d.SectionId, opt => opt.MapFrom(s => s.SectionId))
                .ForPath(d => d.SectionCode, opt => opt.MapFrom(s => s.SectionCode))
                .ForPath(d => d.SectionName, opt => opt.MapFrom(s => s.SectionName))

                .ReverseMap();
        }
    }
}
