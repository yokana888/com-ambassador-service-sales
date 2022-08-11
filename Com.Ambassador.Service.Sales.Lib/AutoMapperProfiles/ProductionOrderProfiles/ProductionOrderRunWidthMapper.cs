using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.ViewModels.ProductionOrder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.ProductionOrderProfiles
{
    public class ProductionOrderRunWidthMapper : Profile
    {
        public ProductionOrderRunWidthMapper()
        {
            CreateMap<ProductionOrder_RunWidthModel, ProductionOrder_RunWidthViewModel>()
            .ReverseMap();
        }
    }
}
