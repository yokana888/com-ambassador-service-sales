using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.DOSales
{
    public class StorageViewModel
    {
        public int? _id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public UnitViewModel unit { get; set; }
    }
}