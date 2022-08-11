using Com.Ambassador.Service.Sales.Lib.Models.ROGarments;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.ROGarmentLogics
{
    public class ROGarmentSizeBreakdownDetailLogic : BaseLogic<RO_Garment_SizeBreakdown_Detail>
    {
        public ROGarmentSizeBreakdownDetailLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {

        }
        public override ReadResponse<RO_Garment_SizeBreakdown_Detail> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<RO_Garment_SizeBreakdown_Detail> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
              ""
            };

            Query = QueryHelper<RO_Garment_SizeBreakdown_Detail>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<RO_Garment_SizeBreakdown_Detail>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "LastModifiedUtc"
            };

            Query = Query
                .Select(field => new RO_Garment_SizeBreakdown_Detail
                {
                    Id = field.Id,
                    LastModifiedUtc = field.LastModifiedUtc
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<RO_Garment_SizeBreakdown_Detail>.Order(Query, OrderDictionary);

            Pageable<RO_Garment_SizeBreakdown_Detail> pageable = new Pageable<RO_Garment_SizeBreakdown_Detail>(Query, page - 1, size);
            List<RO_Garment_SizeBreakdown_Detail> data = pageable.Data.ToList<RO_Garment_SizeBreakdown_Detail>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<RO_Garment_SizeBreakdown_Detail>(data, totalData, OrderDictionary, SelectedFields);
        }

        public HashSet<long> GetIds(long id)
        {
            return new HashSet<long>(DbSet.Where(d => d.RO_Garment_SizeBreakdown.Id == id).Select(d => d.Id));
        }
    }
}