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
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib
{
    public class DeliveryNoteProductionLogic : BaseLogic<DeliveryNoteProductionModel>
    {
        private readonly SalesDbContext _dbContext;
        public DeliveryNoteProductionLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
            _dbContext = dbContext;
        }

        public override ReadResponse<DeliveryNoteProductionModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            var query = DbSet.AsQueryable();

            List<string> SearchAttributes = new List<string>()
            {
                "Unit","MonthandYear"
            };
            query = QueryHelper<DeliveryNoteProductionModel>.Search(query, SearchAttributes, keyword);
            List<string> SelectedFields = new List<string>()
            {
                "Id", "CreatedUtc", "LastModifiedUtc","Code", "SalesContract", "Unit", "Subject", "OtherSubject",
                "Month","Year","BallMark","Sample","Remark","YarnSales","MonthandYear"
            };
            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DeliveryNoteProductionModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DeliveryNoteProductionModel>.Order(query, OrderDictionary);

            Pageable<DeliveryNoteProductionModel> pageable = new Pageable<DeliveryNoteProductionModel>(query, page - 1, size);
            List<DeliveryNoteProductionModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DeliveryNoteProductionModel>(data, totalData, OrderDictionary, SelectedFields);
        }

        public override Task<DeliveryNoteProductionModel> ReadByIdAsync(long id)
        {
            return DbSet.FirstOrDefaultAsync(d => d.Id.Equals(id));
        }

        public override async Task DeleteAsync(long id)
        {
            DeliveryNoteProductionModel model = await ReadByIdAsync(id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            DbSet.Update(model);
        }

        public override void UpdateAsync(long id, DeliveryNoteProductionModel model)
        {
            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            DbSet.Update(model);
        }

        public override void Create(DeliveryNoteProductionModel model)
        {
            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            DbSet.Add(model);
        }
    }
}