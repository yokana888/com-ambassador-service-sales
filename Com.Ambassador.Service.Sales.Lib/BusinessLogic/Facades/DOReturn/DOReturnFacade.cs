using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.DOReturn;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.DOReturn;
using Com.Ambassador.Service.Sales.Lib.Models.DOReturn;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.DOReturn
{
    public class DOReturnFacade : IDOReturnContract
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<DOReturnModel> DbSet;
        private IdentityService identityService;
        private readonly IServiceProvider _serviceProvider;
        private DOReturnLogic doReturnLogic;
        public DOReturnFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            _serviceProvider = serviceProvider;
            this.DbSet = DbContext.Set<DOReturnModel>();
            this.identityService = serviceProvider.GetService<IdentityService>();
            doReturnLogic = serviceProvider.GetService<DOReturnLogic>();
        }

        public async Task<int> CreateAsync(DOReturnModel model)
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

                    DOReturnNumberGenerator(model, index);
                    doReturnLogic.Create(model);
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
                    DOReturnModel model = await doReturnLogic.ReadByIdAsync(id);
                    if (model != null)
                    {
                        DOReturnModel doReturnModel = new DOReturnModel();
                        doReturnModel = model;
                        await doReturnLogic.DeleteAsync(id);
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

        public ReadResponse<DOReturnModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return doReturnLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<DOReturnModel> ReadByIdAsync(int id)
        {
            return await doReturnLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, DOReturnModel model)
        {
            doReturnLogic.UpdateAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

        private void DOReturnNumberGenerator(DOReturnModel model, int index)
        {

            index = 0;
            int YearNow = DateTime.Now.Year;
            var YearNowString = DateTime.Now.ToString("yy");

            DOReturnModel lastLocalData = DbSet.IgnoreQueryFilters().Where(w => w.DOReturnType.Equals(model.DOReturnType)).OrderByDescending(o => o.AutoIncreament).FirstOrDefault();

            if (lastLocalData == null)
            {
                model.AutoIncreament = 1 + index;
                model.DOReturnNo = $"{YearNowString}{model.DOReturnType}{model.AutoIncreament.ToString().PadLeft(6, '0')}";
            }
            else
            {
                if (YearNow > lastLocalData.CreatedUtc.Year)
                {
                    model.AutoIncreament = 1 + index;
                    model.DOReturnNo = $"{YearNowString}{model.DOReturnType}{model.AutoIncreament.ToString().PadLeft(6, '0')}";
                }
                else
                {
                    model.AutoIncreament = lastLocalData.AutoIncreament + (1 + index);
                    model.DOReturnNo = $"{YearNowString}{model.DOReturnType}{model.AutoIncreament.ToString().PadLeft(6, '0')}";
                }
            }
        }
    }
}
