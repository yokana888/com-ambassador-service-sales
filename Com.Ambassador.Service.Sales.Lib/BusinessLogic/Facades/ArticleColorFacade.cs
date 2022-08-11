using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic;
using Com.Ambassador.Service.Sales.Lib.Models;
using Com.Ambassador.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Threading.Tasks;
using Com.Ambassador.Service.Sales.Lib.Utilities;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades
{
    public class ArticleColorFacade : IArticleColor
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<ArticleColor> DbSet;
        private IdentityService IdentityService;
        private ArticleColorLogic ArticleColorLogic;

        public ArticleColorFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<ArticleColor>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.ArticleColorLogic = serviceProvider.GetService<ArticleColorLogic>();
        }

        public async Task<int> CreateAsync(ArticleColor model)
        {
            ArticleColorLogic.Create(model);

            return await DbContext.SaveChangesAsync();
        }

        public async Task<ArticleColor> ReadByIdAsync(int id)
        {
            return await ArticleColorLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, ArticleColor model)
        {
            ArticleColorLogic.UpdateAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await ArticleColorLogic.DeleteAsync(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<ArticleColor> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return ArticleColorLogic.Read(page, size, order, select, keyword, filter);
        }
    }
}
