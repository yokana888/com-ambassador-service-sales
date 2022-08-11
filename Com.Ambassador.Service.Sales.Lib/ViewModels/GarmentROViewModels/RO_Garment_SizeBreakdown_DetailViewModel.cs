using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentROViewModels
{
    public class RO_Garment_SizeBreakdown_DetailViewModel : BaseViewModel
    {
        public int SizeBreakdownDetailIndex { get; set; }

        public string Code { get; set; }
        public string Information { get; set; }
        public string Size { get; set; }
        public int? Quantity { get; set; }
    }
}
