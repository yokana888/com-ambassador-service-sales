using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment
{
    public class CCROGarmentHistoryBySectionReportViewModel 
    {
        public string RO_Number { get; set; }
        public string Section { get; set; }
        public string Article { get; set; }
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public DateTimeOffset ApprovalMDDate { get; set; }
        public DateTimeOffset ApprovalIEDate { get; set; }
        public DateTimeOffset ApprovalPurchDate { get; set; }
        public DateTimeOffset ApprovalKadivMDDate { get; set; }
        public DateTimeOffset ValidatedMDDate { get; set; }
        public DateTimeOffset ValidatedSampleDate { get; set; }

    }
}
