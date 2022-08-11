using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.DOReturn;
using Com.Ambassador.Service.Sales.Lib.ViewModels.DOReturn;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.DOReturnProfiles
{
    public class DOReturnDetailMapper : Profile
    {
        public DOReturnDetailMapper()
        {
            CreateMap<DOReturnDetailModel, DOReturnDetailViewModel>()

                .ForPath(d => d.SalesInvoice.Id, opt => opt.MapFrom(s => s.SalesInvoiceId))
                .ForPath(d => d.SalesInvoice.SalesInvoiceNo, opt => opt.MapFrom(s => s.SalesInvoiceNo))

                .ReverseMap();
        }
    }
}
