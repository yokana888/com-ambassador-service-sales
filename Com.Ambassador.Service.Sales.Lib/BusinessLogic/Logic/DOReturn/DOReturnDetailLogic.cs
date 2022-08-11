using Com.Ambassador.Service.Sales.Lib.Models.DOReturn;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.DOReturn
{
    public class DOReturnDetailLogic : BaseLogic<DOReturnDetailModel>
    {
        private DOReturnDetailItemLogic doReturnDetailItemLogic;
        private DOReturnItemLogic doReturnItemLogic;
        protected DbSet<DOReturnModel> _dbSet;
        protected DbSet<DOReturnDetailModel> _detailDbSet;
        private SalesDbContext _dbContext;

        public DOReturnDetailLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
            this.doReturnDetailItemLogic = serviceProvider.GetService<DOReturnDetailItemLogic>();
            this.doReturnItemLogic = serviceProvider.GetService<DOReturnItemLogic>();
            _dbSet = dbContext.Set<DOReturnModel>();
            _detailDbSet = dbContext.Set<DOReturnDetailModel>();
            _dbContext = dbContext;
        }

        public override void Create(DOReturnDetailModel detail)
        {
            EntityExtension.FlagForCreate(detail, IdentityService.Username, "sales-service");
            foreach (var detailItem in detail.DOReturnDetailItems)
            {
                EntityExtension.FlagForCreate(detailItem, IdentityService.Username, "sales-service");
                doReturnDetailItemLogic.Create(detailItem);
            }

            //detail.DOReturnItems = detail.DOReturnItems.Where(s => s.Total > 0).ToList();
            foreach (var item in detail.DOReturnItems)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, "sales-service");
                doReturnItemLogic.Create(item);
            }
            base.Create(detail);
        }

        public override void UpdateAsync(long id, DOReturnDetailModel detail)
        {
            try
            {
                if (detail.DOReturnDetailItems != null)
                {
                    HashSet<long> detailIds = doReturnDetailItemLogic.GetIds(id);
                    foreach (var itemId in detailIds)
                    {
                        var data = detail.DOReturnDetailItems.FirstOrDefault(prop => prop.Id.Equals(itemId));
                        if (data == null)
                        {
                            var deletedDetail = _dbContext.DOReturnDetailItems.FirstOrDefault(s => s.Id == itemId);
                            EntityExtension.FlagForDelete(deletedDetail, IdentityService.Username, "sales-service", true);

                        }

                        else
                        {
                            doReturnDetailItemLogic.UpdateAsync(itemId, data);
                        }
                    }

                    foreach (var item in detail.DOReturnDetailItems)
                    {
                        if (item.Id == 0)
                            doReturnDetailItemLogic.Create(item);
                    }
                }

                if (detail.DOReturnItems != null)
                {
                    HashSet<long> detailIds = doReturnItemLogic.GetIds(id);
                    foreach (var itemId in detailIds)
                    {
                        var data = detail.DOReturnItems.FirstOrDefault(prop => prop.Id.Equals(itemId));
                        if (data == null)
                        {
                            var deletedDetail = _dbContext.DOReturnItems.FirstOrDefault(s => s.Id == itemId);
                            EntityExtension.FlagForDelete(deletedDetail, IdentityService.Username, "sales-service", true);

                        }

                        else
                        {
                            doReturnItemLogic.UpdateAsync(itemId, data);
                        }
                    }

                    foreach (var item in detail.DOReturnItems)
                    {
                        if (item.Id == 0)
                            doReturnItemLogic.Create(item);
                    }
                }

                EntityExtension.FlagForUpdate(detail, IdentityService.Username, "sales-service");
                DbSet.Update(detail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override async Task DeleteAsync(long id)
        {
            var detail = await ReadByIdAsync(id);
            EntityExtension.FlagForDelete(detail, IdentityService.Username, "sales-service", true);
            foreach (var detailItem in detail.DOReturnDetailItems)
            {
                EntityExtension.FlagForDelete(detailItem, IdentityService.Username, "sales-service", true);

            }
            foreach (var item in detail.DOReturnItems)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, "sales-service", true);

            }
            DbSet.Update(detail);
        }

        public override ReadResponse<DOReturnDetailModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DOReturnDetailModel> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
              ""
            };

            Query = QueryHelper<DOReturnDetailModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<DOReturnDetailModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "SalesInvoiceId","SalesInvoiceNo",
            };

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<DOReturnDetailModel>.Order(Query, OrderDictionary);

            Pageable<DOReturnDetailModel> pageable = new Pageable<DOReturnDetailModel>(Query, page - 1, size);
            List<DOReturnDetailModel> data = pageable.Data.ToList<DOReturnDetailModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DOReturnDetailModel>(data, totalData, OrderDictionary, SelectedFields);
        }

        public HashSet<long> GetIds(long id)
        {
            return new HashSet<long>(DbSet.Where(d => d.DOReturnModel.Id == id).Select(d => d.Id));
        }

        public async Task<DOReturnDetailModel> ReadByIdAsync(long id)
        {
            var result = await DbSet.Include(s => s.DOReturnDetailItems).Include(s => s.DOReturnItems).FirstOrDefaultAsync(s => s.Id == id);
            result.DOReturnDetailItems = result.DOReturnDetailItems.OrderBy(s => s.Id).ToArray();
            result.DOReturnItems = result.DOReturnItems.OrderBy(s => s.Id).ToArray();

            return result;
        }
    }
}
