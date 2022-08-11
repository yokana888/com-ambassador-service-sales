using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentSalesContractModel;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentSalesContractViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.GarmentSalesContractProfiles
{
    public class GarmentSalesContractMapper : Profile
    {
        public GarmentSalesContractMapper()
        {
            CreateMap<GarmentSalesContract, GarmentSalesContractViewModel>()
            .ForPath(d => d.AccountBank.Id, opt => opt.MapFrom(s => s.AccountBankId))
            .ForPath(d => d.AccountBank.BankName, opt => opt.MapFrom(s => s.AccountBankName))
            .ForPath(d => d.AccountBank.AccountName, opt => opt.MapFrom(s => s.AccountName))
            .ForPath(d => d.Uom.Id, opt => opt.MapFrom(s => s.UomId))
            .ForPath(d => d.Uom.Unit, opt => opt.MapFrom(s => s.UomUnit))

            .ReverseMap();
        }
    }
}
