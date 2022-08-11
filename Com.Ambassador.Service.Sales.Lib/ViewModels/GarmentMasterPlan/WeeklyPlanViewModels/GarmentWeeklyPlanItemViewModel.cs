using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.Garment.WeeklyPlanViewModels
{
    public class GarmentWeeklyPlanItemViewModel : BaseViewModel
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
    }
}
