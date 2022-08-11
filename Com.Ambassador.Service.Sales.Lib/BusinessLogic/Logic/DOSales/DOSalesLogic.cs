using Com.Ambassador.Service.Sales.Lib.Models.DOSales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.DOSales
{
    public class DOSalesLogic : BaseLogic<DOSalesModel>
    {
        private const string STOCK = "STOCK";
        private const string DYEINGPRINTING = "DYEINGPRINTING";
        private DOSalesDetailLogic doSalesLocalLogic;
        private SalesDbContext _dbContext;

        public DOSalesLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
            this.doSalesLocalLogic = serviceProvider.GetService<DOSalesDetailLogic>();
            _dbContext = dbContext;
        }

        public override ReadResponse<DOSalesModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DOSalesModel> Query = DbSet.Include(x => x.DOSalesDetailItems);

            List<string> SearchAttributes = new List<string>()
            {
                "DOSalesNo","DOSalesType",
                "SalesContractNo","BuyerName"
            };

            Query = QueryHelper<DOSalesModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<DOSalesModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id","Code","DOSalesNo","DOSalesType","Type","Date","SalesContract","Material","MaterialConstruction","Commodity","Buyer","DOSalesCategory","DOSalesDetailItems",
            };

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<DOSalesModel>.Order(Query, OrderDictionary);

            Pageable<DOSalesModel> pageable = new Pageable<DOSalesModel>(Query, page - 1, size);
            List<DOSalesModel> data = pageable.Data.ToList<DOSalesModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DOSalesModel>(data, totalData, OrderDictionary, SelectedFields);
        }

        public ReadResponse<DOSalesModel> ReadDPAndStock(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DOSalesModel> Query = DbSet.Include(x => x.DOSalesDetailItems).Where(s => s.DOSalesCategory == STOCK || s.DOSalesCategory == DYEINGPRINTING);

            List<string> SearchAttributes = new List<string>()
            {
                "DOSalesNo", "BuyerName", "DOSalesType"
            };

            Query = QueryHelper<DOSalesModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<DOSalesModel>.Filter(Query, FilterDictionary);


            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<DOSalesModel>.Order(Query, OrderDictionary);

            Pageable<DOSalesModel> pageable = new Pageable<DOSalesModel>(Query, page - 1, size);
            List<DOSalesModel> data = pageable.Data.ToList<DOSalesModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DOSalesModel>(data, totalData, OrderDictionary, new List<string>());
        }


        public override async void UpdateAsync(long id, DOSalesModel model)
        {
            try
            {
                if (model.DOSalesDetailItems != null)
                {
                    HashSet<long> detailIds = doSalesLocalLogic.GetIds(id);
                    foreach (var itemId in detailIds)
                    {
                        DOSalesDetailModel data = model.DOSalesDetailItems.FirstOrDefault(prop => prop.Id.Equals(itemId));
                        if (data == null)
                            await doSalesLocalLogic.DeleteAsync(itemId);
                        else
                        {
                            doSalesLocalLogic.UpdateAsync(itemId, data);
                        }
                    }

                    foreach (DOSalesDetailModel item in model.DOSalesDetailItems)
                    {
                        if (item.Id == 0)
                            doSalesLocalLogic.Create(item);
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

        public override void Create(DOSalesModel model)
        {
            if (model.DOSalesDetailItems.Count > 0)
            {
                foreach (var detail in model.DOSalesDetailItems)
                {
                    doSalesLocalLogic.Create(detail);
                }
            }

            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            DbSet.Add(model);
        }

        public override async Task DeleteAsync(long id)
        {

            DOSalesModel model = await ReadByIdAsync(id);

            foreach (var detail in model.DOSalesDetailItems)
            {
                await doSalesLocalLogic.DeleteAsync(detail.Id);
            }

            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            DbSet.Update(model);
        }

        public override async Task<DOSalesModel> ReadByIdAsync(long id)
        {
            //var DOSales = await DbSet.Where(p => p.DOSalesDetailItems.Select(d => d.DOSalesModel.Id)
            //.Contains(p.Id)).Include(p => p.DOSalesDetailItems).FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
            //DOSales.DOSalesDetailItems = DOSales.DOSalesDetailItems.OrderBy(s => s.Id).ToArray();

            var DOSales = DbSet.Include(x => x.DOSalesDetailItems).FirstOrDefault(x => x.Id == id);
            return DOSales;
        }

    }
}
