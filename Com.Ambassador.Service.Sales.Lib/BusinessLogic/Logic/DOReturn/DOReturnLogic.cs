using Com.Ambassador.Service.Sales.Lib.Models.DOReturn;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.DOReturn
{
    public class DOReturnLogic : BaseLogic<DOReturnModel>
    {
        private DOReturnDetailLogic doReturnDetailLogic;
        private SalesDbContext _dbContext;

        public DOReturnLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
            this.doReturnDetailLogic = serviceProvider.GetService<DOReturnDetailLogic>();
            _dbContext = dbContext;
        }

        public override ReadResponse<DOReturnModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DOReturnModel> Query = DbSet.Include(x => x.DOReturnDetails);

            List<string> SearchAttributes = new List<string>()
            {
                "DOReturnNo","DOReturnType",
            };

            Query = QueryHelper<DOReturnModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<DOReturnModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id","Code","DOReturnNo","DOReturnType","DOReturnDate","ReturnFrom","LTKPNo","HeadOfStorage","Remark",
            };

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<DOReturnModel>.Order(Query, OrderDictionary);

            Pageable<DOReturnModel> pageable = new Pageable<DOReturnModel>(Query, page - 1, size);
            List<DOReturnModel> data = pageable.Data.ToList<DOReturnModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DOReturnModel>(data, totalData, OrderDictionary, SelectedFields);
        }

        public override void UpdateAsync(long id, DOReturnModel model)
        {
            try
            {
                if (model.DOReturnDetails != null)
                {
                    HashSet<long> detailIds = doReturnDetailLogic.GetIds(id);
                    foreach (var itemId in detailIds)
                    {
                        DOReturnDetailModel data = model.DOReturnDetails.FirstOrDefault(prop => prop.Id.Equals(itemId));
                        if (data == null)
                        {
                            var deletedDetail = _dbContext.DOReturnDetails.Include(s => s.DOReturnDetailItems).Include(s => s.DOReturnItems).FirstOrDefault(s => s.Id == itemId);
                            EntityExtension.FlagForDelete(deletedDetail, IdentityService.Username, "sales-service", true);
                            foreach (var detailItem in deletedDetail.DOReturnDetailItems)
                            {
                                EntityExtension.FlagForDelete(detailItem, IdentityService.Username, "sales-service", true);

                            }
                            foreach (var item in deletedDetail.DOReturnItems)
                            {
                                EntityExtension.FlagForDelete(item, IdentityService.Username, "sales-service", true);

                            }
                        }

                        else
                        {
                            doReturnDetailLogic.UpdateAsync(itemId, data);
                        }
                    }

                    foreach (DOReturnDetailModel item in model.DOReturnDetails)
                    {
                        if (item.Id == 0)
                            doReturnDetailLogic.Create(item);
                    }
                }

                EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
                DbSet.Update(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void Create(DOReturnModel model)
        {
            if (model.DOReturnDetails.Count > 0)
            {
                foreach (var detail in model.DOReturnDetails)
                {
                    doReturnDetailLogic.Create(detail);
                }
            }

            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            DbSet.Add(model);
        }

        public override async Task DeleteAsync(long id)
        {

            DOReturnModel model = await ReadByIdAsync(id);

            foreach (var detail in model.DOReturnDetails)
            {
                await doReturnDetailLogic.DeleteAsync(detail.Id);
            }

            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            DbSet.Update(model);
        }

        public override async Task<DOReturnModel> ReadByIdAsync(long id)
        {
            var Result = await DbSet.Include(s => s.DOReturnDetails)
                                            .ThenInclude(s => s.DOReturnDetailItems)
                                     .Include(s => s.DOReturnDetails)
                                            .ThenInclude(s => s.DOReturnItems)
                                    .FirstOrDefaultAsync(s => s.Id == id);

            return Result;
        }

    }
}
