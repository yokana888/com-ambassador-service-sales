using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models;
using Com.Ambassador.Service.Sales.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles
{
    public class EfficiencyMapper :Profile
    {
        public EfficiencyMapper()
        {
            CreateMap<Efficiency, EfficiencyViewModel>()
              .ForPath(d => d.Code, opt => opt.MapFrom(s => s.Code))
              .ForPath(d => d.Name, opt => opt.MapFrom(s => s.Name))
              .ForPath(d => d.Value, opt => opt.MapFrom(s => Math.Round(s.Value *100) ))
              .ForPath(d => d.InitialRange, opt => opt.MapFrom(s => s.InitialRange))
              .ForPath(d => d.FinalRange, opt => opt.MapFrom(s => s.FinalRange))
              .ReverseMap();
        }
    }
}
