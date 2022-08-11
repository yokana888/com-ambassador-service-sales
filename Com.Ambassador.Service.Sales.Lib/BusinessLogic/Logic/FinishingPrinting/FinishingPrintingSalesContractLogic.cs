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
    public class FinishingPrintingSalesContractLogic : BaseLogic<FinishingPrintingSalesContractModel>
    {
        private FinishingPrintingSalesContractDetailLogic FinishingPrintingSalesContractDetailLogic;
        public FinishingPrintingSalesContractLogic(FinishingPrintingSalesContractDetailLogic finishingPrintingSalesContractDetailLogic, IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
            this.FinishingPrintingSalesContractDetailLogic = finishingPrintingSalesContractDetailLogic;
        }

        public override ReadResponse<FinishingPrintingSalesContractModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<FinishingPrintingSalesContractModel> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "SalesContractNo","BuyerName"
            };

            Query = QueryHelper<FinishingPrintingSalesContractModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<FinishingPrintingSalesContractModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "Code", "Buyer", "DeliverySchedule","OrderType","Commodity","SalesContractNo","YarnMaterial","PieceLength","OrderQuantity","LastModifiedUtc","Material","MaterialConstruction","DesignMotive","MaterialWidth", "Details"
            };

            Query = Query
                .Select(field => new FinishingPrintingSalesContractModel
                {
                    Id = field.Id,
                    Code = field.Code,
                    SalesContractNo = field.SalesContractNo,
                    BuyerCode = field.BuyerCode,
                    BuyerType = field.BuyerType,
                    BuyerName = field.BuyerName,
                    BuyerID = field.BuyerID,
                    CommodityID = field.CommodityID,
                    CommodityCode = field.CommodityCode,
                    CommodityName = field.CommodityName,
                    DesignMotiveID = field.DesignMotiveID,
                    DesignMotiveCode = field.DesignMotiveCode,
                    DesignMotiveName = field.DesignMotiveName,
                    MaterialConstructionId = field.MaterialConstructionId,
                    MaterialConstructionCode = field.MaterialConstructionCode,
                    MaterialConstructionName = field.MaterialConstructionName,
                    MaterialCode = field.MaterialCode,
                    MaterialID = field.MaterialID,
                    MaterialName = field.MaterialName,
                    OrderQuantity = field.OrderQuantity,
                    OrderTypeCode = field.OrderTypeCode,
                    OrderTypeID = field.OrderTypeID,
                    OrderTypeName = field.OrderTypeName,
                    PieceLength = field.PieceLength,
                    DeliverySchedule = field.DeliverySchedule,
                    YarnMaterialID = field.YarnMaterialID,
                    YarnMaterialCode = field.YarnMaterialCode,
                    YarnMaterialName = field.YarnMaterialName,
                    MaterialWidth = field.MaterialWidth,
                    LastModifiedUtc = field.LastModifiedUtc,
                    Details = field.Details
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<FinishingPrintingSalesContractModel>.Order(Query, OrderDictionary);

            Pageable<FinishingPrintingSalesContractModel> pageable = new Pageable<FinishingPrintingSalesContractModel>(Query, page - 1, size);
            List<FinishingPrintingSalesContractModel> data = pageable.Data.ToList<FinishingPrintingSalesContractModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<FinishingPrintingSalesContractModel>(data, totalData, OrderDictionary, SelectedFields);
        }

        public override void Create(FinishingPrintingSalesContractModel model)
        {
            SalesContractNumberGenerator(model);
            foreach (var detail in model.Details)
            {
                FinishingPrintingSalesContractDetailLogic.Create(detail);
                //EntityExtension.FlagForCreate(detail, IdentityService.Username, "sales-service");
            }

            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            DbSet.Add(model);
        }

        public override async Task<FinishingPrintingSalesContractModel> ReadByIdAsync(long id)
        {
            try
            {
                var finishingPrintingSalesContract = await DbSet.Include(p => p.Details).FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));               
                if (finishingPrintingSalesContract != null)
                {
                    finishingPrintingSalesContract.Details = finishingPrintingSalesContract.Details.OrderBy(s => s.Id).ToArray();
                }
                return finishingPrintingSalesContract;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public override async void UpdateAsync(long id, FinishingPrintingSalesContractModel model)
        {
            try
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

                EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
                DbSet.Update(model);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public override async Task DeleteAsync(long id)
        {
            var model = await ReadByIdAsync(id);

            foreach (var Detail in model.Details)
            {
                EntityExtension.FlagForDelete(Detail, IdentityService.Username, "sales-service");
            }

            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            DbSet.Update(model);
        }

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
