using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.SalesInvoice;
using Com.Ambassador.Service.Sales.Lib.ViewModels.SalesInvoice;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.SalesInvoiceProfiles
{
    public class SalesInvoiceItemMapper : Profile
    {
        public SalesInvoiceItemMapper()
        {
            CreateMap<SalesInvoiceItemModel, SalesInvoiceItemViewModel>()

                .ReverseMap();
        }
    }
}
