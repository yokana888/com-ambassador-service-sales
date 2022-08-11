using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.ProductionOrder
{
    public class ProductionOrder_LampStandardModel : BaseModel
    {
        [MaxLength(1000)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public long LampStandardId { get; set; }

        public virtual ProductionOrderModel ProductionOrderModel { get; set; }
    }
}
