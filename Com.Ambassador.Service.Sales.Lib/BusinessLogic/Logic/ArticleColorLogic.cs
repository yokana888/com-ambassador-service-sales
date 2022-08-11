using Com.Ambassador.Service.Sales.Lib.Models;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic
{
    public class ArticleColorLogic : BaseLogic<ArticleColor>
    {
        public ArticleColorLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
        }

        public override ReadResponse<ArticleColor> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<ArticleColor> Query = this.DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "Name"
            };

            Query = QueryHelper<ArticleColor>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<ArticleColor>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "Name","Description"
            };

            Query = Query
                .Select(field => new ArticleColor
                {
                    Id = field.Id,
                    Name = field.Name,
                    Description = field.Description,
                    LastModifiedUtc = field.LastModifiedUtc
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<ArticleColor>.Order(Query, OrderDictionary);

            Pageable<ArticleColor> pageable = new Pageable<ArticleColor>(Query, page - 1, size);
            List<ArticleColor> data = pageable.Data.ToList<ArticleColor>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<ArticleColor>(data, totalData, OrderDictionary, SelectedFields);
        }
    }
}
