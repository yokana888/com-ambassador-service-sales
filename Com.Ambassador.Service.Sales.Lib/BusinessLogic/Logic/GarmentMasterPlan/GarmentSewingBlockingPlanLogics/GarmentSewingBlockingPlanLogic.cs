using Com.Ambassador.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel;
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

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.GarmentSewingBlockingPlanLogics
{
    public class GarmentSewingBlockingPlanLogic : BaseLogic<GarmentSewingBlockingPlan>
    {
        private readonly SalesDbContext DbContext;
        public GarmentSewingBlockingPlanLogic(IIdentityService IdentityService, SalesDbContext dbContext) : base(IdentityService, dbContext)
        {
            DbContext = dbContext;
        }

        public override ReadResponse<GarmentSewingBlockingPlan> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<GarmentSewingBlockingPlan> Query = this.DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "BookingOrderNo", "BuyerName"
            };

            Query = QueryHelper<GarmentSewingBlockingPlan>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<GarmentSewingBlockingPlan>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = select ?? new List<string>()
            {
                "BookingOrderNo", "Buyer", "DeliveryDate", "BookingOrderDate","Remark","Status"
            };

            Query = Query
                .Select(field => new GarmentSewingBlockingPlan
                {
                    Id = field.Id,
                    BookingOrderNo=field.BookingOrderNo,
                    BookingOrderDate=field.BookingOrderDate,
                    BuyerName=field.BuyerName,
                    OrderQuantity=field.OrderQuantity,
                    DeliveryDate=field.DeliveryDate,
                    Remark=field.Remark,
                    LastModifiedUtc = field.LastModifiedUtc,
                    Status=field.Status,
                    Items=field.Items
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<GarmentSewingBlockingPlan>.Order(Query, OrderDictionary);

            Pageable<GarmentSewingBlockingPlan> pageable = new Pageable<GarmentSewingBlockingPlan>(Query, page - 1, size);
            List<GarmentSewingBlockingPlan> data = pageable.Data.ToList<GarmentSewingBlockingPlan>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<GarmentSewingBlockingPlan>(data, totalData, OrderDictionary, SelectedFields);
        }

        public override void Create(GarmentSewingBlockingPlan model)
        {
            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            model.Status = "Booking";
            GarmentBookingOrder booking = DbContext.GarmentBookingOrders.FirstOrDefault(b => b.Id == model.BookingOrderId);
            booking.IsBlockingPlan = true;
            foreach (var item in model.Items)
            {
                GarmentWeeklyPlanItem week = DbContext.GarmentWeeklyPlanItems.FirstOrDefault(a => a.Id == item.WeeklyPlanItemId);
                week.UsedEH += (int)item.EHBooking;
                week.RemainingEH-= (int)item.EHBooking;
                if (item.IsConfirm)
                {
                    week.WHConfirm += Math.Round((item.EHBooking / (week.Operator * week.Efficiency)), 2);
                }
                
                
                EntityExtension.FlagForCreate(item, IdentityService.Username, "sales-service");
            }
            base.Create(model);
        }

        public override async Task<GarmentSewingBlockingPlan> ReadByIdAsync(long id)
        {
            var model = await DbSet.AsNoTracking().Include(d => d.Items).FirstOrDefaultAsync(d => d.Id == id);
            model.Items = model.Items.ToList();
            return model;
        }

        public override async Task DeleteAsync(long id)
        {
            var model = await DbSet.Include(d => d.Items).FirstOrDefaultAsync(d => d.Id == id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);

            GarmentBookingOrder booking = DbContext.GarmentBookingOrders.FirstOrDefault(b => b.Id == model.BookingOrderId);
            if(booking!=null)
                booking.IsBlockingPlan = false;
            foreach (var item in model.Items)
            {
                GarmentWeeklyPlanItem week = DbContext.GarmentWeeklyPlanItems.FirstOrDefault(a => a.Id == item.WeeklyPlanItemId);
                week.UsedEH -= (int)item.EHBooking;
                week.RemainingEH += (int)item.EHBooking;
                if (item.IsConfirm)
                    week.WHConfirm -= Math.Round((item.EHBooking / (week.Operator * week.Efficiency)), 2);

                EntityExtension.FlagForDelete(item, IdentityService.Username, "sales-service", true);
            }
        }

        public override void UpdateAsync(long id, GarmentSewingBlockingPlan newModel)
        {
            var model = DbSet.AsNoTracking().Include(d => d.Items).FirstOrDefault(d => d.Id == id);

            if(model.Status=="Booking Ada Perubahan")
            {
                newModel.Status = "Booking";
            }

            foreach (var item in model.Items)
            {
                GarmentWeeklyPlanItem week = DbContext.GarmentWeeklyPlanItems.FirstOrDefault(a => a.Id == item.WeeklyPlanItemId);
                week.UsedEH -= (int)item.EHBooking;
                week.RemainingEH += (int)item.EHBooking;
                if (item.IsConfirm)
                    week.WHConfirm -= Math.Round((item.EHBooking / (week.Operator * week.Efficiency)), 2);
            }

            foreach (var newPlan in newModel.Items)
            {
                GarmentWeeklyPlanItem week = DbContext.GarmentWeeklyPlanItems.FirstOrDefault(a => a.Id == newPlan.WeeklyPlanItemId);
                //var oldItem = model.Items.FirstOrDefault(i => i.Id == newPlan.Id);
                if (newPlan.Id==0)
                {
                    
                    week.UsedEH += (int)newPlan.EHBooking;
                    week.RemainingEH -= (int)newPlan.EHBooking;
                    if (newPlan.IsConfirm)
                        week.WHConfirm += Math.Round((newPlan.EHBooking / (week.Operator * week.Efficiency)), 2);

                    EntityExtension.FlagForCreate(newPlan, IdentityService.Username, "sales-service");
                }
                else
                {
                    week.UsedEH += (int)newPlan.EHBooking;
                    week.RemainingEH -= (int)newPlan.EHBooking;
                    if (newPlan.IsConfirm)
                        week.WHConfirm += Math.Round((newPlan.EHBooking / (week.Operator * week.Efficiency)), 2);

                    EntityExtension.FlagForUpdate(newPlan, IdentityService.Username, "sales-service");
                }
            }


            DbSet.Update(newModel);

            foreach (var oldItem in model.Items)
            {

                var newItem = newModel.Items.FirstOrDefault(i => i.Id == oldItem.Id);
                if (newItem == null)
                {
                    EntityExtension.FlagForDelete(oldItem, IdentityService.Username, "sales-service");
                    DbContext.GarmentSewingBlockingPlanItems.Update(oldItem);
                }
            }


            //DbSet.Update(model);

            EntityExtension.FlagForUpdate(newModel, IdentityService.Username, "sales-service");

        }
    }
}
