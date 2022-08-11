using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System.ComponentModel.DataAnnotations;

namespace Com.Ambassador.Service.Sales.Lib.Models.CostCalculationGarments
{
    public class CostCalculationGarmentUnpostReason : BaseModel
    {
        public long CostCalculationId { get; set; }
        [MaxLength(50)]
        public string RONo { get; set; }
        [MaxLength(2000)]
        public string UnpostReason { get; set; }
    }
}
