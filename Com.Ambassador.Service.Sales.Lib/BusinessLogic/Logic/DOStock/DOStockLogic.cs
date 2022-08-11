using Com.Ambassador.Service.Sales.Lib.Models.DOSales;
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
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.DOStock
{
    public class DOStockLogic : BaseLogic<DOSalesModel>
    {
        private const string STOCK = "STOCK";
        private const string DYEINGPRINTING = "DYEINGPRINTING";
        private const string UserAgent = "sales-service";
        public DOStockLogic(IIdentityService IdentityService, SalesDbContext dbContext) : base(IdentityService, dbContext)
        {
        }

        public override ReadResponse<DOSalesModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DOSalesModel> Query = DbSet.Include(x => x.DOSalesDetailItems).Where(s => s.DOSalesCategory == STOCK);

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

        
        public override void Create(DOSalesModel model)
        {
            foreach (var item in model.DOSalesDetailItems)
            {
                item.FlagForCreate(IdentityService.Username, UserAgent);
            }
            base.Create(model);
        }

        public override async Task DeleteAsync(long id)
        {
            var model = await ReadByIdAsync(id);
            model.FlagForDelete(IdentityService.Username, UserAgent);
            foreach (var item in model.DOSalesDetailItems)
            {
                item.FlagForDelete(IdentityService.Username, UserAgent);
            }
            DbSet.Update(model);
        }

        public override Task<DOSalesModel> ReadByIdAsync(long id)
        {
            return DbSet.Include(s => s.DOSalesDetailItems).FirstOrDefaultAsync(s => s.Id == id);
        }

        public override void UpdateAsync(long id, DOSalesModel model)
        {
            var dbModel = ReadByIdAsync(id).GetAwaiter().GetResult();

            if (dbModel != null)
            {
                dbModel.Date = model.Date;
                dbModel.DestinationBuyerName = model.DestinationBuyerName;
                dbModel.DestinationBuyerAddress = model.DestinationBuyerAddress;
                dbModel.SalesName = model.SalesName;
                dbModel.HeadOfStorage = model.HeadOfStorage;
                dbModel.Disp = model.Disp;
                dbModel.Remark = model.Remark;

                dbModel.FlagForUpdate(IdentityService.Username, UserAgent);

                var addedLossItems = model.DOSalesDetailItems.Where(x => !dbModel.DOSalesDetailItems.Any(y => y.Id == x.Id)).ToList();
                var updatedLossItems = model.DOSalesDetailItems.Where(x => dbModel.DOSalesDetailItems.Any(y => y.Id == x.Id)).ToList();
                var deletedLossItems = dbModel.DOSalesDetailItems.Where(x => !model.DOSalesDetailItems.Any(y => y.Id == x.Id)).ToList();

                foreach (var item in updatedLossItems)
                {
                    var dbItem = dbModel.DOSalesDetailItems.FirstOrDefault(x => x.Id == item.Id);

                    dbItem.ProductionOrderId = item.ProductionOrderId;
                    dbItem.ProductionOrderNo = item.ProductionOrderNo;
                    dbItem.MaterialWidth = item.MaterialWidth;
                    dbItem.ConstructionName = item.ConstructionName;
                    dbItem.MaterialId = item.MaterialId;
                    dbItem.MaterialCode = item.MaterialCode;
                    dbItem.MaterialName = item.MaterialName;
                    dbItem.MaterialPrice = item.MaterialPrice;
                    dbItem.MaterialTags = item.MaterialTags;
                    dbItem.MaterialConstructionCode = item.MaterialConstructionCode;
                    dbItem.MaterialConstructionId = item.MaterialConstructionId;
                    dbItem.MaterialConstructionName = item.MaterialConstructionName;
                    dbItem.MaterialConstructionRemark = item.MaterialConstructionRemark;
                    dbItem.ColorRequest = item.ColorRequest;
                    dbItem.ColorTemplate = item.ColorTemplate;
                    dbItem.Length = item.Length;
                    dbItem.Packing = item.Packing;
                    dbItem.UnitOrCode = item.UnitOrCode;

                    dbItem.FlagForUpdate(IdentityService.Username, UserAgent);


                }


                foreach (var item in deletedLossItems)
                {
                    item.FlagForDelete(IdentityService.Username, UserAgent);
                }

                foreach (var item in addedLossItems)
                {
                    item.FlagForCreate(IdentityService.Username, UserAgent);

                    dbModel.DOSalesDetailItems.Add(item);
                }
            }

        }
    }
}
