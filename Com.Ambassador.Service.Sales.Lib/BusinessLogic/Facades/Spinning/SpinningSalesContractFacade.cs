using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.Spinning;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.Spinning;
using Com.Ambassador.Service.Sales.Lib.Models.Spinning;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.Spinning
{
    public class SpinningSalesContractFacade : ISpinningSalesContract
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<SpinningSalesContractModel> DbSet;
        private IdentityService IdentityService;
        private SpinningSalesContractLogic SpinningSalesContractLogic;
        public SpinningSalesContractFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<SpinningSalesContractModel>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.SpinningSalesContractLogic = serviceProvider.GetService<SpinningSalesContractLogic>();
        }
        public async Task<int> CreateAsync(SpinningSalesContractModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (this.DbSet.Any(d => d.Code.Equals(model.Code)));

            var data = await CustomCodeGenerator(model);
            SpinningSalesContractLogic.Create(data);

            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await SpinningSalesContractLogic.DeleteAsync(id);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<SpinningSalesContractModel> ReadByIdAsync(int id)
        {
            return await SpinningSalesContractLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, SpinningSalesContractModel model)
        {
            SpinningSalesContractLogic.UpdateAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<SpinningSalesContractModel> CustomCodeGenerator(SpinningSalesContractModel Model)
        {

            string type = Model.BuyerType.ToLower().Equals("ekspor") || Model.BuyerType.ToLower().Equals("export") ? "SPE" : "SPL";

            var lastData = await this.DbSet.IgnoreQueryFilters().Where(w => w.BuyerType.Equals(Model.BuyerType, StringComparison.OrdinalIgnoreCase)&& w.IsDeleted==false).OrderByDescending(o => o.CreatedUtc).FirstOrDefaultAsync();

            DateTime Now = DateTime.Now;
            string Year = Now.ToString("yyyy");
            string month = Now.ToString("MM");

            if (lastData == null)
            {
                Model.AutoIncrementNumber = 1;
                string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
                Model.SalesContractNo = $"{Number}/{type}/{month}.{Year}";
            }
            else
            {
                if (lastData.CreatedUtc.Year < Now.Year)
                {
                    Model.AutoIncrementNumber = 1;
                    string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
                    Model.SalesContractNo = $"{Number}/{type}/{month}.{Year}";
                }
                else
                {
                    Model.AutoIncrementNumber = lastData.AutoIncrementNumber + 1;
                    string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
                    Model.SalesContractNo = $"{Number}/{type}/{month}.{Year}";
                }
            }
            return Model;
        }

        public ReadResponse<SpinningSalesContractModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return SpinningSalesContractLogic.Read(page, size, order, select, keyword, filter);
        }
    }
}
