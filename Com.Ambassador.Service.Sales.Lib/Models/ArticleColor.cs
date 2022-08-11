using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models
{
    public class ArticleColor : BaseModel
    {
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(4000)]
        public string Description { get; set; }

    }
}
