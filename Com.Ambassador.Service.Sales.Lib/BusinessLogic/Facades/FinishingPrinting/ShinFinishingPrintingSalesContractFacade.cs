using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting
{
    public class ShinFinishingPrintingSalesContractFacade : IShinFinishingPrintingSalesContractFacade
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<FinishingPrintingSalesContractModel> DbSet;
        private readonly ShinFinishingPrintingSalesContractLogic finishingPrintingSalesContractLogic;

        public ShinFinishingPrintingSalesContractFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<FinishingPrintingSalesContractModel>();
            finishingPrintingSalesContractLogic = serviceProvider.GetService<ShinFinishingPrintingSalesContractLogic>();
        }

        public async Task<int> CreateAsync(FinishingPrintingSalesContractModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (this.DbSet.Any(d => d.Code.Equals(model.Code)));

            finishingPrintingSalesContractLogic.Create(model);

            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await finishingPrintingSalesContractLogic.DeleteAsync(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<FinishingPrintingSalesContractModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return finishingPrintingSalesContractLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<FinishingPrintingSalesContractModel> ReadByIdAsync(int id)
        {
            return await finishingPrintingSalesContractLogic.ReadByIdAsync(id);
        }

        public Task<FinishingPrintingSalesContractModel> ReadParent(long id)
        {
            return finishingPrintingSalesContractLogic.ReadParent(id);
        }

        public async Task<int> UpdateAsync(int id, FinishingPrintingSalesContractModel model)
        {
            finishingPrintingSalesContractLogic.UpdateAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

    }
}
