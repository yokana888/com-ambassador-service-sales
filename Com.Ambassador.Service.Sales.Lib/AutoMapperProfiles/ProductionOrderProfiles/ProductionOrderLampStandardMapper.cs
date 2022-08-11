using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.ViewModels.ProductionOrder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.ProductionOrderProfiles
{
    public class ProductionOrderLampStandardMapper : Profile
    {
        public ProductionOrderLampStandardMapper()
        {
            CreateMap<ProductionOrder_LampStandardModel, ProductionOrder_LampStandardViewModel>()
            .ForPath(d => d.LampStandardId, opt => opt.MapFrom(s => s.LampStandardId))
            .ForPath(d => d.Name, opt => opt.MapFrom(s => s.Name))
            .ForPath(d => d.Code, opt => opt.MapFrom(s => s.Code))
            .ForPath(d => d.Description, opt => opt.MapFrom(s => s.Description))

            .ReverseMap();

        }
    }
}
