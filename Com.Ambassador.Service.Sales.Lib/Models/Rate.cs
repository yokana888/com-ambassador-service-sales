using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System.ComponentModel.DataAnnotations;

namespace Com.Ambassador.Service.Sales.Lib.Models
{
    public class Rate : BaseModel
    {
        [MaxLength(50)]
        public string Code { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public double Value { get; set; }

        public long UnitId { get; set; }
        [MaxLength(50)]
        public string UnitCode { get; set; }
        [MaxLength(255)]
        public string UnitName { get; set; }
    }
}
