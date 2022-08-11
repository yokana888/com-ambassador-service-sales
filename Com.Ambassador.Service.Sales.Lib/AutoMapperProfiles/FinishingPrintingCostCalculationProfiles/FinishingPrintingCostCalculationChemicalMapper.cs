using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.FinishingPrintingCostCalculationProfiles
{
    public class FinishingPrintingCostCalculationChemicalMapper : Profile
    {
        public FinishingPrintingCostCalculationChemicalMapper()
        {
            CreateMap<FinishingPrintingCostCalculationChemicalModel, FinishingPrintingCostCalculationChemicalViewModel>()
                .ForPath(d => d.Chemical.Currency.Code, opt => opt.MapFrom(s => s.ChemicalCurrency))
                .ForPath(d => d.Chemical.Id, opt => opt.MapFrom(s => s.ChemicalId))
                .ForPath(d => d.Chemical.Name, opt => opt.MapFrom(s => s.ChemicalName))
                .ForPath(d => d.Chemical.Price, opt => opt.MapFrom(s => s.ChemicalPrice))
                .ForPath(d => d.Chemical.UOM.Unit, opt => opt.MapFrom(s => s.ChemicalUom))
                .ReverseMap();
        }
    }
}
