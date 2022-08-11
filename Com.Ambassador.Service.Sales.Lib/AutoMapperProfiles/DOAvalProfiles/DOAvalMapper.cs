using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.DOSales;
using Com.Ambassador.Service.Sales.Lib.ViewModels.DOAval;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.DOAvalProfiles
{
    public class DOAvalMapper : Profile
    {
        public DOAvalMapper()
        {
            CreateMap<DOSalesModel, DOAvalViewModel>()
                .ForPath(d => d.Buyer.Id, opt => opt.MapFrom(s => s.BuyerId))
                .ForPath(d => d.Buyer.Code, opt => opt.MapFrom(s => s.BuyerCode))
                .ForPath(d => d.Buyer.Name, opt => opt.MapFrom(s => s.BuyerName))
                .ForPath(d => d.Buyer.Type, opt => opt.MapFrom(s => s.BuyerType))
                .ForPath(d => d.Buyer.Address, opt => opt.MapFrom(s => s.BuyerAddress))
                .ForPath(d => d.DOAvalNo, opt => opt.MapFrom(s => s.DOSalesNo))
                .ForPath(d => d.DOAvalCategory, opt => opt.MapFrom(s => s.DOSalesCategory))
                .ForPath(d => d.DOAvalType, opt => opt.MapFrom(s => s.DOSalesType))
                .ForPath(d => d.DOAvalItems, opt => opt.MapFrom(s => s.DOSalesDetailItems))
                .ReverseMap();

            CreateMap<DOSalesDetailModel, DOAvalItemViewModel>()
                .ReverseMap();
        }
    }
}
