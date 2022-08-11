using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.MaxWHConfirmInterfaces;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MaxWHConfirmLogics;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentMasterPlan.MaxWHConfirmModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.MaxWHConfirmFacades
{
    public class MaxWHConfirmFacade : IMaxWHConfirmFacade
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<MaxWHConfirm> DbSet;
        private IdentityService IdentityService;
        private MaxWHConfirmLogic MaxWHConfirmLogic;

        public MaxWHConfirmFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<MaxWHConfirm>();
            IdentityService = serviceProvider.GetService<IdentityService>();
            MaxWHConfirmLogic = serviceProvider.GetService<MaxWHConfirmLogic>();
        }
        public async Task<int> CreateAsync(MaxWHConfirm model)
        {
            MaxWHConfirmLogic.Create(model);
            return await DbContext.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public ReadResponse<MaxWHConfirm> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return MaxWHConfirmLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<MaxWHConfirm> ReadByIdAsync(int id)
        {
            return await MaxWHConfirmLogic.ReadByIdAsync(id);
        }

        public Task<int> UpdateAsync(int id, MaxWHConfirm model)
        {
            throw new NotImplementedException();
        }
    }
}
