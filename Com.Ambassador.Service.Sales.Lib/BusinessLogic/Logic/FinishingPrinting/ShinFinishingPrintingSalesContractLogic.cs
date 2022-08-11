using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
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

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting
{
    public class ShinFinishingPrintingSalesContractLogic : BaseLogic<FinishingPrintingSalesContractModel>
    {
        private readonly string Agent = "new-sc-service-sales";
        private readonly FinishingPrintingSalesContractDetailLogic FinishingPrintingSalesContractDetailLogic;
        private readonly SalesDbContext DbContext;
        public ShinFinishingPrintingSalesContractLogic(FinishingPrintingSalesContractDetailLogic finishingPrintingSalesContractDetailLogic, IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
            FinishingPrintingSalesContractDetailLogic = finishingPrintingSalesContractDetailLogic;
            DbContext = dbContext;
        }

        public override ReadResponse<FinishingPrintingSalesContractModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<FinishingPrintingSalesContractModel> Query = DbSet.Include(x => x.Details);

            List<string> SearchAttributes = new List<string>()
            {
                "SalesContractNo","BuyerName", "UnitName"
            };

            Query = QueryHelper<FinishingPrintingSalesContractModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<FinishingPrintingSalesContractModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "Code", "PreSalesContract", "DeliverySchedule", "YarnMaterial","Quality","Packing",
                "ShippingQuantityTolerance", "LastModifiedUtc", "Details", "SalesContractNo", "Material", "UOM", "Sales"
            };

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<FinishingPrintingSalesContractModel>.Order(Query, OrderDictionary);

            Pageable<FinishingPrintingSalesContractModel> pageable = new Pageable<FinishingPrintingSalesContractModel>(Query, page - 1, size);
            List<FinishingPrintingSalesContractModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<FinishingPrintingSalesContractModel>(data, totalData, OrderDictionary, SelectedFields.OrderBy(x => x).ToList());
        }

        public override void Create(FinishingPrintingSalesContractModel model)
        {
            SalesContractNumberGenerator(model);
            foreach (var detail in model.Details)
            {
                FinishingPrintingSalesContractDetailLogic.Create(detail);
                //EntityExtension.FlagForCreate(detail, IdentityService.Username, "sales-service");
            }

            EntityExtension.FlagForCreate(model, IdentityService.Username, Agent);
            DbSet.Add(model);
            //UpdateFPCostCalculationIsSCCreated(model, true);
        }

        public override async Task<FinishingPrintingSalesContractModel> ReadByIdAsync(long id)
        {
            var finishingPrintingSalesContract = await DbSet.Include(p => p.Details).FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
            finishingPrintingSalesContract.Details = finishingPrintingSalesContract.Details.OrderBy(s => s.Id).ToArray();
            return finishingPrintingSalesContract;
        }

        public Task<FinishingPrintingSalesContractModel> ReadParent(long id)
        {
            var finishingPrintingSalesContract = DbSet.Include(p => p.Details).FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
            return finishingPrintingSalesContract;
        }

        public override async void UpdateAsync(long id, FinishingPrintingSalesContractModel model)
        {
            if (model.Details != null)
            {
                HashSet<long> detailIds = FinishingPrintingSalesContractDetailLogic.GetFPSalesContractIds(id);
                foreach (var itemId in detailIds)
                {
                    FinishingPrintingSalesContractDetailModel data = model.Details.FirstOrDefault(prop => prop.Id.Equals(itemId));
                    if (data == null)
                        await FinishingPrintingSalesContractDetailLogic.DeleteAsync(itemId);
                    else
                    {
                        FinishingPrintingSalesContractDetailLogic.UpdateAsync(itemId, data);
                    }

                    foreach (FinishingPrintingSalesContractDetailModel item in model.Details)
                    {
                        if (item.Id == 0)
                            FinishingPrintingSalesContractDetailLogic.Create(item);
                    }
                }
            }

            EntityExtension.FlagForUpdate(model, IdentityService.Username, Agent);
            DbSet.Update(model);
        }

        public override async Task DeleteAsync(long id)
        {
            var model = await ReadByIdAsync(id);

            foreach (var Detail in model.Details)
            {
                EntityExtension.FlagForDelete(Detail, IdentityService.Username, Agent);
            }

            EntityExtension.FlagForDelete(model, IdentityService.Username, Agent);
            DbSet.Update(model);

            //UpdateFPCostCalculationIsSCCreated(model, false);
        }


        //private void UpdateFPCostCalculationIsSCCreated(FinishingPrintingSalesContractModel model, bool flagSC)
        //{
        //    var relatedFPCC = DbContext.FinishingPrintingCostCalculations.FirstOrDefault(x => x.Id == model.CostCalculationId);
        //    relatedFPCC.IsSCCreated = flagSC;
        //}

        private void SalesContractNumberGenerator(FinishingPrintingSalesContractModel model)
        {
            FinishingPrintingSalesContractModel lastData;
            if (model.BuyerType.Equals("ekspor", StringComparison.OrdinalIgnoreCase) || model.BuyerType.Equals("export", StringComparison.OrdinalIgnoreCase))
            {
                lastData = DbSet.IgnoreQueryFilters()
                    .Where(w => w.BuyerType == "ekspor" || w.BuyerType == "export")
                    .OrderByDescending(o => o.CreatedUtc).FirstOrDefault();
            }
            else
            {
                lastData = DbSet.IgnoreQueryFilters()
                    .Where(w => w.BuyerType != "ekspor" && w.BuyerType != "export")
                    .OrderByDescending(o => o.CreatedUtc).FirstOrDefault();
            }

            string DocumentType = model.BuyerType.ToLower().Equals("ekspor") || model.BuyerType.ToLower().Equals("export") ? "FPE" : "FPL";

            DateTime Now = DateTime.Now;
            string Year = Now.ToString("yyyy");
            string month = Now.ToString("MM");

            if (lastData == null)
            {
                model.AutoIncrementNumber = 1;
                model.SalesContractNo = $"0001/{DocumentType}/{month}.{Year}";
            }
            else
            {
                if (Now.Year > lastData.CreatedUtc.Year)
                {
                    model.AutoIncrementNumber = 1;
                    model.SalesContractNo = $"0001/{DocumentType}/{month}.{Year}";
                }
                else
                {
                    model.AutoIncrementNumber = lastData.AutoIncrementNumber + 1;
                    model.SalesContractNo = $"{model.AutoIncrementNumber.ToString().PadLeft(4, '0')}/{DocumentType}/{month}.{Year}";
                }
            }
        }
    }
}
