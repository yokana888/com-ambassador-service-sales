using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting
{
    public class FinishingPrintingPreSalesContractLogic : BaseLogic<FinishingPrintingPreSalesContractModel>
    {
        public FinishingPrintingPreSalesContractLogic(IIdentityService IdentityService, SalesDbContext dbContext) : base(IdentityService, dbContext)
        {
        }

        public override ReadResponse<FinishingPrintingPreSalesContractModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<FinishingPrintingPreSalesContractModel> query = DbSet;
            List<string> SearchAttributes = new List<string>()
            {
                "No", "Type", "BuyerName", "UnitName"
            };

            query = QueryHelper<FinishingPrintingPreSalesContractModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<FinishingPrintingPreSalesContractModel>.Filter(query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "LastModifiedUtc", "CreatedUtc", "No", "Date", "Type", "Buyer",  "Unit",  "ProcessType",
               "OrderQuantity", "Remark", "IsPosted"
            };

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<FinishingPrintingPreSalesContractModel>.Order(query, OrderDictionary);

            Pageable<FinishingPrintingPreSalesContractModel> pageable = new Pageable<FinishingPrintingPreSalesContractModel>(query, page - 1, size);
            List<FinishingPrintingPreSalesContractModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<FinishingPrintingPreSalesContractModel>(data, totalData, OrderDictionary, SelectedFields);
        }

        public override void Create(FinishingPrintingPreSalesContractModel model)
        {
            GenerateNo(model);
            base.Create(model);
        }

        private void GenerateNo(FinishingPrintingPreSalesContractModel model)
        {
            string Year = model.Date.ToString("yy");
            //string Month = model.CreatedUtc.ToString("MM");

            string code = model.UnitCode == "F1" ? "D" : "F";

            string no = $"SC-{model.BuyerCode}-{code}-{Year}-";
            int Padding = 5;

            var lastData = DbSet.IgnoreQueryFilters().Where(w => w.No.StartsWith(no) && w.Type == model.Type && !w.IsDeleted).OrderByDescending(o => o.CreatedUtc).FirstOrDefault();

            //string DocumentType = model.BuyerType.ToLower().Equals("ekspor") || model.BuyerType.ToLower().Equals("export") ? "FPE" : "FPL";

            int YearNow = DateTime.Now.Year;

            if (lastData == null)
            {
                model.No = no + "1".PadLeft(Padding, '0');
            }
            else
            {
                string tempSCNo = lastData.No;
                if (model.Type.ToUpper() == "SAMPLE")
                {
                    tempSCNo = lastData.No.Substring(0, lastData.No.Length - 2);
                }
                int lastNoNumber = Int32.Parse(tempSCNo.Replace(no, "")) + 1;
                model.No = no + lastNoNumber.ToString().PadLeft(Padding, '0');
            }

            if (model.Type.ToUpper() == "SAMPLE")
            {
                model.No += "-S";
            }
        }

        public async Task PreSalesPost(List<long> listId)
        {
            foreach (var id in listId)
            {
                var model = await ReadByIdAsync(id);
                model.IsPosted = true;
                UpdateAsync(id, model);
            }
        }

        public async Task PreSalesUnpost(long id)
        {
            var model = await ReadByIdAsync(id);
            model.IsPosted = false;
            UpdateAsync(id, model);
        }
    }
}
