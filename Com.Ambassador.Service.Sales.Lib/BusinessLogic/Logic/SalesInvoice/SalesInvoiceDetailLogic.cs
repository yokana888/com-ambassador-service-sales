using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Ambassador.Service.Sales.Lib.Models.SalesInvoice;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.SalesInvoice
{
    public class SalesInvoiceDetailLogic : BaseLogic<SalesInvoiceDetailModel>
    {
        public SalesInvoiceDetailLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
        }

        public override void Create(SalesInvoiceDetailModel model)
        {
            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            foreach (var item in model.SalesInvoiceItems)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, "sales-service");

            }
            base.Create(model);
        }

        public override void UpdateAsync(long id, SalesInvoiceDetailModel model)
        {
            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            foreach (var item in model.SalesInvoiceItems)
            {
                EntityExtension.FlagForUpdate(item, IdentityService.Username, "sales-service");

            }
            base.UpdateAsync(id, model);
        }

        public override async Task DeleteAsync(long id)
        {
            var model = await ReadByIdAsync(id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            foreach (var item in model.SalesInvoiceItems)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, "sales-service", true);

            }
            DbSet.Update(model);
        }

        public override ReadResponse<SalesInvoiceDetailModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<SalesInvoiceDetailModel> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
              ""
            };

            Query = QueryHelper<SalesInvoiceDetailModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<SalesInvoiceDetailModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "DOSalesId","DOSalesNo",
            };

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<SalesInvoiceDetailModel>.Order(Query, OrderDictionary);

            Pageable<SalesInvoiceDetailModel> pageable = new Pageable<SalesInvoiceDetailModel>(Query, page - 1, size);
            List<SalesInvoiceDetailModel> data = pageable.Data.ToList<SalesInvoiceDetailModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<SalesInvoiceDetailModel>(data, totalData, OrderDictionary, SelectedFields);
        }
        public HashSet<long> GetIds(long id)
        {
            return new HashSet<long>(DbSet.Where(d => d.SalesInvoiceModel.Id == id).Select(d => d.Id));
        }

        public Task<SalesInvoiceDetailModel> ReadByIdAsync(long id)
        {
            var result = DbSet.Include(x => x.SalesInvoiceItems).FirstOrDefaultAsync(s => s.Id == id);
            return result;
        }
    }
}
