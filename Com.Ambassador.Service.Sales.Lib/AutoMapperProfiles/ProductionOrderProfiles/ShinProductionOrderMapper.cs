using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.ViewModels.ProductionOrder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.ProductionOrderProfiles
{
    public class ShinProductionOrderMapper : Profile
    {
        public ShinProductionOrderMapper()
        {
            CreateMap<ProductionOrderModel, ShinProductionOrderViewModel>()

            .ForPath(d => d.FinishingPrintingSalesContract.PreSalesContract.Buyer.Id, opt => opt.MapFrom(s => s.BuyerId))
            .ForPath(d => d.FinishingPrintingSalesContract.PreSalesContract.Buyer.Code, opt => opt.MapFrom(s => s.BuyerCode))
            .ForPath(d => d.FinishingPrintingSalesContract.PreSalesContract.Buyer.Name, opt => opt.MapFrom(s => s.BuyerName))
            .ForPath(d => d.FinishingPrintingSalesContract.PreSalesContract.Buyer.Type, opt => opt.MapFrom(s => s.BuyerType))

            .ForPath(d => d.FinishingPrintingSalesContract.Id, opt => opt.MapFrom(s => s.SalesContractId))
            .ForPath(d => d.FinishingPrintingSalesContract.SalesContractNo, opt => opt.MapFrom(s => s.SalesContractNo))
            .ForPath(d => d.ProductionOrderNo, opt => opt.MapFrom(s => s.OrderNo))

            .ForPath(d => d.FinishingPrintingSalesContract.YarnMaterial.Id, opt => opt.MapFrom(s => s.YarnMaterialId))
            .ForPath(d => d.FinishingPrintingSalesContract.YarnMaterial.Code, opt => opt.MapFrom(s => s.YarnMaterialCode))
            .ForPath(d => d.FinishingPrintingSalesContract.YarnMaterial.Name, opt => opt.MapFrom(s => s.YarnMaterialName))
            .ForPath(d => d.FinishingPrintingSalesContract.YarnMaterial.Remark, opt => opt.MapFrom(s => s.YarnMaterialRemark))

            .ForPath(d => d.FinishingPrintingSalesContract.PreSalesContract.ProcessType.Id, opt => opt.MapFrom(s => s.ProcessTypeId))
            .ForPath(d => d.FinishingPrintingSalesContract.PreSalesContract.ProcessType.Code, opt => opt.MapFrom(s => s.ProcessTypeCode))
            .ForPath(d => d.FinishingPrintingSalesContract.PreSalesContract.ProcessType.Name, opt => opt.MapFrom(s => s.ProcessTypeName))
            .ForPath(d => d.FinishingPrintingSalesContract.PreSalesContract.ProcessType.Remark, opt => opt.MapFrom(s => s.ProcessTypeRemark))
            .ForPath(d => d.FinishingPrintingSalesContract.PreSalesContract.ProcessType.Unit, opt => opt.MapFrom(s => s.ProcessTypeUnit))
            .ForPath(d => d.FinishingPrintingSalesContract.PreSalesContract.ProcessType.SPPCode, opt => opt.MapFrom(s => s.ProcessTypeSPPCode))
            .ForPath(d => d.FinishingPrintingSalesContract.PreSalesContract.ProcessType.OrderType.Id, opt => opt.MapFrom(s => s.OrderTypeId))
            .ForPath(d => d.FinishingPrintingSalesContract.PreSalesContract.ProcessType.OrderType.Code, opt => opt.MapFrom(s => s.OrderTypeCode))
            .ForPath(d => d.FinishingPrintingSalesContract.PreSalesContract.ProcessType.OrderType.Name, opt => opt.MapFrom(s => s.OrderTypeName))
            .ForPath(d => d.FinishingPrintingSalesContract.PreSalesContract.ProcessType.OrderType.Remark, opt => opt.MapFrom(s => s.OrderTypeRemark))

            .ForPath(d => d.FinishingPrintingSalesContract.PreSalesContract.Unit.Id, opt => opt.MapFrom(s => s.UnitId))
            .ForPath(d => d.FinishingPrintingSalesContract.PreSalesContract.Unit.Code, opt => opt.MapFrom(s => s.UnitCode))
            .ForPath(d => d.FinishingPrintingSalesContract.PreSalesContract.Unit.Name, opt => opt.MapFrom(s => s.UnitName))

            .ForPath(d => d.FinishingPrintingSalesContract.Material.Id, opt => opt.MapFrom(s => s.MaterialId))
            .ForPath(d => d.FinishingPrintingSalesContract.Material.Code, opt => opt.MapFrom(s => s.MaterialCode))
            .ForPath(d => d.FinishingPrintingSalesContract.Material.Name, opt => opt.MapFrom(s => s.MaterialName))
            .ForPath(d => d.FinishingPrintingSalesContract.Material.Price, opt => opt.MapFrom(s => s.MaterialPrice))
            .ForPath(d => d.FinishingPrintingSalesContract.Material.Tags, opt => opt.MapFrom(s => s.MaterialTags))


            .ForPath(d => d.FinishingPrintingSalesContract.UOM.Id, opt => opt.MapFrom(s => s.UomId))
            .ForPath(d => d.FinishingPrintingSalesContract.UOM.Unit, opt => opt.MapFrom(s => s.UomUnit))

            .ForPath(d => d.MaterialConstruction.Id, opt => opt.MapFrom(s => s.MaterialConstructionId))
            .ForPath(d => d.MaterialConstruction.Code, opt => opt.MapFrom(s => s.MaterialConstructionCode))
            .ForPath(d => d.MaterialConstruction.Name, opt => opt.MapFrom(s => s.MaterialConstructionName))
            .ForPath(d => d.MaterialConstruction.Remark, opt => opt.MapFrom(s => s.MaterialConstructionRemark))

            .ForPath(d => d.StandardTests.Id, opt => opt.MapFrom(s => s.StandardTestId))
            .ForPath(d => d.StandardTests.Code, opt => opt.MapFrom(s => s.StandardTestCode))
            .ForPath(d => d.StandardTests.Name, opt => opt.MapFrom(s => s.StandardTestName))
            .ForPath(d => d.StandardTests.Remark, opt => opt.MapFrom(s => s.StandardTestRemark))

            .ForPath(d => d.Account.Id, opt => opt.MapFrom(s => s.AccountId))
            .ForPath(d => d.Account.UserName, opt => opt.MapFrom(s => s.AccountUserName))
            .ForPath(d => d.Account.FirstName, opt => opt.MapFrom(s => s.ProfileFirstName))
            .ForPath(d => d.Account.LastName, opt => opt.MapFrom(s => s.ProfileLastName))
            .ForPath(d => d.Account.Gender, opt => opt.MapFrom(s => s.ProfileGender))

            .ForPath(d => d.RunWidth, opt => opt.MapFrom(s => s.RunWidths))
            .ForPath(d => d.MaterialWidth, opt => opt.MapFrom(s => s.MaterialWidth))
            .ForPath(d => d.FinishingPrintingSalesContract.PreSalesContract.OrderQuantity, opt => opt.MapFrom(s => s.OrderQuantity))
            .ForPath(d => d.FinishingPrintingSalesContract.ShippingQuantityTolerance, opt => opt.MapFrom(s => s.ShippingQuantityTolerance))

            .ForPath(d => d.ApprovalMD.IsApproved, opt => opt.MapFrom(s => s.IsApprovedMD))
            .ForPath(d => d.ApprovalMD.ApprovedBy, opt => opt.MapFrom(s => s.ApprovedMDBy))
            .ForPath(d => d.ApprovalMD.ApprovedDate, opt => opt.MapFrom(s => s.ApprovedMDDate))

            .ForPath(d => d.ApprovalSample.IsApproved, opt => opt.MapFrom(s => s.IsApprovedSample))
            .ForPath(d => d.ApprovalSample.ApprovedBy, opt => opt.MapFrom(s => s.ApprovedSampleBy))
            .ForPath(d => d.ApprovalSample.ApprovedDate, opt => opt.MapFrom(s => s.ApprovedSampleDate))

            .ReverseMap();
        }
    }
}
