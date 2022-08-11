using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting
{
    public class FinishingPrintingPreSalesContractFacade : IFinishingPrintingPreSalesContractFacade
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<FinishingPrintingPreSalesContractModel> DbSet;
        private readonly FinishingPrintingPreSalesContractLogic finishingPrintingPreSalesContractLogic;

        public FinishingPrintingPreSalesContractFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<FinishingPrintingPreSalesContractModel>();
            finishingPrintingPreSalesContractLogic = serviceProvider.GetService<FinishingPrintingPreSalesContractLogic>();
        }

        public async Task<int> CreateAsync(FinishingPrintingPreSalesContractModel model)
        {
            finishingPrintingPreSalesContractLogic.Create(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await finishingPrintingPreSalesContractLogic.DeleteAsync(id);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> PreSalesPost(List<long> listId)
        {
            await finishingPrintingPreSalesContractLogic.PreSalesPost(listId);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> PreSalesUnpost(long id)
        {
            await finishingPrintingPreSalesContractLogic.PreSalesUnpost(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<FinishingPrintingPreSalesContractModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return finishingPrintingPreSalesContractLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<FinishingPrintingPreSalesContractModel> ReadByIdAsync(int id)
        {
            return await finishingPrintingPreSalesContractLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, FinishingPrintingPreSalesContractModel model)
        {
            finishingPrintingPreSalesContractLogic.UpdateAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
