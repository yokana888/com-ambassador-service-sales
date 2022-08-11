using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting
{
    public class FinishingPrintingSalesContractDetailViewModel : BaseViewModel
    {
        public string Color { get; set; }
        public CurrencyViewModel Currency { get; set; }
        public double? Price { get; set; }
        public decimal ScreenCost { get; set; }
        public bool UseIncomeTax { get; set; }

        public long CostCalculationId { get; set; }

        public string ProductionOrderNo { get; set; }
    }
}