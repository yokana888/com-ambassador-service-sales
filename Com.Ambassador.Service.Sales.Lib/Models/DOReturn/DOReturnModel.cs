using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Ambassador.Service.Sales.Lib.Models.DOReturn
{
    public class DOReturnModel : BaseModel
    {
        [MaxLength(255)]
        public string Code { get; set; }
        public long AutoIncreament { get; set; }
        [MaxLength(255)]
        public string DOReturnNo { get; set; }
        [MaxLength(255)]
        public string DOReturnType { get; set; }
        public DateTimeOffset DOReturnDate { get; set; }
        #region ReturnFrom
        public int ReturnFromId { get; set; }
        [MaxLength(255)]
        public string ReturnFromName { get; set; }
        #endregion
        [MaxLength(255)]
        public string LTKPNo { get; set; }
        [MaxLength(255)]
        public string HeadOfStorage { get; set; }
        [MaxLength(1000)]
        public string Remark { get; set; }
        public virtual ICollection<DOReturnDetailModel> DOReturnDetails { get; set; }

    }
}
