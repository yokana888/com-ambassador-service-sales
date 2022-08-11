using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Com.Ambassador.Service.Sales.Lib.ViewModels.ProductionOrder;
using System.ComponentModel.DataAnnotations;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.DOSales
{
    public class DOSalesDetailViewModel : BaseViewModel
    {
        public ProductionOrderViewModel ProductionOrder { get; set; }
        public MaterialViewModel Material { get; set; }
        public MaterialConstructionViewModel MaterialConstruction { get; set; }
        public string ConstructionName { get; set; }
        public string ColorRequest { get; set; }
        public string ColorTemplate { get; set; }
        public string UnitOrCode { get; set; }
        public double Packing { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public double ConvertionValue { get; set; }
        public string NoSOP { get; set; }
        public string ThreadNumber { get; set; }
        public string Grade { get; set; }
    }
}
