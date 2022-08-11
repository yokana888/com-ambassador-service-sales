using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting
{
    public class FinishingPrintingPreSalesContractModel : BaseModel
    {
        [MaxLength(64)]
        public string No { get; set; }

        public DateTimeOffset Date { get; set; }

        [MaxLength(32)]
        public string Type { get; set; }

        public int BuyerId { get; set; }

        [MaxLength(128)]
        public string BuyerCode { get; set; }

        [MaxLength(512)]
        public string BuyerName { get; set; }

        [MaxLength(512)]
        public string BuyerType { get; set; }

        public int UnitId { get; set; }
        
        [MaxLength(128)]
        public string UnitCode { get; set; }
        
        [MaxLength(512)]
        public string UnitName { get; set; }

        public int ProcessTypeId { get; set; }

        [MaxLength(128)]
        public string ProcessTypeCode { get; set; }

        [MaxLength(512)]
        public string ProcessTypeName { get; set; }

        public int OrderTypeId { get; set; }

        [MaxLength(128)]
        public string OrderTypeCode { get; set; }

        [MaxLength(512)]
        public string OrderTypeName { get; set; }

        public double OrderQuantity { get; set; }

        [MaxLength(4096)]
        public string Remark { get; set; }

        public bool IsPosted { get; set; }

    }
}
