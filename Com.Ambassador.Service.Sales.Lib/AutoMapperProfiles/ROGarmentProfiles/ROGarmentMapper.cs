using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.ROGarments;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentROViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.ROGarmentProfiles
{
    public class ROGarmentMapper : Profile
	{
		public ROGarmentMapper()
        {
            CreateMap<RO_Garment, RO_GarmentViewModel>()
              .ForPath(d => d.CostCalculationGarment.Id, opt => opt.MapFrom(s => s.CostCalculationGarmentId))
              .ForPath(d => d.CostCalculationGarment, opt => opt.MapFrom(s => s.CostCalculationGarment))
              .ForPath(d => d.ImagesFile, opt => opt.MapFrom(s => s.ImagesFile))
              .ReverseMap();

            CreateMap<RO_Garment, RO_GarmentViewModel>()
              .ForPath(d => d.ImagesPath, opt => opt.MapFrom(s => JsonConvert.DeserializeObject<List<string>>(s.ImagesPath)))
              .ForPath(d => d.ImagesName, opt => opt.MapFrom(s => JsonConvert.DeserializeObject<List<string>>(s.ImagesName)))
              .ForPath(d => d.DocumentsPath, opt => opt.MapFrom(s => JsonConvert.DeserializeObject<List<string>>(s.DocumentsPath)))
              .ForPath(d => d.DocumentsFileName, opt => opt.MapFrom(s => JsonConvert.DeserializeObject<List<string>>(s.DocumentsFileName)));

            CreateMap<RO_GarmentViewModel, RO_Garment>()
              .ForPath(d => d.ImagesPath, opt => opt.MapFrom(s => JsonConvert.SerializeObject(s.ImagesPath)))
              .ForPath(d => d.ImagesName, opt => opt.MapFrom(s => JsonConvert.SerializeObject(s.ImagesName)))
              .ForPath(d => d.DocumentsPath, opt => opt.MapFrom(s => JsonConvert.SerializeObject(s.DocumentsPath)))
              .ForPath(d => d.DocumentsFileName, opt => opt.MapFrom(s => JsonConvert.SerializeObject(s.DocumentsFileName)));
        }
    }
}
