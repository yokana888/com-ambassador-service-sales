using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentSalesContractModel;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentSalesContractViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.GarmentSalesContractProfiles
{
    public class GarmentSalesContractItemMapper: Profile
    {
        public GarmentSalesContractItemMapper()
        {
            CreateMap<GarmentSalesContractItem, GarmentSalesContractItemViewModel>()
            .ReverseMap();
        }
    }
}
