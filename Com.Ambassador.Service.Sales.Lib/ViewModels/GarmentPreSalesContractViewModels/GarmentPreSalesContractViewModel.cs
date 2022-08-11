using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentPreSalesContractFacades;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentPreSalesContractInterface;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentPreSalesContractModel;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentPreSalesContractViewModels
{
    public class GarmentPreSalesContractViewModel : BaseViewModel, IValidatableObject
    {
        public string SCNo { get; set; }
        public DateTimeOffset SCDate { get; set; }
        public string SCType { get; set; }
        public int SectionId { get; set; }
        public string SectionCode { get; set; }
        public string BuyerAgentId { get; set; }
        public string BuyerAgentName { get; set; }
        public string BuyerAgentCode { get; set; }
        public string BuyerBrandId { get; set; }
        public string BuyerBrandName { get; set; }
        public string BuyerBrandCode { get; set; }
        public int OrderQuantity { get; set; }
        public string Remark { get; set; }
        public bool IsCC { get; set; }
        public bool IsPR { get; set; }
        public bool IsPosted { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //SalesDbContext dbContext = (SalesDbContext)validationContext.GetService(typeof(SalesDbContext));
            //var duplicateRONo = dbContext.GarmentPurchaseRequests.Where(m => m.RONo.Equals(RONo) && m.Id != Id).Count();
            //IGarmentSalesContract Service = (IGarmentSalesContract)validationContext.GetService(typeof(IGarmentSalesContract));
            if (SCDate == DateTimeOffset.MinValue || SCDate == null)
            {
                yield return new ValidationResult("Tanggal Sales Contract harus diisi", new List<string> { "RONumber" });
            }
            if (SectionCode == null)
            {
                yield return new ValidationResult("Seksi harus diisi", new List<string> { "Section" });
            }
                
            if (BuyerAgentCode == null)
            {
                yield return new ValidationResult("Buyer Agent harus diisi", new List<string> { "BuyerAgent" });
            }

            if (BuyerBrandCode == null)
            {
                yield return new ValidationResult("Buyer Brand harus diisi", new List<string> { "BuyerBrand" });
            }

            if (OrderQuantity <= 0)
            {
                yield return new ValidationResult("Jumlah Order harus lebih dari 0", new List<string> { "OrderQuantity" });
            }
        }

    }
}