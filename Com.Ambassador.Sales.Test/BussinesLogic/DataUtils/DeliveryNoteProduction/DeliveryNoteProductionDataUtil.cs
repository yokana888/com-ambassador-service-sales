using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.DeliveryNoteProduction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.DeliveryNoteProduction
{
    public class DeliveryNoteProductionDataUtil : BaseDataUtil<DeliveryNoteProductionFacade, DeliveryNoteProductionModel>
    {
        public DeliveryNoteProductionDataUtil(DeliveryNoteProductionFacade facade) : base(facade)
        {
        }

        public override Task<DeliveryNoteProductionModel> GetNewData()
        {
            return Task.FromResult(new DeliveryNoteProductionModel()
            {
                Code = "code",
                Date = DateTimeOffset.UtcNow,
                SalesContractNo = "name",
                Unit = "Unit",
                Subject = "no",
                OtherSubject = "OtherSubject",
                BuyerName = "name",
                BuyerType = "name",
                TypeandNumber = "TypeandNumber",
                Total = 1,
                Uom = "cpe",
                Blended = "code",
                DeliveredTo = "remark",
                Month = "type",
                Year = "name",
                BallMark = "cpde",
                Sample = "cpde",
                Remark = "name",
                YarnSales = "cpde",
                MonthandYear = "cpde",
            });
        }
    }
}
