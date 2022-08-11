using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.GarmentOmzetTargetModel
{
    public class GarmentOmzetTarget : BaseModel
    {
        [MaxLength(4)]
        public string YearOfPeriod { get; set; }
        [MaxLength(10)]
        public string MonthOfPeriod { get; set; }
        [MaxLength(5)]
        public string QuaterCode { get; set; }
        public int SectionId { get; set; }
        [MaxLength(5)]
        public string SectionCode { get; set; }
        [MaxLength(50)]
        public string SectionName { get; set; }
        public double Amount { get; set; }
    }
}