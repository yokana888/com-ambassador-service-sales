using Com.Ambassador.Service.Sales.Lib.Models.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.ProductionOrder
{
    public class ProductionOrderViewModel : BaseViewModel, IValidatableObject
    {
        public ProductionOrderViewModel()
        {
            RunWidth = new HashSet<ProductionOrder_RunWidthViewModel>();
        }
        
        [MaxLength(50)]
        public string POType { get; set; }

        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(255)]
        public string OrderNo { get; set; }
        public double? OrderQuantity { get; set; }
        public double? ShippingQuantityTolerance { get; set; }
        [MaxLength(255)]
        public string MaterialOrigin { get; set; }
        [MaxLength(255)]
        public string FinishWidth { get; set; }
        [MaxLength(255)]
        public string DesignNumber { get; set; }
        [MaxLength(255)]
        public string DesignCode { get; set; }
        [MaxLength(255)]
        public string HandlingStandard { get; set; }
        [MaxLength(255)]
        public string Run { get; set; }
        [MaxLength(255)]
        public string ShrinkageStandard { get; set; }
        [MaxLength(1000)]
        public string ArticleFabricEdge { get; set; }
        [MaxLength(1000)]
        public string MaterialWidth { get; set; }
        [MaxLength(1000)]
        public string PackingInstruction { get; set; }
        [MaxLength(1000)]
        public string Sample { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        [MaxLength(1000)]
        public string Remark { get; set; }
        public double? DistributedQuantity { get; set; }
        public bool? IsUsed { get; set; }
        public bool? IsClosed { get; set; }
        public bool? IsRequested { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsCalculated { get; set; }
        public long? AutoIncreament { get; set; }
        public string SalesContractNo { get; set; }

        public virtual ICollection<ProductionOrder_DetailViewModel> Details { get; set; }
        public virtual ICollection<ProductionOrder_RunWidthViewModel> RunWidth { get; set; }
        public virtual ICollection<ProductionOrder_LampStandardViewModel> LampStandards { get; set; }

        public FinishingPrintingSalesContractViewModel FinishingPrintingSalesContract { get; set; }
        public YarnMaterialViewModel YarnMaterial { get; set; }
        public BuyerViewModel Buyer { get; set; }
        public MaterialViewModel Material { get; set; }
        public UomViewModel Uom { get; set; }
        public MaterialConstructionViewModel MaterialConstruction { get; set; }
        public ProcessTypeViewModel ProcessType { get; set; }
        public OrderTypeViewModel OrderType { get; set; }
        public DesignMotiveViewModel DesignMotive { get; set; }
        public StandardTestsViewModel StandardTests { get; set; }
        public FinishTypeViewModel FinishType { get; set; }
        public AccountViewModel Account { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (string.IsNullOrWhiteSpace(this.POType) || this.POType == "")
            {
                yield return new ValidationResult("Jenis SPP harus di isi", new List<string> { "POType" });
            }

            if (this.POType == "SALES")
                if (this.Buyer == null || this.Buyer.Id.Equals(0))
                {
                    yield return new ValidationResult("Buyer harus di isi", new List<string> { "Buyer" });
                }

            if (this.Uom == null || this.Uom.Id.Equals(0))
            {
                yield return new ValidationResult("Satuan harus di isi", new List<string> { "Uom" });
            }

            if (this.POType == "SALES")
                if (this.FinishingPrintingSalesContract == null || this.FinishingPrintingSalesContract.Id.Equals(0))
                {
                    yield return new ValidationResult("sales contract harus di isi", new List<string> { "FinishingPrintingSalesContract" });
                }
           
            if (this.Material == null || this.Material.Id.Equals(0))
            {
                yield return new ValidationResult("material harus di isi", new List<string> { "Material" });
            }

            if (this.ProcessType == null || this.ProcessType.Id.Equals(0))
            {
                yield return new ValidationResult("Process Type harus di isi", new List<string> { "ProcessType" });
            }
            else
            {
                if (string.IsNullOrEmpty(ProcessType.Unit) || string.IsNullOrEmpty(ProcessType.SPPCode))
                {
                    yield return new ValidationResult("Process Type yang dipilih harus memiliki Unit dan Kode SPP", new List<string> { "ProcessType" });
                }
                else
                {
                    if (this.ProcessType.Unit.Trim().ToLower() == "printing")
                    {
                        //if (string.IsNullOrWhiteSpace(this.Run))
                        //{
                        //    yield return new ValidationResult("Run harus di isi", new List<string> { "Run" });
                        //}
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

                            if (string.IsNullOrWhiteSpace(this.DesignNumber))
                            {
                                yield return new ValidationResult("DesignNumber harus di isi", new List<string> { "DesignNumber" });
                            }

                            if (string.IsNullOrWhiteSpace(this.DesignCode))
                            {
                                yield return new ValidationResult("DesignCode harus di isi", new List<string> { "DesignCode" });
                            }

                        }
                    }
                }
            }

            if (this.OrderType == null || this.OrderType.Id.Equals(0))
            {
                yield return new ValidationResult("Order Type harus di isi", new List<string> { "OrderType" });
            }
            //else if (!this.OrderType.Id.Equals(0) || this.OrderType != null)
            //{
                
            //    //else if (this.OrderType.Name.ToLower() == "yarn dyed"|| this.OrderType.Name.ToLower() == "yarn dyed")
            //    //{

            //    //}
            //}

            if (this.YarnMaterial == null || this.YarnMaterial.Id.Equals(0))
            {
                yield return new ValidationResult("Yarn Material harus di isi", new List<string> { "YarnMaterial" });
            }

            if (this.MaterialConstruction == null || this.MaterialConstruction.Id.Equals(0))
            {
                yield return new ValidationResult("Material Construction harus di isi", new List<string> { "MaterialConstruction" });
            }

            if (this.FinishType == null || this.FinishType.Id.Equals(0))
            {
                yield return new ValidationResult("Finish Type harus di isi", new List<string> { "FinishType" });
            }

            if (this.StandardTests == null || this.StandardTests.Id.Equals(0))
            {
                yield return new ValidationResult("Standard Tests harus di isi", new List<string> { "StandardTests" });
            }

            if (this.Account == null)
            {
                yield return new ValidationResult("Account harus di isi", new List<string> { "Account" });
            }

            if (string.IsNullOrWhiteSpace(this.PackingInstruction) || this.PackingInstruction == "")
            {
                yield return new ValidationResult("Packing Instruction harus di isi", new List<string> { "PackingInstruction" });
            }

            if (string.IsNullOrWhiteSpace(this.MaterialOrigin) || this.MaterialOrigin == "")
            {
                yield return new ValidationResult("Material Origin harus di isi", new List<string> { "MaterialOrigin" });
            }

            if (string.IsNullOrWhiteSpace(this.FinishWidth) || this.FinishWidth == "")
            {
                yield return new ValidationResult("Finish Width harus di isi", new List<string> { "FinishWidth" });
            }

            if (string.IsNullOrWhiteSpace(this.Sample))
            {
                yield return new ValidationResult("Sample harus di isi", new List<string> { "Sample" });
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
                    double totalqty = 0;

                    foreach (ProductionOrder_DetailViewModel data in this.Details)
                    {
                        totalqty += (double)data.Quantity;
                    }

                    if (!this.OrderQuantity.Equals(Math.Round(totalqty, 3)))
                    {
                        yield return new ValidationResult("OrderQuantity tidak sama", new List<string> { "OrderQuantity" });
                    }

                }
            }

            if (this.ShippingQuantityTolerance > 100)
            {
                yield return new ValidationResult("OrderQuantity harus di isi", new List<string> { "OrderQuantity" });
            }

            if (string.IsNullOrWhiteSpace(this.MaterialWidth))
            {
                yield return new ValidationResult("MaterialWidth harus di isi", new List<string> { "MaterialWidth" });
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
                    if (detail.Uom.Id.Equals(0))
                    {
                        CountDetailsError++;
                        DetailsError += "{ 'satuan harus di isi' }, ";
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
