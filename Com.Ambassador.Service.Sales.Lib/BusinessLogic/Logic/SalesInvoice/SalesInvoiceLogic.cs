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
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.SalesInvoice
{
    public class SalesInvoiceLogic : BaseLogic<SalesInvoiceModel>
    {
        private SalesInvoiceDetailLogic salesInvoiceDetailLogic;
        private SalesDbContext _dbContext;

        public SalesInvoiceLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
            this.salesInvoiceDetailLogic = serviceProvider.GetService<SalesInvoiceDetailLogic>();
            _dbContext = dbContext;
        }

        public override ReadResponse<SalesInvoiceModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<SalesInvoiceModel> Query = DbSet.Include(x => x.SalesInvoiceDetails);

            List<string> SearchAttributes = new List<string>()
            {
                "SalesInvoiceNo","DeliveryOrderNo","BuyerName",
            };

            Query = QueryHelper<SalesInvoiceModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<SalesInvoiceModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id","Code","SalesInvoiceNo","SalesInvoiceType","SalesInvoiceCategory","SalesInvoiceDate","DueDate","DeliveryOrderNo",
                "Buyer","Currency","PaymentType","VatType","TotalPayment","TotalPaid","Remark","IsPaidOff","SalesInvoiceDetails",
            };

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<SalesInvoiceModel>.Order(Query, OrderDictionary);

            Pageable<SalesInvoiceModel> pageable = new Pageable<SalesInvoiceModel>(Query, page - 1, size);
            List<SalesInvoiceModel> data = pageable.Data.ToList<SalesInvoiceModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<SalesInvoiceModel>(data, totalData, OrderDictionary, SelectedFields);
        }

        public override async void UpdateAsync(long id, SalesInvoiceModel model)
        {
            try
            {
                if (model.SalesInvoiceDetails != null)
                {
                    HashSet<long> detailIds = salesInvoiceDetailLogic.GetIds(id);
                    foreach (var itemId in detailIds)
                    {
                        SalesInvoiceDetailModel data = model.SalesInvoiceDetails.FirstOrDefault(prop => prop.Id.Equals(itemId));
                        if (data == null)
                            await salesInvoiceDetailLogic.DeleteAsync(itemId);
                        else
                        {
                            salesInvoiceDetailLogic.UpdateAsync(itemId, data);
                        }
                    }

                    foreach (SalesInvoiceDetailModel item in model.SalesInvoiceDetails)
                    {
                        if (item.Id == 0)
                            salesInvoiceDetailLogic.Create(item);
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

        public override void Create(SalesInvoiceModel model)
        {
            if (model.SalesInvoiceDetails.Count > 0)
            {
                foreach (var detail in model.SalesInvoiceDetails)
                {
                    salesInvoiceDetailLogic.Create(detail);
                }
            }

            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            DbSet.Add(model);
        }

        public override async Task DeleteAsync(long id)
        {

            SalesInvoiceModel model = await ReadByIdAsync(id);

            foreach (var detail in model.SalesInvoiceDetails)
            {
                await salesInvoiceDetailLogic.DeleteAsync(detail.Id);
            }

            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            DbSet.Update(model);
        }

        public override async Task<SalesInvoiceModel> ReadByIdAsync(long id)
        {
            var Result = await DbSet.Include(s => s.SalesInvoiceDetails).ThenInclude(s => s.SalesInvoiceItems).FirstOrDefaultAsync(s => s.Id == id);
            return Result;
        }

        public List<SalesInvoiceModel> ReadByBuyerId(int buyerId)
        {
            var result = DbSet.Include(x => x.SalesInvoiceDetails).ThenInclude(x => x.SalesInvoiceItems).Where(p => p.BuyerId == buyerId && p.IsPaidOff == false);
            //var result = DbSet.Include(x => x.SalesInvoiceDetails).Where(p => p.BuyerId == buyerId).Select(x => x.Id);
            //var salesReceipt = _dbContext.SalesReceiptDetails.Where(x => x.IsPaidOff == true || x.IsPaidOff == null && result.Contains(x.SalesInvoiceId)).Select(x => x.SalesInvoiceId);
            //var invoices = DbSet.Include(x => x.SalesInvoiceDetails).Where(x => salesReceipt.Any(y => y == x.Id));
            return result.ToList();
        }

        internal void UpdateFromSalesReceiptAsync(int id, SalesInvoiceUpdateModel model)
        {
            var data = DbSet.Single(m => m.Id == id);
            data.TotalPaid = model.TotalPaid;
            data.IsPaidOff = model.IsPaidOff;
            EntityExtension.FlagForUpdate(data, IdentityService.Username, "sales-service");
        }
    }
}
