using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentSalesContractInterface;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentSalesContractLogics;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentSalesContractModel;
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

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentSalesContractFacades
{
    public class GarmentSalesContractFacade : IGarmentSalesContract
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<GarmentSalesContract> DbSet;
        private readonly IdentityService identityService;
        private readonly GarmentSalesContractLogic garmentSalesContractLogic;
        private readonly ICostCalculationGarment costCalGarmentLogic;

        public GarmentSalesContractFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<GarmentSalesContract>();
            identityService = serviceProvider.GetService<IdentityService>();
            garmentSalesContractLogic = serviceProvider.GetService<GarmentSalesContractLogic>();
            costCalGarmentLogic = serviceProvider.GetService<ICostCalculationGarment>();
        }

        public async Task<int> CreateAsync(GarmentSalesContract model)
        {
            //do
            //{
            //    model.Code = CodeGenerator.Generate();
            //}
            //while (this.DbSet.Any(d => d.Code.Equals(model.Code)));
            CostCalculationGarment costCal = await costCalGarmentLogic.ReadByIdAsync(model.CostCalculationId); //await DbContext.CostCalculationGarments.FirstOrDefaultAsync(a => a.Id.Equals(model.CostCalculationId));
            //costCal.SCGarmentId=
            garmentSalesContractLogic.Create(model);

            int result =  await DbContext.SaveChangesAsync();
            return result += await UpdateCostCalAsync(costCal, (int)model.Id);
        }

        public async Task<int> UpdateCostCalAsync(CostCalculationGarment costCalculationGarment, int Id)
        {
            costCalculationGarment.SCGarmentId = Id;
            int result = await costCalGarmentLogic.UpdateAsync((int)costCalculationGarment.Id, costCalculationGarment);

            return result += await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            GarmentSalesContract sc = await ReadByIdAsync(id);
            CostCalculationGarment costCal = await DbContext.CostCalculationGarments.Include(cc => cc.CostCalculationGarment_Materials).FirstOrDefaultAsync(a => a.Id.Equals(sc.CostCalculationId));
            costCal.SCGarmentId = null;
            await costCalGarmentLogic.UpdateAsync((int)sc.CostCalculationId, costCal);
            await garmentSalesContractLogic.DeleteAsync(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<GarmentSalesContract> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return garmentSalesContractLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<GarmentSalesContract> ReadByIdAsync(int id)
        {
            return await garmentSalesContractLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, GarmentSalesContract model)
        {
            garmentSalesContractLogic.UpdateAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> UpdatePrinted(int id, GarmentSalesContract model)
        {
            //garmentSalesContractLogic.UpdateAsync(id, model);
            model.DocPrinted = true;
            DbSet.Update(model);
            return await DbContext.SaveChangesAsync();
        }

        public GarmentSalesContract ReadByCostCal(int id)
        {
            return  garmentSalesContractLogic.ReadByCostCal(id);
        }
    }
}
