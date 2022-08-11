using AutoMapper;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.DOAval;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.DOSales;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.DOAvalProfiles;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.DOAval;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.DOAval;
using Com.Ambassador.Service.Sales.Lib.Models.DOSales;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.DOAval;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.DOAval
{
    public class DOAvalFacadeTest : BaseFacadeTest<SalesDbContext, DOAvalFacade, DOAvalLogic, DOSalesModel, DOAvalDataUtil>
    {
        private const string ENTITY = "DOAval";
        public DOAvalFacadeTest() : base(ENTITY)
        {
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DOAvalMapper>();
            });
            var mapper = configuration.CreateMapper();

            DOAvalViewModel vm = new DOAvalViewModel
            {
                Id = 1,
                DOAvalItems = new List<DOAvalItemViewModel>()
                {
                    new DOAvalItemViewModel()
                }
            };
            DOSalesModel model = mapper.Map<DOSalesModel>(vm);

            Assert.Equal(vm.Id, model.Id);

            var vm2 = mapper.Map<DOAvalViewModel>(model);

            Assert.Equal(vm2.Id, model.Id);
        }

        [Fact]
        public virtual void ValidateVM()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DOAvalFacade facade = new DOAvalFacade(serviceProvider, dbContext);

            var data = new DOAvalViewModel();
            var validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(data));

            data.DOAvalItems = new List<DOAvalItemViewModel>()
            {
                new DOAvalItemViewModel()
            };

            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(data));
        }
    }
}
