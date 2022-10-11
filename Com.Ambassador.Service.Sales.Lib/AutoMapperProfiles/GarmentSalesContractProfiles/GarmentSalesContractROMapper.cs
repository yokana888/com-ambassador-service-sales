using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentSalesContractModel;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentSalesContractViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.GarmentSalesContractProfiles
{
    public class GarmentSalesContractROMapper : Profile
    {
        public GarmentSalesContractROMapper()
        {
            CreateMap<GarmentSalesContractRO, GarmentSalesContractROViewModel>()
            .ForPath(d => d.Uom.Id, opt => opt.MapFrom(s => s.UomId))
            .ForPath(d => d.Uom.Unit, opt => opt.MapFrom(s => s.UomUnit))
            .ReverseMap();
        }
    }
}
