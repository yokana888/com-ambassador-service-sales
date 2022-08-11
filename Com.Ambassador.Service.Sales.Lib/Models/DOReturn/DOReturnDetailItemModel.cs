using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Ambassador.Service.Sales.Lib.Models.DOReturn
{
    public class DOReturnDetailItemModel : BaseModel
    {
        public int DOSalesId { get; set; }
        [MaxLength(255)]
        public string DOSalesNo { get; set; }

        public virtual DOReturnDetailModel DOReturnDetailModel { get; set; }
    }
}
