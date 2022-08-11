using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.FinishingPrintingCostCalculation;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Moq;
using Com.Ambassador.Service.Sales.Lib.Services;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Xunit;
using Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.FinishingPrintingCostCalculationProfiles;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrintingCostCalculation;
using System.Threading.Tasks;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationFacadeTest : BaseFacadeTest<SalesDbContext, FinishingPrintingCostCalculationFacade, FinishingPrintingCostCalculationLogic, FinishingPrintingCostCalculationModel, FinishingPrintingCostCalculationDataUtils>
    {
        private const string ENTITY = "FinishingPrintingCostCalculation";

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = base.GetServiceProviderMock(dbContext);

            var azureImageFacadeMock = new Mock<IAzureImageFacade>();
            azureImageFacadeMock
                .Setup(s => s.DownloadImage(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("");

            azureImageFacadeMock
                .Setup(s => s.UploadImage(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                .ReturnsAsync("");

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IAzureImageFacade)))
                .Returns(azureImageFacadeMock.Object);

            return serviceProviderMock;
        }
        public FinishingPrintingCostCalculationFacadeTest() : base(ENTITY)
        {
        }

        [Fact]
        public async void Update_Chemical_Success()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FinishingPrintingCostCalculationMapper>();
                cfg.AddProfile<FinishingPrintingCostCalculationMachineMapper>();
                cfg.AddProfile<FinishingPrintingCostCalculationChemicalMapper>();
            });
            var mapper = configuration.CreateMapper();

            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            FinishingPrintingCostCalculationFacade facade = Activator.CreateInstance(typeof(FinishingPrintingCostCalculationFacade), serviceProvider, dbContext) as FinishingPrintingCostCalculationFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();
            var vmch = mapper.Map<FinishingPrintingCostCalculationViewModel>(data);
            vmch.Machines.FirstOrDefault().Chemicals.Add(new FinishingPrintingCostCalculationChemicalViewModel()
            {
                Chemical = new ProductViewModel()
                {
                    Id = 1,
                    Name = "a",
                    Price = 1,
                    Currency = new CurrencyViewModel()
                    {
                        Id = 1
                    },
                },
                ChemicalQuantity = 1,
            });

            var modelCH = mapper.Map<FinishingPrintingCostCalculationModel>(vmch);

            var response = await facade.UpdateAsync((int)modelCH.Id, modelCH);

            Assert.NotEqual(response, 0);


            var vmData = mapper.Map<FinishingPrintingCostCalculationViewModel>(data);
            vmData.Machines.FirstOrDefault().Chemicals.Clear();

            var model = mapper.Map<FinishingPrintingCostCalculationModel>(vmData);
            FinishingPrintingCostCalculationFacade facade2 = Activator.CreateInstance(typeof(FinishingPrintingCostCalculationFacade), serviceProvider, dbContext) as FinishingPrintingCostCalculationFacade;

            response = await facade2.UpdateAsync((int)model.Id, model);
            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void UpdateMachine_Success()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FinishingPrintingCostCalculationMapper>();
                cfg.AddProfile<FinishingPrintingCostCalculationMachineMapper>();
                cfg.AddProfile<FinishingPrintingCostCalculationChemicalMapper>();
            });
            var mapper = configuration.CreateMapper();
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            FinishingPrintingCostCalculationFacade facade = Activator.CreateInstance(typeof(FinishingPrintingCostCalculationFacade), serviceProvider, dbContext) as FinishingPrintingCostCalculationFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();
            var vmDataCreated = mapper.Map<FinishingPrintingCostCalculationViewModel>(data);

            vmDataCreated.Machines.Add(new FinishingPrintingCostCalculationMachineViewModel()
            {
                Chemicals = new List<FinishingPrintingCostCalculationChemicalViewModel>()
                        {
                            new FinishingPrintingCostCalculationChemicalViewModel()
                            {
                                Chemical = new ProductViewModel()
                                {
                                    Id = 1,
                                    Name = "a",
                                    Price = 1,
                                    Currency = new CurrencyViewModel(){
                                        Id = 1
                                    },
                                },
                                ChemicalQuantity = 1,

                            }
                        },
                Machine = new MachineViewModel()
                {
                    Name = "name",
                    Id = 1,
                    Electric = 1
                },
                Step = new StepViewModel()
                {
                    Id = 1,
                    Process = "aa"
                },
                Depretiation = 1,
            });
            var modelCreated = mapper.Map<FinishingPrintingCostCalculationModel>(vmDataCreated);

            var response = await facade.UpdateAsync((int)modelCreated.Id, modelCreated);

            Assert.NotEqual(response, 0);


            var vmData = mapper.Map<FinishingPrintingCostCalculationViewModel>(data);
            vmData.Machines.Clear();

            var model = mapper.Map<FinishingPrintingCostCalculationModel>(vmData);
            FinishingPrintingCostCalculationFacade facade2 = Activator.CreateInstance(typeof(FinishingPrintingCostCalculationFacade), serviceProvider, dbContext) as FinishingPrintingCostCalculationFacade;

            response = await facade2.UpdateAsync((int)model.Id, model);
            Assert.NotEqual(response, 0);
        }


        [Fact]
        public virtual async void Create_Printing_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            FinishingPrintingCostCalculationFacade facade = Activator.CreateInstance(typeof(FinishingPrintingCostCalculationFacade), serviceProvider, dbContext) as FinishingPrintingCostCalculationFacade;

            var data = await DataUtil(facade, dbContext).GetNewData();
            data.UnitName = "Printing";
            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public  async Task CreateAsync_Throws_Exception()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            FinishingPrintingCostCalculationFacade facade = new FinishingPrintingCostCalculationFacade(serviceProvider, dbContext);
            var data = await DataUtil(facade, dbContext).GetNewData();
            data.Machines = null;
            await Assert.ThrowsAsync<System.ArgumentNullException>(() => facade.CreateAsync(data));
        }

        [Fact]
        public async void CCPost_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            FinishingPrintingCostCalculationFacade facade = new FinishingPrintingCostCalculationFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();
            List<long> listData = new List<long> { data.Id };
            var Response = await facade.CCPost(listData);
            Assert.NotEqual(Response, 0);
        }

        [Fact]
        public async void CCApprovePPIC_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            FinishingPrintingCostCalculationFacade facade = new FinishingPrintingCostCalculationFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();

            var Response = await facade.CCApprovePPIC(data.Id);
            Assert.NotEqual(Response, 0);
        }

        [Fact]
        public async void CCApproveMD_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            FinishingPrintingCostCalculationFacade facade = new FinishingPrintingCostCalculationFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();

            var Response = await facade.CCApproveMD(data.Id);
            Assert.NotEqual(Response, 0);
        }

        [Fact]
        public async void ReadParent_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            FinishingPrintingCostCalculationFacade facade = new FinishingPrintingCostCalculationFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();

            var Response = await facade.ReadParent(data.Id);
            Assert.NotNull(Response);
        }

        [Fact]
        public async void ValidatePreSalesContractId_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            FinishingPrintingCostCalculationFacade facade = new FinishingPrintingCostCalculationFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();

            var Response = await facade.ValidatePreSalesContractId(data.PreSalesContractId);
            Assert.True(Response);
        }

        [Fact]
        public async void ValidateVM()
        {

            var vm = new FinishingPrintingCostCalculationViewModel()
            {
                Remark = "1",

                ProductionOrderNo = "ee",
                IsPosted = false,
                ManufacturingServiceCost = 1,
                HelperMaterial = 1,
                MiscMaterial = 1,
                Lubricant = 1,
                SparePart = 1,
                StructureMaintenance = 1,
                MachineMaintenance = 1,
                Embalase = 1,
                GeneralAdministrationCost = 1,
                DirectorOfficeCost = 1,
                BankMiscCost = 1

            };

            var sp = GetServiceProviderMock(DbContext(GetCurrentMethod()));

            var facade = new FinishingPrintingCostCalculationFacade(sp.Object, DbContext(GetCurrentMethod()));
            sp.Setup(s => s.GetService(typeof(IFinishingPrintingCostCalculationService)))
                .Returns(facade);

            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(vm, sp.Object, null);
            var response = vm.Validate(validationContext);
            Assert.NotEmpty(response);
            Assert.NotNull(vm.Remark);
            Assert.NotNull(vm.ProductionOrderNo);
            Assert.False(vm.IsPosted);


            var data = await DataUtil(facade, DbContext(GetCurrentMethod())).GetTestData();

            vm.PreSalesContract = new FinishingPrintingPreSalesContractViewModel()
            {
                Id = data.PreSalesContractId,
                Unit = new UnitViewModel()
                {
                    Name = data.UnitName
                }
            };
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.PreSalesContract = new FinishingPrintingPreSalesContractViewModel()
            {
                Id = 0,
                Unit = new UnitViewModel()
                {
                    Name = "Printing"
                }
            };

            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.Instruction = new InstructionViewModel()
            {
                Id = 1
            };
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.UOM = new UomViewModel()
            {
                Id = 1,
            };
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.Sales = new AccountViewModel()
            {
                profile = new ProfileViewModel()
                {
                    firstname = "a",
                    lastname = "a"
                },
                UserName = "a"
            };
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.Color = "a";
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.Greige = new ProductViewModel()
            {
                Id = 1
            };
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.Date = DateTimeOffset.UtcNow;
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.Material = new MaterialViewModel()
            {
                Id = 1
            };
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.CurrencyRate = 1;
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.ProductionUnitValue = 1;
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.ScreenCost = 1;
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.ScreenDocumentNo = "as";
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.PreparationFabricWeight = 1;
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.RFDFabricWeight = 1;
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.ActualPrice = 1;
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.ConfirmPrice = 1;
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.FreightCost = 1;
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.Machines = new List<FinishingPrintingCostCalculationMachineViewModel>()
            {
                new FinishingPrintingCostCalculationMachineViewModel()
            };
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.Machines = new List<FinishingPrintingCostCalculationMachineViewModel>()
            {
                new FinishingPrintingCostCalculationMachineViewModel()
                {
                    Chemicals = new List<FinishingPrintingCostCalculationChemicalViewModel>()
                    {
                        new FinishingPrintingCostCalculationChemicalViewModel()
                    }
                }
            };
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);

            vm.Machines = new List<FinishingPrintingCostCalculationMachineViewModel>()
            {
                new FinishingPrintingCostCalculationMachineViewModel()
                {
                    Machine = new MachineViewModel()
                    {
                        Id = 1,
                        Name = "a",
                        Process = "a",
                        LPG = 1,
                        Solar = 1,
                        Steam = 1,
                        Water = 1,
                        Electric = 1,
                    },
                    Step = new StepViewModel()
                    {
                        Id = 1,
                        Process = "a"
                    },
                    Depretiation = 1,
                    Index = 1,
                    CostCalculationId = 1,

                    Chemicals = new List<FinishingPrintingCostCalculationChemicalViewModel>()
                    {
                        new FinishingPrintingCostCalculationChemicalViewModel()
                        {
                            Chemical = new ProductViewModel()
                            {
                                Id = 1,
                                Price = 1,
                                Name = "a"
                            },
                            ChemicalQuantity = 1,
                            CostCalculationId = 1,
                            CostCalculationMachineId = 1
                        }
                    }
                }
            };

            response = vm.Validate(validationContext);
            Assert.Empty(response);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FinishingPrintingCostCalculationMapper>();
                cfg.AddProfile<FinishingPrintingCostCalculationMachineMapper>();
                cfg.AddProfile<FinishingPrintingCostCalculationChemicalMapper>();
            });
            var mapper = configuration.CreateMapper();

            FinishingPrintingCostCalculationViewModel vm = new FinishingPrintingCostCalculationViewModel { Id = 1 };
            FinishingPrintingCostCalculationModel model = mapper.Map<FinishingPrintingCostCalculationModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }

        [Fact]
        public async void GetByPreSalesContract_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            FinishingPrintingCostCalculationFacade facade = new FinishingPrintingCostCalculationFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();

            var Response = facade.GetByPreSalesContract(data.PreSalesContractId);
            Assert.NotEmpty(Response.Data);
        }
    }


}
