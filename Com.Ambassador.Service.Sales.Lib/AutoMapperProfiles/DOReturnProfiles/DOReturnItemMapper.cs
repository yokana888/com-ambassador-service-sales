using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.DOReturn;
using Com.Ambassador.Service.Sales.Lib.ViewModels.DOReturn;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.DOReturnProfiles
{
    public class DOReturnItemMapper : Profile
    {
        public DOReturnItemMapper()
        {
            CreateMap<DOReturnItemModel, DOReturnItemViewModel>()

                //.ForPath(d => d.SalesInvoiceDetail.ShippingOutId, opt => opt.MapFrom(s => s.ShippingOutId))
                //.ForPath(d => d.SalesInvoiceDetail.BonNo, opt => opt.MapFrom(s => s.BonNo))

                //.ForPath(d => d.SalesInvoiceItem.ProductId, opt => opt.MapFrom(s => s.ProductId))
                //.ForPath(d => d.SalesInvoiceItem.ProductCode, opt => opt.MapFrom(s => s.ProductCode))
                //.ForPath(d => d.SalesInvoiceItem.ProductName, opt => opt.MapFrom(s => s.ProductName))
                //.ForPath(d => d.SalesInvoiceItem.QuantityPacking, opt => opt.MapFrom(s => s.QuantityPacking))
                //.ForPath(d => d.SalesInvoiceItem.PackingUom, opt => opt.MapFrom(s => s.PackingUom))
                //.ForPath(d => d.SalesInvoiceItem.ItemUom, opt => opt.MapFrom(s => s.ItemUom))
                //.ForPath(d => d.SalesInvoiceItem.QuantityItem, opt => opt.MapFrom(s => s.QuantityItem))

                .ReverseMap();
        }
    }
}
