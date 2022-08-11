using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models
{
    public class Efficiency : BaseModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int InitialRange { get; set; }
        public int FinalRange { get; set; }
        public double Value { get; set; }
    }
}
