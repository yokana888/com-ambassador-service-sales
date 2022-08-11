using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel
{
    public class GarmentSewingBlockingPlanItem : BaseModel
    {
        public virtual long BlockingPlanId { get; set; }
        [ForeignKey("BlockingPlanId")]
        public virtual GarmentSewingBlockingPlan GarmentSewingBlockingPlan { get; set; }
        public bool IsConfirm { get; set; }
        public long ComodityId { get; set; }
        [MaxLength(255)]
        public string ComodityCode { get; set; }
        [MaxLength(500)]
        public string ComodityName { get; set; }
        public double SMVSewing { get; set; }
        public long WeeklyPlanId { get; set; }
        public long UnitId { get; set; }
        [MaxLength(255)]
        public string UnitCode { get; set; }
        [MaxLength(255)]
        public string UnitName { get; set; }
        public short Year { get; set; }
        public long WeeklyPlanItemId { get; set; }
        public byte WeekNumber { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public double OrderQuantity { get; set; }
        public string Remark { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public double Efficiency { get; set; }
        public double EHBooking { get; set; }

    }
}
