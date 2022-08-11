using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.ComponentModel.DataAnnotations;

namespace Com.Ambassador.Service.Sales.Lib
{
    public class DeliveryNoteProductionModel : BaseModel
    {
        [MaxLength(255)]
        public string Code { get; set; }
        public DateTimeOffset Date { get; set; }
        [MaxLength(255)]
        public string SalesContractNo { get; set; }
        [MaxLength(255)]
        public string Unit { get; set; }
        [MaxLength(255)]
        public string Subject { get; set; }
        [MaxLength(255)]
        public string OtherSubject { get; set; }
        [MaxLength(255)]
        public string BuyerName { get; set; }
        [MaxLength(255)]
        public string BuyerType { get; set; }
        [MaxLength(255)]
        public string TypeandNumber { get; set; }
        
        public double Total { get; set; }
        [MaxLength(255)]
        public string Uom { get; set; }
        [MaxLength(255)]
        public string Blended { get; set; }
        [MaxLength(255)]
        public string DeliveredTo { get; set; }
        [MaxLength(255)]
        public string Month { get; set; }
        [MaxLength(255)]
        public string Year { get; set; }
        [MaxLength(255)]
        public string BallMark { get; set; }
        [MaxLength(255)]
        public string Sample { get; set; }
        [MaxLength(255)]
        public string Remark { get; set; }
        [MaxLength(255)]
        public string YarnSales { get; set; }
        [MaxLength(255)]
        public string MonthandYear { get; set; }

    }
}