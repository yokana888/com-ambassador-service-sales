using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.DOStock;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.DOStock;
using Com.Ambassador.Service.Sales.Lib.Models.DOSales;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.DOStock
{
    public class DOStockFacade : IDOStockFacade
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<DOSalesModel> DbSet;
        private readonly IServiceProvider _serviceProvider;
        private DOStockLogic doStockLogic;

        public DOStockFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            _serviceProvider = serviceProvider;
            DbSet = DbContext.Set<DOSalesModel>();
            doStockLogic = serviceProvider.GetService<DOStockLogic>();
        }

        public Task<int> CreateAsync(DOSalesModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(d => d.Code.Equals(model.Code)));

            DOSalesNumberGenerator(model);
            doStockLogic.Create(model);

            return DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await doStockLogic.DeleteAsync(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<DOSalesModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return doStockLogic.Read(page, size, order, select, keyword, filter);
        }

        public Task<DOSalesModel> ReadByIdAsync(int id)
        {
            return doStockLogic.ReadByIdAsync(id);
        }

        public Task<int> UpdateAsync(int id, DOSalesModel model)
        {
            doStockLogic.UpdateAsync(id, model);
            return DbContext.SaveChangesAsync();
        }

        private void DOSalesNumberGenerator(DOSalesModel model)
        {
            int YearNow = DateTime.Now.Year;
            var YearNowString = DateTime.Now.ToString("yy");

            DOSalesModel lastLocalData = DbSet.IgnoreQueryFilters().Where(w => w.Type.Equals(model.Type)).OrderByDescending(o => o.CreatedUtc).FirstOrDefault();

            if (lastLocalData == null)
            {
                model.AutoIncreament = 1;
                model.DOSalesNo = $"{YearNowString}{model.Type}{model.AutoIncreament.ToString().PadLeft(6, '0')}";
            }
            else
            {
                if (YearNow > lastLocalData.CreatedUtc.Year)
                {
                    model.AutoIncreament = 1;
                    model.DOSalesNo = $"{YearNowString}{model.Type}{model.AutoIncreament.ToString().PadLeft(6, '0')}";
                }
                else
                {
                    model.AutoIncreament = lastLocalData.AutoIncreament + 1;
                    model.DOSalesNo = $"{YearNowString}{model.Type}{model.AutoIncreament.ToString().PadLeft(6, '0')}";
                }
            }
        }
    }
}
