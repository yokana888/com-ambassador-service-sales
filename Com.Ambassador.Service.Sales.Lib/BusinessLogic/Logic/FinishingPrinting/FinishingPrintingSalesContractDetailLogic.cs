using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting
{
    public class FinishingPrintingSalesContractDetailLogic : BaseLogic<FinishingPrintingSalesContractDetailModel>
    {
        public FinishingPrintingSalesContractDetailLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService,serviceProvider, dbContext)
        {
        }

        public override ReadResponse<FinishingPrintingSalesContractDetailModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<long> GetFPSalesContractIds(long id)
        {
            return new HashSet<long>(DbSet.Where(d => d.FinishingPrintingSalesContract.Id == id).Select(d => d.Id));
        }

        public override void UpdateAsync(long id, FinishingPrintingSalesContractDetailModel model)
        {
            try
            {
                EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
                DbSet.Update(model);
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public override async Task DeleteAsync(long id)
        {
            try
            {
                var model = await ReadByIdAsync(id);

                EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
                DbSet.Update(model);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
