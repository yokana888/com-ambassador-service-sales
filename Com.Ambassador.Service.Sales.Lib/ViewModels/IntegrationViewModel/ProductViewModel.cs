using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(1000)]
        public string Name { get; set; }
        public double Price { get; set; }
        [MaxLength(255)]
        public string Tags { get; set; }
        public UomViewModel UOM { get; set; }
        public CurrencyViewModel Currency { get; set; }
    }
}
