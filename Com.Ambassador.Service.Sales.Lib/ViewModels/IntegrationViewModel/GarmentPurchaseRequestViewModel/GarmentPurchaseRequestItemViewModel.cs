using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel.GarmentPurchaseRequestViewModel
{
    public class GarmentPurchaseRequestItemViewModel : BaseViewModel
    {
        public string PO_SerialNumber { get; set; }

        public ProductViewModel Product { get; set; }

        public double Quantity { get; set; }
        public double BudgetPrice { get; set; }

        public UomViewModel Uom { get; set; }

        public CategoryViewModel Category { get; set; }

        public string ProductRemark { get; set; }

        public string Status { get; set; }
        public bool IsUsed { get; set; }
    }
}
