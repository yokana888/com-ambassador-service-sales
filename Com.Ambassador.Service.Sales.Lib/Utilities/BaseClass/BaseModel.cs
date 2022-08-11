using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass
{
    public abstract class BaseModel : StandardEntity<long>
    {
        
        [MaxLength(255)]
        public string UId { get; set; } /* Object Id MongoDb */
    }
}
