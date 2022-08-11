using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.SalesInvoice;
using System.Collections.Generic;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.DOReturn
{
    public class DOReturnDetailViewModel : BaseViewModel
    {
        public SalesInvoiceViewModel SalesInvoice { get; set; }

        public virtual ICollection<DOReturnDetailItemViewModel> DOReturnDetailItems { get; set; }
        public virtual ICollection<DOReturnItemViewModel> DOReturnItems { get; set; }
    }
}
