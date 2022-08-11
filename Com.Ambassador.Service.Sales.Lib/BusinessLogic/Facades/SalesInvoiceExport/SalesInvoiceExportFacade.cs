using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.SalesInvoiceExport;
using Com.Ambassador.Service.Sales.Lib.Models.SalesInvoiceExport;
using Com.Ambassador.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.SalesInvoiceExport;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.SalesInvoiceExport
{
    public class SalesInvoiceExportFacade : ISalesInvoiceExportContract
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<SalesInvoiceExportModel> DbSet;
        private IdentityService identityService;
        private readonly IServiceProvider _serviceProvider;
        private SalesInvoiceExportLogic salesInvoiceExportLogic;
        public SalesInvoiceExportFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            _serviceProvider = serviceProvider;
            this.DbSet = DbContext.Set<SalesInvoiceExportModel>();
            this.identityService = serviceProvider.GetService<IdentityService>();
            this.salesInvoiceExportLogic = serviceProvider.GetService<SalesInvoiceExportLogic>();
        }

        public async Task<int> CreateAsync(SalesInvoiceExportModel model)
        {
            int result = 0;
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    int index = 0;
                    do
                    {
                        model.Code = CodeGenerator.Generate();
                    }
                    while (DbSet.Any(d => d.Code.Equals(model.Code)));

                    SalesInvoiceNumberGenerator(model, index);

                    salesInvoiceExportLogic.Create(model);
                    index++;

                    result = await DbContext.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return result;
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    SalesInvoiceExportModel model = await salesInvoiceExportLogic.ReadByIdAsync(id);
                    if (model != null)
                    {
                        SalesInvoiceExportModel salesInvoiceModel = new SalesInvoiceExportModel();
                        salesInvoiceModel = model;
                        await salesInvoiceExportLogic.DeleteAsync(id);
                    }


                    transaction.Commit();
                }
                catch (Exception e)
                {

                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<SalesInvoiceExportModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return salesInvoiceExportLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<SalesInvoiceExportModel> ReadByIdAsync(int id)
        {
            return await salesInvoiceExportLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, SalesInvoiceExportModel model)
        {
            salesInvoiceExportLogic.UpdateAsync(id, model);

            return await DbContext.SaveChangesAsync();
        }

        private void SalesInvoiceNumberGenerator(SalesInvoiceExportModel model, int index)
        {
            SalesInvoiceExportModel lastData = DbSet.IgnoreQueryFilters().Where(w => w.LetterOfCreditNumberType.Equals(model.LetterOfCreditNumberType)).OrderByDescending(o => o.AutoIncreament).FirstOrDefault();

            int MonthNow = DateTime.Now.Month;
            int YearNow = DateTime.Now.Year;
            var MonthNowString = DateTime.Now.ToString("MM");
            var YearNowString = DateTime.Now.ToString("yy");
            var formatCode = "";

            if (model.SalesInvoiceCategory == "SPINNING")
            {
                formatCode = "B";
            }
            else if (model.SalesInvoiceCategory == "DYEINGPRINTING" && model.FPType == "Dyeing/Finishing")
            {
                formatCode = "F";
            }
            else if (model.SalesInvoiceCategory == "DYEINGPRINTING" && model.FPType == "Printing")
            {
                formatCode = "P";
            }
            else
            {
                formatCode = "";
            }

            if (lastData == null)
            {
                index = 0;
                model.AutoIncreament = 1 + index;
                model.SalesInvoiceNo = $"DL {model.AutoIncreament.ToString().PadLeft(2, '0')}{formatCode}/4.2.1/{MonthNowString}.{YearNowString}";
            }
            else
            {
                if (YearNow > lastData.CreatedUtc.Year)
                {
                    model.AutoIncreament = 1 + index;
                    model.SalesInvoiceNo = $"DL {model.AutoIncreament.ToString().PadLeft(2, '0')}{formatCode}/4.2.1/{MonthNowString}.{YearNowString}";
                }
                else
                {
                    model.AutoIncreament = lastData.AutoIncreament + (1 + index);
                    model.SalesInvoiceNo = $"DL {model.AutoIncreament.ToString().PadLeft(2, '0')}{formatCode}/4.2.1/{MonthNowString}.{YearNowString}";
                }
            }
        }
    }
}
