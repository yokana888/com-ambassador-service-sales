using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System.ComponentModel.DataAnnotations;

namespace Com.Ambassador.Service.Sales.Lib.Models.DOSales
{
    public class DOSalesDetailModel : BaseModel
    {
        #region ProductionOrder
        public int ProductionOrderId { get; set; }
        [MaxLength(64)]
        public string ProductionOrderNo { get; set; }
        #endregion
        #region Material
        public long MaterialId { get; set; }

        [MaxLength(255)]
        public string MaterialCode { get; set; }

        [MaxLength(1000)]
        public string MaterialName { get; set; }

        public double MaterialPrice { get; set; }

        [MaxLength(255)]
        public string MaterialTags { get; set; }
        #endregion
        #region Material Construction
        public long MaterialConstructionId { get; set; }
        [MaxLength(1000)]
        public string MaterialConstructionName { get; set; }
        [MaxLength(255)]
        public string MaterialConstructionCode { get; set; }
        public string MaterialConstructionRemark { get; set; }
        #endregion
        [MaxLength(1000)]
        public string MaterialWidth { get; set; }
        [MaxLength(1000)]
        public string ConstructionName { get; set; }
        [MaxLength(255)]
        public string ColorRequest { get; set; }
        [MaxLength(255)]
        public string ColorTemplate { get; set; }
        [MaxLength(512)]
        public string UnitOrCode { get; set; }
        public double Packing { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public double ConvertionValue { get; set; }
        [MaxLength(255)]
        public string NoSOP { get; set; }
        [MaxLength(255)]
        public string ThreadNumber { get; set; }
        [MaxLength(255)]
        public string Grade { get; set; }

        [MaxLength(128)]
        public string AvalType { get; set; }

        public virtual DOSalesModel DOSalesModel { get; set; }
    }
}
