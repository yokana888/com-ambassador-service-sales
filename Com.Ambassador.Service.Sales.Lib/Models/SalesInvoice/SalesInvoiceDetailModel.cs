using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Ambassador.Service.Sales.Lib.Models.SalesInvoice
{
    public class SalesInvoiceDetailModel : BaseModel
    {
        public int ShippingOutId { get; set; }
        [MaxLength(255)]
        public string BonNo { get; set; }

        public virtual SalesInvoiceModel SalesInvoiceModel { get; set; }
        public virtual ICollection<SalesInvoiceItemModel> SalesInvoiceItems { get; set; }
    }
}
