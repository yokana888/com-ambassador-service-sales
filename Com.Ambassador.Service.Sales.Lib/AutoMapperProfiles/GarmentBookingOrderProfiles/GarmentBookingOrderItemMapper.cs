using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.GarmentBookingOrderProfiles
{
    public class GarmentBookingOrderItemMapper : Profile
    {
        public GarmentBookingOrderItemMapper()
        {
            CreateMap<GarmentBookingOrderItem, GarmentBookingOrderItemViewModel>()
            .ReverseMap();
        }
    }
}
