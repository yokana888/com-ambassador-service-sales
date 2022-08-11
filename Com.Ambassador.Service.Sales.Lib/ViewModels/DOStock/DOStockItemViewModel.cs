using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Com.Ambassador.Service.Sales.Lib.ViewModels.ProductionOrder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.DOStock
{
    public class DOStockItemViewModel : BaseViewModel
    {
        public ProductionOrderViewModel ProductionOrder { get; set; }
        public string ConstructionName { get; set; }
        public string ColorRequest { get; set; }
        public string ColorTemplate { get; set; }
        public string UnitOrCode { get; set; }
        public double Packing { get; set; }
        public double Length { get; set; }

    }
}
