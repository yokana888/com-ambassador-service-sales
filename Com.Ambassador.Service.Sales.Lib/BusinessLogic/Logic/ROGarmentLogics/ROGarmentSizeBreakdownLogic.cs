using Com.Ambassador.Service.Sales.Lib.Models.ROGarments;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;
using Com.Moonlay.Models;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.ROGarmentLogics
{
    public class ROGarmentSizeBreakdownLogic : BaseLogic<RO_Garment_SizeBreakdown>
    {

        private ROGarmentSizeBreakdownDetailLogic roGarmentSizeBreakdownDetailLogic;
        private readonly SalesDbContext DbContext;
        public ROGarmentSizeBreakdownLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
            this.roGarmentSizeBreakdownDetailLogic = serviceProvider.GetService<ROGarmentSizeBreakdownDetailLogic>();
            this.DbContext = dbContext;
        }
        public override ReadResponse<RO_Garment_SizeBreakdown> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<RO_Garment_SizeBreakdown> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
              ""
            };

            Query = QueryHelper<RO_Garment_SizeBreakdown>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<RO_Garment_SizeBreakdown>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "LastModifiedUtc"
            };

            Query = Query
                .Select(field => new RO_Garment_SizeBreakdown
                {
                    Id = field.Id,
                    ColorName=field.ColorName,
                    LastModifiedUtc = field.LastModifiedUtc
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<RO_Garment_SizeBreakdown>.Order(Query, OrderDictionary);

            Pageable<RO_Garment_SizeBreakdown> pageable = new Pageable<RO_Garment_SizeBreakdown>(Query, page - 1, size);
            List<RO_Garment_SizeBreakdown> data = pageable.Data.ToList<RO_Garment_SizeBreakdown>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<RO_Garment_SizeBreakdown>(data, totalData, OrderDictionary, SelectedFields);
        }

        public HashSet<long> GetIds(long id)
        {
            return new HashSet<long>(DbSet.Where(d => d.RO_Garment.Id == id).Select(d => d.Id));
        }

        public override void Create(RO_Garment_SizeBreakdown model)
        {
            foreach (var size in model.RO_Garment_SizeBreakdown_Details)
            {
                 roGarmentSizeBreakdownDetailLogic.Create(size);
                
            }

            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            DbSet.Add(model);
        }

        public override void UpdateAsync(long id, RO_Garment_SizeBreakdown model)
        {
            if (model.RO_Garment_SizeBreakdown_Details != null)
            {
                HashSet<long> detailIds = roGarmentSizeBreakdownDetailLogic.GetIds(id);
                foreach (var itemId in detailIds)
                {
                    RO_Garment_SizeBreakdown_Detail data = model.RO_Garment_SizeBreakdown_Details.FirstOrDefault(prop => prop.Id.Equals(itemId));
                    if (data == null)
                    {
                        RO_Garment_SizeBreakdown_Detail dataItem = DbContext.RO_Garment_SizeBreakdown_Details.FirstOrDefault(prop => prop.Id.Equals(itemId));
                        EntityExtension.FlagForDelete(dataItem, IdentityService.Username, "sales-service");
                        //await roGarmentSizeBreakdownDetailLogic.DeleteAsync(itemId);

                    }
                    else
                    {
                        roGarmentSizeBreakdownDetailLogic.UpdateAsync(itemId, data);
                    }

                    foreach (RO_Garment_SizeBreakdown_Detail item in model.RO_Garment_SizeBreakdown_Details)
                    {
                        if (item.Id == 0)
                            roGarmentSizeBreakdownDetailLogic.Create(item);
                    }
                }
            }

            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            DbSet.Update(model);
        }

        public override async Task DeleteAsync(long id)
        {
            RO_Garment_SizeBreakdown model = await ReadByIdAsync(id);
            if (model.RO_Garment_SizeBreakdown_Details != null)
            {
                HashSet<long> detailIds = roGarmentSizeBreakdownDetailLogic.GetIds(id);
                foreach (var itemId in detailIds)
                {
                    await roGarmentSizeBreakdownDetailLogic.DeleteAsync(itemId);
                }
            }

            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service");
            DbSet.Update(model);
        }
    }
}
