using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.FinishingPrintingProfiles
{
    public class ShinFinishingPrintingSalesContractMapper : Profile
    {
        public ShinFinishingPrintingSalesContractMapper()
        {
            CreateMap<FinishingPrintingSalesContractModel, ShinFinishingPrintingSalesContractViewModel>()
                //.ForPath(d => d.CostCalculation.Id, opt => opt.MapFrom(s => s.CostCalculationId))
                .ForPath(d => d.PreSalesContract.No, opt => opt.MapFrom(s => s.PreSalesContractNo))
                //.ForPath(d => d.CostCalculation.ProductionOrderNo, opt => opt.MapFrom(s => s.ProductionOrderNo))
                .ForPath(d => d.PreSalesContract.Buyer.Name, opt => opt.MapFrom(s => s.BuyerName))
                .ForPath(d => d.PreSalesContract.Buyer.Type, opt => opt.MapFrom(s => s.BuyerType))
                .ForPath(d => d.PreSalesContract.Unit.Name, opt => opt.MapFrom(s => s.UnitName))
                .ForPath(d => d.PreSalesContract.Id, opt => opt.MapFrom(s => s.PreSalesContractId))

                .ForPath(d => d.AccountBank.Id, opt => opt.MapFrom(s => s.AccountBankID))
                .ForPath(d => d.AccountBank.Code, opt => opt.MapFrom(s => s.AccountBankCode))
                .ForPath(d => d.AccountBank.AccountName, opt => opt.MapFrom(s => s.AccountBankAccountName))
                .ForPath(d => d.AccountBank.BankName, opt => opt.MapFrom(s => s.AccountBankName))
                .ForPath(d => d.AccountBank.AccountNumber, opt => opt.MapFrom(s => s.AccountBankNumber))
                .ForPath(d => d.AccountBank.Currency.Id, opt => opt.MapFrom(s => s.AccountBankCurrencyID))
                .ForPath(d => d.AccountBank.Currency.Code, opt => opt.MapFrom(s => s.AccountBankCurrencyCode))
                .ForPath(d => d.AccountBank.Currency.Symbol, opt => opt.MapFrom(s => s.AccountBankCurrencySymbol))
                .ForPath(d => d.AccountBank.Currency.Rate, opt => opt.MapFrom(s => s.AccountBankCurrencyRate))

                .ForPath(d => d.Agent.Id, opt => opt.MapFrom(s => s.AgentID))
                .ForPath(d => d.Agent.Code, opt => opt.MapFrom(s => s.AgentCode))
                .ForPath(d => d.Agent.Name, opt => opt.MapFrom(s => s.AgentName))


                .ForPath(d => d.Commodity.Id, opt => opt.MapFrom(s => s.CommodityID))
                .ForPath(d => d.Commodity.Code, opt => opt.MapFrom(s => s.CommodityCode))
                .ForPath(d => d.Commodity.Name, opt => opt.MapFrom(s => s.CommodityName))


                //.ForPath(d => d.MaterialConstruction.Id, opt => opt.MapFrom(s => s.MaterialConstructionId))
                //.ForPath(d => d.MaterialConstruction.Code, opt => opt.MapFrom(s => s.MaterialConstructionCode))
                //.ForPath(d => d.MaterialConstruction.Name, opt => opt.MapFrom(s => s.MaterialConstructionName))


                .ForPath(d => d.Quality.Id, opt => opt.MapFrom(s => s.QualityID))
                .ForPath(d => d.Quality.Code, opt => opt.MapFrom(s => s.QualityCode))
                .ForPath(d => d.Quality.Name, opt => opt.MapFrom(s => s.QualityName))

                .ForPath(d => d.TermOfPayment.Id, opt => opt.MapFrom(s => s.TermOfPaymentID))
                .ForPath(d => d.TermOfPayment.Code, opt => opt.MapFrom(s => s.TermOfPaymentCode))
                .ForPath(d => d.TermOfPayment.Name, opt => opt.MapFrom(s => s.TermOfPaymentName))
                .ForPath(d => d.TermOfPayment.IsExport, opt => opt.MapFrom(s => s.TermOfPaymentIsExport))


                .ForPath(d => d.YarnMaterial.Id, opt => opt.MapFrom(s => s.YarnMaterialID))
                .ForPath(d => d.YarnMaterial.Code, opt => opt.MapFrom(s => s.YarnMaterialCode))
                .ForPath(d => d.YarnMaterial.Name, opt => opt.MapFrom(s => s.YarnMaterialName))

                .ForPath(d => d.Material.Id, opt => opt.MapFrom(s => s.MaterialID))
                .ForPath(d => d.Material.Code, opt => opt.MapFrom(s => s.MaterialCode))
                .ForPath(d => d.Material.Name, opt => opt.MapFrom(s => s.MaterialName))

                .ForPath(d => d.UOM.Id, opt => opt.MapFrom(s => s.UOMID))
                .ForPath(d => d.UOM.Unit, opt => opt.MapFrom(s => s.UOMUnit))

                .ForPath(d => d.Sales.Id, opt => opt.MapFrom(s => s.SalesId))
                .ForPath(d => d.Sales.profile.firstname, opt => opt.MapFrom(s => s.SalesFirstName))
                .ForPath(d => d.Sales.profile.lastname, opt => opt.MapFrom(s => s.SalesLastName))
                .ForPath(d => d.Sales.UserName, opt => opt.MapFrom(s => s.SalesUserName))

                .ReverseMap();
        }
    }
}
