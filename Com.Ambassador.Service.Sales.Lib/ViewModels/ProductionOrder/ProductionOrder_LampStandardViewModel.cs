using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.ProductionOrder
{
    public class ProductionOrder_LampStandardViewModel : BaseViewModel
    {
        [MaxLength(1000)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public long LampStandardId { get; set; }
    }
}
