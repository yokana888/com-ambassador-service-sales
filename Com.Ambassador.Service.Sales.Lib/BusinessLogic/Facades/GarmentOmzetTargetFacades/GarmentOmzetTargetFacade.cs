
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentOmzetTargetInterface;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentOmzetTargetLogics;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentOmzetTargetModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Com.Ambassador.Service.Purchasing.Lib.Interfaces;
using Com.Ambassador.Service.Sales.Lib.Helpers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentOmzetTargetFacades
{
    public class GarmentOmzetTargetFacade : IGarmentOmzetTarget
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<GarmentOmzetTarget> DbSet;
        private readonly IdentityService identityService;
        private readonly GarmentOmzetTargetLogic garmentOmzetTargetLogic;

        public GarmentOmzetTargetFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<GarmentOmzetTarget>();
            identityService = serviceProvider.GetService<IdentityService>();
            garmentOmzetTargetLogic = serviceProvider.GetService<GarmentOmzetTargetLogic>();
        }

        public async Task<int> CreateAsync(GarmentOmzetTarget model)
        {
            garmentOmzetTargetLogic.Create(model);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<GarmentOmzetTarget> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return garmentOmzetTargetLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<GarmentOmzetTarget> ReadByIdAsync(int id)
        {
            return await garmentOmzetTargetLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, GarmentOmzetTarget model)
        {
            garmentOmzetTargetLogic.UpdateAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await garmentOmzetTargetLogic.DeleteAsync(id);
            return await DbContext.SaveChangesAsync();
        }
    }
}