using Com.Ambassador.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using System.Linq;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.DeliveryNoteProduction
{
    public class DeliveryNoteProductionFacade : IDeliveryNoteProduction
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<DeliveryNoteProductionModel> DbSet;
        private IdentityService identityService;
        private readonly IServiceProvider _serviceProvider;
        private DeliveryNoteProductionLogic deliveryNoteProductionLogic;
        public DeliveryNoteProductionFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            _serviceProvider = serviceProvider;
            this.DbSet = DbContext.Set<DeliveryNoteProductionModel>();
            this.identityService = serviceProvider.GetService<IdentityService>();
            this.deliveryNoteProductionLogic = serviceProvider.GetService<DeliveryNoteProductionLogic>();
        }

        public async Task<int> CreateAsync(DeliveryNoteProductionModel model)
        {
            int result = 0;
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    int index = 0;
                    do
                    {
                        model.Code = CodeGenerator.Generate();
                        model.MonthandYear = model.Month + " " + model.Year;
                    }   
                    while (DbSet.Any(d => d.Code.Equals(model.Code)));

                    //DOSalesNumberGenerator(model, index);
                    deliveryNoteProductionLogic.Create(model);
                    index++;

                    result = await DbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return result;
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    DeliveryNoteProductionModel model = await deliveryNoteProductionLogic.ReadByIdAsync(id);
                    if (model != null)
                    {
                        DeliveryNoteProductionModel doSalesModel = new DeliveryNoteProductionModel();
                        doSalesModel = model;
                        await deliveryNoteProductionLogic.DeleteAsync(id);
                    }
                }
                catch (Exception e)
                {

                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<DeliveryNoteProductionModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return deliveryNoteProductionLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<DeliveryNoteProductionModel> ReadByIdAsync(int id)
        {
            return await deliveryNoteProductionLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, DeliveryNoteProductionModel model)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    model.MonthandYear = model.Month + " " + model.Year;
                    deliveryNoteProductionLogic.UpdateAsync(id, model);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return await DbContext.SaveChangesAsync();
        }


    }
}
