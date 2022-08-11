using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.FinishingPrintingCostCalculationProfiles
{
    public class FinishingPrintingCostCalculationMachineMapper : Profile
    {
        public FinishingPrintingCostCalculationMachineMapper()
        {
            CreateMap<FinishingPrintingCostCalculationMachineModel, FinishingPrintingCostCalculationMachineViewModel>()
                .ForPath(d => d.Step.Id, opt => opt.MapFrom(s => s.StepId))
                .ForPath(d => d.Step.Process, opt => opt.MapFrom(s => s.StepProcess))
                .ForPath(d => d.Step.ProcessArea, opt => opt.MapFrom(s => s.StepProcessArea))

                .ForPath(d => d.Machine.Electric, opt => opt.MapFrom(s => s.MachineElectric))
                .ForPath(d => d.Machine.Id, opt => opt.MapFrom(s => s.MachineId))
                .ForPath(d => d.Machine.LPG, opt => opt.MapFrom(s => s.MachineLPG))
                .ForPath(d => d.Machine.Name, opt => opt.MapFrom(s => s.MachineName))
                .ForPath(d => d.Machine.Process, opt => opt.MapFrom(s => s.MachineProcess))
                .ForPath(d => d.Machine.Solar, opt => opt.MapFrom(s => s.MachineSolar))
                .ForPath(d => d.Machine.Steam, opt => opt.MapFrom(s => s.MachineSteam))
                .ForPath(d => d.Machine.Water, opt => opt.MapFrom(s => s.MachineWater))
                .ReverseMap();
        }
    }
}
