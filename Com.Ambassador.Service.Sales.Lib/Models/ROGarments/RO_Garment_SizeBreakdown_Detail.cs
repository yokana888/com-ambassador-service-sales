using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.ROGarments
{
    public class RO_Garment_SizeBreakdown_Detail : BaseModel
    {
        public int SizeBreakdownDetailIndex { get; set; }

        public long RO_Garment_SizeBreakdownId { get; set; }
        [ForeignKey("RO_Garment_SizeBreakdownId")]
        public virtual RO_Garment_SizeBreakdown RO_Garment_SizeBreakdown { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
        [MaxLength(3000)]
        public string Information { get; set; }
        [MaxLength(50)]
        public string Size { get; set; }
        public int Quantity { get; set; }
    }
}
