using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.Garment.GarmentMerchandiser;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.ROGarment;
using Com.Ambassador.Service.Sales.Lib.Models.ROGarments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.RoGarmentDataUtils
{
    public class ROGarmentDataUtil : BaseDataUtil<ROGarmentFacade, RO_Garment>
    {
        private readonly CostCalculationGarmentDataUtil costCalculationGarmentDataUtil;

        public ROGarmentDataUtil(ROGarmentFacade facade, CostCalculationGarmentDataUtil costCalculationGarmentDataUtil) : base(facade)
        {
            this.costCalculationGarmentDataUtil = costCalculationGarmentDataUtil;
        }

        public override async Task<RO_Garment> GetNewData()
        {
            var costCalculation = await costCalculationGarmentDataUtil.GetTestData();

            var data = await base.GetNewData();
            data.CostCalculationGarment = costCalculation;
            data.CostCalculationGarmentId = costCalculation.Id;

            data.Code = "test";
            data.Instruction = "test";
            data.Total = 1;
            data.ImagesPath ="[\"/sales/RO_Garment/IMG_1_0_201907300436141716\"]";
            data.RO_Garment_SizeBreakdowns = new List<RO_Garment_SizeBreakdown>()
            {
                new RO_Garment_SizeBreakdown
                {
                    RO_GarmentId = data.Id,
                    Code = "FABRIC",
                    ColorId = 1,
                    ColorName = "test",
                    Total = 1,
                    RO_Garment_SizeBreakdown_Details = new List<RO_Garment_SizeBreakdown_Detail>()
                    {
                        new RO_Garment_SizeBreakdown_Detail
                        {
                            Code = "FABRIC",
                            Information = "test",
                            Size = "L",
                            Quantity = 1
                        }

                    }
                }
            };

            return data;
        }
    }
}