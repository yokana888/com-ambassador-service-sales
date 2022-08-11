using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentBookingOrderDataUtils;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentMasterPlan.MaxWHConfirmDataUtils;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentMasterPlan.WeeklyPlanDataUtils;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.GarmentSewingBlockingPlanFacades;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentMasterPlan.GarmentSewingBlockingPlanDataUtils
{
    public class GarmentSewingBlockingPlanDataUtil : BaseDataUtil<GarmentSewingBlockingPlanFacade, GarmentSewingBlockingPlan>
    {

        private readonly WeeklyPlanDataUtil weeklyPlanDataUtil;
        private readonly GarmentBookingOrderDataUtil garmentBookingOrderDataUtil;
        private readonly MaxWHConfirmDataUtil maxWHConfirmDataUtil;
        public GarmentSewingBlockingPlanDataUtil(GarmentSewingBlockingPlanFacade facade, WeeklyPlanDataUtil weeklyPlanDataUtil, GarmentBookingOrderDataUtil garmentBookingOrderDataUtil, MaxWHConfirmDataUtil maxWHConfirmDataUtil) : base(facade)
        {
            this.weeklyPlanDataUtil = weeklyPlanDataUtil;
            this.garmentBookingOrderDataUtil = garmentBookingOrderDataUtil;
            this.maxWHConfirmDataUtil = maxWHConfirmDataUtil;
        }

        public override Task<GarmentSewingBlockingPlan> GetNewData()
        {
            var datas = Task.Run(() => garmentBookingOrderDataUtil.GetTestData()).Result;
            var dataWeek= Task.Run(() => weeklyPlanDataUtil.GetTestData()).Result;
            var dataWH= Task.Run(() => maxWHConfirmDataUtil.GetTestData()).Result;
            var week = dataWeek.Items.First();
            var garmentSewingBlockingPlan = new GarmentSewingBlockingPlan
            {
                BookingOrderId=datas.Id,
                BookingOrderNo=datas.BookingOrderNo,
                BookingOrderDate=datas.BookingOrderDate,
                BuyerCode=datas.BuyerCode,
                BuyerId=datas.BuyerId,
                BuyerName=datas.BuyerName,
                DeliveryDate=datas.DeliveryDate,
                Remark=datas.Remark,
                Items = new List<GarmentSewingBlockingPlanItem> {
                    new GarmentSewingBlockingPlanItem
                    {
                        IsConfirm=true,
                        WeekNumber=week.WeekNumber,
                        WeeklyPlanId=dataWeek.Id,
                        WeeklyPlanItemId=week.Id,
                        Year=dataWeek.Year,
                        ComodityCode="ComodityTest",
                        ComodityId=1,
                        ComodityName="TestComo",
                        DeliveryDate=datas.DeliveryDate.AddDays(-1),
                        Efficiency=week.Efficiency,
                        EHBooking=10,
                        EndDate=week.EndDate,
                        StartDate=week.StartDate,
                        OrderQuantity=10,
                        UnitName="UnitNameTest",
                        UnitCode="UnitCodeTest",
                        UnitId=1,
                        SMVSewing=1,
                        Remark="test"
                    },
                    new GarmentSewingBlockingPlanItem
                    {
                        WeekNumber=week.WeekNumber,
                        WeeklyPlanId=dataWeek.Id,
                        WeeklyPlanItemId=week.Id,
                        Year=dataWeek.Year,
                        ComodityCode="ComodityTest",
                        ComodityId=1,
                        ComodityName="TestComo",
                        DeliveryDate=datas.DeliveryDate.AddDays(-1),
                        Efficiency=week.Efficiency,
                        EHBooking=10,
                        EndDate=week.EndDate,
                        StartDate=week.StartDate,
                        OrderQuantity=10,
                        UnitName="SK",
                        UnitCode="SK",
                        UnitId=1,
                        SMVSewing=1,
                        Remark="test"
                    }
                }
            };
            
            return Task.FromResult(garmentSewingBlockingPlan);
        }
    }
}
