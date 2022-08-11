using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using Com.Moonlay.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationLogic : BaseLogic<FinishingPrintingCostCalculationModel>
    {
        private readonly SalesDbContext _dbContext;
        public FinishingPrintingCostCalculationLogic(IIdentityService IdentityService, SalesDbContext dbContext) : base(IdentityService, dbContext)
        {
            _dbContext = dbContext;
        }

        public override ReadResponse<FinishingPrintingCostCalculationModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            var query = DbSet.AsQueryable();
            List<string> SearchAttributes = new List<string>()
            {
                "ProductionOrderNo", "PreSalesContractNo", "UnitName"
            };
            query = QueryHelper<FinishingPrintingCostCalculationModel>.Search(query, SearchAttributes, keyword);
            List<string> SelectedFields = new List<string>()
            {
                "Id", "CreatedUtc", "LastModifiedUtc", "ProductionOrderNo", "PreSalesContract", "ConfirmPrice", "IsPosted", "Material", "UOM", "ScreenCost",
                "ApprovalMD", "ApprovalPPIC", "FreightCost", "Color"
            };
            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<FinishingPrintingCostCalculationModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<FinishingPrintingCostCalculationModel>.Order(query, OrderDictionary);

            Pageable<FinishingPrintingCostCalculationModel> pageable = new Pageable<FinishingPrintingCostCalculationModel>(query, page - 1, size);
            List<FinishingPrintingCostCalculationModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<FinishingPrintingCostCalculationModel>(data, totalData, OrderDictionary, SelectedFields);
        }

        public override Task<FinishingPrintingCostCalculationModel> ReadByIdAsync(long id)
        {
            return DbSet.Include(x => x.Machines).ThenInclude(y => y.Chemicals).FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
        }

        public Task<FinishingPrintingCostCalculationModel> ReadParent(long id)
        {
            return DbSet.FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
        }

        public override void Create(FinishingPrintingCostCalculationModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(entity => entity.Code.Equals(model.Code)));
            ProductionOrderNumberGenerator(model);
            int indexM = 0;
            model.Machines.ToList().ForEach(machine =>
            {
                machine.Index = indexM;
                indexM++;
                EntityExtension.FlagForCreate(machine, IdentityService.Username, "sales-service");
                machine.Chemicals.ToList().ForEach(chemical =>
                {
                    EntityExtension.FlagForCreate(chemical, IdentityService.Username, "sales-service");
                });
            });
            base.Create(model);
        }

        public override async Task DeleteAsync(long id)
        {
            FinishingPrintingCostCalculationModel model = await ReadByIdAsync(id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);

            foreach (var machine in model.Machines)
            {
                EntityExtension.FlagForDelete(machine, IdentityService.Username, "sales-service", true);
                foreach (var chemical in machine.Chemicals)
                {
                    EntityExtension.FlagForDelete(chemical, IdentityService.Username, "sales-service", true);
                }
            }

            DbSet.Update(model);
        }

        public override void UpdateAsync(long id, FinishingPrintingCostCalculationModel model)
        {
            var dbModel = DbSet.Include(x => x.Machines).ThenInclude(x => x.Chemicals).FirstOrDefault(x => x.Id == id);

            dbModel.ActualPrice = model.ActualPrice;
            dbModel.BankMiscCost = model.BankMiscCost;
            dbModel.Color = model.Color;
            dbModel.ConfirmPrice = model.ConfirmPrice;
            dbModel.CurrencyRate = model.CurrencyRate;
            dbModel.Date = model.Date;
            dbModel.DirectorOfficeCost = model.DirectorOfficeCost;
            dbModel.Embalase = model.Embalase;
            dbModel.FreightCost = model.FreightCost;
            dbModel.GeneralAdministrationCost = model.GeneralAdministrationCost;
            dbModel.GreigeId = model.GreigeId;
            dbModel.GreigeName = model.GreigeName;
            dbModel.GreigePrice = model.GreigePrice;
            dbModel.HelperMaterial = model.HelperMaterial;
            dbModel.InstructionId = model.InstructionId;
            dbModel.InstructionName = model.InstructionName;
            dbModel.IsPosted = model.IsPosted;
            dbModel.Lubricant = model.Lubricant;
            dbModel.MachineMaintenance = model.MachineMaintenance;
            dbModel.ManufacturingServiceCost = model.ManufacturingServiceCost;
            dbModel.MaterialId = model.MaterialId;
            dbModel.MaterialName = model.MaterialName;
            dbModel.MiscMaterial = model.MiscMaterial;
            dbModel.PreparationFabricWeight = model.PreparationFabricWeight;
            dbModel.PreSalesContractId = model.PreSalesContractId;
            dbModel.PreSalesContractNo = model.PreSalesContractNo;
            dbModel.ProductionOrderNo = model.ProductionOrderNo;
            dbModel.ProductionUnitValue = model.ProductionUnitValue;
            dbModel.Remark = model.Remark;
            dbModel.RFDFabricWeight = model.RFDFabricWeight;
            dbModel.SalesFirstName = model.SalesFirstName;
            dbModel.SalesId = model.SalesId;
            dbModel.SalesLastName = model.SalesLastName;
            dbModel.SalesUserName = model.SalesUserName;
            dbModel.ScreenCost = model.ScreenCost;
            dbModel.ScreenDocumentNo = model.ScreenDocumentNo;
            dbModel.SparePart = model.SparePart;
            dbModel.StructureMaintenance = model.StructureMaintenance;
            dbModel.UnitId = model.UnitId;
            dbModel.UnitName = model.UnitName;
            dbModel.UomId = model.UomId;
            dbModel.UomUnit = model.UomUnit;


            var addedMachines = model.Machines.Where(x => !dbModel.Machines.Any(y => y.Id == x.Id));
            var updateMachines = dbModel.Machines.Where(x => model.Machines.Any(y => y.Id == x.Id));
            var deletedMachines = dbModel.Machines.Where(x => !model.Machines.Any(y => y.Id == x.Id));

            int maxIndex = dbModel.Machines.Max(x => x.Index);
            foreach (var item in addedMachines)
            {
                item.Index = maxIndex + 1;
                EntityExtension.FlagForCreate(item, IdentityService.Username, "sales-service");
                foreach (var chemical in item.Chemicals)
                {
                    EntityExtension.FlagForCreate(chemical, IdentityService.Username, "sales-service");

                }
                dbModel.Machines.Add(item);
                maxIndex++;
            }

            foreach (var item in updateMachines)
            {
                var specificMachineModel = model.Machines.FirstOrDefault(x => x.Id == item.Id);

                item.Depretiation = specificMachineModel.Depretiation;
                item.MachineElectric = specificMachineModel.MachineElectric;
                item.MachineId = specificMachineModel.MachineId;
                item.MachineLPG = specificMachineModel.MachineLPG;
                item.MachineName = specificMachineModel.MachineName;
                item.MachineProcess = specificMachineModel.MachineProcess;
                item.MachineSolar = specificMachineModel.MachineSolar;
                item.MachineSteam = specificMachineModel.MachineSteam;
                item.MachineWater = specificMachineModel.MachineWater;
                item.StepId = specificMachineModel.StepId;
                item.StepProcess = specificMachineModel.StepProcess;
                item.StepProcessArea = specificMachineModel.StepProcessArea;


                var addedChemicals = specificMachineModel.Chemicals.Where(x => !item.Chemicals.Any(y => y.Id == x.Id));
                var updatedChemicals = item.Chemicals.Where(x => specificMachineModel.Chemicals.Any(y => y.Id == x.Id));
                var deletedChemicals = item.Chemicals.Where(x => !specificMachineModel.Chemicals.Any(y => y.Id == x.Id));

                foreach (var chemical in addedChemicals)
                {
                    chemical.CostCalculationId = dbModel.Id;
                    EntityExtension.FlagForCreate(chemical, IdentityService.Username, "sales-service");
                    item.Chemicals.Add(chemical);
                }

                foreach (var chemical in updatedChemicals)
                {
                    var specificChemical = specificMachineModel.Chemicals.FirstOrDefault(x => x.Id == chemical.Id);

                    chemical.ChemicalCurrency = specificChemical.ChemicalCurrency;
                    chemical.ChemicalId = specificChemical.ChemicalId;
                    chemical.ChemicalName = specificChemical.ChemicalName;
                    chemical.ChemicalPrice = specificChemical.ChemicalPrice;
                    chemical.ChemicalQuantity = specificChemical.ChemicalQuantity;
                    chemical.ChemicalUom = specificChemical.ChemicalUom;


                    EntityExtension.FlagForUpdate(chemical, IdentityService.Username, "sales-service");
                }

                foreach (var chemical in deletedChemicals)
                {
                    EntityExtension.FlagForDelete(chemical, IdentityService.Username, "sales-service");
                }

                EntityExtension.FlagForUpdate(item, IdentityService.Username, "sales-service");
            }

            foreach (var item in deletedMachines)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, "sales-service");
                foreach (var chemical in item.Chemicals)
                {
                    EntityExtension.FlagForDelete(chemical, IdentityService.Username, "sales-service");

                }
            }
            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");

        }

        public async Task CCPost(List<long> listId)
        {
            foreach (var id in listId)
            {
                var model = await DbSet.FirstOrDefaultAsync(d => d.Id == id);
                model.IsPosted = true;
                EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            }
        }

        public async Task ApproveMD(long id)
        {
            var model = await DbSet.FirstOrDefaultAsync(d => d.Id == id);
            model.IsApprovedMD = true;
            model.ApprovedMDBy = IdentityService.Username;
            model.ApprovedMDDate = DateTimeOffset.UtcNow;

            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
        }

        public async Task ApprovePPIC(long id)
        {
            var model = await DbSet.FirstOrDefaultAsync(d => d.Id == id);
            model.IsApprovedPPIC = true;
            model.ApprovedPPICBy = IdentityService.Username;
            model.ApprovedPPICDate = DateTimeOffset.UtcNow;

            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
        }

        public Task<bool> ValidatePreSalesContractId(long id)
        {
            return DbSet.AnyAsync(x => x.PreSalesContractId == id);
        }

        public ReadResponse<FinishingPrintingCostCalculationModel> GetByPreSalesContract(long preSalesContractId)
        {
            var data = DbSet.Where(s => s.PreSalesContractId == preSalesContractId).ToList();
            

            return new ReadResponse<FinishingPrintingCostCalculationModel>(data, data.Count, new Dictionary<string, string>(), new List<string>());
        }

        private void ProductionOrderNumberGenerator(FinishingPrintingCostCalculationModel model)
        {
            var lastData = DbSet.IgnoreQueryFilters().Where(w => w.UnitName.Equals(model.UnitName)).OrderByDescending(x => x.CreatedUtc);

            string DocumentType = model.UnitName.ToLower().Equals("printing") ? "P" : "F";

            int YearNow = DateTime.Now.Year;
            int MonthNow = DateTime.Now.Month;
            int count = 0;
            if (lastData.Count() == 0)
            {
                count = 1;
                model.ProductionOrderNo = $"{DocumentType}/{YearNow}/{count.ToString().PadLeft(4, '0')}";
            }
            else
            {
                var lastCC = lastData.FirstOrDefault();
                if (YearNow > lastCC.CreatedUtc.Year)
                {
                    count = 1;
                    model.ProductionOrderNo = $"{DocumentType}/{YearNow}/{count.ToString().PadLeft(4, '0')}";
                }
                else
                {
                    count = lastData.Count() + 1;
                    model.ProductionOrderNo = $"{DocumentType}/{YearNow}/{count.ToString().PadLeft(4, '0')}";
                }
            }
        }

    }
}
