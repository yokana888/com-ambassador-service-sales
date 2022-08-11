using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.GarmentPreSalesContractModel
{
    public class GarmentPreSalesContract : BaseModel
    {
        [MaxLength(50)]
        public string SCNo { get; set; }
        public DateTimeOffset SCDate { get; set; }
        [MaxLength(25)]
        public string SCType { get; set; }
        public int SectionId { get; set; }
        [MaxLength(25)]
        public string SectionCode { get; set; }
        public int BuyerAgentId { get; set; }
        [MaxLength(25)]
        public string BuyerAgentCode { get; set; }
        [MaxLength(100)]
        public string BuyerAgentName { get; set; }
        public int BuyerBrandId { get; set; }
        [MaxLength(25)]
        public string BuyerBrandCode { get; set; }
        [MaxLength(100)]
        public string BuyerBrandName { get; set; }
        public int OrderQuantity { get; set; }
        public string Remark { get; set; }
        public bool IsCC { get; set; }
        public bool IsPR { get; set; }
        public bool IsPosted { get; set; }
    }
}