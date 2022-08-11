using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.ROGarments
{
    public class RO_Garment_SizeBreakdown : BaseModel
    {
        public int SizeBreakdownIndex { get; set; }

        public long RO_GarmentId { get; set; }
        [ForeignKey("RO_GarmentId")]
        public virtual RO_Garment RO_Garment { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
        public int ColorId { get; set; }
        [MaxLength(255)]
        public string ColorName { get; set; }
        public ICollection<RO_Garment_SizeBreakdown_Detail> RO_Garment_SizeBreakdown_Details { get; set; }
        public int Total { get; set; }
    }
}
