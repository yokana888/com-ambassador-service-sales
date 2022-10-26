using Com.Ambassador.Service.Sales.Lib.Models.GarmentSalesContractModel;
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
using Microsoft.Extensions.DependencyInjection;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentSalesContractLogics
{
    public class GarmentSalesContractLogic : BaseLogic<GarmentSalesContract>
    {
        private GarmentSalesContractItemLogic garmentSalesContractItemLogic;
        private GarmentSalesContractROLogic garmentSalesContractROLogic;
        private readonly SalesDbContext DbContext;
        public GarmentSalesContractLogic( IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {

            this.garmentSalesContractROLogic = serviceProvider.GetService<GarmentSalesContractROLogic>();
            this.garmentSalesContractItemLogic = serviceProvider.GetService<GarmentSalesContractItemLogic>();
            this.DbContext = dbContext;
        }

        public override ReadResponse<GarmentSalesContract> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<GarmentSalesContract> Query = DbSet.Include(x => x.SalesContractROs);

            List<string> SearchAttributes = new List<string>()
            {
                "SalesContractNo", "BuyerBrandName", "BuyerBrandCode"
            };

            Query = QueryHelper<GarmentSalesContract>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<GarmentSalesContract>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "BuyerBrandName", "BuyerBrandCode", "SalesContractNo", "LastModifiedUtc", "CreatedUtc"
            };

            Query = Query
                .Select(sc => new GarmentSalesContract
                {
                    Id = sc.Id,
                    SalesContractNo = sc.SalesContractNo,
                    BuyerBrandCode = sc.BuyerBrandCode,
                    BuyerBrandId = sc.BuyerBrandId,
                    BuyerBrandName = sc.BuyerBrandName,
                    CreatedUtc = sc.CreatedUtc,
                    LastModifiedUtc = sc.LastModifiedUtc,
                    AccountBankId = sc.AccountBankId,
                    AccountBankName = sc.AccountBankName,
                    AccountName = sc.AccountName,
                    Amount = sc.Amount,
                    IsDeleted = sc.IsDeleted,
                    IsEmbrodiary = sc.IsEmbrodiary,
                    DocPrinted = sc.DocPrinted
                }).OrderByDescending(s => s.LastModifiedUtc);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<GarmentSalesContract>.Order(Query, OrderDictionary);

            Pageable<GarmentSalesContract> pageable = new Pageable<GarmentSalesContract>(Query, page - 1, size);
            List<GarmentSalesContract> data = pageable.Data.ToList<GarmentSalesContract>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<GarmentSalesContract>(data, totalData, OrderDictionary, SelectedFields);
        }

        public override void Create(GarmentSalesContract model)
        {
            GenerateNo(model);
            if (model.SalesContractROs.Count > 0)
                foreach (var detail in model.SalesContractROs)
                {
                    garmentSalesContractROLogic.Create(detail);
                    if (detail.Items.Count > 0)
                    {
                        foreach(var item in detail.Items)
                        {
                            garmentSalesContractItemLogic.Create(item);
                        }
                    }
                    //EntityExtension.FlagForCreate(detail, IdentityService.Username, "sales-service");
                }

            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            DbSet.Add(model);
        }

        public override async Task<GarmentSalesContract> ReadByIdAsync(long id)
        {
            var garmentSalesContract = await DbSet.Include(p => p.SalesContractROs).ThenInclude(o=>o.Items).FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
            garmentSalesContract.SalesContractROs = garmentSalesContract.SalesContractROs.OrderBy(s => s.Id).ToArray();
            return garmentSalesContract;
        }

        public GarmentSalesContract ReadByCostCal(int id)
        {
            var garmentSalesContract = (from a in DbSet join b in DbContext.GarmentSalesContractROs
                                       on a.Id equals b.SalesContractId
                                       where b.CostCalculationId==id select a).FirstOrDefault();

            //DbSet.Where(d => d.CostCalculationId.Equals(id) && d.IsDeleted.Equals(false)).FirstOrDefault();

            return garmentSalesContract;
        }

        public override void UpdateAsync(long id, GarmentSalesContract model)
        {
            if (model.SalesContractROs != null)
            {
                HashSet<long> roIds = garmentSalesContractROLogic.GetGSalesContractIds(id);
                if (roIds.Count.Equals(0))
                {
                    foreach (var detail in model.SalesContractROs)
                    {
                        garmentSalesContractROLogic.Create(detail);
                        EntityExtension.FlagForCreate(detail, IdentityService.Username, "sales-service");
                        foreach(var item in detail.Items)
                        {
                            garmentSalesContractItemLogic.Create(item);
                            EntityExtension.FlagForCreate(item, IdentityService.Username, "sales-service");
                        }
                    }
                }
                else
                {
                    foreach (var roId in roIds)
                    {

                        GarmentSalesContractRO data = model.SalesContractROs.FirstOrDefault(prop => prop.Id.Equals(roId));
                        if (data == null)
                        {

                            GarmentSalesContractRO dataRO = DbContext.GarmentSalesContractROs.AsNoTracking().Include(a=>a.Items).FirstOrDefault(prop => prop.Id.Equals(roId));
                            if (dataRO.Items != null)
                            {
                                foreach (var item in dataRO.Items)
                                {
                                    GarmentSalesContractItem dataItem = DbContext.GarmentSalesContractItems.AsNoTracking().FirstOrDefault(prop => prop.Id.Equals(item.Id));
                                    EntityExtension.FlagForDelete(dataItem, IdentityService.Username, "sales-service");
                                    garmentSalesContractItemLogic.UpdateAsync(dataItem.Id, item);
                                }
                            }

                            garmentSalesContractROLogic.UpdateAsync(dataRO.Id,dataRO);
                            EntityExtension.FlagForDelete(dataRO, IdentityService.Username, "sales-service");
                            
                        }
                        else
                        {
                            garmentSalesContractROLogic.UpdateAsync(roId, data);
                            foreach(var item in data.Items)
                            {
                                GarmentSalesContractItem dataItem = DbContext.GarmentSalesContractItems.FirstOrDefault(prop => prop.Id.Equals(item.Id));
                                if (dataItem == null && item.Id>0)
                                {
                                    EntityExtension.FlagForDelete(dataItem, IdentityService.Username, "sales-service");
                                }
                                else if (dataItem != null && item.Id > 0)
                                {
                                    garmentSalesContractItemLogic.UpdateAsync(item.Id, item);
                                }
                            }
                            
                        }

                        foreach(var ro in model.SalesContractROs)
                        {
                            if (ro.Id == 0)
                            {
                                garmentSalesContractROLogic.Create(ro);
                                foreach(var item in ro.Items)
                                {
                                    garmentSalesContractItemLogic.Create(item);
                                    EntityExtension.FlagForCreate(item, IdentityService.Username, "sales-service");
                                }
                            }
                            else
                            {
                                foreach (GarmentSalesContractItem i in ro.Items)
                                {
                                    if (i.Id <= 0)
                                    {
                                        garmentSalesContractItemLogic.Create(i);
                                        EntityExtension.FlagForCreate(i, IdentityService.Username, "sales-service");
                                    }
                                }
                            }
                        }
                        
                    }

                }

            }

            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            DbSet.Update(model);
        }

        public override async Task DeleteAsync(long id)
        {
            var model = await ReadByIdAsync(id);
            foreach(var ro in model.SalesContractROs)
            {
                foreach (var item in ro.Items)
                {
                    EntityExtension.FlagForDelete(item, IdentityService.Username, "sales-service");
                }
                EntityExtension.FlagForDelete(ro, IdentityService.Username, "sales-service");
            }
            

            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            DbSet.Update(model);
        }

        private void GenerateNo(GarmentSalesContract model)
        {
            string Year = model.CreatedUtc.ToString("yy");
            string Month = model.CreatedUtc.ToString("MM");

            string no = $"{model.BuyerBrandCode}/SC/AG/{Year}";
            int Padding = 5;

            var lastData = DbSet.IgnoreQueryFilters().Where(w => w.SalesContractNo.StartsWith(no) && !w.IsDeleted).OrderByDescending(o => o.CreatedUtc).FirstOrDefault();

            //string DocumentType = model.BuyerType.ToLower().Equals("ekspor") || model.BuyerType.ToLower().Equals("export") ? "FPE" : "FPL";

            int YearNow = DateTime.Now.Year;
            int MonthNow = DateTime.Now.Month;

            if (lastData == null)
            {
                model.SalesContractNo = no + "1".PadLeft(Padding, '0');
            }
            else
            {
                int lastNoNumber = Int32.Parse(lastData.SalesContractNo.Replace(no, "")) + 1;
                model.SalesContractNo = no + lastNoNumber.ToString().PadLeft(Padding, '0');
            }

        }

        public GarmentSalesContract ReadByRO(string ro)
        {
            var garmentSalesContract = this.DbSet
               .Include(d => d.SalesContractROs)
               .ThenInclude(e => e.Items)
               .Where(d => d.IsDeleted.Equals(false) && d.SalesContractROs.Any(e => e.IsDeleted.Equals(false) && e.RONumber.Equals(ro) && (e.Items.Count > 0 ? e.Items.Any(f => f.IsDeleted.Equals(false)) : true))).FirstOrDefault();

            return garmentSalesContract;
        }

        //async Task<string> GenerateNo(GarmentSalesContract model)
        //{
        //    string Year = model.CreatedUtc.ToString("yy");
        //    string Month = model.CreatedUtc.ToString("MM");

        //    string no = $"{model.ComodityCode}/SC/DL/{Year}/";
        //    int Padding = 4;

        //    var lastNo = await DbContext.GarmentSalesContracts.Where(w => w.SalesContractNo.StartsWith(no) && !w.IsDeleted).OrderByDescending(o => o.CreatedUtc).FirstOrDefaultAsync();

        //    if (lastNo == null)
        //    {
        //        return no + "1".PadLeft(Padding, '0');
        //    }
        //    else
        //    {
        //        int lastNoNumber = Int32.Parse(lastNo.SalesContractNo.Replace(no, "")) + 1;
        //        return no + lastNoNumber.ToString().PadLeft(Padding, '0');
        //    }
        //}
    }
}
