using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models;
using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles
{
    public class RateMapper : Profile
    {
        public RateMapper()
        {
            CreateMap<Rate, RateViewModel>()
              .ForPath(d => d.Code, opt => opt.MapFrom(s => s.Code))
              .ForPath(d => d.Name, opt => opt.MapFrom(s => s.Name))
              .ForPath(d => d.Value, opt => opt.MapFrom(s => s.Value))
              .ForPath(d => d.Unit.Id, opt => opt.MapFrom(s => s.UnitId))
              .ForPath(d => d.Unit.Code, opt => opt.MapFrom(s => s.UnitCode))
              .ForPath(d => d.Unit.Name, opt => opt.MapFrom(s => s.UnitName))
              .ReverseMap();
        }
    }
}
