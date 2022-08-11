using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.Garment.GarmentMerchandiser;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic;
using Com.Ambassador.Service.Sales.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.Garment.GarmentMerchandiser
{
    public class RateFacadeTest : BaseFacadeTest<SalesDbContext, RateFacade, RateLogic, Rate, RateDataUtil>
    {
        private const string ENTITY = "Rate";

        public RateFacadeTest() : base(ENTITY)
        {
        }
    }
}
