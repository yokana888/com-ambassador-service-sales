using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel
{
    public class BuyerViewModel
    {
        public long? Id { get; set; }
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(1000)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Type { get; set; }
        [MaxLength(1000)]
        public string Address { get; set; }
        [MaxLength(255)]
        public string City { get; set; }
        [MaxLength(255)]
        public string Country { get; set; }
        [MaxLength(255)]
        public string Contact { get; set; }
        [MaxLength(255)]
        public string NPWP { get; set; }
        [MaxLength(255)]
        public string NIK { get; set; }
        public string Job { get; set; }
    }
}
