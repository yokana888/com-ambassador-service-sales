using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades;
using Com.Ambassador.Service.Sales.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.Garment.GarmentMerchandiser
{
    public class RateDataUtil : BaseDataUtil<RateFacade, Rate>
    {
        public RateDataUtil(RateFacade facade) : base(facade)
        {
        }

        public override Task<Rate> GetNewData()
        {
            return Task.FromResult(new Rate
            {
                Name = ""
            });
        }
    }
}
