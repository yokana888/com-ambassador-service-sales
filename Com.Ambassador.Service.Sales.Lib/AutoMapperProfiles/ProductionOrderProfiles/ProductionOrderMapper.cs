using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.ViewModels.ProductionOrder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.ProductionOrderProfiles
{
    public class ProductionOrderMapper : Profile
    {
        public ProductionOrderMapper()
        {
            CreateMap<ProductionOrderModel, ProductionOrderViewModel>()

            .ForPath(d => d.Buyer.Id, opt => opt.MapFrom(s => s.BuyerId))
            .ForPath(d => d.Buyer.Code, opt => opt.MapFrom(s => s.BuyerCode))
            .ForPath(d => d.Buyer.Name, opt => opt.MapFrom(s => s.BuyerName))
            .ForPath(d => d.Buyer.Type, opt => opt.MapFrom(s => s.BuyerType))

            .ForPath(d => d.FinishingPrintingSalesContract.Id, opt => opt.MapFrom(s => s.SalesContractId))
            .ForPath(d => d.FinishingPrintingSalesContract.SalesContractNo, opt => opt.MapFrom(s => s.SalesContractNo))

            .ForPath(d => d.YarnMaterial.Id, opt => opt.MapFrom(s => s.YarnMaterialId))
            .ForPath(d => d.YarnMaterial.Code, opt => opt.MapFrom(s => s.YarnMaterialCode))
            .ForPath(d => d.YarnMaterial.Name, opt => opt.MapFrom(s => s.YarnMaterialName))
            .ForPath(d => d.YarnMaterial.Remark, opt => opt.MapFrom(s => s.YarnMaterialRemark))

            .ForPath(d => d.ProcessType.Id, opt => opt.MapFrom(s => s.ProcessTypeId))
            .ForPath(d => d.ProcessType.Code, opt => opt.MapFrom(s => s.ProcessTypeCode))
            .ForPath(d => d.ProcessType.Name, opt => opt.MapFrom(s => s.ProcessTypeName))
            .ForPath(d => d.ProcessType.Remark, opt => opt.MapFrom(s => s.ProcessTypeRemark))

            .ForPath(d => d.ProcessType.Id, opt => opt.MapFrom(s => s.ProcessTypeId))
            .ForPath(d => d.ProcessType.Code, opt => opt.MapFrom(s => s.ProcessTypeCode))
            .ForPath(d => d.ProcessType.Name, opt => opt.MapFrom(s => s.ProcessTypeName))
            .ForPath(d => d.ProcessType.Remark, opt => opt.MapFrom(s => s.ProcessTypeRemark))
            .ForPath(d => d.ProcessType.Unit, opt => opt.MapFrom(s => s.ProcessTypeUnit))
            .ForPath(d => d.ProcessType.SPPCode, opt => opt.MapFrom(s => s.ProcessTypeSPPCode))

            .ForPath(d => d.OrderType.Id, opt => opt.MapFrom(s => s.OrderTypeId))
            .ForPath(d => d.OrderType.Code, opt => opt.MapFrom(s => s.OrderTypeCode))
            .ForPath(d => d.OrderType.Name, opt => opt.MapFrom(s => s.OrderTypeName))
            .ForPath(d => d.OrderType.Remark, opt => opt.MapFrom(s => s.OrderTypeRemark))

            .ForPath(d => d.Material.Id, opt => opt.MapFrom(s => s.MaterialId))
            .ForPath(d => d.Material.Code, opt => opt.MapFrom(s => s.MaterialCode))
            .ForPath(d => d.Material.Name, opt => opt.MapFrom(s => s.MaterialName))
            .ForPath(d => d.Material.Price, opt => opt.MapFrom(s => s.MaterialPrice))
            .ForPath(d => d.Material.Tags, opt => opt.MapFrom(s => s.MaterialTags))

            .ForPath(d => d.DesignMotive.Id, opt => opt.MapFrom(s => s.DesignMotiveID))
            .ForPath(d => d.DesignMotive.Code, opt => opt.MapFrom(s => s.DesignMotiveCode))
            .ForPath(d => d.DesignMotive.Name, opt => opt.MapFrom(s => s.DesignMotiveName))

            .ForPath(d => d.Uom.Id, opt => opt.MapFrom(s => s.UomId))
            .ForPath(d => d.Uom.Unit, opt => opt.MapFrom(s => s.UomUnit))

            .ForPath(d => d.MaterialConstruction.Id, opt => opt.MapFrom(s => s.MaterialConstructionId))
            .ForPath(d => d.MaterialConstruction.Code, opt => opt.MapFrom(s => s.MaterialConstructionCode))
            .ForPath(d => d.MaterialConstruction.Name, opt => opt.MapFrom(s => s.MaterialConstructionName))
            .ForPath(d => d.MaterialConstruction.Remark, opt => opt.MapFrom(s => s.MaterialConstructionRemark))

            .ForPath(d => d.FinishType.Id, opt => opt.MapFrom(s => s.FinishTypeId))
            .ForPath(d => d.FinishType.Code, opt => opt.MapFrom(s => s.FinishTypeCode))
            .ForPath(d => d.FinishType.Name, opt => opt.MapFrom(s => s.FinishTypeName))
            .ForPath(d => d.FinishType.Remark, opt => opt.MapFrom(s => s.FinishTypeRemark))

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
            .ReverseMap();
        }
    }
}
