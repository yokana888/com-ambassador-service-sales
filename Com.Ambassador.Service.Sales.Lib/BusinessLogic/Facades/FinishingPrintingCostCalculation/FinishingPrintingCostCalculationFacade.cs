using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationFacade : IFinishingPrintingCostCalculationService
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<FinishingPrintingCostCalculationModel> DbSet;
        private readonly FinishingPrintingCostCalculationLogic finishingPrintingCostCalculationLogic;
        private readonly DbSet<FinishingPrintingCostCalculationChemicalModel> ChemicalDBSet;
        private readonly IAzureImageFacade AzureImageFacade;

        public FinishingPrintingCostCalculationFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<FinishingPrintingCostCalculationModel>();
            ChemicalDBSet = DbContext.Set<FinishingPrintingCostCalculationChemicalModel>();
            finishingPrintingCostCalculationLogic = serviceProvider.GetService<FinishingPrintingCostCalculationLogic>();
            AzureImageFacade = serviceProvider.GetService<IAzureImageFacade>();
        }

        public async Task<int> CreateAsync(FinishingPrintingCostCalculationModel model)
        {
            int created = 0;
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    finishingPrintingCostCalculationLogic.Create(model);
                    created += await DbContext.SaveChangesAsync();

                    foreach(var machine in model.Machines)
                    {
                        foreach(var chemical in machine.Chemicals)
                        {
                            chemical.CostCalculationId = model.Id;
                        }
                    }


                    if (!string.IsNullOrWhiteSpace(model.ImageFile))
                    {
                        model.ImagePath = await AzureImageFacade.UploadImage(model.GetType().Name, model.Id, model.CreatedUtc, model.ImageFile);
                    }

                    created += await DbContext.SaveChangesAsync();


                    transaction.Commit();
                    return created;

                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                
            }
            
        }

        public async Task<int> DeleteAsync(int id)
        {
            await finishingPrintingCostCalculationLogic.DeleteAsync(id);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> CCPost(List<long> listId)
        {
            await finishingPrintingCostCalculationLogic.CCPost(listId);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<FinishingPrintingCostCalculationModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return finishingPrintingCostCalculationLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<FinishingPrintingCostCalculationModel> ReadByIdAsync(int id)
        {
            var model = await finishingPrintingCostCalculationLogic.ReadByIdAsync(id);

            if (model.ImagePath != null)
            {
                model.ImageFile = await AzureImageFacade.DownloadImage(model.GetType().Name, model.ImagePath);
            }

            return model;
        }

        public async Task<int> UpdateAsync(int id, FinishingPrintingCostCalculationModel model)
        {
            finishingPrintingCostCalculationLogic.UpdateAsync(id, model);
            if (!string.IsNullOrWhiteSpace(model.ImageFile))
            {
                model.ImagePath = await this.AzureImageFacade.UploadImage(model.GetType().Name, model.Id, model.CreatedUtc, model.ImageFile);
            }
            return await DbContext.SaveChangesAsync();
        }

        public async Task<FinishingPrintingCostCalculationModel> ReadParent(long id)
        {
            return await finishingPrintingCostCalculationLogic.ReadParent(id);
        }

        public async Task<int> CCApproveMD(long id)
        {
            await finishingPrintingCostCalculationLogic.ApproveMD(id);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> CCApprovePPIC(long id)
        {
            await finishingPrintingCostCalculationLogic.ApprovePPIC(id);
            return await DbContext.SaveChangesAsync();
        }

        public Task<bool> ValidatePreSalesContractId(long id)
        {
            return finishingPrintingCostCalculationLogic.ValidatePreSalesContractId(id);
        }

        public ReadResponse<FinishingPrintingCostCalculationModel> GetByPreSalesContract(long preSalesContractId)
        {
            return finishingPrintingCostCalculationLogic.GetByPreSalesContract(preSalesContractId);
        }
    }
}
