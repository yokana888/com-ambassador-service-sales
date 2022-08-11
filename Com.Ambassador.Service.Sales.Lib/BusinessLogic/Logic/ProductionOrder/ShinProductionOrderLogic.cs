using Com.Ambassador.Service.Sales.Lib.Models.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Com.Moonlay.NetCore.Lib;
using Com.Moonlay.Models;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.ProductionOrder
{
    public class ShinProductionOrderLogic : BaseLogic<ProductionOrderModel>
    {
        private ProductionOrder_DetailLogic productionOrder_DetailLogic;
        private ProductionOrder_LampStandardLogic productionOrder_LampStandardLogic;
        private ProductionOrder_RunWidthLogic productionOrder_RunWidthLogic;
        private readonly string Agent = "new-po-service-sales";


        public ShinProductionOrderLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
            this.productionOrder_DetailLogic = serviceProvider.GetService<ProductionOrder_DetailLogic>();
            this.productionOrder_LampStandardLogic = serviceProvider.GetService<ProductionOrder_LampStandardLogic>();
            this.productionOrder_RunWidthLogic = serviceProvider.GetService<ProductionOrder_RunWidthLogic>();
        }

        public override ReadResponse<ProductionOrderModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<ProductionOrderModel> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
              "OrderNo", "SalesContractNo", "UnitName", "BuyerName"
            };

            Query = QueryHelper<ProductionOrderModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<ProductionOrderModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {

                "Id", "Code", "FinishingPrintingSalesContract", "DeliveryDate", "IsClosed", "LastModifiedUtc", "ApprovalMD", "ApprovalSample", "OrderQuantity",
                "ProductionOrderNo"

            };

            

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<ProductionOrderModel>.Order(Query, OrderDictionary);

            Pageable<ProductionOrderModel> pageable = new Pageable<ProductionOrderModel>(Query, page - 1, size);
            List<ProductionOrderModel> data = pageable.Data.ToList<ProductionOrderModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<ProductionOrderModel>(data, totalData, OrderDictionary, SelectedFields);
        }

        public override async void UpdateAsync(long id, ProductionOrderModel model)
        {
            try
            {
                if (model.Details != null)
                {
                    HashSet<long> detailIds = productionOrder_DetailLogic.GetIds(id);
                    foreach (var itemId in detailIds)
                    {
                        ProductionOrder_DetailModel data = model.Details.FirstOrDefault(prop => prop.Id.Equals(itemId));
                        if (data == null)
                            await productionOrder_DetailLogic.DeleteAsync(itemId);
                        else
                        {
                            productionOrder_DetailLogic.UpdateAsync(itemId, data);
                        }


                    }
                    foreach (ProductionOrder_DetailModel item in model.Details)
                    {
                        if (item.Id == 0)
                            productionOrder_DetailLogic.Create(item);
                    }

                    HashSet<long> LampStandardIds = productionOrder_LampStandardLogic.GetIds(id);
                    foreach (var itemId in LampStandardIds)
                    {
                        ProductionOrder_LampStandardModel data = model.LampStandards.FirstOrDefault(prop => prop.Id.Equals(itemId));
                        if (data == null)
                            await productionOrder_LampStandardLogic.DeleteAsync(itemId);
                        else
                        {
                            productionOrder_LampStandardLogic.UpdateAsync(itemId, data);
                        }
                    }

                    foreach (ProductionOrder_LampStandardModel item in model.LampStandards)
                    {
                        if (item.Id == 0)
                            productionOrder_LampStandardLogic.Create(item);
                    }

                    if (model.RunWidths.Count > 0)
                    {
                        HashSet<long> RunWidthIds = productionOrder_RunWidthLogic.GetIds(id);
                        foreach (var itemId in RunWidthIds)
                        {
                            ProductionOrder_RunWidthModel data = model.RunWidths.FirstOrDefault(prop => prop.Id.Equals(itemId));
                            if (data == null)
                                await productionOrder_RunWidthLogic.DeleteAsync(itemId);
                            else
                            {
                                productionOrder_RunWidthLogic.UpdateAsync(itemId, data);
                            }


                        }
                        foreach (ProductionOrder_RunWidthModel item in model.RunWidths)
                        {
                            if (item.Id == 0)
                                productionOrder_RunWidthLogic.Create(item);
                        }

                    }

                }

                EntityExtension.FlagForUpdate(model, IdentityService.Username, Agent);
                DbSet.Update(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void Create(ProductionOrderModel model)
        {
            if (model.Details.Count > 0)
            {
                foreach (var detail in model.Details)
                {
                    productionOrder_DetailLogic.Create(detail);
                }
            }

            if (model.LampStandards.Count > 0)
            {
                foreach (var lampStandards in model.LampStandards)
                {
                    lampStandards.Id = 0;
                    productionOrder_LampStandardLogic.Create(lampStandards);
                }
            }

            if (model.RunWidths.Count > 0)
            {
                foreach (var runWidths in model.RunWidths)
                {
                    productionOrder_RunWidthLogic.Create(runWidths);
                }
            }

            EntityExtension.FlagForCreate(model, IdentityService.Username, Agent);
            DbSet.Add(model);
        }

        public override async Task DeleteAsync(long id)
        {
            ProductionOrderModel model = await ReadByIdAsync(id);

            foreach (var detail in model.Details)
            {
                await productionOrder_DetailLogic.DeleteAsync(detail.Id);
            }

            foreach (var lampStandards in model.LampStandards)
            {
                await productionOrder_LampStandardLogic.DeleteAsync(lampStandards.Id);
            }


            if (model.RunWidths.Count > 0)
            {
                foreach (var runWidths in model.RunWidths)
                {
                    await productionOrder_RunWidthLogic.DeleteAsync(runWidths.Id);
                }
            }

            EntityExtension.FlagForDelete(model, IdentityService.Username, Agent, true);
            DbSet.Update(model);
        }

        public override async Task<ProductionOrderModel> ReadByIdAsync(long id)
        {
            var ProductionOrder = await DbSet.Include(p => p.Details).Include(p => p.LampStandards).Include(p => p.RunWidths)
               .Where(p => p.Details.Select(d => d.ProductionOrderModel.Id).Contains(p.Id))
               .FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
            ProductionOrder.Details = ProductionOrder.Details.OrderBy(s => s.Id).ToArray();
            ProductionOrder.LampStandards = ProductionOrder.LampStandards.OrderBy(s => s.Id).ToArray();
            ProductionOrder.RunWidths = ProductionOrder.RunWidths.OrderBy(s => s.Id).ToArray();
            return ProductionOrder;
        }

        public double GetTotalQuantityBySalesContractId(long id)
        {
            return DbSet.Include(x => x.Details).Where(x => x.SalesContractId == id).SelectMany(x => x.Details).Sum(x => x.Quantity);
        }

        public async Task ApproveMD(long id)
        {
            var model = await DbSet.FirstOrDefaultAsync(d => d.Id == id);
            model.IsApprovedMD = true;
            model.ApprovedMDBy = IdentityService.Username;
            model.ApprovedMDDate = DateTimeOffset.UtcNow;

            EntityExtension.FlagForUpdate(model, IdentityService.Username, Agent);
        }

        //public List<ProductionOrderModel> ReadBySalesContractNo(string salesContractNo)
        //{
        //    var result = DbSet.Where(p => p.SalesContractNo == salesContractNo);
        //    return result.ToList();
        //}
        
        public async Task ApproveSample(long id)
        {
            var model = await DbSet.FirstOrDefaultAsync(d => d.Id == id);
            model.IsApprovedSample = true;
            model.ApprovedSampleBy = IdentityService.Username;
            model.ApprovedSampleDate = DateTimeOffset.UtcNow;

            EntityExtension.FlagForUpdate(model, IdentityService.Username, Agent);
        }
    }
}
