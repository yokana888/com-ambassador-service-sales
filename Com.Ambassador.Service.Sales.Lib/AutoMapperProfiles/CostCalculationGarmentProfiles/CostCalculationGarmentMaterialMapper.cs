using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.CostCalculationGarmentProfiles
{
	public class CostCalculationGarmentMaterialMapper : Profile
	{
		public CostCalculationGarmentMaterialMapper()
		{
			CreateMap<CostCalculationGarment_Material, CostCalculationGarment_MaterialViewModel>()
				 .ForPath(d => d.Product.Id, opt => opt.MapFrom(s => s.ProductId))
				 .ForPath(d => d.Product.Code, opt => opt.MapFrom(s => s.ProductCode))
				 .ForPath(d => d.Product.Yarn, opt => opt.MapFrom(s => s.Yarn))
				 .ForPath(d => d.Product.Width, opt => opt.MapFrom(s => s.Width))
				 .ForPath(d => d.Product.Const, opt => opt.MapFrom(s => s.Construction))
				 .ForPath(d => d.Product.Composition, opt => opt.MapFrom(s => s.Composition))
				 .ForPath(d => d.Category.Id, opt => opt.MapFrom(s => s.CategoryId))
				 .ForPath(d => d.Category.code, opt => opt.MapFrom(s => s.CategoryCode))
				 .ForPath(d => d.Category.name, opt => opt.MapFrom(s => s.CategoryName))
				 .ForPath(d => d.UOMQuantity.Id, opt => opt.MapFrom(s => s.UOMQuantityId))
				 .ForPath(d => d.UOMQuantity.Unit, opt => opt.MapFrom(s => s.UOMQuantityName))
				 .ForPath(d => d.UOMPrice.Id, opt => opt.MapFrom(s => s.UOMPriceId))
				 .ForPath(d => d.UOMPrice.Unit, opt => opt.MapFrom(s => s.UOMPriceName))
				 .ForPath(d => d.ShippingFeePortion, opt => opt.MapFrom(s => s.ShippingFeePortion))
                 .ForPath(d => d.Total, opt => opt.MapFrom(s => s.Total))
                 .ForPath(d => d.TotalShippingFee, opt => opt.MapFrom(s => s.TotalShippingFee))
                 .ReverseMap();

		}
	}
}
