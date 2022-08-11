using Com.Ambassador.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.CostCalculationGarments
{
    public class CostCalculationGarmentMaterialLogic : BaseLogic<CostCalculationGarment_Material>
	{
		//private CostCalculationGarmentMaterialLogic costCalculationGarmentMaterialLogic;
		private readonly SalesDbContext DbContext;
		public CostCalculationGarmentMaterialLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
		{
            DbContext = dbContext;
        }

		public override ReadResponse<CostCalculationGarment_Material> Read(int page, int size, string order, List<string> select, string keyword, string filter)
		{
			IQueryable<CostCalculationGarment_Material> Query = this.DbContext.CostCalculationGarment_Materials;

			List<string> SearchAttributes = new List<string>()
				{
					"Code"
				};
			Query = QueryHelper<CostCalculationGarment_Material>.Search(Query, SearchAttributes, keyword);

			Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
			Query = QueryHelper<CostCalculationGarment_Material>.Filter(Query, FilterDictionary);

			List<string> SelectedFields = new List<string>()
				{
					"Id", "Code"
				};
			Query = Query
				.Select(b => new CostCalculationGarment_Material
				{
					Id = b.Id,
					Code = b.Code
				});

			Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
			Query = QueryHelper<CostCalculationGarment_Material>.Order(Query, OrderDictionary);

			Pageable<CostCalculationGarment_Material> pageable = new Pageable<CostCalculationGarment_Material>(Query, page - 1, size);
			List<CostCalculationGarment_Material> data = pageable.Data.ToList<CostCalculationGarment_Material>();
			int totalData = pageable.TotalCount;

			return new ReadResponse<CostCalculationGarment_Material>(data, totalData, OrderDictionary, SelectedFields);
		}

		 public HashSet<long> GetCostCalculationIds(long id)
		{
			return new HashSet<long>(DbSet.Where(d => d.CostCalculationGarment.Id == id).Select(d => d.Id));
		}

        internal ReadResponse<dynamic> ReadMaterials(int page, int size, string order, string select, string keyword, string filter, string search)
        {
            IQueryable<CostCalculationGarment_Material> Query = DbSet;

            List<string> SearchAttributes = JsonConvert.DeserializeObject<List<string>>(search);
            if (SearchAttributes.Count < 1)
            {
                SearchAttributes = new List<string>() { "Code", "PO_SerialNumber" };
            }
            Query = QueryHelper<CostCalculationGarment_Material>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<CostCalculationGarment_Material>.Filter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<CostCalculationGarment_Material>.Order(Query, OrderDictionary);

            IQueryable SelectedQuery = Query;
            if (!string.IsNullOrWhiteSpace(select))
            {
                SelectedQuery = QueryHelper<CostCalculationGarment_Material>.Select(Query, select);
            }

            int totalData = SelectedQuery.Count();

            List<dynamic> Data = SelectedQuery
                .Skip((page - 1) * size)
                .Take(size)
                .ToDynamicList();

            return new ReadResponse<dynamic>(Data, totalData, OrderDictionary, new List<string>());
        }

        internal ReadResponse<dynamic> ReadMaterialsByPRMasterItemIds(int page, int size, string order, string select, string keyword, string filter, string search, string prmasteritemids)
        {
            IQueryable<CostCalculationGarment_Material> Query = DbSet;

            List<long> PRMasterItemIds = JsonConvert.DeserializeObject<List<long>>(prmasteritemids);
            Query = Query.Where(w => PRMasterItemIds.Contains(w.PRMasterItemId));

            List<string> SearchAttributes = JsonConvert.DeserializeObject<List<string>>(search);
            if (SearchAttributes.Count < 1)
            {
                SearchAttributes = new List<string>() { "Code", "PO_SerialNumber" };
            }
            Query = QueryHelper<CostCalculationGarment_Material>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<CostCalculationGarment_Material>.Filter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<CostCalculationGarment_Material>.Order(Query, OrderDictionary);

            IQueryable SelectedQuery = Query;
            if (!string.IsNullOrWhiteSpace(select))
            {
                SelectedQuery = QueryHelper<CostCalculationGarment_Material>.Select(Query, select);
            }

            int totalData = SelectedQuery.Count();

            if (size > 0)
            {
                SelectedQuery = SelectedQuery
                    .Skip((page - 1) * size)
                    .Take(size);
            }

            List<dynamic> Data = SelectedQuery
                .ToDynamicList();

            return new ReadResponse<dynamic>(Data, totalData, OrderDictionary, new List<string>());
        }
    }
}
