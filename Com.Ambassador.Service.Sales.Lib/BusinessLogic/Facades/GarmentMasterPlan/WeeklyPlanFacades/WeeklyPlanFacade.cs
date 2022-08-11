using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.Garment.WeeklyPlanInterfaces;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.WeeklyPlanLogics;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.WeeklyPlanFacades
{
    public class WeeklyPlanFacade : IWeeklyPlanFacade
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<GarmentWeeklyPlan> DbSet;
        private IdentityService IdentityService;
        private WeeklyPlanLogic WeeklyPlanLogic;

        public WeeklyPlanFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<GarmentWeeklyPlan>();
            IdentityService = serviceProvider.GetService<IdentityService>();
            WeeklyPlanLogic = serviceProvider.GetService<WeeklyPlanLogic>();
        }

        public async Task<int> CreateAsync(GarmentWeeklyPlan model)
        {
            WeeklyPlanLogic.Create(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await WeeklyPlanLogic.DeleteAsync(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<GarmentWeeklyPlan> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return WeeklyPlanLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<GarmentWeeklyPlan> ReadByIdAsync(int id)
        {
            return await WeeklyPlanLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, GarmentWeeklyPlan model)
        {
            WeeklyPlanLogic.UpdateAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public List<string> GetYears(string keyword)
        {
            return WeeklyPlanLogic.GetYears(keyword);
        }

        public GarmentWeeklyPlanItem GetWeekById(long id)
        {
            return WeeklyPlanLogic.GetWeekById(id);
        }
        
    }
}
