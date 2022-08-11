using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentOmzetTargetFacades;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentOmzetTargetModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentOmzetTargetDataUtils
{
    public class GarmentOmzetTargetDataUtil : BaseDataUtil<GarmentOmzetTargetFacade, GarmentOmzetTarget>
    {
        public GarmentOmzetTargetDataUtil(GarmentOmzetTargetFacade facade) : base(facade)
        {
        }

        public override Task<GarmentOmzetTarget> GetNewData()
        {
            return Task.FromResult(new GarmentOmzetTarget()
            {
                SectionId = 1,
                SectionCode = "Test",
                SectionName = "Test",
                QuaterCode = "Test",
                YearOfPeriod = "1900",
                MonthOfPeriod = "Test",
                Amount = 12345.67,
            });
        }
    }
}