using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrintingCostCalculation
{
    public interface IFinishingPrintingCostCalculationService : IBaseFacade<FinishingPrintingCostCalculationModel>
    {
        Task<int> CCPost(List<long> listId);
        Task<FinishingPrintingCostCalculationModel> ReadParent(long id);
        Task<int> CCApproveMD(long id);
        Task<int> CCApprovePPIC(long id);
        Task<bool> ValidatePreSalesContractId(long id);
        ReadResponse<FinishingPrintingCostCalculationModel> GetByPreSalesContract(long preSalesContractId);
    }
}
