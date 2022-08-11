using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentSewingBlockingPlanViewModels
{
    public class GarmentSewingBlockingPlanItemViewModel : BaseViewModel
    {
        public bool IsConfirm { get; set; }
        public CommodityViewModel Comodity { get; set; }
        public double SMVSewing { get; set; }
        public long WeeklyPlanId { get; set; }
        public UnitViewModel Unit { get; set; }
        public short Year { get; set; }
        public long WeeklyPlanItemId { get; set; }
        public byte WeekNumber { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public double OrderQuantity { get; set; }
        public string Remark { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public double Efficiency { get; set; }
        public double whConfirm { get; set; }
        public double EHBooking { get; set; }
        public string Status { get; set; }
    }
}
