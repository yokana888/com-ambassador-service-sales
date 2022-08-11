using Com.Ambassador.Sales.Test.WebApi.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.MaxWHConfirmInterfaces;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentMasterPlan.MaxWHConfirmModel;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MaxWHConfirmViewModels;
using Com.Ambassador.Service.Sales.WebApi.Controllers.GarmentMasterPlan.MaxWHConfirmControllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers.GarmentMasterPlan.MaxWHConfirmControllerTests
{
    public class MaxWHConfirmControllerTest : BaseControllerTest<MaxWHConfirmController, MaxWHConfirm, MaxWHConfirmViewModel, IMaxWHConfirmFacade>
    {
        protected override MaxWHConfirmViewModel ViewModel
        {
            get
            {
                var viewModel = base.ViewModel;
                
                return viewModel;
            }
        }
    }
}
