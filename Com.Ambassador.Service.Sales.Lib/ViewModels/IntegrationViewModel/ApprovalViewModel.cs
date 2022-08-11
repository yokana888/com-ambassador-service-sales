using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel
{
    public class ApprovalViewModel
    {
        public bool IsApproved { get; set; }
        public DateTimeOffset ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
    }
}
