using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel
{
    public class ProcessTypeViewModel : BaseViewModel
    {
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(1000)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Remark { get; set; }
        public OrderTypeViewModel OrderType { get; set; }

        public string Unit { get; set; }

        public string SPPCode { get; set; }
    }
}
