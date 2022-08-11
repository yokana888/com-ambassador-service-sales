using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.DOSales;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.DOSales;
using Com.Ambassador.Service.Sales.Lib.Models.DOSales;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.DOSales
{
    public class DOSalesFacade : IDOSalesContract
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<DOSalesModel> DbSet;
        private IdentityService identityService;
        private readonly IServiceProvider _serviceProvider;
        private DOSalesLogic doSalesLogic;
        public DOSalesFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            _serviceProvider = serviceProvider;
            this.DbSet = DbContext.Set<DOSalesModel>();
            this.identityService = serviceProvider.GetService<IdentityService>();
            this.doSalesLogic = serviceProvider.GetService<DOSalesLogic>();
        }

        public async Task<int> CreateAsync(DOSalesModel model)
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
                    }
                    while (DbSet.Any(d => d.Code.Equals(model.Code)));

                    DOSalesNumberGenerator(model, index);
                    doSalesLogic.Create(model);
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
                    DOSalesModel model = await doSalesLogic.ReadByIdAsync(id);
                    if (model != null)
                    {
                        DOSalesModel doSalesModel = new DOSalesModel();
                        doSalesModel = model;
                        await doSalesLogic.DeleteAsync(id);
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

        public ReadResponse<DOSalesModel> ReadDPAndStock(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return doSalesLogic.ReadDPAndStock(page, size, order, select, keyword, filter);
        }

        public ReadResponse<DOSalesModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return doSalesLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<DOSalesModel> ReadByIdAsync(int id)
        {
            return await doSalesLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, DOSalesModel model)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    doSalesLogic.UpdateAsync(id, model);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return await DbContext.SaveChangesAsync();
        }

        private void DOSalesNumberGenerator(DOSalesModel model, int index)
        {

            index = 0;
            int YearNow = DateTime.Now.Year;
            var YearNowString = DateTime.Now.ToString("yy");

            DOSalesModel lastLocalData = DbSet.IgnoreQueryFilters().Where(w => w.Type.Equals(model.Type)).OrderByDescending(i=> i.CreatedUtc).FirstOrDefault();

            if (lastLocalData == null)
            {
                model.AutoIncreament = 1 + index;
                model.DOSalesNo = $"{YearNowString}{model.Type}{model.AutoIncreament.ToString().PadLeft(6, '0')}";
            }
            else
            {
                if (YearNow > lastLocalData.CreatedUtc.Year)
                {
                    model.AutoIncreament = 1 + index;
                    model.DOSalesNo = $"{YearNowString}{model.Type}{model.AutoIncreament.ToString().PadLeft(6, '0')}";
                }
                else
                {
                    model.AutoIncreament = lastLocalData.AutoIncreament + (1 + index);
                    model.DOSalesNo = $"{YearNowString}{model.Type}{model.AutoIncreament.ToString().PadLeft(6, '0')}";
                }
            }
        }
    }
}
