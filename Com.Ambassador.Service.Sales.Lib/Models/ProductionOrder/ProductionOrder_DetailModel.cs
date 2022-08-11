using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.ProductionOrder
{
    public class ProductionOrder_DetailModel : BaseModel
    {
        //public virtual ProductionOrderModel ProductionOrderModel { get; set; }
        [MaxLength(255)]
        public string ColorRequest { get; set; }
        [MaxLength(255)]
        public string ColorTemplate { get; set; }
        [MaxLength(255)]
        public string ColorTypeId { get; set; }
        [MaxLength(255)]
        public string ColorType { get; set; }
        public double Quantity { get; set; }

        public virtual ProductionOrderModel ProductionOrderModel { get; set; }

        /*Uom*/
        public long UomId { get; set; }
        [MaxLength(255)]
        public string UomUnit { get; set; }

        [NotMapped]
        public string ProductionOrderNo { get; set; }
    }
}
