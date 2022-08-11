using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.GarmentSewingBlockingPlanInterfaces;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.GarmentSewingBlockingPlanLogics;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentBookingOrderModel;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.GarmentSewingBlockingPlanFacades
{
    public class GarmentSewingBlockingPlanFacade : IGarmentSewingBlockingPlan
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<GarmentSewingBlockingPlan> DbSet;
        private readonly IdentityService identityService;
        private readonly GarmentSewingBlockingPlanLogic garmentSewingBlockingPlanLogic;
        public IServiceProvider ServiceProvider;

        public GarmentSewingBlockingPlanFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<GarmentSewingBlockingPlan>();
            identityService = serviceProvider.GetService<IdentityService>();
            garmentSewingBlockingPlanLogic = serviceProvider.GetService<GarmentSewingBlockingPlanLogic>();
            ServiceProvider = serviceProvider;
        }
        public async Task<int> CreateAsync(GarmentSewingBlockingPlan model)
        {
            garmentSewingBlockingPlanLogic.Create(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await garmentSewingBlockingPlanLogic.DeleteAsync(id);

            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<GarmentSewingBlockingPlan> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return garmentSewingBlockingPlanLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<GarmentSewingBlockingPlan> ReadByIdAsync(int id)
        {
            return await garmentSewingBlockingPlanLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, GarmentSewingBlockingPlan model)
        {
            garmentSewingBlockingPlanLogic.UpdateAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

    }
}
