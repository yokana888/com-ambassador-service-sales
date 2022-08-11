using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel
{
    public class TermOfPaymentViewModel
    {
        public long Id { get; set; }
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(1000)]
        public string Name { get; set; }
        public bool IsExport { get; set; }
    }
}
