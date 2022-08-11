using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Com.Ambassador.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels
{
    public class GarmentWeeklyPlanItem : BaseModel
    {
        public byte WeekNumber { get; set; }

        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public byte Month { get; set; }

        public double Efficiency { get; set; }
        public int Operator { get; set; }
        public double WorkingHours { get; set; }

        public double AHTotal { get; set; }
        public int EHTotal { get; set; }
        public int UsedEH { get; set; }
        public int RemainingEH { get; set; }

        public double WHConfirm { get; set; }

        public virtual long WeeklyPlanId { get; set; }
        [ForeignKey("WeeklyPlanId")]
        public virtual GarmentWeeklyPlan WeeklyPlan { get; set; }
    }
}
