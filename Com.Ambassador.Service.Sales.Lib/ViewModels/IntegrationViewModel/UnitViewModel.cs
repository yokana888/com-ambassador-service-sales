using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel
{
    public class UnitViewModel
    {
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DivisionViewModel Division { get; set; }
    }
}
