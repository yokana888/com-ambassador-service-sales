using Com.Ambassador.Service.Sales.Lib.Models.GarmentPreSalesContractModel;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseInterface;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentPreSalesContractViewModels;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentPreSalesContractInterface
{
    public interface IGarmentPreSalesContract : IBaseFacade<GarmentPreSalesContract>
    {
        Task<int> Patch(long id, JsonPatchDocument<GarmentPreSalesContract> jsonPatch);
        Task<int> PreSalesPost(List<long> listId, string user);
        Task<int> PreSalesUnpost(long id, string user);
    }
}