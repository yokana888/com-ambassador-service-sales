using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.FinishingPrintingCostCalculationProfiles
{
    public class FinishingPrintingCostCalculationMapper : Profile
    {
        public FinishingPrintingCostCalculationMapper()
        {
            CreateMap<FinishingPrintingCostCalculationModel, FinishingPrintingCostCalculationViewModel>()
                .ForPath(d => d.Instruction.Id, opt => opt.MapFrom(s => s.InstructionId))
                .ForPath(d => d.Instruction.Name, opt => opt.MapFrom(s => s.InstructionName))

                .ForPath(d => d.PreSalesContract.Id, opt => opt.MapFrom(s => s.PreSalesContractId))
                .ForPath(d => d.PreSalesContract.No, opt => opt.MapFrom(s => s.PreSalesContractNo))
                .ForPath(d => d.PreSalesContract.Unit.Name, opt => opt.MapFrom(s => s.UnitName))
                .ForPath(d => d.PreSalesContract.Unit.Id, opt => opt.MapFrom(s => s.UnitId))

                .ForPath(d => d.UOM.Id, opt => opt.MapFrom(s => s.UomId))
                .ForPath(d => d.UOM.Unit, opt => opt.MapFrom(s => s.UomUnit))

                .ForPath(d => d.Greige.Id, opt => opt.MapFrom(s => s.GreigeId))
                .ForPath(d => d.Greige.Name, opt => opt.MapFrom(s => s.GreigeName))
                .ForPath(d => d.Greige.Price, opt => opt.MapFrom(s => s.GreigePrice))


                .ForPath(d => d.Material.Id, opt => opt.MapFrom(s => s.MaterialId))
                .ForPath(d => d.Material.Name, opt => opt.MapFrom(s => s.MaterialName))
                .ForPath(d => d.Material.Code, opt => opt.MapFrom(s => s.MaterialCode))
                .ForPath(d => d.Material.Price, opt => opt.MapFrom(s => s.MaterialPrice))
                .ForPath(d => d.Material.Tags , opt => opt.MapFrom(s => s.MaterialTags))

                .ForPath(d => d.ApprovalMD.IsApproved, opt => opt.MapFrom(s => s.IsApprovedMD))
                .ForPath(d => d.ApprovalMD.ApprovedBy, opt => opt.MapFrom(s => s.ApprovedMDBy))
                .ForPath(d => d.ApprovalMD.ApprovedDate, opt => opt.MapFrom(s => s.ApprovedMDDate))

                .ForPath(d => d.ApprovalPPIC.IsApproved, opt => opt.MapFrom(s => s.IsApprovedPPIC))
                .ForPath(d => d.ApprovalPPIC.ApprovedBy, opt => opt.MapFrom(s => s.ApprovedPPICBy))
                .ForPath(d => d.ApprovalPPIC.ApprovedDate, opt => opt.MapFrom(s => s.ApprovedPPICDate))

                .ForPath(d => d.Sales.Id, opt => opt.MapFrom(s => s.SalesId))
                .ForPath(d => d.Sales.profile.firstname, opt => opt.MapFrom(s => s.SalesFirstName))
                .ForPath(d => d.Sales.profile.lastname, opt => opt.MapFrom(s => s.SalesLastName))
                .ForPath(d => d.Sales.UserName, opt => opt.MapFrom(s => s.SalesUserName))
                .ReverseMap();
        }
    }
}
