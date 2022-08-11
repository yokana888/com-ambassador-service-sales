using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Ambassador.Service.Sales.Lib.Models;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic;
using System.Linq;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades
{
    public class EfficiencyFacade : IEfficiency
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<Efficiency> DbSet;
        private readonly IdentityService identityService;
        private readonly EfficiencyLogic efficiencyLogic;
        public IServiceProvider ServiceProvider;
        public EfficiencyFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<Efficiency>();
            identityService = serviceProvider.GetService<IdentityService>();
            efficiencyLogic = serviceProvider.GetService<EfficiencyLogic>();
            ServiceProvider = serviceProvider;
        }
        public async Task<int> CreateAsync(Efficiency model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (this.DbSet.Any(d => d.Code.Equals(model.Code)));
            model.Value /= 100;
            efficiencyLogic.Create(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await efficiencyLogic.DeleteAsync(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<Efficiency> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return efficiencyLogic.Read(page, size, order, select, keyword, filter);
        }
        public async Task<Efficiency> ReadByIdAsync(int id)
        {
            Efficiency read = await this.DbSet
               .Where(d => d.Id.Equals(id) && d.IsDeleted.Equals(false))
               .FirstOrDefaultAsync();
            return read;
        }

        public async Task<int> UpdateAsync(int id, Efficiency model)
        {
            model.Value /= 100;
            efficiencyLogic.UpdateAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<Efficiency> ReadModelByQuantity(int Quantity)
        {
            Efficiency result = await this.DbSet
                .FirstOrDefaultAsync(eff => Quantity > 0 && eff.InitialRange <= Quantity && eff.FinalRange >= Quantity && eff.IsDeleted == false);
            if (result == null)
            {
                return new Efficiency()
                {
                    Id = 0,
                    Value = 0
                };
            }
            else
            {
                result.Value *= 100;
            }
            return result;
        }
    }
}
