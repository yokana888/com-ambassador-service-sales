using Com.Ambassador.Service.Sales.Lib.Models.ROGarments;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.ROGarmentInterface
{
    public interface IROGarment : IBaseFacade<RO_Garment>
    {
        Task<int> PostRO(List<long> listId);
        Task<int> UnpostRO(long id);
    }
}