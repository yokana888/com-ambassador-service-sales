using Com.Ambassador.Service.Sales.Lib.Models.SalesInvoiceExport;
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

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.SalesInvoiceExport
{
    public class SalesInvoiceExportDetailLogic : BaseLogic<SalesInvoiceExportDetailModel>
    {
        public SalesInvoiceExportDetailLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
        }

        public override void Create(SalesInvoiceExportDetailModel model)
        {
            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            foreach (var item in model.SalesInvoiceExportItems)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, "sales-service");

            }
            base.Create(model);
        }

        public override void UpdateAsync(long id, SalesInvoiceExportDetailModel model)
        {
            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            foreach (var item in model.SalesInvoiceExportItems)
            {
                EntityExtension.FlagForUpdate(item, IdentityService.Username, "sales-service");

            }
            base.UpdateAsync(id, model);
        }

        public override async Task DeleteAsync(long id)
        {
            var model = await ReadByIdAsync(id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            foreach (var item in model.SalesInvoiceExportItems)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, "sales-service", true);

            }
            DbSet.Update(model);
        }

        public override ReadResponse<SalesInvoiceExportDetailModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<SalesInvoiceExportDetailModel> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
              ""
            };

            Query = QueryHelper<SalesInvoiceExportDetailModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<SalesInvoiceExportDetailModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id","BonId","BonNo","ContractNo","WeightUom","TotalUom","GrossWeight","NetWeight","TotalMeas"
            };

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<SalesInvoiceExportDetailModel>.Order(Query, OrderDictionary);

            Pageable<SalesInvoiceExportDetailModel> pageable = new Pageable<SalesInvoiceExportDetailModel>(Query, page - 1, size);
            List<SalesInvoiceExportDetailModel> data = pageable.Data.ToList<SalesInvoiceExportDetailModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<SalesInvoiceExportDetailModel>(data, totalData, OrderDictionary, SelectedFields);
        }
        public HashSet<long> GetIds(long id)
        {
            return new HashSet<long>(DbSet.Where(d => d.SalesInvoiceExportModel.Id == id).Select(d => d.Id));
        }

        public Task<SalesInvoiceExportDetailModel> ReadByIdAsync(long id)
        {
            var result = DbSet.Include(x => x.SalesInvoiceExportItems).FirstOrDefaultAsync(s => s.Id == id);
            return result;
        }
    }
}
