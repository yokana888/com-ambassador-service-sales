using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel
{
    public class UomViewModel
    {
        public long? Id { get; set; }
        [MaxLength(255)]
        public string Unit { get; set; }
    }
}
