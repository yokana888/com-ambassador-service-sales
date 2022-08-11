using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.Report
{
    public class ProductionOrderReportDetailViewModel
    {
        public virtual ICollection<ProductionOrderDetailViewModel> SPPList { get; set; }
        public virtual ICollection<FabricQualityControlViewModel> QCList { get; set; }
        public virtual ICollection<DailyOperationViewModel> DailyOPList { get; set; }
    }

}
