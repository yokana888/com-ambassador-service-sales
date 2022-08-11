using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System.ComponentModel.DataAnnotations;

namespace Com.Ambassador.Service.Sales.Lib.Models.DOReturn
{
    public class DOReturnItemModel : BaseModel
    {
        public int ShippingOutId { get; set; }
        [MaxLength(255)]
        public string BonNo { get; set; }
        public int ProductId { get; set; }
        [MaxLength(255)]
        public string ProductCode { get; set; }
        [MaxLength(255)]
        public string ProductName { get; set; }
        [MaxLength(255)]
        public double QuantityPacking { get; set; }
        [MaxLength(255)]
        public string PackingUom { get; set; }
        [MaxLength(255)]
        public string ItemUom { get; set; }
        public double QuantityItem { get; set; }

        public virtual DOReturnDetailModel DOReturnDetailModel { get; set; }
    }
}
