using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.GarmentSalesContractModel
{
    public class GarmentSalesContractItem : BaseModel
    {
        [MaxLength(3000)]
        public string Description { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public virtual long GSCId { get; set; }
        [ForeignKey("GSCId")]
        public virtual GarmentSalesContract GarmentSalesContract { get; set; }
    }
}
