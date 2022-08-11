using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;

namespace Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.FinishingPrintingProfiles
{
    public class FinishingPrintingSalesContractDetailMapper : Profile
    {
        public FinishingPrintingSalesContractDetailMapper()
        {
            CreateMap<FinishingPrintingSalesContractDetailModel, FinishingPrintingSalesContractDetailViewModel>()
                .ForPath(d => d.Currency.Id, opt => opt.MapFrom(s => s.CurrencyID))
                .ForPath(d => d.Currency.Code, opt => opt.MapFrom(s => s.CurrencyCode))
                .ForPath(d => d.Currency.Symbol, opt => opt.MapFrom(s => s.CurrencySymbol))
                .ForPath(d => d.Currency.Rate, opt => opt.MapFrom(s => s.CurrencyRate))
                .ReverseMap();
        }
    }
}
