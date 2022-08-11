using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades;
using Com.Ambassador.Service.Sales.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Sales.Test.BussinesLogic.DataUtils
{
    public class EfficiencyDataUtil : BaseDataUtil<EfficiencyFacade, Efficiency>
    {
        public EfficiencyDataUtil(EfficiencyFacade facade) : base(facade)
        {
        }

        public override Task<Efficiency> GetNewData()
        {
            return Task.FromResult(new Efficiency()
            {
                Value = 1,
                Code = "c",
                FinalRange = 1,
                InitialRange = 1,
                Name = "name"
            });
        }
    }
}
