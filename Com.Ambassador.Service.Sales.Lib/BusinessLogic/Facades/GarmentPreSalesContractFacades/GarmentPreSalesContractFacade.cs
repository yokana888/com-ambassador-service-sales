using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentPreSalesContractInterface;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentSalesContractLogics;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentPreSalesContractModel;
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
using Com.Ambassador.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.CostCalculationGarmentLogic;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentPreSalesContractLogics;
using System.Linq;
using Com.Moonlay.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentPreSalesContractFacades
{
    public class GarmentPreSalesContractFacade : IGarmentPreSalesContract
    {
        private string USER_AGENT = "Facade";

        private readonly SalesDbContext DbContext;
        private readonly DbSet<GarmentPreSalesContract> DbSet;
        private readonly IdentityService identityService;
        private readonly GarmentPreSalesContractLogic garmentPreSalesContractLogic;

        public GarmentPreSalesContractFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<GarmentPreSalesContract>();
            identityService = serviceProvider.GetService<IdentityService>();
            garmentPreSalesContractLogic = serviceProvider.GetService<GarmentPreSalesContractLogic>();
        }

        public async Task<int> CreateAsync(GarmentPreSalesContract model)
        {
            garmentPreSalesContractLogic.Create(model);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<GarmentPreSalesContract> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return garmentPreSalesContractLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<GarmentPreSalesContract> ReadByIdAsync(int id)
        {
            return await garmentPreSalesContractLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, GarmentPreSalesContract model)
        {
            garmentPreSalesContractLogic.UpdateAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await garmentPreSalesContractLogic.DeleteAsync(id);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> Patch(long id, JsonPatchDocument<GarmentPreSalesContract> jsonPatch)
        {
            int Updated = 0;

            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    garmentPreSalesContractLogic.Patch(id, jsonPatch);
                    Updated = await DbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }

            return Updated;
        }

        public async Task<int> PreSalesPost(List<long> listId, string user)
        {
            int Updated = 0;

            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var listData = DbSet.
                        Where(w => listId.Contains(w.Id))
                        .ToList();

                    foreach (var data in listData)
                    {
                        EntityExtension.FlagForUpdate(data, user, USER_AGENT);
                        data.IsPosted = true;
                    }

                    Updated = await DbContext.SaveChangesAsync();

                    if (Updated < 1)
                    {
                        throw new Exception("No data updated");
                    }

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return Updated;
        }

        public async Task<int> PreSalesUnpost(long id, string user)
        {
            int Updated = 0;

            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var data = DbSet
                        .Where(w => w.Id == id)
                        .Single();

                    EntityExtension.FlagForUpdate(data, user, USER_AGENT);
                    data.IsPosted = false;

                    Updated = await DbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return Updated;
        }
    }
}