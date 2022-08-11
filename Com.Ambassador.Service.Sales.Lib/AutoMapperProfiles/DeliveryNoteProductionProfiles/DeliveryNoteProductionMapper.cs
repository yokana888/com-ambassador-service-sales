using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.DeliveryNoteProductionProfiles
{
    public class DeliveryNoteProductionMapper : Profile
    {

        public DeliveryNoteProductionMapper()
        {
            CreateMap<DeliveryNoteProductionModel, DeliveryNoteProductionViewModel>()

                .ForPath(d => d.SalesContract.ComodityDescription, opt => opt.MapFrom(s => s.Blended))
                .ForPath(d => d.SalesContract.OrderQuantity, opt => opt.MapFrom(s => s.Total))
                .ForPath(d => d.SalesContract.SalesContractNo, opt => opt.MapFrom(s => s.SalesContractNo))
                .ForPath(d => d.SalesContract.UomUnit, opt => opt.MapFrom(s => s.Uom))
                .ForPath(d => d.SalesContract.DeliveredTo, opt => opt.MapFrom(s => s.DeliveredTo))

                .ForPath(d => d.SalesContract.Buyer.Name, opt => opt.MapFrom(s => s.BuyerName))
                .ForPath(d => d.SalesContract.Buyer.Type, opt => opt.MapFrom(s => s.BuyerType))

                .ForPath(d => d.SalesContract.Comodity.Name, opt => opt.MapFrom(s => s.TypeandNumber))

                .ReverseMap();
        }

    }
}
