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
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.SalesInvoiceExport
{
    public class SalesInvoiceExportLogic : BaseLogic<SalesInvoiceExportModel>
    {
        private SalesInvoiceExportDetailLogic salesInvoiceExportDetailLogic;
        private SalesDbContext _dbContext;

        public SalesInvoiceExportLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
            this.salesInvoiceExportDetailLogic = serviceProvider.GetService<SalesInvoiceExportDetailLogic>();
            _dbContext = dbContext;
        }

        public override ReadResponse<SalesInvoiceExportModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<SalesInvoiceExportModel> Query = DbSet.Include(x => x.SalesInvoiceExportDetails);

            List<string> SearchAttributes = new List<string>()
            {
                "SalesInvoiceNo","BuyerName"
            };

            Query = QueryHelper<SalesInvoiceExportModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<SalesInvoiceExportModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id","Code","SalesInvoiceNo","SalesInvoiceCategory","LetterOfCreditNumberType","SalesInvoiceDate","FPType","BuyerName","BuyerAddress","Authorized","ShippedPer",
                "SailingDate","LetterOfCreditNumber","LCDate","IssuedBy","From","To","HSCode","TermOfPaymentType","TermOfPaymentRemark","Remark","SalesInvoiceExportDetails",
            };

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<SalesInvoiceExportModel>.Order(Query, OrderDictionary);

            Pageable<SalesInvoiceExportModel> pageable = new Pageable<SalesInvoiceExportModel>(Query, page - 1, size);
            List<SalesInvoiceExportModel> data = pageable.Data.ToList<SalesInvoiceExportModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<SalesInvoiceExportModel>(data, totalData, OrderDictionary, SelectedFields);
        }

        public override async void UpdateAsync(long id, SalesInvoiceExportModel model)
        {
            try
            {
                if (model.SalesInvoiceExportDetails != null)
                {
                    HashSet<long> detailIds = salesInvoiceExportDetailLogic.GetIds(id);
                    foreach (var itemId in detailIds)
                    {
                        SalesInvoiceExportDetailModel data = model.SalesInvoiceExportDetails.FirstOrDefault(prop => prop.Id.Equals(itemId));
                        if (data == null)
                            await salesInvoiceExportDetailLogic.DeleteAsync(itemId);
                        else
                        {
                            salesInvoiceExportDetailLogic.UpdateAsync(itemId, data);
                        }
                    }

                    foreach (SalesInvoiceExportDetailModel item in model.SalesInvoiceExportDetails)
                    {
                        if (item.Id == 0)
                            salesInvoiceExportDetailLogic.Create(item);
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

        public override void Create(SalesInvoiceExportModel model)
        {
            if (model.SalesInvoiceExportDetails.Count > 0)
            {
                foreach (var detail in model.SalesInvoiceExportDetails)
                {
                    salesInvoiceExportDetailLogic.Create(detail);
                }
            }

            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            DbSet.Add(model);
        }

        public override async Task DeleteAsync(long id)
        {

            SalesInvoiceExportModel model = await ReadByIdAsync(id);

            foreach (var detail in model.SalesInvoiceExportDetails)
            {
                await salesInvoiceExportDetailLogic.DeleteAsync(detail.Id);
            }

            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            DbSet.Update(model);
        }

        public override async Task<SalesInvoiceExportModel> ReadByIdAsync(long id)
        {
            var Result = await DbSet.Include(s => s.SalesInvoiceExportDetails).ThenInclude(s => s.SalesInvoiceExportItems).FirstOrDefaultAsync(s => s.Id == id);
            return Result;
        }
    }
}
