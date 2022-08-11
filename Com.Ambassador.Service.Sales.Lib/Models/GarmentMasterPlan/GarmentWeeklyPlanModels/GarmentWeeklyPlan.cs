using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Ambassador.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels
{
    public class GarmentWeeklyPlan : BaseModel
    {
        public short Year { get; set; }

        public long UnitId { get; set; }
        [MaxLength(255)]
        public string UnitCode { get; set; }
        [MaxLength(255)]
        public string UnitName { get; set; }

        public virtual ICollection<GarmentWeeklyPlanItem> Items { get; set; }
    }
}
