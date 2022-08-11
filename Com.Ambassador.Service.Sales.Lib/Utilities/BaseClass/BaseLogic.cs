using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseInterface;
using System.Linq;

namespace Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass
{
    public abstract class BaseLogic<TModel> : IBaseLogic<TModel>
       where TModel : BaseModel
    {
        protected DbSet<TModel> DbSet;
        protected IIdentityService IdentityService;

        public BaseLogic(IIdentityService IdentityService,IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbSet = dbContext.Set<TModel>();
            this.IdentityService = IdentityService;
        }

        public BaseLogic(IIdentityService IdentityService, SalesDbContext dbContext)
        {
            this.DbSet = dbContext.Set<TModel>();
            this.IdentityService = IdentityService;
        }

        public abstract ReadResponse<TModel> Read(int page, int size, string order, List<string> select, string keyword, string filter);

        public virtual void Create(TModel model)
        {
            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            DbSet.Add(model);
        }

        public virtual Task<TModel> ReadByIdAsync(long id)
        {
            return DbSet.FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
        }

        public virtual void UpdateAsync(long id, TModel model)
        {
            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            DbSet.Update(model);
        }

        public virtual async Task DeleteAsync(long id)
        {
            TModel model = await ReadByIdAsync(id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            DbSet.Update(model);
        }
    }
}
