using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting
{
    public class FinishingPrintingSalesContractDetailModel : BaseModel
    {
        public virtual FinishingPrintingSalesContractModel FinishingPrintingSalesContract { get; set; }

        public long CostCalculationId { get; set; }

        [MaxLength(64)]
        public string ProductionOrderNo { get; set; }

        [MaxLength(255)]
        public string Color { get; set; }
        #region Currency
        public int CurrencyID { get; set; }
        [MaxLength(25)]
        public string CurrencyCode { get; set; }
        [MaxLength(25)]
        public string CurrencySymbol { get; set; }
        public double CurrencyRate { get; set; }
        #endregion
        public double Price { get; set; }

        public decimal ScreenCost { get; set; }
        public bool UseIncomeTax { get; set; }
    }
}
