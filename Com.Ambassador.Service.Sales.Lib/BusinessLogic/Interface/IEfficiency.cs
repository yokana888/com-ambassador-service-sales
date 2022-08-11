using Com.Ambassador.Service.Sales.Lib.Models;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface
{
    public interface IEfficiency : IBaseFacade<Efficiency>
    {
        Task<Efficiency>  ReadModelByQuantity(int Quantity);
    }
}
