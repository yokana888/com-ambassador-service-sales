using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Ambassador.Service.Sales.Lib.Models.DOReturn
{
    public class DOReturnDetailModel : BaseModel
    {
        public int SalesInvoiceId { get; set; }
        [MaxLength(255)]
        public string SalesInvoiceNo { get; set; }

        public virtual DOReturnModel DOReturnModel { get; set; }
        public virtual ICollection<DOReturnDetailItemModel> DOReturnDetailItems { get; set; }
        public virtual ICollection<DOReturnItemModel> DOReturnItems { get; set; }
    }
}
