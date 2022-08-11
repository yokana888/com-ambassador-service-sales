using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.SalesInvoice;
using Com.Ambassador.Service.Sales.Lib.ViewModels.SalesInvoice;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.SalesInvoiceProfiles
{
    public class SalesInvoiceDetailMapper : Profile
    {
        public SalesInvoiceDetailMapper()
        {
            CreateMap<SalesInvoiceDetailModel, SalesInvoiceDetailViewModel>()

                .ReverseMap();
        }
    }
}
