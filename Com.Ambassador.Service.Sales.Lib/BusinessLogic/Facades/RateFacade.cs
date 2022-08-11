using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic;
using Com.Ambassador.Service.Sales.Lib.Models;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades
{
    public class RateFacade: IRate
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<Rate> DbSet;
        private readonly IdentityService identityService;
        private readonly RateLogic rateLogic;
        public IServiceProvider ServiceProvider;
        public RateFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<Rate>();
            identityService = serviceProvider.GetService<IdentityService>();
            rateLogic = serviceProvider.GetService<RateLogic>();
            ServiceProvider = serviceProvider;
        }
        public async Task<int> CreateAsync(Rate model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (this.DbSet.Any(d => d.Code.Equals(model.Code)));
            rateLogic.Create(model);
            return await DbContext.SaveChangesAsync();
        }
 
        public async Task<int> DeleteAsync(int id)
        {
            await rateLogic.DeleteAsync(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<Rate> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return rateLogic.Read(page, size, order, select, keyword, filter);
        }
        public async Task<Rate> ReadByIdAsync(int id)
        {
            Rate read = await this.DbSet
               .Where(d => d.Id.Equals(id) && d.IsDeleted.Equals(false))
               .FirstOrDefaultAsync();
            return read;
        }

        public async Task<int> UpdateAsync(int id, Rate model)
        {
            rateLogic .UpdateAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

    }
}
