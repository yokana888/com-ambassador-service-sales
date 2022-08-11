using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.ProductionOrder
{
    public class ShinProductionOrderViewModel : BaseViewModel, IValidatableObject
    {
        public ShinProductionOrderViewModel()
        {
            RunWidth = new HashSet<ProductionOrder_RunWidthViewModel>();
        }

        public string ProductionOrderNo { get; set; }
        [MaxLength(255)]
        public string Code { get; set; }

        public ShinFinishingPrintingSalesContractViewModel FinishingPrintingSalesContract { get; set; }

        public double OrderQuantity { get; set; }

        [MaxLength(255)]
        public string MaterialOrigin { get; set; }

        [MaxLength(255)]
        public string HandlingStandard { get; set; }

        [MaxLength(255)]
        public string Run { get; set; }

        public virtual ICollection<ProductionOrder_RunWidthViewModel> RunWidth { get; set; }

        [MaxLength(1000)]
        public string ArticleFabricEdge { get; set; }

        [MaxLength(255)]
        public string ShrinkageStandard { get; set; }

        public StandardTestsViewModel StandardTests { get; set; }

        public DateTimeOffset DeliveryDate { get; set; }

        public AccountViewModel Account { get; set; }

        public MaterialConstructionViewModel MaterialConstruction { get; set; }

        public string MaterialWidth { get; set; }


        [MaxLength(1000)]
        public string Remark { get; set; }

        public virtual ICollection<ProductionOrder_LampStandardViewModel> LampStandards { get; set; }

        public virtual ICollection<ProductionOrder_DetailViewModel> Details { get; set; }

        public double? DistributedQuantity { get; set; }
        public bool? IsUsed { get; set; }
        public bool? IsClosed { get; set; }
        public bool? IsRequested { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsCalculated { get; set; }
        public long? AutoIncreament { get; set; }

        public ApprovalViewModel ApprovalMD { get; set; }

        public ApprovalViewModel ApprovalSample { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {


            if (this.FinishingPrintingSalesContract == null || this.FinishingPrintingSalesContract.Id.Equals(0))
            {
                yield return new ValidationResult("sales contract harus di isi", new List<string> { "FinishingPrintingSalesContract" });
            }
            else
            {
                if (this.FinishingPrintingSalesContract.PreSalesContract.Unit.Name.Trim().ToLower() == "printing")
                {
                    if (string.IsNullOrWhiteSpace(this.Run))
                    {
                        yield return new ValidationResult("Run harus di isi", new List<string> { "Run" });
                    }

                    if (!string.IsNullOrWhiteSpace(this.Run) && this.Run != "Tanpa RUN")
                    {
                        if (this.RunWidth.Count.Equals(0))
                        {
                            yield return new ValidationResult("RunWidths harus di isi", new List<string> { "RunWidths" });
                        }
                        else if (!this.RunWidth.Count.Equals(0))
                        {
                            int Count = 0;
                            string RunWidths = "[";

                            foreach (ProductionOrder_RunWidthViewModel data in this.RunWidth)
                            {
                                if (data.Value <= 0)
                                {

                                    Count++;
                                    RunWidths += "{ 'RunWidth harus lebih besar dari 0' }, ";
                                }
                            }

                            RunWidths += "]";

                            if (Count > 0)
                            {
                                yield return new ValidationResult(RunWidths, new List<string> { "RunWidths" });
                            }
                        }


                    }
                }
            }



            if (this.StandardTests == null || this.StandardTests.Id.Equals(0))
            {
                yield return new ValidationResult("Standard Tests harus di isi", new List<string> { "StandardTests" });
            }

            if (this.Account == null)
            {
                yield return new ValidationResult("Account harus di isi", new List<string> { "Account" });
            }



            if (string.IsNullOrWhiteSpace(this.MaterialOrigin) || this.MaterialOrigin == "")
            {
                yield return new ValidationResult("Material Origin harus di isi", new List<string> { "MaterialOrigin" });
            }



            if (string.IsNullOrWhiteSpace(this.HandlingStandard) || this.HandlingStandard == "")
            {
                yield return new ValidationResult("Handling Standard harus di isi", new List<string> { "HandlingStandard" });
            }

            if (string.IsNullOrWhiteSpace(this.ShrinkageStandard) || this.ShrinkageStandard == "")
            {
                yield return new ValidationResult("Shrinkage Standard harus di isi", new List<string> { "ShrinkageStandard" });
            }

            if (this.DeliveryDate == null)
                yield return new ValidationResult("DeliveryDate harus di isi", new List<string> { "DeliveryDate" });

            if (this.OrderQuantity.Equals(0))
            {
                yield return new ValidationResult("OrderQuantity harus di isi", new List<string> { "OrderQuantity" });
            }
            else if (!this.OrderQuantity.Equals(0))
            {

                if (!this.Details.Count.Equals(0))
                {
                    double totalqty = Details.Sum(s => s.Quantity.GetValueOrDefault());

                    var poService = validationContext.GetService<IShinProductionOrder>();
                    var createdQuantity = poService.GetTotalQuantityBySalesContractId(FinishingPrintingSalesContract.Id);

                    if (Math.Round(totalqty, 3) > Math.Round(OrderQuantity - createdQuantity, 3))
                    {
                        yield return new ValidationResult("Jumlah Quantity di Detail melebihi selish Jumlah Order dengan Quantity yang sudah dibuat", 
                            new List<string> { "OrderQuantity" });
                    }

                }
            }


            if (this.LampStandards.Count.Equals(0))
            {
                yield return new ValidationResult("LampStandards harus di isi", new List<string> { "LampStandards" });
            }
            else if (!this.LampStandards.Count.Equals(0))
            {
                int CountError = 0;
                string LampStandardError = "[";
                foreach (ProductionOrder_LampStandardViewModel data in LampStandards)
                {
                    if (String.IsNullOrWhiteSpace(data.Name))
                    {
                        CountError++;
                        LampStandardError += "{ 'LampStandards harus di isi' }, ";
                    }
                }

                LampStandardError += "]";

                if (CountError > 0)
                {
                    yield return new ValidationResult(LampStandardError, new List<string> { "LampStandards" });
                }

            }

            if (this.Details.Count.Equals(0))
            {
                yield return new ValidationResult("Details harus di isi", new List<string> { "Details" });
            }
            else if (!this.Details.Count.Equals(0))
            {
                int CountDetailsError = 0;
                string DetailsError = "[";

                foreach (var detail in this.Details)
                {
                    if (string.IsNullOrWhiteSpace(detail.ColorRequest))
                    {
                        CountDetailsError++;
                        DetailsError += "{ 'ColorRequest harus di isi' }, ";
                    }
                    if (detail.Quantity <= 0)
                    {
                        CountDetailsError++;
                        DetailsError += "{ 'Quantity harus lebih dari 0' }, ";
                    }
                    if (string.IsNullOrWhiteSpace(detail.ColorTemplate))
                    {
                        CountDetailsError++;
                        DetailsError += "{ 'ColorTemplate harus di isi' }, ";
                    }
                }

                DetailsError += "]";

                if (CountDetailsError > 0)
                {
                    yield return new ValidationResult(DetailsError, new List<string> { "Details" });
                }

            }

        }
    }
}
