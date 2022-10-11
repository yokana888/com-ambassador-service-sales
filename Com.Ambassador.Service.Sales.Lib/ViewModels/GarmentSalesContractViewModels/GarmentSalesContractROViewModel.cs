using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentSalesContractViewModels
{
    public class GarmentSalesContractROViewModel : BaseViewModel
    {
        public int CostCalculationId { get; set; }
        public string RONumber { get; set; }
        public string ComodityId { get; set; }
        public string ComodityName { get; set; }
        public string ComodityCode { get; set; }
        public string Article { get; set; }
        public double Quantity { get; set; }

        public UomViewModel Uom { get; set; }

        public string Description { get; set; }
        public string Material { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public List<GarmentSalesContractItemViewModel> Items { get; set; }
    }
}
